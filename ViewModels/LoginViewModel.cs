using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using FlowersStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace FlowersStore.ViewModels
{
    public class LoginViewModel
    {
        [Display(Name = "Enter your Login:")]
        [Required(ErrorMessage = "Name isn't be empty")]
        [StringLength(20, ErrorMessage = "Name can't be more than 20 characters")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Name should not contain special character,numbers or space")]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        [Display(Name = "Enter your Password:")]
        [Required(ErrorMessage = "Password is required")]
        [StringLength(255, ErrorMessage = "Password must be between 5 and 255 characters", MinimumLength = 5)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
