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
}