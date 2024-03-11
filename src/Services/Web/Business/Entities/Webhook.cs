using PixieFit.Web.Business.Enums;

namespace PixieFit.Web.Business.Entities;

public class Webhook: BaseEntity
{
    public WebhookType WebhookType { get; set; }
    public IntegrationPartner IntegrationPartner { get; set; }  
    public string? DataJson { get; set; }
}