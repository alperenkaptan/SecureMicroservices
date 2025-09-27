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
            //new IdentityResources.OpenId(),
            //new IdentityResources.Profile(),
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
                Username = "test",
                Password = "123"
            }
        };
    }
}
