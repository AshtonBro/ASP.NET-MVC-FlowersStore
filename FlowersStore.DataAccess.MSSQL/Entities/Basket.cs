using System;

namespace FlowersStore.DataAccess.MSSQL.Entities
{
    public class Basket
    {
        public Guid BasketId { get; set; }

        public Guid Id { get; set; }

        public User User { get; set; }

        public DateTime DateCreated { get; set; }
    }
}
