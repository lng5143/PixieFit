using Microsoft.AspNetCore.Mvc;
using PixieFit.Web.Business.Models;
using PixieFit.Web.Services;

namespace PixieFit.Web.Controllers;

public class CreditController
{
    private readonly IPaymentService _paymentService;
    private readonly UserManager<User> _userManager;

    public CreditController(
        IPaymentService paymentService,
        UserManager<User> userManager
        )
    {
        _paymentService = paymentService;
        _userManager = userManager;
    }
}