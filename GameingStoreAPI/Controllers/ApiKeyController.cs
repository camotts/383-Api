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

            response.Headers.Add("xcmps383authenticationid", user.ID.ToString());
            response.Headers.Add("xcmps383authenticationkey", user.APIKey);
           
            return response;
        } 


    }
}
