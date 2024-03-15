using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace PixieFit.Web.Business.Entities;

public class User : IdentityUser<string>
{
    [Required]
    public string Email { get; set; }

    // [Required]
    // public string PasswordHash { get; set; }

    // [Required]
    // public string Salt { get; set; }

    // public string Username { get; set; }
    public long CreditAmount { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public bool IsDeleted { get; set; }
}