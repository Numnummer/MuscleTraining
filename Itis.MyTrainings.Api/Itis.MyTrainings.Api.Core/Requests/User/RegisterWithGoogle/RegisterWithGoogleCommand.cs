using Itis.MyTrainings.Api.Contracts.Requests.User.RegisterWithGoogle;
using MediatR;

namespace Itis.MyTrainings.Api.Core.Requests.User.RegisterWithGoogle;

public class RegisterWithGoogleCommand : IRequest<RegisterWithGoogleResponse>
{
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="token">Токен</param>
    public RegisterWithGoogleCommand(string token)
    {
        Token = token;
    }
    
    /// <summary>
    /// Токен
    /// </summary>
    public string Token { get; set; }
}