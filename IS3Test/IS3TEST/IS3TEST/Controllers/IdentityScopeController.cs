using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IdentityModel.Client;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Http;
using System.Web.Configuration;

namespace IS3TEST.Controllers
{
    public class IdentityScopeController : Controller
    {
        // GET: IdentityScope
        public async Task<ActionResult> Index()
        {
            var tokenResponse =await GetTokenAsync();
            var introspecResponse = await ValidateToken(tokenResponse.AccessToken);
            if (introspecResponse.IsActive)
            {
                ViewBag.Token = tokenResponse.AccessToken;
            }
            else
            {
                ViewBag.Token = introspecResponse.Error;
            }

            return View();
        }

        //private async Task<string> CallApi(string token)
        //{
        //    var client = new HttpClient();
        //    client.SetBearerToken(token);

        //   // var json = await client.GetStringAsync(WebConfigurationManager.AppSettings["is3host"] + "/samples/mvcapi/identity");
        //    return JArray.Parse(json).ToString();
        //}

        private async Task<TokenResponse> GetTokenAsync()
        {

            var client = new TokenClient(
                WebConfigurationManager.AppSettings["is3host"] + "/identity/core/connect/token",
                "mvc_service",
                "secret");

            return  await client.RequestClientCredentialsAsync("sampleApi");

        }

        private async Task<IntrospectionResponse> ValidateToken(string token)
        {

            var clientInstroption = new IntrospectionClient(WebConfigurationManager.AppSettings["is3host"] + "/identity/core/connect/introspect",
               "sampleAPI",
               "dmsecret");

            var request = new IntrospectionRequest
            {
                Token = token
            };

            return clientInstroption.SendAsync(request).Result;
        }

    }
}