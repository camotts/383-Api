using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GamingStoreAPI.DAL;

namespace GamingStoreAPI.Controllers
{
    public class ApiKeyController : ApiController
    {

        //Endpoint requirement to acquire apikey from login
        // with valid email and password.
        [HttpGet]
        public string getApiKey()
        {
            using (DataContext db = new DataContext()){

                if (User.Identity.IsAuthenticated)
                {
                    return db.Users.Find(User.Identity.Name).APIKey;
                }
                else
                {
                    return null;
                }
            }
        }


    }
}
