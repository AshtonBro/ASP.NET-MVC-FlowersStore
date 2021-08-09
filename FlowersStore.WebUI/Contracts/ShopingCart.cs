using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlowersStore.WebUI.Contracts
{
    [Table("ShopingCart")]
    public class ShopingCart
    {
        [Key]
        public Guid CartId { get; set; }

        [Required]
        public Guid BasketId { get; set; }

        [Required]
        public Guid ProductId { get; set; }

        [Range(0, 3000)]
        public int Quantity { get; set; } = 0;

        public DateTime DateCreated { get; set; }

        public virtual Basket Basket { get; set; }

        public virtual Product Product { get; set; }
    }
}
