using System;
using Microsoft.AspNetCore.Identity;

namespace FlowersStore.DataAccess.MSSQL.Entities
{
    public class User : IdentityUser<Guid>
    {
        public override Guid Id { get; set; }

        public string Name { get; set; }

        public string SecondName { get; set; }

        public override string Email { get; set; }

        public virtual Basket Basket { get; set; }

        public DateTime DateCreated { get; set; }
    }
}
