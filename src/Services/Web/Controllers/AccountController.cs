using Microsoft.AspNetCore.Mvc;
using PixieFit.Web.Business.Models;
using PixieFit.Web.Services;

namespace PixieFit.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly IAccountService _accountService;

    public AccountController(IAccountService accountService)
    {
        _accountService = accountService;
    }

    [HttpPost("signup")]
    public IActionResult SignUp([FromBody] SignUpRequest request)
    {
        var result = _accountService.CreateAccount(request);
        return Ok();
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        return Ok();
    }

    
}