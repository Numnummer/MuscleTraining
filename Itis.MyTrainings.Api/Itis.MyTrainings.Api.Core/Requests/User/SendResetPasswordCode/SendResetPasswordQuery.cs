using Itis.MyTrainings.Api.Contracts.Requests.User.GetResetPasswordCode;
using MediatR;

namespace Itis.MyTrainings.Api.Core.Requests.User.GetResetPasswordCode;

/// <summary>
/// Команда запроса <see cref="SendResetPasswordCodeRequest"/>
/// </summary>
public class SendResetPasswordQuery: SendResetPasswordCodeRequest, IRequest<SendResetPasswordCodeResponse>
{
}