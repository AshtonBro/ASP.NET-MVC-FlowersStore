using System.ComponentModel.DataAnnotations;
using System;

namespace FlowersStore.ViewModels
{
    public class ProfileViewModel
    {
        public Guid Id { get; set; }
        public string Role { get; set; }
        [Required(ErrorMessage = "Name isn't be empty")]
        [StringLength(20, ErrorMessage = "Name can't be more than 20 characters")]
        [DataType(DataType.Text)]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Name should not contain special character,numbers or space")]
        public string Name { get; set; }

        [Required(ErrorMessage = "SecondName isn't be empty")]
        [StringLength(40, ErrorMessage = "SecondName can't be more than 40 characters")]
        [DataType(DataType.Text)]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "SecondName should not contain special character,numbers or space")]
        public string SecondName { get; set; }

        [Required(ErrorMessage = "You must provide a phone number")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?\+[0-9]{1,3}\)? ?-?[0-9]{1,3} ?-?[0-9]{3,5} ?-?[0-9]{4}( ?-?[0-9]{3})?", ErrorMessage = "Invalid Phone number")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "The Email field is required.")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Not a valid email")]
        public string Email { get; set; }

        public DateTime CreateDate { get; set; }
    }
}
