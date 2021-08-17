using System;
using System.Collections.Generic;

namespace FlowersStore.WebUI.Contracts
{
    public class Basket
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public User User { get; set; }

        public ICollection<ProductCard> ProductCards { get; set; }
    }
}