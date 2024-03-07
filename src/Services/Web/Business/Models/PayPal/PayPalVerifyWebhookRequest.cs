using System.Text.Json.Serialization;

namespace PixieFit.Web.Business.Models;

public class PayPalVerifyWebhookRequest
{
    [JsonPropertyName("auth_algo")]
    public string AuthAlgo { get; set; }

    [JsonPropertyName("cert_url")]
    public string CertUrl { get; set; }

    [JsonPropertyName("transmission_id")]
    public string TransmissionId { get; set; }

    [JsonPropertyName("transmission_sig")]
    public string TransmissionSig { get; set; }

    [JsonPropertyName("transmission_time")]
    public DateTime? TransmissionTime { get; set; }

    [JsonPropertyName("webhook_id")]
    public string WebhookId { get; set; }

    [JsonPropertyName("webhook_event")]
    public WebhookEvent WebhookEvent { get; set; }
}

public class WebhookEvent
{
    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("create_time")]
    public DateTime? CreateTime { get; set; }

    [JsonPropertyName("resource_type")]
    public string ResourceType { get; set; }

    [JsonPropertyName("event_type")]
    public string EventType { get; set; }

    [JsonPropertyName("summary")]
    public string Summary { get; set; }

    [JsonPropertyName("resource")]
    public Resource Resource { get; set; }
}

public class Resource
{
    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("create_time")]
    public DateTime? CreateTime { get; set; }

    [JsonPropertyName("update_time")]
    public DateTime? UpdateTime { get; set; }

    [JsonPropertyName("state")]
    public string State { get; set; }

    [JsonPropertyName("amount")]
    public Amount Amount { get; set; }

    [JsonPropertyName("parent_payment")]
    public string ParentPayment { get; set; }

    [JsonPropertyName("valid_until")]
    public DateTime? ValidUntil { get; set; }

    [JsonPropertyName("links")]
    public Link[] Links { get; set; }
}

public class Amount
{
    [JsonPropertyName("total")]
    public string Total { get; set; }

    [JsonPropertyName("currency")]
    public string Currency { get; set; }

    [JsonPropertyName("details")]
    public Details Details { get; set; }
}

public class Details
{
    [JsonPropertyName("subtotal")]
    public string Subtotal { get; set; }
}

public class Link
{
    [JsonPropertyName("href")]
    public string Href { get; set; }

    [JsonPropertyName("rel")]
    public string Rel { get; set; }

    [JsonPropertyName("method")]
    public string Method { get; set; }
}