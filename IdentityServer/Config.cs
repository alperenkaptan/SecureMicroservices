using Duende.IdentityModel;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Test;

namespace IdentityServer;

public class Config
{
    public static IEnumerable<Client> GetClients()
    {
        return new List<Client>
        {
            new Client
            {
                ClientId = "movieClient",
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientSecrets =
                {
                    new Secret("secret".Sha256())
                },
                AllowedScopes = { "movieAPI" }
            },
            new Client
            {
                ClientId = "movieClientInteractive",
                AllowedGrantTypes = GrantTypes.Code,
                ClientSecrets =
                {
                    new Secret("secret".Sha256())
                },
                RedirectUris = { "https://localhost:6060/signin-oidc" },
                PostLogoutRedirectUris = { "https://localhost:6060/signout-callback-oidc" },
                AllowedScopes = { "movieAPI", "openid", "profile" },
                AllowOfflineAccess = true
            }
        };
    }
    public static IEnumerable<ApiScope> GetApiScopes()
    {
        return new List<ApiScope>
        {
            new ApiScope("movieAPI", "Movie API")
        };
    }
    public static IEnumerable<IdentityResource> GetIdentityResources()
    {
        return new List<IdentityResource>
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
        };
    }
    public static IEnumerable<ApiResource> GetApiResources()
    {
        return new List<ApiResource>
        {
            new ApiResource("movieAPI", "Movie API")
            {
                Scopes = { "movieAPI" }
            }
        };
    }

    public static List<TestUser> GetTestUsers()
    {
        return new List<TestUser>
        {
            new TestUser
            {
                SubjectId = "1",
                Username = "admin",
                Password = "admin",
                Claims = new List<System.Security.Claims.Claim>
                {
                    new System.Security.Claims.Claim(JwtClaimTypes.Name, "Super"),
                    new System.Security.Claims.Claim(JwtClaimTypes.FamilyName, "Admin")
                }
            }
        };
    }
}
