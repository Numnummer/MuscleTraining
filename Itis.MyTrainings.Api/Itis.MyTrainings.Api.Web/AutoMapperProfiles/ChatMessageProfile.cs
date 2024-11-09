using AutoMapper;
using ChatMessageDtos;
using Itis.MyTrainings.Api.Core.Entities.SupportChat;

namespace Itis.MyTrainings.Api.Web.AutoMapperProfiles;

public class ChatMessageProfile : Profile
{
    public ChatMessageProfile()
    {
        CreateMap<UnicastChatMessage, UnicastChatMessageDto>();
        CreateMap<ChatMessage, MulticastChatMessageDto>();
    }
}