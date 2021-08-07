using FlowersStore.Contracts;
using System.Collections.Generic;

namespace FlowersStore.ViewModels
{
    public class StoreViewModel
    {
        public string Title { get; set; }
        public string UserName { get; set; }
        public IEnumerable<Product> Products { get; set; }
        // Filtration
    }
}
