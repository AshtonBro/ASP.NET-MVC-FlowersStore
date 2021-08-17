using System;

namespace FlowersStore.Core.CoreModels
{
    public class User
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string SecondName { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string PasswordHash { get; set; }

        public Basket Basket { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}