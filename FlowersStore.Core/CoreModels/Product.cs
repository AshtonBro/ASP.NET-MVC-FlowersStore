﻿using System;

namespace FlowersStore.Core.CoreModels
{
    public class Product
    {
        public Guid Id { get; set; }

        public Guid CategoryId { get; set; }

        public byte[] Image { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Color { get; set; }

        public decimal Price { get; set; }

        public virtual Category Category { get; set; }

    }
}