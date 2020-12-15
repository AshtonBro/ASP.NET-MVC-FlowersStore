using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlowersStore.Models
{
    [Table("Category")]
    public class Category
    {
        [Key]
        public Guid CategoryId { get; set; }

        [StringLength(40, ErrorMessage = "FlowersType can't be more than 40 characters")]
        public string FlowersType { get; set; }
        public virtual IEnumerable<Product> Products { get; set; }
    }
}
