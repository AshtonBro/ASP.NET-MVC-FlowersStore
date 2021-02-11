using FlowersStore.Models;
using System.Collections.Generic;

namespace FlowersStore.Services
{
    public interface IStoreService
    {
        IEnumerable<Product> GetProducts();
    }
}
