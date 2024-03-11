using PixieFit.Web.Business;
using PixieFit.Web.Business.Models;

namespace PixieFit.Web.Services;

public interface IPayoneerService
{
    Task CreatePayment(PaymentRequest request);
}

public class PayoneerService : IPayoneerService
{
    private readonly PFContext _dbContext;

    public PayoneerService(
        PFContext dbContext
        )
    {
        _dbContext = dbContext;
    }

    public async Task CreatePayment(PaymentRequest request)
    {
        
    }
}