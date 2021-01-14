using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlowersStore.Models
{
    [Table("ShopingCart")]
    public class ShopingCart
    {
        [Key]
        public Guid CartId { get; set; }

        [Range(0, 3000)]
        public int Quantity { get; set; } = 0;
        public DateTime DateCreated { get; set; }
        public Guid ProductId { get; set; }
        public virtual Product Product { get; set; }
        public Guid BasketId { get; set; }
        public virtual Basket Basket { get; set; }
    }
}
