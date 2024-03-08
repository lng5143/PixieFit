using PixieFit.Web.Business;

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

    }
}