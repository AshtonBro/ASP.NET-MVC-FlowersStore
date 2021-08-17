using System.Collections.Generic;

namespace FlowersStore.WebUI.ViewModels
{
    public class BasketViewModel
    {
        public string Name { get; set; }

        public string UserLogin { get; set; }

        public ICollection<ProductCardViewModel> ProductCards { get; set; }
    }
}