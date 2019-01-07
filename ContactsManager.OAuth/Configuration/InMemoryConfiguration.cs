using IdentityServer4.Models;
using IdentityServer4.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactsManager.OAuthServer.Configuration
{
    public class InMemoryConfiguration
    {
        public static IEnumerable<ApiResource> ApiResources() {
            return new[] {
                new ApiResource("localUser","Local user")
            };
        }

        public static IEnumerable<Client> Clients() {
            return new[] {
                new Client() {
                    ClientId = "localUser",
                    ClientSecrets = new []{ new Secret("secret".Sha256())},
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                    AllowedScopes = new [] { "localUser" }
                }
            };
        }

        public static IEnumerable<TestUser> Users() {
            return new[] {
                new TestUser() {
                    SubjectId = "1",
                    Username = "majd.albaho@Gmail.com",
                    Password = "P@ssw0rd@123"
                }
            };
        }
    }
}
