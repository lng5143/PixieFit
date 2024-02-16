using System.ComponentModel.DataAnnotations;

namespace PixieFit.Web.Business.Entities;

public class User : BaseEntity
{
    [Required]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }

    public string Username { get; set; }
    public long Credits { get; set; }
}