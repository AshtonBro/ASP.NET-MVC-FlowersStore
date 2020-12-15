using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FlowersStore.Models
{
    [Table("ShopingCart")]
    public class ShopingCart
    {
        [Key]
        public string CartId { get; set; }

        [Range(0, 3000)]
        public int Quantity { get; set; } = 0;
        public DateTime DateCreated { get; set; }
        public virtual Product Product { get; set; }
        public virtual Basket Basket { get; set; }
    }
}
