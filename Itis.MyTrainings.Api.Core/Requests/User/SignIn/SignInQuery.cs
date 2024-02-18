using Itis.MyTrainings.Api.Contracts.Requests.User.SignIn;
using MediatR;

namespace Itis.MyTrainings.Api.Core.Requests.User.SignIn;

/// <summary>
/// Команда запроса <see cref="SignInRequest"/>
/// </summary>
public class SignInQuery: SignInRequest, IRequest<SignInResponse>
{
}