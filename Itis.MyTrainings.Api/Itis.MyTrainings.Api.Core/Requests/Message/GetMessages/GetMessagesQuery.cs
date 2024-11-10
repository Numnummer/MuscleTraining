using Itis.MyTrainings.Api.Contracts.Requests.Message.GetMessages;
using MediatR;

namespace Itis.MyTrainings.Api.Core.Requests.Message.GetMessages;

public class GetMessagesQuery : IRequest<GetMessagesResponse>
{
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="userId">Id пользователя</param>
    public GetMessagesQuery(Guid userId)
    {
        UserId = userId;
    }

    /// <summary>
    /// Id пользователя
    /// </summary>
    public Guid UserId { get; set; }
}