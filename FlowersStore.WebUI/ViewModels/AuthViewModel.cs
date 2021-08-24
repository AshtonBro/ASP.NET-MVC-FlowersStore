using System;
using Microsoft.AspNetCore.Mvc;

namespace FlowersStore.WebUI.ViewModels
{
    public class AuthViewModel
    {
        public SignInModel SignIn { get; set; }

        public SignUpModel SignUp { get; set; }

        public class SignInModel
        {
            public string Name { get; set; }

            public string Password { get; set; }
        }

        public class SignUpModel
        {
            [HiddenInput(DisplayValue = false)]
            public Guid Id { get; set; }

            public string Name { get; set; }

            public string SecondName { get; set; }

            public string PhoneNumber { get; set; }

            public string Email { get; set; }

            public string Password { get; set; }

            public string ConfirmPassword { get; set; }

            public DateTime CreateDate { get; set; }
        }
    }
}