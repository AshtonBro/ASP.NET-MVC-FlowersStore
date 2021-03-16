using FlowersStore.Models;
using System.Collections.Generic;

namespace FlowersStore.Areas.AdminPanel.ViewModels
{
    public class AdminProfileViewModel
    {
        public IEnumerable<User> Users { get; set; }
        public IEnumerable<Product> Products { get; set; }
    }
}
