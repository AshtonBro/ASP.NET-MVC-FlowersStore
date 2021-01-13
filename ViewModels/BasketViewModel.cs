using FlowersStore.Models;
using System.Collections.Generic;

namespace FlowersStore.ViewModels
{
    public class BasketViewModel
    {
        public string UserName { get; set; }
        public IEnumerable<ShopingCart> ShopingCarts { get; set; }

    }
}
