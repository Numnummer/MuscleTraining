using AutoMapper;
using ChatMessageDtos;
using Itis.MyTrainings.ChatHistoryService.Core.Models.SupportChat.Entities;

namespace Itis.MyTrainings.ChatHistoryService.Web.AutoMapperProfiles;

public class ChatMessageProfile : Profile
{
    public ChatMessageProfile()
    {
        CreateMap<UnicastChatMessageDto, UnicastChatMessage>();
        CreateMap<MulticastChatMessageDto, ChatMessage>();
    }
}