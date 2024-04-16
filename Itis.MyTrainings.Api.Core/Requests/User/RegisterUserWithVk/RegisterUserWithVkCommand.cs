﻿using Itis.MyTrainings.Api.Contracts.Requests.User.RegisterUserWithVk;
using MediatR;

namespace Itis.MyTrainings.Api.Core.Requests.User.RegisterUserWithVk;


/// <summary>
/// Команда запроса <see cref="RegisterUserWithVkRequest"/>
/// </summary>
public class RegisterUserWithVkCommand : RegisterUserWithVkRequest, IRequest<RegisterUserWithVkResponse>
{
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="accessToken">Код авторизации</param>
    public RegisterUserWithVkCommand(string accessToken) : 
        base(accessToken)
    {
    }
}