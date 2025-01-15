using StackExchange.Redis;

namespace Itis.MyTrainings.StorageService.Core.Helpers;

public static class RedisCacheHelper
{
    public static void ClearCache()
    {
        var redis = ConnectionMultiplexer.Connect("localhost:6379");
        var endpoints = redis.GetEndPoints();
        var server = redis.GetServer(endpoints.First());

        // Flush all keys in the Redis database
        server.FlushDatabase();
    }
}