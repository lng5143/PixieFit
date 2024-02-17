using Microsoft.EntityFrameworkCore;
using PixieFit.Web.Business;
using PixieFit.Web.Business.Models;

namespace PixieFit.Web.Services;

public interface IAccountService
{
    Task CreateAccount(SignUpRequest request);
}

public class AccountService 
{
    private readonly PFContext _dbContext;

    public AccountService(PFContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task CreateAccount(SignUpRequest request)
    {

    }
}