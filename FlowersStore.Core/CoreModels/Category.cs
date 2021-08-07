using System;
using System.Collections.Generic;

namespace FlowersStore.Core.CoreModels
{
    public class Category
    {
        public Guid CategoryId { get; set; }

        public string FlowersType { get; set; }

        public virtual IEnumerable<Product> Products { get; set; }
    }
}
