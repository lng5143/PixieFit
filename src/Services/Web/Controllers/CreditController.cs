using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PixieFit.Web.Business;
using PixieFit.Web.Business.Entities;
using PixieFit.Web.Business.Models;
using PixieFit.Web.Services;

namespace PixieFit.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CreditController
{
    private readonly UserManager<User> _userManager;
    private readonly PFContext _dbContext;
    private readonly IStripeService _stripeService;
    private readonly IPayoneerService _payoneerService;
    private readonly IPayPalService _payPalService;

    public CreditController(
        UserManager<User> userManager,
        PFContext dbContext,
        IStripeService stripeService,
        IPayoneerService payoneerService,
        IPayPalService payPalService
        )
    {
        _userManager = userManager;
        _dbContext = dbContext;
        _stripeService = stripeService;
        _payoneerService = payoneerService;
        _payPalService = payPalService;
    }

    [HttpPost]
    public async Task<IActionResult> RequestBuyCredits(BuyCreditRequest request)
    {
        var paymentRequest = new PayPalOrderRequest
        {
            
        };
        var response = await _payPalService.CreateOrderAsync(paymentRequest);

        return null;
    }
}