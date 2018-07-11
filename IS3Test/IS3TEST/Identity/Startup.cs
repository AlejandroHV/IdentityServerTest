using Identity.Configuration;
using IdentityServer3.Core.Configuration;
using Microsoft.Owin;
using Owin;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

[assembly: OwinStartup(typeof(Identity.Startup))]

namespace Identity
{
    public class Startup
    {
        public void Configuration(IAppBuilder app) {

            Log.Logger = new LoggerConfiguration()
               .ReadFrom.AppSettings()
               .CreateLogger();


            app.Map(
            "/core",
            coreApp => {
                coreApp.UseIdentityServer(new IdentityServerOptions
                {
                    SiteName = "Standalone Identity Server",
                    SigningCertificate = Cert.Load(),
                   
                    Factory = new IdentityServerServiceFactory()
                            .UseInMemoryClients(Clients.Get())
                            .UseInMemoryScopes(Scopes.Get())
                            .UseInMemoryUsers(Users.Get()),
                    RequireSsl = true,
                    Endpoints = new EndpointOptions
                    {
                        EnableAccessTokenValidationEndpoint = false
                    }
                });
            });
        }

    }
}