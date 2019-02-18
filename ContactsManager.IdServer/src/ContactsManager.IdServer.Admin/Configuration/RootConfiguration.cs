using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using ContactsManager.IdServer.Admin.Configuration.Interfaces;

namespace ContactsManager.IdServer.Admin.Configuration
{
    public class RootConfiguration : IRootConfiguration
    {
        public IAdminConfiguration AdminConfiguration { get; set; }

        public RootConfiguration(IOptions<AdminConfiguration> adminConfiguration)
        {
            AdminConfiguration = adminConfiguration.Value;
        }
    }
}
