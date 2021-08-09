using System;
using System.Collections.Generic;

namespace FlowersStore.WebUI.Contracts
{
    public class Basket
    {
        public string Name { get; set; }

        public string UserName { get; set; }

        public User User { get; set; }

        public IEnumerable<ShopingCart> ShopingCarts { get; set; }
    }
}
