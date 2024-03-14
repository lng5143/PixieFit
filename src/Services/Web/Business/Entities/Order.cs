using PixieFit.Web.Business.Enums;

namespace PixieFit.Web.Business.Entities;

public class Order : BaseEntity
{
    public Guid UserId { get; set; }
    public decimal TotalAmount { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
    public PaymentStatus PaymentStatus { get; set; }
    public Guid CreditPackageId { get; set; }
    public string PaymentPartnerOrderId { get; set; }
}