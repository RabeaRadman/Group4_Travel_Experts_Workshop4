﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace G4TEWS4_MVC.Models
{
    /// <summary>
    /// Login view model.
    /// </summary>
    public class LoginViewModel
    {
        [Required]
        public string username { get; set; }

        [Required]
        public string password { get; set; }
    }
}
