using Microsoft.AspNetCore.Identity;
using System;

namespace FlowersStore.WebUI.Contracts
{
    public class UserRole : IdentityRole<Guid>
    {
        public override Guid Id { get; set; }

        public override string Name { get; set; }

        public override string NormalizedName { get; set; }

        public override string ConcurrencyStamp { get; set; }
    }
}