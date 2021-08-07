using FlowersStore.Contracts;
using System.Collections.Generic;

namespace FlowersStore.ViewModels
{
    public class BasketViewModel
    {
        public string Name { get; set; }
        public string UserName { get; set; }

        public IEnumerable<ShopingCart> ShopingCarts { get; set; }
    }
}
