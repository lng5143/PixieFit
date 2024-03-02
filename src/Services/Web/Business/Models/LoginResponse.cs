using IdentityModel.Client;

namespace PixieFit.Web.Business.Models;

public class LoginResponse
{
    // public string AccessToken { get; set; }
    // public string RefreshToken { get; set; }
    // public string Username { get; set; }

    public TokenResponse TokenResponse { get; set; }
}