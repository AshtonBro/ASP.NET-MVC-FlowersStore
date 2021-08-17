using System;
using Microsoft.AspNetCore.Identity;

namespace FlowersStore.WebUI.Contracts
{
    public class User : IdentityUser<Guid>
    {
        public override Guid Id { get; set; }

        public string Name { get; set; }

        public string SecondName { get; set; }

        public override string UserName { get; set; }

        public override string Email { get; set; }

        public override string PhoneNumber { get; set; }

        public virtual Basket Basket { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }
    }
}