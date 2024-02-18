using Itis.MyTrainings.Api.Contracts.Requests.User.ResetPassword;
using MediatR;

namespace Itis.MyTrainings.Api.Core.Requests.User.ResetPassword;

/// <summary>
/// Команда запроса <see cref="ResetPasswordRequest"/>
/// </summary>
public class ResetPasswordCommand: ResetPasswordRequest, IRequest<ResetPasswordResponse>
{
}