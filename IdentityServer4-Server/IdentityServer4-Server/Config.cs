using IdentityServer4;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer4_Server
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new IdentityResource[]
            {
                //new IdentityResources.OpenId()
            };
        }

        public static IEnumerable<ApiResource> GetApis()
        {
            // 如果您将在生产中使用它，那么为您的API提供逻辑名称非常重要。
            return new List<ApiResource>
            {
                new ApiResource("information", "save click behavior where user click message.")
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "ls365-web",

                    // no interactive user, use the clientid/secret for authentication
                    AllowedGrantTypes = GrantTypes.ClientCredentials,

                    // secret for authentication
                    ClientSecrets =
                    {
                        new Secret("666666".Sha256())
                    },

                    // scopes that client has access to
                    AllowedScopes = {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "information"
                    }
                }
            };
        }

    }
}
