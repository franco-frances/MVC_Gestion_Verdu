﻿using System.ComponentModel.DataAnnotations;

namespace MVC_GestionVerdu.Models
{
    public class Register
    {

        [Required]
        public string? NickName { get; set; }

        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Las contraseñas no coinciden.")]
        public string? ConfirmPassword { get; set; }




    }
}
