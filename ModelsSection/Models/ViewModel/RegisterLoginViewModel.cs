﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModel
{
    public class RegisterLoginViewModel
    {
        [Required]
        [DataType(DataType.Text)]
        [EmailAddress(ErrorMessage = "Enter you email")]
        public string? Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
