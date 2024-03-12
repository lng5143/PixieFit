using Microsoft.AspNetCore.Mvc;
using PixieFit.Web.Business.Managers;

namespace PixieFit.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ResizeController 
{
    private readonly ICreditManager _creditManager;

    public ResizeController(ICreditManager creditManager)
    {
        _creditManager = creditManager;
    }

    [HttpPost]
    public async Task<IActionResult> ResizeImage(ResizeImageRequest request)
    {
        

        return null;
    }
}