using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using GamingStoreAPI.Models;
using GamingStoreAPI.DAL;
using System.Web.Helpers;
using System.Security.Cryptography;
using GamingStoreAPI.Services;


namespace GamingStoreAPI.Controllers
{
    public class UsersController : ApiController
    {
        private IUserRepository repo = new UserRepository();
      
        [AllowAnonymous]
        // GET api/Users
        public IEnumerable<User> GetUsers()
        {
            return repo.getListOfUsers();
        }
        [AllowAnonymous]
        // GET api/Users/5
        public User GetUser(int id)
        {
            User user = repo.getUserById(id);
            if (user == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return user;
        }

        // PUT api/Users/5
        public HttpResponseMessage PutUser(int id, User user)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != user.ID)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            repo.putUser(id, user);
            //try
            //{
            //    db.SaveChanges();
            //}
            //catch (DbUpdateConcurrencyException ex)
            //{
            //    return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            //}

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        // POST api/Users
        public HttpResponseMessage PostUser(User user)
        {

            if (ModelState.IsValid)
            {
                repo.createUser(user);
                 HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, user);
                 response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = user.ID }));
                 return response;
                
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/Users/5
        public HttpResponseMessage DeleteUser(int id)
        {
            User user = repo.getUserById(id);
            if (user == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            repo.deleteUser(user);
            //db.Users.Remove(user);

            //try
            //{
            //    db.SaveChanges();
            //}
            //catch (DbUpdateConcurrencyException ex)
            //{
            //    return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            //}
            return Request.CreateResponse(HttpStatusCode.OK, user);
        }


        // APIKey creation method For new users.
        [HttpGet]
        public static string GetApiKey()
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                var bytes = new byte[16];
                rng.GetBytes(bytes);
                return Convert.ToBase64String(bytes);
            }
        }
        
       
        //protected override void Dispose(bool disposing)
        //{
        //    db.Dispose();
        //    base.Dispose(disposing);
        //}

    }
}