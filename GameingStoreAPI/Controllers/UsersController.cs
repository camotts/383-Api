﻿using System;
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
using GamingStoreAPI.Models.DTOS;
using System.Web.UI.WebControls;


namespace GamingStoreAPI.Controllers
{

    [Authorize(Roles = "Employee, StoreAdmin")]
    public class UsersController : BaseApiController
    {
        private IUserRepository repo = new UserRepository();
      
        // GET api/Users
        public HttpResponseMessage GetUsers()
        {
            List<UserDTO> ListOfUsers = new List<UserDTO>();
            foreach(var item in repo.getListOfUsers()){

                ListOfUsers.Add(TheFactory.Create(item));

            }
            return Request.CreateResponse(HttpStatusCode.OK, ListOfUsers);
        }

        
        // GET api/Users/5
        [Authorize(Roles = "StoreAdmin")]
        public HttpResponseMessage GetUser(int id)
        {
            User user = repo.getUserById(id);
            if (user == null)
            {

                return Request.CreateResponse(HttpStatusCode.NotFound, "User With Id:" + id + " Not Found");
            }

            UserDTO factoredUser = TheFactory.Create(user);

            return Request.CreateResponse(HttpStatusCode.OK, factoredUser);
        }

        // PUT api/Users/5
        [Authorize(Roles = "StoreAdmin")]
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
            UserDTO factoredUser = TheFactory.Create(user);



            return Request.CreateResponse(HttpStatusCode.OK, factoredUser);



        }

        // POST api/Users
        [Authorize(Roles = "StoreAdmin")]
        public HttpResponseMessage PostUser(User user)
        {
            UserDTO factoredUser = TheFactory.Create(user);
            if (ModelState.IsValid)
            {
                repo.createUser(user);
                 HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, user);
                 response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = user.ID }));
                 return Request.CreateResponse(HttpStatusCode.OK, factoredUser);


                
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/Users/5
        [Authorize(Roles = "StoreAdmin")]
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
        private static string GetApiKey()
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