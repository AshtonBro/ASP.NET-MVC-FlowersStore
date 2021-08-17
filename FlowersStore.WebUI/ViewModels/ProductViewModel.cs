using System;
using FlowersStore.WebUI.Contracts;

namespace FlowersStore.WebUI.ViewModels
{
    public class ProductViewModel
    {
        public Guid Id { get; set; }

        public byte[] Image { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Color { get; set; }

        public decimal Price { get; set; }

        public Guid CategoryId { get; set; }

        public Category Category { get; set; }
    }
}