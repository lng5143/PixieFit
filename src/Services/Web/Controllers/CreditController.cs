using Microsoft.AspNetCore.Mvc;
using PixieFit.Web.Business.Models;
using PixieFit.Web.Services;

namespace PixieFit.Web.Controllers;

public class CreditController
{
    private readonly IPaymentService _paymentService;
    private readonly UserManager<User> _userManager;
    private readonly PFContext _dbContext;
    private readonly IStripeService _stripeService;

    [ApiController]
    public CreditController(
        IPaymentService paymentService,
        UserManager<User> userManager,
        PFContext dbContext,
        IStripeService stripeService
        )
    {
        _paymentService = paymentService;
        _userManager = userManager;
        _dbContext = dbContext;
        _stripeService = stripeService;
    }

    [HttpPost]
    public async Task<IActionResult> RequestBuyCredits(BuyCreditRequest request)
    {
        var paymentRequest = new PaymentRequest
        {
            Amount = request.Amount
        };
        var response = await _stripeService.RequestPayment(paymentRequest);

        return response;
    }
}