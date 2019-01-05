using ContactsManager.Constant;
using IdentityServer3.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactsManager.IdSrv.Config
{
    public static class Clients
    {
        public static IEnumerable<Client> Get() {
            return new[] {
                new Client() {
                    Enabled = true,
                    ClientName = "MVC Client (Hybrid Flow)",
                    ClientId= "mvc",
                    Flow = Flows.Hybrid,
                    RequireConsent = true,
                    RedirectUris = new List<string> {
                        ContactsManagerConstant.ContactsManagerWebClient
                    }
                }
            };
        }
    }
}
