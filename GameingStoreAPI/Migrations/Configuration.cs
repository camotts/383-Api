namespace GamingStoreAPI.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Web.Helpers;
    using GamingStoreAPI.Models;
    using System.Security.Cryptography;

    internal sealed class Configuration : DbMigrationsConfiguration<GamingStoreAPI.DAL.DataContext>
    {
        // APIKey creation method requested in requirements
        private static string GetApiKey()
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                var bytes = new byte[16];
                rng.GetBytes(bytes);
                return Convert.ToBase64String(bytes);
            }
        }

        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(GamingStoreAPI.DAL.DataContext context)
        {
            context.Users.AddOrUpdate(
               r => r.ID,
               new User
               {
                   ID = 1,
                   Email = "admin1@383.com",
                   Password = Crypto.HashPassword("selu2015"),
                   APIKey = GetApiKey(),
                   Role = Role.Employee,
               },
               new User
               {
                   ID = 2,
                   Email = "sa@383.com",
                   Password = Crypto.HashPassword("password"),
                   APIKey = GetApiKey(),
                   Role = Role.Customer,
               },
               new User
               {
                   ID = 3,
                   Email = "storeHead1@383.com",
                   Password = Crypto.HashPassword("selu2015"),
                   APIKey = GetApiKey(),
                   Role = Role.StoreAdmin,
               });

            
        }
    }
}
