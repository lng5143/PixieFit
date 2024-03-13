using System.Security.Claims;

namespace PixieFit.Web.Extensions;

public static class HttpContextAccessorExtensions
{
    public static Guid GetUserId(this IHttpContextAccessor httpContextAccessor)
    {
        Guid.TryParse(httpContextAccessor.HttpContext?.User.FindFirst(x => x.Type == "sub")?.Value, out Guid userId);
        return userId;
    }
}