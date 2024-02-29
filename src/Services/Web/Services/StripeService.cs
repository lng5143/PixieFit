using PixieFit.Web.Business.Model;

namespace PixieFit.Web.Services;

public interface IStripeService
{
    Task RequestPayment(PaymentRequest request);
}

public class StripeService : IStripeService
{
    private readonly PFContext _dbContext;
    private readonly HttpClient _httpClient;

    public StripeService(
        PFContext dbContext,
        HttpClient httpClient
        )
    {
        _dbContext = dbContext;
        _httpClient = httpClient;
    }

    public async Task RequestPayment(PaymentRequest request)
    {
        return null;
    }
}