using System.Collections.Generic;

namespace FlowersStore.WebUI.ViewModels
{
    public class StoreViewModel
    {
        public string UserName { get; set; }
        public ICollection<ProductViewModel> Products { get; set; }

        // Filtration
    }
}
