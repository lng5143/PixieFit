using System.ComponentModel.DataAnnotations;

namespace PixieFit.Web.Entities;

public class User : BaseEntity
{
    [Required]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    
}