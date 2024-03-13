namespace PixieFit.Web.Business.Models;

public class PayPalOrderRequest
{
    public decimal Amount { get; set; }
    public Guid CreditPackageId { get; set; }
}