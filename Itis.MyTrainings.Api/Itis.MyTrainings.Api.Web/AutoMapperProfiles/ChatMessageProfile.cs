using AutoMapper;
using ChatMessageDtos;
using Itis.MyTrainings.Api.Core.Entities.SupportChat;

namespace Itis.MyTrainings.Api.Web.AutoMapperProfiles;

public class ChatMessageProfile : Profile
{
    public ChatMessageProfile()
    {
        CreateMap<UnicastChatMessage, UnicastChatMessageDto>()
            .ForMember(dest => dest.FileNames, opt => 
                opt.Ignore())
            .ForMember(dest => dest.FilesContent, opt => 
                opt.Ignore());
        CreateMap<ChatMessage, MulticastChatMessageDto>()
            .ForMember(dest => dest.FileNames, opt => 
                opt.Ignore())
            .ForMember(dest => dest.FilesContent, opt => 
                opt.Ignore());
    }
}