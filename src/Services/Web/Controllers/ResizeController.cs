using Microsoft.AspNetCore.Mvc;
using PixieFit.Web.Business.Managers;
using PixieFit.Web.Business.Models;
using Microsoft.AspNetCore.Identity;
using PixieFit.Web.Business.Entities;  

namespace PixieFit.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ResizeController 
{
    private readonly ICreditManager _creditManager;
    private readonly UserManager<User> _userManager;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ResizeController(
        ICreditManager creditManager,
        UserManager<User> userManager,
        IHttpContextAccessor httpContextAccessor)
    {
        _creditManager = creditManager;
        _userManager = userManager;
        _httpContextAccessor = httpContextAccessor;
    }

    [HttpPost]
    public async Task<IActionResult> ResizeImage(ResizeImageRequest request)
    {
        
        return null;
    }
}