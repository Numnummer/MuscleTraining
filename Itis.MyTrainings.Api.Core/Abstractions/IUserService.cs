using System.Security.Claims;
using Itis.MyTrainings.Api.Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace Itis.MyTrainings.Api.Core.Abstractions;

/// <summary>
/// Сервис для работы с пользователем
/// </summary>
public interface IUserService
{
    /// <summary>
    /// Зарегестрировать пользователя
    /// </summary>
    /// <param name="user">Пользователь</param>
    /// <param name="password">Пароль</param>
    /// <returns></returns>
    public Task<IdentityResult> RegisterUserAsync(User user, string password);

    /// <summary>
    /// Зарегестрировать пользователя
    /// </summary>
    /// <param name="user">Пользователь</param>
    /// <returns></returns>
    public Task<IdentityResult> RegisterUserAsync(User user);

    /// <summary>
    /// Найти пользователя по id
    /// </summary>
    /// <param name="guid"></param>
    /// <returns></returns>
    public Task<User?> FindUserByIdAsync(Guid guid);

    /// <summary>
    /// Добавить связь пользователя с ролью
    /// </summary>
    /// <param name="user">Пользователь</param>
    /// <param name="roleName">Имя роли</param>
    /// <returns></returns>
    public Task<IdentityResult> AddUserRoleAsync(User user, string roleName);
    
    /// <summary>
    /// Добавить дополнительную информацию о пользователе
    /// </summary>
    /// <param name="user">Пользователь</param>
    /// <param name="claims">Доп. информация</param>
    /// <returns></returns>
    public Task<IdentityResult> AddClaimsAsync(User user, IEnumerable<Claim> claims);

    /// <summary>
    /// Получить дополнительную информацию о пользователе
    /// </summary>
    /// <param name="user">Пользователь</param>
    /// <returns></returns>
    public Task<IList<Claim>> GetClaimsAsync(User user);

    /// <summary>
    /// Получить роль пользователя
    /// </summary>
    /// <param name="user">Пользователь</param>
    /// <returns></returns>
    public Task<string?> GetRoleAsync(User user);
    
    /// <summary>
    /// Получить пользователя по Email
    /// </summary>
    /// <param name="email">Email</param>
    /// <returns></returns>
    public Task<User?> FindUserByEmailAsync(string email);

    /// <summary>
    /// Войти с помощью пароля
    /// </summary>
    /// <param name="user">Пользователь</param>
    /// <param name="password">Пароль</param>
    /// <returns></returns>
    public Task<SignInResult> SignInWithPasswordAsync(User user, string password);

    /// <summary>
    /// Сбросить пароль
    /// </summary>
    /// <param name="user">Пользователь</param>
    /// <param name="code">Код подтверждения</param>
    /// <param name="newPassword">Новый пароль</param>
    /// <returns></returns>
    public Task<IdentityResult> SetPasswordWithEmailAsync(User user, string code, string newPassword);

    /// <summary>
    /// Получить персональный токен для сброса пароля
    /// </summary>
    /// <param name="user">Пользователь</param>
    /// <returns></returns>
    public Task<string> GetPasswordResetTokenAsync(User user);

    /// <summary>
    /// Получить профиль пользователя по id
    /// </summary>
    /// <param name="userProfileId">Идентификатор профиля пользователя</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns></returns>
    public Task<UserProfile?> FindUserProfileById(Guid? userProfileId, CancellationToken cancellationToken);
}