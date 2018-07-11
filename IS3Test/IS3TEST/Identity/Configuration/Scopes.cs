using IdentityServer3.Core;
using IdentityServer3.Core.Models;
using System.Collections.Generic;

namespace Identity.Configuration
{
    public class Scopes
    {

        public static IEnumerable<Scope> Get()
        {
            return new[]
            {
                    ////////////////////////
                    // identity scopes
                    ////////////////////////

                    StandardScopes.OpenId,
                    StandardScopes.Profile,
                    StandardScopes.Email,
                    StandardScopes.Address,
                    StandardScopes.OfflineAccess,
                    StandardScopes.RolesAlwaysInclude,
                    StandardScopes.AllClaims,
                    new Scope
                    {
                        Enabled = true,
                        DisplayName = "Sample API",
                        Name = "sampleApi",
                        Description = "Access to a sample API",
                        Type = ScopeType.Resource,

                        //Claims = new List<ScopeClaim>
                        //{
                        //    new ScopeClaim("role")
                        //},
                        ScopeSecrets = new List<Secret>
                        {
                           new Secret("dmsecret".Sha256())
                        }
                    }

            };

        }
    }
}
