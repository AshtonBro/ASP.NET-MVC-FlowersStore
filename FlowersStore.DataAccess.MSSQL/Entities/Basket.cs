﻿using System;
using System.Collections.Generic;

namespace FlowersStore.DataAccess.MSSQL.Entities
{
    public class Basket
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public User User { get; set; }

        public DateTime DateCreated { get; set; }

        public ICollection<ProductCard> ProductCards { get; set; }
    }
}