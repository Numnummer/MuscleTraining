using AutoMapper;
using ChatMessageDtos;
using Grpc.Core;
using Itis.MyTrainings.Api.Core.Abstractions;
using Itis.MyTrainings.Api.Core.Entities.SupportChat;
using MassTransit;
using YourNamespace.Grpc;
using Group = Itis.MyTrainings.Api.Core.Entities.SupportChat.Group;

namespace Itis.MyTrainings.Api.Web.GrpcServices;

/// <summary>
/// 
/// </summary>
public class MessagingService:Messaging.MessagingBase
{
    private readonly IBus _bus;
    private readonly IMapper _mapper;
    private readonly ILogger<MessagingService> _logger;
    private readonly IMessageStatsService _messageStatsService;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="mapper"></param>
    /// <param name="bus"></param>
    public MessagingService(IMapper mapper, IBus bus, ILogger<MessagingService> logger, IMessageStatsService messageStatsService)
    {
        _mapper = mapper;
        _bus = bus;
        _logger = logger;
        _messageStatsService = messageStatsService;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public override async Task<MessageResponse> SendMulticastMessage(MulticastMessageRequest request, ServerCallContext context)
    {
        var filesContent = request.FilesContentBase64
            .Select(content => Convert.FromBase64String(content.ToBase64())).ToArray();
        var date = DateTime.Now;
        var group = new Group();
        _logger.LogInformation("Message received"+" "+request.Destination);
        var message = new ChatMessage();
        var dto = new MulticastChatMessageDto();
        switch (request.Destination)
        {
            case "coach-user":
                group = Group.UserCoach;
                message = new ChatMessage()
                {
                    GroupName = group,
                    MessageText = request.MessageText,
                    Id = Guid.NewGuid(),
                    SendDate = date,
                    SenderEmail = request.Author,
                };
                dto = _mapper.Map<MulticastChatMessageDto>(message);
                var endpoint = await _bus.GetSendEndpoint(new Uri("exchange:messaging_exchange?bind=true&type=direct&queue=flutter_chat_coach-user&durable=true"));
                await endpoint.Send(dto);
                break;
            case "admin-user":
                group = Group.UserAdmin;
                break;
            case "coach-admin":
                group = Group.UserCoach;  
                break;
        }

        await _messageStatsService.ProcessMessageAsync(Guid.Parse(request.Author));

        message = new ChatMessage()
        {
            GroupName = group,
            MessageText = request.MessageText,
            Id = Guid.NewGuid(),
            SendDate = date,
            SenderEmail = request.Author,
        };
        dto = _mapper.Map<MulticastChatMessageDto>(message);
        dto.FileNames = request.FileNames.ToArray();
        dto.FilesContent = filesContent;
        dto.FilesMetadata = request.FilesMetadata.ToArray();
        await _bus.Publish(dto);
        return new MessageResponse()
        {
            Success = true,
            Message = request.MessageText,
        };
    }

    public override Task<MessageResponse> SendUnicastMessage(UnicastMessageRequest request, ServerCallContext context)
    {
        return base.SendUnicastMessage(request, context);
    }
}