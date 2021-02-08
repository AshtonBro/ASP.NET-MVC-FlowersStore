using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlowersStore.Models
{
    [Table("User")]
    public class User: IdentityUser<Guid>
    {
        public override Guid Id { get; set; }

        [Required(ErrorMessage = "Name isn't be empty")]
        [StringLength(20, ErrorMessage = "Name can't be more than 20 characters")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Name should not contain special character,numbers or space")]
        public string Name { get; set; }

        [Required(ErrorMessage = "SecondName isn't be empty")]
        [StringLength(40, ErrorMessage = "SecondName can't be more than 40 characters")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "SecondName should not contain special character,numbers or space")]
        public string SecondName { get; set; }

        [Required(ErrorMessage = "The Email field is required.")]
        [EmailAddress(ErrorMessage = "The Email field is not a valid.")]
        public override string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(255, ErrorMessage = "Must be between 5 and 255 characters", MinimumLength = 5)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public virtual Basket Basket { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime DateCreated { get; set; }
    }

}
