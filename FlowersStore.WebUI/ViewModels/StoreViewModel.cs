using FlowersStore.WebUI.Contracts;
using System.Collections.Generic;

namespace FlowersStore.WebUI.ViewModels
{
    public class StoreViewModel
    {
        public string Title { get; set; }
        public string UserName { get; set; }
        public IEnumerable<Product> Products { get; set; }
        // Filtration
    }
}
