using Itis.MyTrainings.Api.Core.Abstractions;
using Itis.MyTrainings.ChatHistoryService.Core.Models.SupportChat.Entities;
using MassTransit;

namespace Itis.MyTrainings.ChatHistoryService.Web.Masstransit.Consumers;

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