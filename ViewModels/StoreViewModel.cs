using FlowersStore.Data;
using FlowersStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlowersStore.ViewModels
{
    public class StoreViewModel
    {
        public string Title { get; set; }
        // Filtration

        public IEnumerable<Product> Products { get; set; }
    }
}
