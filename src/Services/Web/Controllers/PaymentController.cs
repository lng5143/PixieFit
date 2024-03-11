using PixieFit.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace PixieFit.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PaymentController : ControllerBase
{
    private readonly IStripeService _stripeService;
    private readonly IPayPalService _payPalService;
    private readonly ILogger<PaymentController> _logger;

    public PaymentController(
        IStripeService stripeService,
        IPayPalService payPalService,
        ILogger<PaymentController> logger)
    {
        _stripeService = stripeService;
        _payPalService = payPalService;
        _logger = logger;
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

    [HttpPost]
    [Route("paypal-webhook")]
    public async Task<IActionResult> PayPalWebhook()
    {
        var result = await _payPalService.HandleWebhook(HttpContext.Request);

        return result;
    }

    [HttpPost]
    [Route("paypal-webhook")]
    public async Task<IActionResult> PayPalWebhook(PayPalWebhook request)
    {

        return result;
    }
}