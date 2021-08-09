using FlowersStore.WebUI.Contracts;
using System.Collections.Generic;

namespace FlowersStore.WebUI.ViewModels
{
    public class CheckoutViewModel
    {
        public string UserName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public IEnumerable<ShopingCart> ShopingCarts { get; set; }
    }
}
