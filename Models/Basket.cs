using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlowersStore.Models
{
    [Table("Basket")]
    public class Basket
    {
        [Key]
        public Guid BasketId { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime DateCreated { get; set; }
        public virtual IEnumerable<ShopingCart> ShopingCarts { get; set; } 
    }
}
