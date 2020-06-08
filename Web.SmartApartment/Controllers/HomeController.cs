using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using RestSharp;
using Web.SmartApartment.Models;

namespace Web.SmartApartment.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        public async Task<string> Poll()
        {
            string accessToken = "";
            if (User.Identity.IsAuthenticated)
            {
                accessToken = await HttpContext.GetTokenAsync("access_token");

                // if you need to check the Access Token expiration time, use this value
                // provided on the authorization response and stored.
                // do not attempt to inspect/decode the access token
                DateTime accessTokenExpiresAt = DateTime.Parse(
                    await HttpContext.GetTokenAsync("expires_at"),
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.RoundtripKind);

                string idToken = await HttpContext.GetTokenAsync("id_token");

                // Now you can use them. For more info on when and how to use the
                // Access Token and ID Token, see https://auth0.com/docs/tokens
            }

            return accessToken;
        }
        public async Task<IActionResult> IndexAsync()
        {

            //var client = new RestClient("https://dev-rbku8jzj.auth0.com/oauth/token");
            //var request = new RestRequest(Method.POST);
            //request.AddHeader("content-type", "application/json");
            ////request.AddParameter("application/x-www-form-urlencoded", "grant_type=client_credentials&client_id=%24%7Baccount.clientId%7D&client_secret=YOUR_CLIENT_SECRET&audience=YOUR_API_IDENTIFIER", ParameterType.RequestBody);

            //request.AddParameter("application/json", "{\"client_id\":\"3rOACkVEAfbzDfOp0ijyDK1i7T0txio3\",\"client_secret\":\"f7EQXybzsHHh9EWIDEb-4kjRhvzumwoZWSbcwq_FP5355nOLLNmhZP6zdnS3Ck8Y\",\"audience\":\"https://smartapartmentdataAPI.com\",\"grant_type\":\"client_credentials\"}", ParameterType.RequestBody);
            //IRestResponse response = client.Execute(request);

            //var obj = JObject.Parse(response.Content);
            //string token = (string)obj["access_token"]; // http://s1.uploads.im/D9Y3z.pn

            //var client2 = new RestClient("https://localhost:44305/api/studentapi");
            //var request2 = new RestRequest(Method.GET);
            //string token = await Poll();
            //request2.AddHeader("authorization", "Bearer " + token);
            //IRestResponse response2 = client2.Execute(request2);


            //var client22 = new RestClient("https://localhost:44305/api/studentapi/1");
            //var request22 = new RestRequest(Method.DELETE);
            //request22.AddHeader("authorization", "Bearer " + token);
            //IRestResponse response22 = client2.Execute(request22);

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
