using System;
using System.Collections.Generic;

namespace FlowersStore.DataAccess.MSSQL.Entities
{
    public class Category
    {
        public Guid Id { get; set; }

        public string FlowersType { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}