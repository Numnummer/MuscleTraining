using AutoMapper;
using ChatMessageDtos;
using Itis.MyTrainings.ChatHistoryService.Core.Models.SupportChat.Entities;

namespace Itis.MyTrainings.ChatHistoryService.Web.AutoMapperProfiles;

public class ChatMessageProfile : Profile
{
    public ChatMessageProfile()
    {
        CreateMap<UnicastChatMessageDto, UnicastChatMessage>()
            .ForSourceMember(source=>source.FileNames, opt=>
                opt.DoNotValidate())
            .ForSourceMember(source=>source.FilesContent, opt=>
                opt.DoNotValidate());
        CreateMap<MulticastChatMessageDto, ChatMessage>()
            .ForSourceMember(source=>source.FileNames, opt=>
                opt.DoNotValidate())
            .ForSourceMember(source=>source.FilesContent, opt=>
                opt.DoNotValidate());
    }
}