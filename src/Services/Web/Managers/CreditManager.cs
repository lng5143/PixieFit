using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PixieFit.Web.Business;
using PixieFit.Web.Business.Entities;
using PixieFit.Web.Business.Enums;

namespace PixieFit.Web.Business.Managers;

public interface ICreditManager
{
    Task HandleSuccessfulPayment();
}

public class CreditManager : ICreditManager
{
    private readonly PFContext _dbContext;

    public CreditManager(PFContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task HandleSuccessfulPayment()
    {
        try 
        {
            // var userTransaction = new UserTransaction
            // {
            //     TransactionType = UserTransactionType.Deposit,
            //     CreditAmount = 100
            // };

            var userId = "";
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId);

            if (user is null)
            {
                throw new Exception("User not found");
            }

            using var transaction = _dbContext.Database.BeginTransaction();

            // _dbContext.UserTransactions.Add(userTransaction);
            // user.CreditAmount += userTransaction.CreditAmount;

            transaction.Commit();
        }
        catch(Exception ex)
        {
            throw new Exception("Fail to handle payment");
        }


    }
}