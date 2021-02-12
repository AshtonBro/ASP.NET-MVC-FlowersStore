using FlowersStore.Models;
using System.Collections.Generic;

namespace FlowersStore.Services.ServicesInterfaces
{
    public interface IStoreService
    {
        IEnumerable<Product> GetProducts();
    }
}
