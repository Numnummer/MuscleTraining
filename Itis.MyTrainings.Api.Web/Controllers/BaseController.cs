using System.Security.Claims;
using Itis.MyTrainings.Api.Core.Exceptions;
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
    /// Идентификатор текущего пользователя
    /// </summary>
    protected Guid CurrentUserId => GetCurrentUserId();
    
    private Guid GetCurrentUserId()
    {
        var currentUserId = User.FindFirst(ClaimTypes.NameIdentifier);
        return Guid.TryParse(currentUserId?.Value, out var result)
            ? result
            : throw new NotFoundException("Текущий пользователь не найден");
    }
}