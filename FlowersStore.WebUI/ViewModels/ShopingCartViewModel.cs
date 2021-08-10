﻿using System;
using FlowersStore.WebUI.Contracts;


namespace FlowersStore.WebUI.ViewModels
{
    public class ShopingCartViewModel
    {
        public Guid CartId { get; set; }

        public Guid BasketId { get; set; }

        public Guid ProductId { get; set; }

        public int Quantity { get; set; } = 0;

        public DateTime DateCreated { get; set; }

        public Basket Basket { get; set; }

        public Product Product { get; set; }
    }
}
