using PixieFit.Web.Business.Models;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

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

    public async Task<PayPalAuthResponse> GetAccessToken()
    {
        using var client = new HttpClient();
        var clientId = _configuration["PayPal:ClientId"];
        var clientSecret = _configuration["PayPal:Secret"];
        var baseUrl = _configuration["PayPal:BaseUrl"];

        // Base URL for Sandbox environment
            client.BaseAddress = new Uri(baseUrl);

        // Combine client id and secret for basic authentication
        var authorizationHeader = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{clientId}:{clientSecret}"));

        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authorizationHeader);

        // Set content type for form data
        // client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        client. = new MediaTypeHeaderValue("application/x-www-form-urlencoded");

        // Form data
        var formDataContent = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("grant_type", "client_credentials")
        });

        // Send POST request
        var response = await client.PostAsync("/v1/oauth2/token", formDataContent);

        // Check for successful response
        if (response.IsSuccessStatusCode)
        {
            var responseString = await response.Content.ReadAsStringAsync();
            var responseObj = JsonSerializer.Deserialize<PayPalAuthResponse>(responseString);
            return responseObj;
        }
        else
        {
            throw new Exception($"Error getting access token: {response.StatusCode}");
        }
    }
}