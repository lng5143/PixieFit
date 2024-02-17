using System.ComponentModel.DataAnnotations;

namespace PixieFit.Web.Business.Entities;

public class User : BaseEntity
{
    [Required]
    public string Email { get; set; }

    [Required]
    public string PasswordHash { get; set; }

    [Required]
    public string Salt { get; set; }

    public string Username { get; set; }
    public long CreditAmount { get; set; }
}