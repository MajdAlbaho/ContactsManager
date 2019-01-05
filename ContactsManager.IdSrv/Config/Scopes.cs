using IdentityServer3.Core.Models;
using System.Collections.Generic;


namespace ContactsManager.IdSrv.Config
{
    public static class Scopes
    {
        public static IEnumerable<Scope> Get() {
            var scopes = new List<Scope> {
                StandardScopes.OpenId,
                StandardScopes.Profile
            };

            return scopes;
        }
    }
}
