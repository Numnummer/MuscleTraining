using AutoMapper;
using Itis.MyTrainings.ChatHistoryService.Core.Abstractions.Services;
using Itis.MyTrainings.ChatHistoryService.Core.Models.SupportChat.Entities;
using MassTransit;

namespace Itis.MyTrainings.ChatHistoryService.Web.Masstransit.Consumers;

/// <summary>
/// 
/// </summary>
public class UnicastChatHistoryRecordConsumer : IConsumer<UnicastChatMessage>
{
    private readonly IChatHistoryRecordService _chatHistoryRecordService;
    private readonly IMapper _mapper;

    public UnicastChatHistoryRecordConsumer(IChatHistoryRecordService chatHistoryRecordService, IMapper mapper)
    {
        _chatHistoryRecordService = chatHistoryRecordService;
        _mapper = mapper;
    }
    public async Task Consume(ConsumeContext<UnicastChatMessage> context)
    {
        await _chatHistoryRecordService.RecordUnicastMessage(_mapper.Map<UnicastChatMessage>(context.Message));
    }
}