using System;

namespace FlowersStore.Core.CoreModels
{
    public class ProductCard
    {
        public Guid Id { get; set; }

        public Guid BasketId { get; set; }

        public Guid ProductId { get; set; }

        public int Quantity { get; set; }

        public DateTime DateCreated { get; set; }

        public Basket Basket { get; set; }

        public Product Product { get; set; }
    }
}