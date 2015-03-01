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


    public class AuthorizationHeaderHandler : DelegatingHandler
    {

        private DataContext db = new DataContext();

        protected override System.Threading.Tasks.Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request, System.Threading.CancellationToken cancellationToken)
        {
            int ? id = null;
            string apikey = "";

            CookieHeaderValue cookie = request.Headers.GetCookies().FirstOrDefault();
            if (cookie != null)
            {
                id = Int32.Parse(cookie["xcmps383authenticationid"].Value);
                apikey = cookie["xcmps383authenticationkey"].Value;

                var user = db.Users.Find(id);
                if(user.APIKey == apikey)
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
            return base.SendAsync(request, cancellationToken);
        }


    }
}