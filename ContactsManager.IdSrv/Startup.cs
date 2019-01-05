using ContactsManager.Constant;
using ContactsManager.IdSrv.Config;
using IdentityServer3.Core.Configuration;
using Microsoft.Owin;
using Owin;
using System;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

[assembly: OwinStartup(typeof(ContactsManager.IdSrv.Startup))]

namespace ContactsManager.IdSrv
{
    public class Startup
    {
        public void Configuration(IAppBuilder app) {
            app.Map("/Identity", IdSrvApp => {
                IdSrvApp.UseIdentityServer(new IdentityServerOptions {
                    SiteName = "Embedded IdentityServer",
                    IssuerUri = ContactsManagerConstant.IdSrvIssuerUri,

                    Factory = new IdentityServerServiceFactory()
                            .UseInMemoryClients(Clients.Get())
                            .UseInMemoryScopes(Scopes.Get())
                            .UseInMemoryUsers(Users.Get()),
                    SigningCertificate = LoadCertificate()
                });
            });
        }




        X509Certificate2 LoadCertificate() {
            return new X509Certificate2(
                string.Format(@"{0}\bin\Certificates\idsrv3test.pfx",
                    AppDomain.CurrentDomain.BaseDirectory), "idsrv3test");
        }
    }
}
