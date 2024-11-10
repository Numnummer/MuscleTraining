using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace Itis.MyTrainings.Api.Web.Attributes;

/// <summary>
/// Аттрибут политики
/// </summary>
public class PolicyAttribute : AuthorizeAttribute
{
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="policy">Политики</param>
    public PolicyAttribute(string policy)
        : base(policy)
    {
        AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme;
    }
}