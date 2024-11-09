using Itis.MyTrainings.Api.Core.Abstractions;
using Itis.MyTrainings.ChatHistoryService.Core.Models.SupportChat.Entities;
using MassTransit;

namespace Itis.MyTrainings.Api.Web.Masstransit.Consumers;

/// <summary>
/// 
/// </summary>
public class UnicastChatHistoryRecordConsumer : IConsumer<UnicastChatMessage>
{
    private readonly IChatHistoryRecordService _chatHistoryRecordService;

    public UnicastChatHistoryRecordConsumer(IChatHistoryRecordService chatHistoryRecordService)
    {
        _chatHistoryRecordService = chatHistoryRecordService;
    }
    public async Task Consume(ConsumeContext<UnicastChatMessage> context)
    {
        await _chatHistoryRecordService.RecordUnicastMessage(context.Message);
    }
}