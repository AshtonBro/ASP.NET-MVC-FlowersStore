using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlowersStore.Contracts
{
    [Table("Basket")]
    public class Basket
    {
        [Key]
        public Guid BasketId { get; set; }

        public Guid Id { get; set; }

        public User User { get; set; }


        [DataType(DataType.DateTime)]
        public DateTime DateCreated { get; set; }
    }
}
