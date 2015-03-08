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

            context.Games.AddOrUpdate(
               new Game
               {
                   ID = 1,
                   Name = "Grand Theft Auto V",
                   Price = 59.99M,
                   InventoryCount = 56,
                   ReleaseDate = DateTime.Parse("04/14/2015"),
               },
               new Game
               {
                   ID = 2,
                   Name = "Call of Duty: Advanced Warfare",
                   Price = 59.99M,
                   InventoryCount = 56,
                   ReleaseDate = DateTime.Parse("11/03/2014"),
               },
               new Game
               {
                   ID = 3,
                   Name = "Dying Light",
                   Price = 59.99M,
                   InventoryCount = 56,
                   ReleaseDate = DateTime.Parse("01/27/2015"),
               },
               new Game
               {
                   ID = 4,
                   Name = "Hotline Miami 2: Wrong Number",
                   Price = 13.49M,
                   InventoryCount = 56,
                   ReleaseDate = DateTime.Parse("03/10/2015"),
               },
               new Game
               {
                   ID = 5,
                   Name = "Assassin’s Creed® Rogue",
                   Price = 49.99M,
                   InventoryCount = 56,
                   ReleaseDate = DateTime.Parse("03/10/2015"),
               },
               new Game
               {
                   ID = 6,
                   Name = "Counter/Strike: Global Offensive",
                   Price = 14.99M,
                   InventoryCount = 56,
                   ReleaseDate = DateTime.Parse("08/21/2010"),
               },
               new Game
               {
                   ID = 7,
                   Name = "Dragon Ball Xenoverse",
                   Price = 49.99M,
                   InventoryCount = 56,
                   ReleaseDate = DateTime.Parse("02/26/2015"),
               },
               new Game
               {
                   ID = 8,
                   Name = "FTL: Faster Than Light",
                   Price = 9.99M,
                   InventoryCount = 56,
                   ReleaseDate = DateTime.Parse("09/14/2012"),
               },
               new Game
               {
                   ID = 9,
                   Name = "BattleBlock Theater",
                   Price = 5.09M,
                   InventoryCount = 56,
                   ReleaseDate = DateTime.Parse("05/15/2014"),
               },
               new Game
               {
                   ID = 10,
                   Name = "Saints Row IV",
                   Price = 19.99M,
                   InventoryCount = 56,
                   ReleaseDate = DateTime.Parse("08/19/2013"),
               }
          );

            context.Genres.AddOrUpdate(
              new Genre
              {
                  ID = 1,
                  Type = "Action",
              },
              new Genre
              {
                  ID = 2,
                  Type = "Adventure",
              },
              new Genre
              {
                  ID = 3,
                  Type = "RPG",
              },
              new Genre
              {
                  ID = 4,
                  Type = "Indie",
              },
              new Genre
              {
                  ID = 5,
                  Type = "Simulation",
              },
              new Genre
              {
                  ID = 6,
                  Type = "Strategy",
              },
              new Genre
              {
                  ID = 7,
                  Type = "Casual",
              }
           );

            context.Tags.AddOrUpdate(
              new Tags
              {
                  ID = 1,
                  Name = "Open World",
              },
              new Tags
              {
                  ID = 2,
                  Name = "Action",
              },
              new Tags
              {
                  ID = 3,
                  Name = "Multiplayer",
              },
              new Tags
              {
                  ID = 4,
                  Name = "First-Person",
              },
              new Tags
              {
                  ID = 5,
                  Name = "FPS",
              },
              new Tags
              {
                  ID = 6,
                  Name = "Shooter",
              },
              new Tags
              {
                  ID = 7,
                  Name = "Futuristic",
              },
              new Tags
              {
                  ID = 8,
                  Name = "Zombies",
              },
              new Tags
              {
                  ID = 9,
                  Name = "Survival",
              },
              new Tags
              {
                  ID = 10,
                  Name = "Parkour",
              },
              new Tags
              {
                  ID = 11,
                  Name = "Co-op",
              },
              new Tags
              {
                  ID = 12,
                  Name = "Gore",
              },
              new Tags
              {
                  ID = 13,
                  Name = "Great Soundtrack",
              },
              new Tags
              {
                  ID = 14,
                  Name = "Pixel Graphics",
              },
              new Tags
              {
                  ID = 15,
                  Name = "Stealth",
              },
              new Tags
              {
                  ID = 16,
                  Name = "Assassins",
              },
              new Tags
              {
                  ID = 17,
                  Name = "Fighting",
              }
           );
        }
    }
}
