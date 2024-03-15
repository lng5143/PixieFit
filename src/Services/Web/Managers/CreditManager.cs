using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PixieFit.Web.Business;
using PixieFit.Web.Business.Entities;
using PixieFit.Web.Business.Enums;
using Microsoft.AspNetCore.Identity;
using PixieFit.Web.Extensions;

namespace PixieFit.Web.Business.Managers;

public interface ICreditManager
{
    Task HandleSuccessfulPayment();
}

public class CreditManager : ICreditManager
{
    private readonly PFContext _dbContext;
    private readonly UserManager<User> _userManager;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CreditManager(
        PFContext dbContext,
        UserManager<User> userManager,
        IHttpContextAccessor httpContextAccessor)
    {
        _dbContext = dbContext;
        _userManager = userManager;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task HandleSuccessfulPayment()
    {
        try 
        {
            var userId = _httpContextAccessor.GetUserId();
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId.ToString());

            if (user is null)
            {
                throw new Exception("User not found");
            }

            using var transaction = _dbContext.Database.BeginTransaction();

            // user.CreditAmount += userTransaction.CreditAmount;

            transaction.Commit();
        }
        catch(Exception ex)
        {
            throw new Exception("Fail to handle payment");
        }


    }
}