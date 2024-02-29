namespace PixieFit.Web.Services;

public interface IPaymentService
{
    Task CreatePayment(PaymentRequest request);
}

public class PaymentService 
{
    private readonly PFContext _dbContext;
    private readonly HttpClient _httpClient;

    public PaymentService(
        PFContext dbContext,
        HttpClient httpClient
        )
    {
        _dbContext = dbContext;
        _httpClient = httpClient;
    }

    
}