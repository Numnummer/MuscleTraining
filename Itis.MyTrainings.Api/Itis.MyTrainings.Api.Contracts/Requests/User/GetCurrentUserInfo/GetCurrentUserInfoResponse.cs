﻿using Itis.MyTrainings.Api.Contracts.Enums;
using Itis.MyTrainings.Api.Contracts.Requests.User.GetResetPasswordCode;

namespace Itis.MyTrainings.Api.Contracts.Requests.User.GetCurrentUserInfo;

/// <summary>
/// Ответ на запрос <see cref="SendResetPasswordCodeRequest"/>
/// </summary>
public class GetCurrentUserInfoResponse
{
    /// <summary>
    /// Идентификатор пользователя
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Идентификатор профиля пользователя
    /// </summary>
    public Guid? UserProfileId { get; set; }

    /// <summary>
    /// Почта
    /// </summary>
    public string? Email { get; set; }
    
    /// <summary>
    /// Имя пользователя
    /// </summary>
    public string FirstName { get; set; } = default!;
        
    /// <summary>
    /// Фамилия пользователя
    /// </summary>
    public string LastName { get; set; } = default!;
    
    /// <summary>
    /// Пол
    /// </summary>
    public string? Gender { get; set; }
    
    /// <summary>
    /// Дата рождения
    /// </summary>
    public DateTime? DateOfBirth { get; set; }
    
    /// <summary>
    /// Номер телефона
    /// </summary>
    public string? PhoneNumber { get; set; }

    /// <summary>
    /// Рост
    /// </summary>
    public int? Height { get; set; }
    
    /// <summary>
    /// Вес
    /// </summary>
    public int? Weight { get; set; }
}