using Duende.IdentityModel;
using Duende.IdentityServer;
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
                                ClientId = "movieClientInteractive",
                                AllowedGrantTypes = GrantTypes.Hybrid,
                                RequirePkce =false,
                                ClientSecrets =
                                {
                                    new Secret("secret".Sha256())
                                },
                                RedirectUris = { "https://localhost:6060/signin-oidc" },
                                PostLogoutRedirectUris = { "https://localhost:6060/signout-callback-oidc" },
                                AllowedScopes = {
                                    "movieAPI",
                                    IdentityServerConstants.StandardScopes.OpenId,
                                    IdentityServerConstants.StandardScopes.Profile,
                                    IdentityServerConstants.StandardScopes.Address,
                                    IdentityServerConstants.StandardScopes.Email,
                                    JwtClaimTypes.Roles
                                }
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
                            new IdentityResources.Address(),
                            new IdentityResources.Email(),
                            new IdentityResource(JwtClaimTypes.Roles, new[] { JwtClaimTypes.Role })
                        };
    }
}
