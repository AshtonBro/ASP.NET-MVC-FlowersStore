using FlowersStore.WebUI.Contracts;
using System.Collections.Generic;

namespace FlowersStore.WebUI.Areas.AdminPanel.ViewModels
{
    public class AdminProfileViewModel
    {
        public IEnumerable<User> Users { get; set; }
        public IEnumerable<Product> Products { get; set; }
    }
}
