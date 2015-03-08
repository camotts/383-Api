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
using System.Web.Mvc;

namespace GamingStoreAPI.Services
{
    public class UserRepository:IUserRepository
    {

        private DataContext db = new DataContext();

        public IEnumerable<User> getListOfUsers()
        {
            return db.Users.AsEnumerable();
        }


        public Models.User getUserById(int Id)
        {
            User searchUser = db.Users.Find(Id);
            return searchUser;
        }

        //POST 
        public void createUser(Models.User user)
        {
                user.Password = Crypto.HashPassword(user.Password);
                user.APIKey = getApiKey();
                db.Users.Add(user);
                db.SaveChanges();   
        }

    

        public void deleteUser(Models.User user)
        {
            db.Users.Remove(user); 
            db.SaveChanges();
            
        }

        private static string getApiKey()
        {
           using (var rng = new RNGCryptoServiceProvider())
           {
               var bytes = new byte[16];
               rng.GetBytes(bytes);
                return Convert.ToBase64String(bytes);
            }
        }

        public void putUser(int id, Models.User user)
        {
            User modifyUser = getUserById(id);
            if (modifyUser.Password != Crypto.HashPassword(user.Password))
            {
                modifyUser.Password = Crypto.HashPassword(user.Password);
            }
            modifyUser.Email = user.Email;
            modifyUser.Role = user.Role;
            db.Entry(modifyUser).State = EntityState.Modified;
        }
    }
}