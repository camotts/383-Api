using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GamingStoreAPI.DAL;
using System.Web.Helpers;

namespace GamingStoreAPI.Services
{
    public class ApiKeyRepository : IApiKeyRepository
    {

        private DataContext db = new DataContext();


        public Models.User getApiKey(string email, string password)
        {
            var user = db.Users.Where(u => u.Email.Equals(email)).FirstOrDefault();
            if (Crypto.VerifyHashedPassword(user.Password, password) && user != null)
            {
                return user;
            }
            else
            {
                return null;
            }
        }



    }
}