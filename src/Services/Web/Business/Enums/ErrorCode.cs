using System.ComponentModel;

namespace PixieFit.Web.Business.Enums;

public enum ErrorCode 
{
    [Description("Email is required")]
    EMAIL_REQUIRED,
    [Description("Password is required")]
    PASSWORD_REQUIRED,
    [Description("Username is required")]
    USERNAME_REQUIRED
}