using Duende.IdentityServer.Models;

namespace IdentityServer.Duende;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        { 
            new IdentityResources.OpenId()
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new ApiScope[]
        {
          new ApiScope("weatherapi", "Full access to the weather API")
        };

    public static IEnumerable<Client> Clients =>
        new Client[] 
            {
                new Client
                {
                    ClientId = "console_app",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets =
                    {
                        new Secret("my_secret".Sha256())
                    },
                    AllowedScopes = { "weatherapi" }
                }
            };
}