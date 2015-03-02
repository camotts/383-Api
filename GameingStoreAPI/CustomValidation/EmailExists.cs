using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using GamingStoreAPI.DAL;

namespace GamingStoreAPI.CustomValidation
{
    public class EmailExists : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            var newuser = validationContext.ObjectInstance as Models.User;
            if (newuser == null)
            {
                return new ValidationResult("Model is Empty");
            }

            DataContext db = new DataContext();
            var user = db.Users.FirstOrDefault(u => u.Email == (string)value && u.ID != newuser.ID);
            if (user == null)
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("Email Already Exists");
            }
        }


    }
}