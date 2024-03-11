using PixieFit.Web.Services;

namespace PixieFit.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
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
    public async Task<IActionResult> StripeIPN(StripeIPNWebHook request)
    {

    }

    [HttpPost]
    public async Task<IActionResult> StripeRedirect(StripeRedirect request)
    {

    }

    [HttpPost]
    [Route("paypal-webhook")]
    public async Task<IActionResult> PayPalWebhook(PayPalWebhook request)
    {
        _logger.LogInformation("PayPal Webhook received");
        return Ok();
    }
}