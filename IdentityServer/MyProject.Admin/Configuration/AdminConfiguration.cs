﻿using MyProject.Admin.Configuration.Interfaces;

namespace MyProject.Admin.Configuration
{
    public class AdminConfiguration : IAdminConfiguration
    {
        public string IdentityAdminBaseUrl { get; set; } = "http://localhost:9000";

        public string IdentityAdminRedirectUri { get; set; } = "http://localhost:9000/signin-oidc";

        public string IdentityServerBaseUrl { get; set; } = "http://localhost:5000";
    }
}
