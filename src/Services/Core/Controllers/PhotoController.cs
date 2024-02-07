using Microsoft.AspNetCore.Mvc;
using PixieFit.Core.Models;
using PixieFit.Core.Services;

namespace PixieFit.Core.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PhotoController : Controller
{
    IPhotoService _photoService;

    public PhotoController(
        IPhotoService photoService
    )
    {
        _photoService = photoService;
    }

    [HttpPost]
    [Route("resize")]
    public async Task<IActionResult> Resize(ResizingRequest request)
    {
        var byteResult = _photoService.Resize(request);
        return File(byteResult, "image/jpeg");
    }
}