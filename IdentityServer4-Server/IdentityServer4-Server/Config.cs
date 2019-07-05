using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
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
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
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
                    ClientId = "ls365-tasks",

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
                },
                new Client
                {
                    ClientId="client.ro",
                    AllowedGrantTypes=GrantTypes.ResourceOwnerPassword,
                    ClientSecrets =
                    {
                        new Secret("666666".Sha256())
                    },
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "information"
                    }
                },
                new Client
                {
                    ClientId="ls365-web",
                    AllowedGrantTypes=GrantTypes.Code,
                    ClientSecrets =
                    {
                        new Secret("666666".Sha256())
                    },
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "information"
                    }
                }
            };
        }

        public static List<TestUser> GetTestUsers()
        {
            return new List<TestUser>
            {
                new TestUser
                {
                    SubjectId="subject",
                    IsActive=true,
                    Username="chenye",
                    Password="chenye",
                    Claims=
                    {
                        // profile:name, family_name, given_name, middle_name, nickname, preferred_username, profile, picture, website, gender, birthdate, zoneinfo, locale, and updated_at.
                        new System.Security.Claims.Claim("name","chenye"),
                        new System.Security.Claims.Claim("family_name","chen"),
                        new System.Security.Claims.Claim("given_name","chen"),
                        new System.Security.Claims.Claim("middle_name","chen"),
                        new System.Security.Claims.Claim("nickname","chen"),
                        new System.Security.Claims.Claim("preferred_username","chen"),
                        new System.Security.Claims.Claim("profile","xxxx"),
                        new System.Security.Claims.Claim("picture","xxx"),
                        new System.Security.Claims.Claim("website","psyedu.ls365.com"),
                        new System.Security.Claims.Claim("gender","male"),
                        new System.Security.Claims.Claim("birthdate","1986-1-1"),
                        new System.Security.Claims.Claim("zoneinfo","wh"),
                        new System.Security.Claims.Claim("locale","cn"),
                        new System.Security.Claims.Claim("updated_at","2019-7-3"),
                    }
                }
            };
        }
    }
}
