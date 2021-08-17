using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace FlowersStore.WebUI.ViewModels
{
    public class AuthViewModel
    {
        public SignInModel SignIn { get; set; }
        public SignUpModel SignUp { get; set; }

        public class SignInModel
        {
            [Display(Name = "Enter your Name:")]
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

        public class SignUpModel
        {
            [HiddenInput(DisplayValue = false)]
            public Guid Id { get; set; }

            [Required(ErrorMessage = "Name isn't be empty")]
            [StringLength(20, ErrorMessage = "Name can't be more than 20 characters")]
            [DataType(DataType.Text)]
            [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Name should not contain special character,numbers or space")]
            public string Name { get; set; }

            [Required(ErrorMessage = "SecondName isn't be empty")]
            [StringLength(40, ErrorMessage = "SecondName can't be more than 40 characters")]
            [DataType(DataType.Text)]
            [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "SecondName should not contain special character,numbers or space")]
            public string SecondName { get; set; }

            [Required(ErrorMessage = "You must provide a phone number")]
            [DataType(DataType.PhoneNumber)]
            [RegularExpression(@"^\(?\+[0-9]{1,3}\)? ?-?[0-9]{1,3} ?-?[0-9]{3,5} ?-?[0-9]{4}( ?-?[0-9]{3})?", ErrorMessage = "Invalid Phone number")]
            public string PhoneNumber { get; set; }

            [Required(ErrorMessage = "The Email field is required.")]
            [DataType(DataType.EmailAddress)]
            [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Not a valid email")]
            public string Email { get; set; }

            [Required(ErrorMessage = "Password is required")]
            [StringLength(255, ErrorMessage = "Password must be between 5 and 255 characters", MinimumLength = 5)]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Required(ErrorMessage = "Confirm Password is required")]
            [StringLength(255, ErrorMessage = "Password must be between 5 and 255 characters", MinimumLength = 5)]
            [DataType(DataType.Password)]
            [Compare("Password")]
            public string ConfirmPassword { get; set; }

            public DateTime CreateDate { get; set; }
        }
    }
}