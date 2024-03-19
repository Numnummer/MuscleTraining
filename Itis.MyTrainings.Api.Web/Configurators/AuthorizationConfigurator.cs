using System.Security.Claims;
using Itis.MyTrainings.Api.Core.Constants;
using Microsoft.AspNetCore.Authorization;

namespace Itis.MyTrainings.Api.Web.Configurators;

public static class AuthorizationConfigurator
{
    /// <summary>
    /// Добавить и настроить роли
    /// </summary>
    /// <param name="opt">AuthorizationOptions</param>
    public static void AddRoles(this AuthorizationOptions opt)
    {
        opt.AddPolicy(PolicyConstants.IsAdministrator, policyBuilder =>
        {
            policyBuilder.RequireClaim(ClaimTypes.Role, Roles.Administrator);
        });
        opt.AddPolicy(PolicyConstants.IsCoach, policyBuilder =>
        {
            policyBuilder
                .RequireAssertion(
                    x => 
                        x.User.HasClaim(ClaimTypes.Role, Roles.Coach) || 
                        x.User.HasClaim(ClaimTypes.Role, Roles.Administrator));
        });
        opt.AddPolicy(PolicyConstants.IsDefaultUser, policyBuilder =>
        {
            policyBuilder
                .RequireAssertion(
                    x =>
                        x.User.HasClaim(ClaimTypes.Role, Roles.User) ||
                        x.User.HasClaim(ClaimTypes.Role, Roles.Coach) ||
                        x.User.HasClaim(ClaimTypes.Role, Roles.Administrator));
        });
    }
}