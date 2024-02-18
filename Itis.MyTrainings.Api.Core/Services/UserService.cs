using System.Security.Claims;
using Itis.MyTrainings.Api.Core.Abstractions;
using Itis.MyTrainings.Api.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Itis.MyTrainings.Api.Core.Services;

/// <inheritdoc />
public class UserService: IUserService
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IDbContext _dbContext;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="userManager">API для управления пользователем</param>
    /// <param name="signInManager">API для входа пользователей</param>
    /// <param name="dbContext">Контекст бд</param>
    public UserService(
        UserManager<User> userManager,
        SignInManager<User> signInManager,
        IDbContext dbContext)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _dbContext = dbContext;
    }
    
    /// <inheritdoc />
    public async Task<User?> FindUserByIdAsync(Guid guid)
        => await _userManager.FindByIdAsync(guid.ToString());

    /// <inheritdoc />
    public async Task<IdentityResult> RegisterUserAsync(User user, string password)
        => await _userManager.CreateAsync(user, password);

    /// <inheritdoc />
    public async Task<IdentityResult> RegisterUserAsync(User user)
        => await _userManager.CreateAsync(user);

    /// <inheritdoc />
    public async Task<IdentityResult> AddUserRoleAsync(User user, string roleName)
        => await _userManager.AddToRoleAsync(user, roleName);

    /// <inheritdoc />
    public async Task<IdentityResult> AddClaimsAsync(User user, IEnumerable<Claim> claims)
        => await _userManager.AddClaimsAsync(user, claims);
    
    /// <inheritdoc />
    public async Task<IList<Claim>> GetClaimsAsync(User user)
        => await _userManager.GetClaimsAsync(user);

    /// <inheritdoc />
    public async Task<string?> GetRoleAsync(User user)
    {
        var claims = await _userManager.GetClaimsAsync(user);
        return claims.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value;
    }

    /// <inheritdoc />
    public async Task<User?> FindUserByEmailAsync(string email)
        => await _userManager.FindByEmailAsync(email);

    /// <inheritdoc />
    public async Task<SignInResult> SignInWithPasswordAsync(User user, string password)
        => await _signInManager.PasswordSignInAsync(user, password, false, false);

    /// <inheritdoc />
    public async Task<IdentityResult> SetPasswordWithEmailAsync(User user, string code, string newPassword)
        => await _userManager.ResetPasswordAsync(user, code, newPassword);

    /// <inheritdoc />
    public async Task<string> GetPasswordResetTokenAsync(User user)
    {
        await _userManager.UpdateSecurityStampAsync(user);
        
        var result = await _userManager.GeneratePasswordResetTokenAsync(user);
        
        return result;
    }

    /// <inheritdoc />
    public async Task<UserProfile?> FindUserProfileById(Guid? userProfileId, CancellationToken cancellationToken)
    {
        if (userProfileId == null)
            throw new ArgumentNullException(nameof(userProfileId));
            
        var result = await _dbContext.UserProfiles
            .FirstOrDefaultAsync(x => x.Id == userProfileId, cancellationToken);

        return result;
    }
}