using FlowersStore.WebUI.Contracts;
using System.Collections.Generic;

namespace FlowersStore.WebUI.Areas.AdminPanel.ViewModels
{
    public class AdminProfileViewModel
    {
        public ICollection<User> Users { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
