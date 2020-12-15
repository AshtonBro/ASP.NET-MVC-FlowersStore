using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlowersStore.Models
{
    [Table("Basket")]
    public class Order
    {
        [Key]
        public Guid OrderId { get; set; }

        public User User { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime OrderDate { get; set; }

        public virtual IEnumerable<Product> Products { get; set; } 
    }
}
