﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace App.Models
{
    public class LoginModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class RegisterModel
    {

        public string FirstName { get; set; }
        
        public string LastName { get; set; }

       
        public string Email { get; set; }

      
        public string Username { get; set; }

       
        public string Password { get; set; }
        
        public string ConfirmPassword { get; set; }
    }
}
    