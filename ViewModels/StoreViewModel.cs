using FlowersStore.Models;
using System.Collections.Generic;

namespace FlowersStore.ViewModels
{
    public class StoreViewModel
    {
        public string Title { get; set; }

        // Filtration

        public IEnumerable<Product> Products { get; set; }
    }
}
