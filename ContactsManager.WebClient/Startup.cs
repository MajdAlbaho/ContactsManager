using ContactsManager.Constant;
using ContactsManager.WebClient.Helper;
using Microsoft.Owin;
using Microsoft.Owin.Security.OpenIdConnect;
using Owin;
using System;
using System.Threading.Tasks;

[assembly: OwinStartup(typeof(ContactsManager.WebClient.Startup))]

namespace ContactsManager.WebClient
{
    public class Startup
    {
        public void Configuration(IAppBuilder app) {
            app.UseCookieAuthentication(new Microsoft.Owin.Security.Cookies.CookieAuthenticationOptions() {
                AuthenticationType = "Cookies"
            });
            app.UseOpenIdConnectAuthentication(new OpenIdConnectAuthenticationOptions {
                ClientId = "mvc",
                Authority = ContactsManagerConstant.IdSrv,
                RedirectUri = ContactsManagerConstant.ContactsManagerWebClient,
                SignInAsAuthenticationType = "Cookies",
                ResponseType = "code id_token",
                Scope = "openid profile",

                Notifications = new OpenIdConnectAuthenticationNotifications() {
                    MessageReceived = async n => {
                        EndpointAndTokenHelper.DecodeAndWrite(n.ProtocolMessage.IdToken);
                    }
                }
            });
        }
    }
}
