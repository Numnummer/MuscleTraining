using AutoMapper;
using ChatMessageDtos;
using Itis.MyTrainings.ChatHistoryService.Core.Abstractions.Services;
using Itis.MyTrainings.ChatHistoryService.Core.Models.SupportChat.Entities;
using MassTransit;

namespace Itis.MyTrainings.ChatHistoryService.Web.Masstransit.Consumers;

/// <summary>
/// 
/// </summary>
public class ChatHistoryRecordConsumer : IConsumer<MulticastChatMessageDto>
{
    private readonly IChatHistoryRecordService _chatHistoryRecordService;
    private readonly IMapper _mapper;

    public ChatHistoryRecordConsumer(IChatHistoryRecordService chatHistoryRecordService, IMapper mapper)
    {
        _chatHistoryRecordService = chatHistoryRecordService;
        _mapper = mapper;
    }
    
    public async Task Consume(ConsumeContext<MulticastChatMessageDto> context)
    {
        await _chatHistoryRecordService.RecordMessage(_mapper.Map<ChatMessage>(context.Message),
            context.Message.FileNames, context.Message.FilesContent, context.Message.FilesMetadata);
    }
}