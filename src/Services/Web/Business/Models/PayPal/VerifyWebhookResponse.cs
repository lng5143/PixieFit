using System.Text.Json.Serialization;

namespace PixieFit.Web.Business.Models;

public class VerifyWebhookResponse
{
    [JsonPropertyName("verification_status")]
    public string VerificationStatus { get; set; }
}
