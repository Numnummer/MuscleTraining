using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace Itis.MyTrainings.Api.Web.Controllers;

/// <summary>
/// Базовый контроллер
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class BaseController: Controller
{
    /// <summary>
    /// Получить идентификатор текущего пользователя
    /// </summary>
    /// <returns>Идентификатор текущего пользователя</returns>
    protected Guid? GetCurrentUserInfo()
    {
        var currentUserId = User != null
            ? User.FindFirst(ClaimTypes.NameIdentifier)
            : null;
        return currentUserId != null
            ? Guid.Parse(currentUserId.Value)
            : null;
    }
}