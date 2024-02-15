using System.ComponentModel.DataAnnotations;

namespace PixieFit.Web.Business.Entities;

public class CreditPackage : BaseEntity
{
    [Required]
    public int Credits { get; set; }
    [Required]
    public decimal Price { get; set; }
}
