using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using GamingStoreAPI.Models;

namespace GamingStoreAPI.DAL
{
    public class DataContext : DbContext
    {
        public DataContext()
            : base("DataContext")
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Tags> Tags { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Sale> Sales { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }


    }
}