using Microsoft.AspNetCore.Mvc;
using PixieFit.Core.Models;
using PixieFit.Core.Services;

namespace PixieFit.Core.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PhotoController : Controller
{
    PhotoService _photoService;

    public PhotoController(
        PhotoService photoService
    )
    {
        _photoService = photoService;
    }

    [HttpPost]
    [Route("resize")]
    public async Task<IActionResult> Resize(ResizingRequest request)
    {
        return null;
    }
}