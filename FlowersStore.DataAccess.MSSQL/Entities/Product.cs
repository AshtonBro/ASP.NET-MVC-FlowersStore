using System;
using System.Collections.Generic;

namespace FlowersStore.DataAccess.MSSQL.Entities
{
    public class Product
    {
        public Guid ProductId { get; set; }

        public Guid CategoryId { get; set; }

        public byte[] Image { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Color { get; set; }

        public decimal Price { get; set; }

        public Category Category { get; set; }

        public ICollection<ShopingCart> ShopingCarts { get; set; }
    }
}