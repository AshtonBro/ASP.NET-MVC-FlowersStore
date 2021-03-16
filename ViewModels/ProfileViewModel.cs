using System.ComponentModel.DataAnnotations;
using System;

namespace FlowersStore.ViewModels
{
    public class ProfileViewModel : LoginViewModel.RegistrationUserModel
    {
        public string Role { get; set; }
    }
}
