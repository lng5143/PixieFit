using IdentityServer4.Models;

namespace PixieFit.Web.Identity;

public class Config 
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        {
            new IdentityResources.OpenId()
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new ApiScope[]
        {
            new ApiScope("resize", "Resize API")
        };

    public static IEnumerable<Client> Clients =>
        new Client[]
        {
            new Client
            {
                ClientId = "web",
                ClientSecrets = 
                {
                    new Secret("secret".Sha256())
                },
                RedirectUris = { "https://localhost:5261" },
                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword
            }
        };
}