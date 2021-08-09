using FlowersStore.WebUI.Contracts;
using System.Collections.Generic;

namespace FlowersStore.WebUI.ViewModels
{
    public class BasketViewModel
    {
        public string Name { get; set; }
        public string UserName { get; set; }

        public IEnumerable<ShopingCart> ShopingCarts { get; set; }
    }
}
