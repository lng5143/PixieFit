using Microsoft.AspNetCore.Mvc;
using PixieFit.Web.Business.Managers;
using PixieFit.Web.Business.Models;
using Microsoft.AspNetCore.Identity;
using PixieFit.Web.Business.Entities;
using PixieFit.Web.Business.Consts;
using PixieFit.Web.Business.Enums;
using Microsoft.AspNetCore.Http.HttpResults;
using PixieFit.Web.Extensions;
using PixieFit.Web.Business;
using Grpc.Net.Client;
using Google.Protobuf;

namespace PixieFit.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ResizeController 
{
    private readonly ICreditManager _creditManager;
    private readonly UserManager<User> _userManager;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly PFContext _dbContext;

    public ResizeController(
        ICreditManager creditManager,
        UserManager<User> userManager,
        IHttpContextAccessor httpContextAccessor,
        PFContext dbContext)
    {
        _creditManager = creditManager;
        _userManager = userManager;
        _httpContextAccessor = httpContextAccessor;
        _dbContext = dbContext;
    }

    [HttpPost]
    public async Task<ResizeImageResponse> ResizeImage(ResizeImageRequest request)
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request));

        var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext?.User);

        if (user is null)
            throw new Exception("User not found");

        if (user.CreditAmount < PixieFitConsts.ResizeCreditCost)
            throw new Exception("Insufficient credit.");

        var resize = new ResizeAttempt
        {
            UserId = _httpContextAccessor.GetUserId(),
            FileName = request.FileName,
            ResizeHeight = request.ResizeHeight,
            ResizeWidth = request.ResizeWidth,
            ImageSize = request.ImageSize,
            OriginalWidth = request.OriginalWidth,
            OriginalHeight = request.OriginalHeight,
            Status = ResizeStatus.Pending
        };

        user.CreditAmount -= PixieFitConsts.ResizeCreditCost;

        await _dbContext.Resizes.AddAsync(resize);
        await _userManager.UpdateAsync(user);

        await _dbContext.SaveChangesAsync();

        // TODO: Resize image
        var channel = GrpcChannel.ForAddress("http://localhost:5274");
        var client = new Resize.ResizeClient(channel);

        var response = await client.ResizeAsync(new ResizeRequest 
        {
                Image = ByteString.CopyFrom(request.Image),
                Width = request.ResizeWidth,
                Height = request.ResizeHeight
        });

        if (response.Result == (int)ResizeResult.Success)
        {
            resize.Status = ResizeStatus.Completed;
            await _dbContext.SaveChangesAsync();

            return new ResizeImageResponse
            {
                Image = response.ResizedImage.ToByteArray(),
                Message = "Image resized successfully."
            };
        }
        else
        {
            resize.Status = ResizeStatus.Failed;
            user.CreditAmount += PixieFitConsts.ResizeCreditCost;
            await _dbContext.SaveChangesAsync();

            return new ResizeImageResponse
            {
                Message = "Image resize failed."
            };
        }
    }
}