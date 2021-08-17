using System;
using System.Collections.Generic;

namespace FlowersStore.WebUI.Contracts
{
    public class Category
    {
        public Guid Id { get; set; }

        public string FlowersType { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}