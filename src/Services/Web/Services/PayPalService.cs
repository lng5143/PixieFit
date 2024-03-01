using PixieFit.Web.Business.Models;

namespace PixieFit.Web.Services;

public interface IPayPalService
{
    Task<PayPalOrderResponse> CreateOrderAsync(PayPalOrderRequest request);
}

public class PayPalService : IPayPalService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;

    public PayPalService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;
    }

    public async Task<PayPalOrderResponse> CreateOrderAsync(PayPalOrderRequest request)
    {
        return null;

    }
}