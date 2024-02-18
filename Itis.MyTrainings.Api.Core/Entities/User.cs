using System.ComponentModel.DataAnnotations;
using Itis.MyTrainings.Api.Core.Abstractions;
using Itis.MyTrainings.Api.Core.Exceptions;
using Microsoft.AspNetCore.Identity;

namespace Itis.MyTrainings.Api.Core.Entities;

/// <summary>
/// Пользователь
/// </summary>
public class User: IdentityUser<Guid>, IEntity
{
    private UserProfile? _profile;
    
    /// <summary>
    /// Имя пользователя
    /// </summary>
    [Required]
    public string FirstName { get; set; } = default!;
        
    /// <summary>
    /// Фамилия пользователя
    /// </summary>
    [Required]
    public string LastName { get; set; } = default!;

    /// <summary>
    /// Идентификатор профиля пользователя
    /// </summary>
    public Guid? ProfileId { get; private set; }

    /// <summary>
    /// Профиль пользователя
    /// </summary>
    public UserProfile? Profile 
    { 
        get => _profile;
        set 
        {
            _profile = value;
            ProfileId = _profile?.Id;
        }
    }
}