using PixieFit.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace PixieFit.Web.Controllers;

[ApiController]
public class PaymentController
{
    private readonly IStripeService _stripeService;

    public PaymentController(IStripeService stripeService)
    {
        _stripeService = stripeService;
    }

    // public async Task<IActionResult> CreatePayment(PaymentRequest request)
    // {
    //     await _stripeService.CreatePayment(request);
    //     return Ok();
    // }

    [HttpPost]
    public async Task<IActionResult> StripeIPN()
    {
        return null;
    }

    [HttpPost]
    public async Task<IActionResult> StripeRedirect()
    {
        return null;
    }
}