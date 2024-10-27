using Itis.MyTrainings.Api.Core.Entities.SupportChat;
using Itis.MyTrainings.Api.Core.Services;
using MassTransit;

namespace Itis.MyTrainings.Api.Web.Masstransit.Consumers;

/// <summary>
/// 
/// </summary>
public class ChatHistoryRecordConsumer : IConsumer<ChatMessage>
{
    private readonly ChatHistoryRecordService _chatHistoryRecordService;

    public ChatHistoryRecordConsumer(ChatHistoryRecordService chatHistoryRecordService)
    {
        _chatHistoryRecordService = chatHistoryRecordService;
    }

    public async Task Consume(ConsumeContext<ChatMessage> context)
    {
        await _chatHistoryRecordService.RecordMessage(context.Message);
    }
}