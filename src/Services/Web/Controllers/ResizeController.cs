using Microsoft.AspNetCore.Mvc;
using PixieFit.Web.Business.Managers;
using PixieFit.Web.Business.Models;
using Microsoft.AspNetCore.Identity;
using PixieFit.Web.Business.Entities;
using PixieFit.Web.Business.Consts;
using PixieFit.Web.Business.Enums;

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
        var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);

        if (user.CreditAmount < PixieFitConsts.ResizeCreditCost)
        {
            return BadRequest("Not enough credits");
        }

        user.CreditAmount -= PixieFitConsts.ResizeCreditCost;

        await _userManager.UpdateAsync(user);

        // TODO: Resize image
        var channel = GrpcChannel.ForAddress("http://localhost:5274");
        var client = new Resizer.ResizerClient(channel);

        var response = client.Resize(new ResizeRequest 
        {

        });

        if (response.ResultCode != ResizeResult.Success)
        {

        }

        return null;
    }
}