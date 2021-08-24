using System;

namespace FlowersStore.WebUI.ViewModels
{
    public class UserViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string SecondName { get; set; }

        public string PhoneNumber { get; set; }

        public string Role { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        public DateTime CreateDate { get; set; }
    }
}