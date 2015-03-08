using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GamingStoreAPI.Services;
using System.Net.Http.Headers;

namespace GamingStoreAPI.Controllers
{

    //Endpoint to acquire APIKey
    public class ApiKeyController : BaseApiController
    {

        private IApiKeyRepository repo = new ApiKeyRepository();


        [AllowAnonymous]
        public HttpResponseMessage GetAPIKey(string email, string password)
        {
            var user = repo.getApiKey(email, password);
            if (user == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.Forbidden, "Invalid Email or Password");
            }
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            var response = Request.CreateResponse(HttpStatusCode.OK);

            var idcookie = new CookieHeaderValue("xcmps383authenticationid", user.ID.ToString());
            idcookie.Domain = Request.RequestUri.Host;
            idcookie.Path = "/";

            var keycookie = new CookieHeaderValue("xcmps383authenticationkey", user.APIKey);
            keycookie.Domain = Request.RequestUri.Host;
            keycookie.Path = "/";

            response.Headers.AddCookies(new CookieHeaderValue[] {idcookie, keycookie});
            return response;
        } 


    }
}
