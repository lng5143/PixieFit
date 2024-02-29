namespace PixieFit.Web.Business.Models
{
    public class CheckoutSessionResponse
    {
        public string Id { get; set; }
        public string Object { get; set; }
        public object AfterExpiration { get; set; }
        public object AllowPromotionCodes { get; set; }
        public int AmountSubtotal { get; set; }
        public int AmountTotal { get; set; }
        public AutomaticTax AutomaticTax { get; set; }
        public object BillingAddressCollection { get; set; }
        public object CancelUrl { get; set; }
        public object ClientReferenceId { get; set; }
        public object Consent { get; set; }
        public object ConsentCollection { get; set; }
        public long Created { get; set; }
        public string Currency { get; set; }
        public List<object> CustomFields { get; set; }
        public CustomText CustomText { get; set; }
        public object Customer { get; set; }
        public string CustomerCreation { get; set; }
        public object CustomerDetails { get; set; }
        public object CustomerEmail { get; set; }
        public long ExpiresAt { get; set; }
        public object Invoice { get; set; }
        public InvoiceCreation InvoiceCreation { get; set; }
        public bool Livemode { get; set; }
        public object Locale { get; set; }
        public Dictionary<string, object> Metadata { get; set; }
        public string Mode { get; set; }
        public object PaymentIntent { get; set; }
        public object PaymentLink { get; set; }
        public string PaymentMethodCollection { get; set; }
        public Dictionary<string, object> PaymentMethodOptions { get; set; }
        public List<string> PaymentMethodTypes { get; set; }
        public string PaymentStatus { get; set; }
        public PhoneNumberCollection PhoneNumberCollection { get; set; }
        public object RecoveredFrom { get; set; }
        public object SetupIntent { get; set; }
        public object ShippingAddressCollection { get; set; }
        public object ShippingCost { get; set; }
        public object ShippingDetails { get; set; }
        public List<object> ShippingOptions { get; set; }
        public string Status { get; set; }
        public object SubmitType { get; set; }
        public object Subscription { get; set; }
        public string SuccessUrl { get; set; }
        public TotalDetails TotalDetails { get; set; }
        public string Url { get; set; }
    }

    public class AutomaticTax
    {
        public bool Enabled { get; set; }
        public object Liability { get; set; }
        public object Status { get; set; }
    }

    public class CustomText
    {
        public object ShippingAddress { get; set; }
        public object Submit { get; set; }
    }

    public class InvoiceCreation
    {
        public bool Enabled { get; set; }
        public InvoiceData InvoiceData { get; set; }
    }

    public class InvoiceData
    {
        public object AccountTaxIds { get; set; }
        public object CustomFields { get; set; }
        public object Description { get; set; }
        public object Footer { get; set; }
        public object Issuer { get; set; }
        public Dictionary<string, object> Metadata { get; set; }
        public object RenderingOptions { get; set; }
    }

    public class PhoneNumberCollection
    {
        public bool Enabled { get; set; }
    }

    public class TotalDetails
    {
        public int AmountDiscount { get; set; }
        public int AmountShipping { get; set; }
        public int AmountTax { get; set; }
    }
}