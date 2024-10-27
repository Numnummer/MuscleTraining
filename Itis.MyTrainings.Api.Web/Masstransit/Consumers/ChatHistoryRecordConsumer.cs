using Itis.MyTrainings.Api.Core.Abstractions;
using Itis.MyTrainings.Api.Core.Entities.SupportChat;
using MassTransit;

namespace Itis.MyTrainings.Api.Web.Masstransit.Consumers;

/// <summary>
/// 
/// </summary>
public class ChatHistoryRecordConsumer : IConsumer<ChatMessage>
{
    private readonly IChatHistoryRecordService _chatHistoryRecordService;

    public ChatHistoryRecordConsumer(IChatHistoryRecordService chatHistoryRecordService)
    {
        _chatHistoryRecordService = chatHistoryRecordService;
    }

    public async Task Consume(ConsumeContext<ChatMessage> context)
    {
        await _chatHistoryRecordService.RecordMessage(context.Message);
    }
}