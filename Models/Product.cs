using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlowersStore.Models
{
    [Table("Product")]
    public class Product
    {
        [Key]
        public Guid ProductId { get; set; }

        [Required]
        [Range(1,8000)]
        public byte[] Image { get; set; }

        [Required]
        [StringLength(40, ErrorMessage = "Name can't be more than 40 characters")]
        public string Name { get; set; }

        [MaxLength(250, ErrorMessage = "Description can't be more than 250 characters")]
        public string Description { get; set; }
        
        [Required]
        [StringLength(40, ErrorMessage = "Please Enter color name")]
        public string Color { get; set; }
        
        [Required]
        [Range(0, 9999999999999999.99, ErrorMessage = "Invalid Price, Max 18 digits")]
        [RegularExpression(@"^\d+.?\d{0,2}$", ErrorMessage = "Invalid Price, Maximum Two Decimal Points.")]
        public decimal Price { get; set; }
        
        [Required]
        public virtual Category Category { get; set; }

    }
}
