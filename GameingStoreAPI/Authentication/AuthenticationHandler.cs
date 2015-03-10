using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Web;
using GamingStoreAPI.DAL;
using System.Threading;

namespace GamingStoreAPI.Authentication
{
    public class AuthenticationHandler : DelegatingHandler
    {
        private int? id = null;
        private string key = null;
       
        protected async override System.Threading.Tasks.Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request, System.Threading.CancellationToken cancellationToken)
        {

            if (id != null)
            {
                using (DataContext db = new DataContext())
                {
                    var user = db.Users.Find(id);
                    if (user.APIKey == key)
                    {
                        IList<Claim> claim = new List<Claim>
                    {
                        new Claim (ClaimTypes.Name, user.Email),
                        new Claim (ClaimTypes.Role, user.Role.ToString())
                    };
                        var identity = new ClaimsIdentity(claim, "APIKey");
                        var principal = new ClaimsPrincipal(identity);

                        Thread.CurrentPrincipal = principal;
                    }
                }
            }

            var response = await base.SendAsync(request, cancellationToken);

            var headers = response.Headers;

            if (headers.Contains("xcmps383authenticationid") && headers.Contains("xcmps383authenticationid"))
            {
                id = Int32.Parse(response.Headers.GetValues("xcmps383authenticationid").FirstOrDefault());
                key = response.Headers.GetValues("xcmps383authenticationkey").FirstOrDefault();
            }

            return response;
        }




    }
}