using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using GamingStoreAPI.CustomValidation;

namespace GamingStoreAPI.Models
{
    public class User
    {
        public int ID { get; set; }

        [Required]
        [EmailExists]
        [EmailAddress(ErrorMessage="Not a valid email address")]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
        [Required]
        public string APIKey { get; set; }

        [Required]
        public Role Role { get; set; }
    }
}