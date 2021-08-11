using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FlowersStore.WebUI.Contracts
{
    public class NewCategory
    {
        [Key]
        public Guid CategoryId { get; set; }

        [Required]
        [StringLength(40, ErrorMessage = "FlowersType can't be more than 40 characters")]
        public string FlowersType { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}