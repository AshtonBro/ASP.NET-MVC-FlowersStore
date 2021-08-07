using FlowersStore.Core.CoreModels;
using System.Collections.Generic;

namespace FlowersStore.Core.Services
{
    public interface IStoreService
    {
        IEnumerable<Product> GetProducts();
    }
}
