using PixieFit.Web.Business;
using PixieFit.Web.Business.Models;
using PixieFit.Web.Business.Managers;
using PixieFit.Web.Business.Entities;
using PixieFit.Web.Business.Enums;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http.HttpResults;
using Serilog;
using PixieFit.Web.Extensions;

namespace PixieFit.Web.Services;

public interface IPayPalService
{
    Task<PayPalOrderResponse> CreateOrderAsync(PayPalOrderRequest request);
    Task HandleWebhook(HttpRequest request);
}

public class PayPalService : IPayPalService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;
    private readonly ICreditManager _creditManager;
    private readonly PFContext _dbContext;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public PayPalService(
        HttpClient httpClient, 
        IConfiguration configuration,
        ICreditManager creditManager,
        PFContext dbContext,
        IHttpContextAccessor httpContextAccessor)
    {
        _httpClient = httpClient;
        _configuration = configuration;
        _creditManager = creditManager;
        _dbContext = dbContext;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<PayPalOrderResponse> CreateOrderAsync(PayPalOrderRequest request)
    {
        var order = new Order
        {
            UserId = _httpContextAccessor.GetUserId(),
            TotalAmount = request.Amount,
            PaymentMethod = PaymentMethod.PAYPAL,
            PaymentStatus = PaymentStatus.PENDING,
            CreditPackageId = request.CreditPackageId
        };

        _dbContext.Orders.Add(order);
        await _dbContext.SaveChangesAsync();

        // TODO: call PayPalService to create order 

        return null;
    }

    public async Task<string> GetAccessToken()
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

            Log.Information($"PayPal access token response: {responseString}");

            var responseObj = JsonSerializer.Deserialize<PayPalAuthResponse>(responseString);
            return responseObj.AccessToken;
        }
        else
        {
            throw new Exception($"Error getting access token: {response.StatusCode}");
        }
    }

    public async Task HandleWebhook(HttpRequest request)
    {
        var json = await new StreamReader(request.Body).ReadToEndAsync();
        // var headers = request.Headers;

        var verifyResult = await VerifyWebhookSignatureV2(request);
        if (verifyResult.Equals("SUCCESS"))
        {
            var webhookEvent = JsonSerializer.Deserialize<WebhookEvent>(json);
            if (webhookEvent?.EventType == "CHECKOUT.ORDER.APPROVED")
            {
                await _creditManager.HandleSuccessfulPayment();
            }
        }
        else
        {
            throw new Exception("failed to verify webhook response");
        }

    }

    public async Task VerifyWebHookSignature(string json, IHeaderDictionary headerDictionary)
    {
        // !!IMPORTANT!!
        // Without this direct JSON serialization, PayPal WILL ALWAYS return verification_status = "FAILURE".
        // This is probably because the order of the fields are different and PayPal does not sort them. 
        var paypalVerifyRequestJsonString = $@"{{
            ""transmission_id"": ""{headerDictionary["PAYPAL-TRANSMISSION-ID"][0]}"",
            ""transmission_time"": ""{headerDictionary["PAYPAL-TRANSMISSION-TIME"][0]}"",
            ""cert_url"": ""{headerDictionary["PAYPAL-CERT-URL"][0]}"",
            ""auth_algo"": ""{headerDictionary["PAYPAL-AUTH-ALGO"][0]}"",
            ""transmission_sig"": ""{headerDictionary["PAYPAL-TRANSMISSION-SIG"][0]}"",
            ""webhook_id"": ""<get from paypal developer dashboard>"",
            ""webhook_event"": {json}
            }}";

        using var client = new HttpClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await GetAccessToken());

        var content = new StringContent(paypalVerifyRequestJsonString, Encoding.UTF8, "application/json");

        var resultResponse = await client.PostAsync("https://api-m.sandbox.paypal.com/v1/notifications/verify-webhook-signature", content);

        var responseBody = await resultResponse.Content.ReadAsStringAsync();

        Log.Information($"PayPal verify webhook response: {responseBody}");

        var verifyWebhookResponse = JsonSerializer.Deserialize<VerifyWebhookResponse>(responseBody);

        if (verifyWebhookResponse?.VerificationStatus != "SUCCESS")
        {
            throw new Exception("failed to verify webhook response");
        }
    }

    public async Task<string> VerifyWebhookSignatureV2(HttpRequest request)
    {

        var webhookEvent = request.Body;
        var headers = request.Headers;

        var verifyRequest = new PayPalVerifyWebhookRequest {
            AuthAlgo = headers["PAYPAL-AUTH-ALGO"],
            CertUrl = headers["PAYPAL-CERT-URL"],
            TransmissionId = headers["PAYPAL-TRANSMISSION-ID"],
            TransmissionSig = headers["PAYPAL-TRANSMISSION-SIG"],
            TransmissionTime = DateTime.Parse(headers["PAYPAL-TRANSMISSION-TIME"]),
            WebhookId = "<get from paypal developer dashboard>",
            WebhookEvent = webhookEvent
        };

        using var client = new HttpClient();

        var baseUrl = _configuration["PayPal:BaseUrl"];

        client.BaseAddress = new Uri(baseUrl);
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await GetAccessToken());

        var content = new StringContent(JsonSerializer.Serialize(verifyRequest), Encoding.UTF8, "application/json");
        var resultResponse = await client.PostAsync("https://api-m.sandbox.paypal.com/v1/notifications/verify-webhook-signature", content);

        if (resultResponse is null)
        {
            throw new Exception("failed to verify webhook response");
        }

        var responseBody = await resultResponse.Content.ReadAsStringAsync();
        var verifyWebhookResponse = JsonSerializer.Deserialize<VerifyWebhookResponse>(responseBody);

        return verifyWebhookResponse?.VerificationStatus;
    }
}
