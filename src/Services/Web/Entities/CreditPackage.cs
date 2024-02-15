using System.ComponentModel.DataAnnotations;

namespace PixieFit.Web.Entities;

public class CreditPackage : BaseEntity
{
    // public string Name { get; set; }
    [Required]
    public int Credits { get; set; }
    [Required]
    public decimal Price { get; set; }
}
