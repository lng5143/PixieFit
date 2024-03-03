using PixieFit.Web.Business.Model;
using 

namespace PixieFit.Web.Services;

public interface IStripeService
{
    Task RequestPayment(PaymentRequest request);
}

public class StripeService : IStripeService
{
    private readonly PFContext _dbContext;
    

    public StripeService(
        PFContext dbContext
        )
    {
        _dbContext = dbContext;
    }

    public async Task<string> RequestPayment(PaymentRequest request)
    {
        var httpClient = new HttpClient();
        httpClient.BaseAddress = new Uri("https://api.stripe.com");

        var response = await httpClient.PostAsJsonAsync("/v1/payment_intents", request);

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("Payment failed");
        }

        var responseString = JsonSerializer.Deserialize<CheckoutSessionResponse>(response.Content.ReadAsStringAsync());
        return responseString.Url;
    }

    
}