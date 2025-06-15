using System.Data;
using ClickHouse.Client.ADO;
using ClickHouse.Client.Utility;
using Itis.MyTrainings.Api.Core.Abstractions;
using Itis.MyTrainings.Api.Core.Models;
using MassTransit;

public class ClickHouseMessageService : IDisposable, IMessageStatsService
{
    private readonly ClickHouseConnection _connection;
    private readonly IPublishEndpoint _rabbitMqService;

    public ClickHouseMessageService(IConfiguration config, IPublishEndpoint rabbitMqService)
    {
        _connection = new ClickHouseConnection(config.GetConnectionString("ClickHouse"));
        _rabbitMqService = rabbitMqService;
        EnsureDatabaseInitialized();
    }
    
    public async Task EnsureDatabaseInitialized()
    {
        await _connection.OpenAsync();
        
        // Создаем таблицы, если они не существуют
        await ExecuteNonQueryAsync(@"
        CREATE TABLE IF NOT EXISTS default.message_events
        (
            event_id UUID DEFAULT generateUUIDv4(),
            user_id UUID,
            event_time DateTime DEFAULT now(),
            message_type String
        ) ENGINE = MergeTree()
        ORDER BY (user_id, event_time)");
        
        await ExecuteNonQueryAsync(@"
        CREATE TABLE IF NOT EXISTS default.user_messages
        (
            user_id UUID,
            message_date Date DEFAULT today(),
            message_count UInt64 DEFAULT 0,
            last_updated DateTime DEFAULT now()
        ) ENGINE = ReplacingMergeTree(last_updated)
        ORDER BY (user_id, message_date)
        PARTITION BY message_date");
    }
    
    private async Task ExecuteNonQueryAsync(string sql)
    {
        using var cmd = _connection.CreateCommand();
        cmd.CommandText = sql;
        await cmd.ExecuteNonQueryAsync();
    }

    public async Task<MessageStats> ProcessMessageAsync(Guid userId)
    {
        try
        {
            await _connection.OpenAsync();
            
            // 1. Вставляем событие (асинхронно)
            await InsertMessageEventAsync(userId);
            
            // 2. Обновляем счетчик (асинхронно)
            await UpdateMessageCountAsync(userId);
            
            // 3. Получаем статистику
            var stats = await GetUserStatsAsync(userId);
            
            // 4. Отправляем в RabbitMQ
            await _rabbitMqService.Publish<MessageStats>(stats, context =>
            {
                // Устанавливаем routing key = UserId
                context.SetRoutingKey(stats.UserId.ToString());
                
                // Дополнительные заголовки если нужно
                context.Headers.Set("MessageType", "StatsUpdate");
                context.Headers.Set("Timestamp", DateTime.UtcNow);
            });
            
            return stats;
        }
        catch (Exception ex)
        {
            // Логирование ошибки
            Console.WriteLine($"Error processing message: {ex.Message}");
            throw;
        }
    }

    private async Task InsertMessageEventAsync(Guid userId)
    {
        var sql = @"
        INSERT INTO message_events (user_id, message_type)
        VALUES ({userId:UUID}, {messageType:String})";
        
        await ExecuteCommandAsync(sql, 
            ("userId", userId),
            ("messageType", "text"));
    }

    private async Task UpdateMessageCountAsync(Guid userId)
    {
        var sql = @"
    INSERT INTO default.user_messages (user_id, message_count, last_updated)
    VALUES ({userId:UUID}, 1, now())";
        
        await ExecuteCommandAsync(sql, ("userId", userId));
    }

    private async Task<MessageStats> GetUserStatsAsync(Guid userId)
    {
        var sql = @"
        SELECT 
            {userId:UUID} as user_id,
            sum(message_count) as total_count,
            max(message_date) as last_date
        FROM user_messages
        WHERE user_id = {userId:UUID}";
        
        using var cmd = _connection.CreateCommand();
        cmd.CommandText = sql;
        cmd.AddParameter("userId", DbType.String.ToString(), userId.ToString());
        
        using var reader = await cmd.ExecuteReaderAsync();
        if (await reader.ReadAsync())
        {
            return new MessageStats
            {
                UserId = userId,
                TotalCount = Convert.ToInt64(reader["total_count"]),
                LastMessageDate = reader.GetDateTime(2),
                UpdatedAt = DateTime.UtcNow
            };
        }
    
        return new MessageStats
        {
            UserId = userId,
            TotalCount = 0,
            LastMessageDate = DateTime.MinValue,
            UpdatedAt = DateTime.UtcNow
        };
    }

    private async Task ExecuteCommandAsync(string sql, params (string Name, object Value)[] parameters)
    {
        using var cmd = _connection.CreateCommand();
        cmd.CommandText = sql;
        
        foreach (var (name, value) in parameters)
        {
            var processedValue = value is Guid guid ? guid.ToString() : value;
            var dbType = processedValue switch
            {
                int _ => DbType.Int32,
                long _ => DbType.Int64,
                string _ => DbType.String,
                DateTime _ => DbType.DateTime,
                _ => DbType.Object
            };
            cmd.AddParameter(name, dbType.ToString(), processedValue);
        }
        
        await cmd.ExecuteNonQueryAsync();
    }

    public void Dispose()
    {
        _connection?.Dispose();
    }
}