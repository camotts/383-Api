using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace GamingStoreAPI.Models
{
    public class User
    {
        public int ID { get; set; }
        //[Remote attribute]
        public string Email { get; set; }
        public string Password { get; set; }
        public string APIKey { get; set; }
        public Role Role { get; set; }
    }
}