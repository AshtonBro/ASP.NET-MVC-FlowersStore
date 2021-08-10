using System.Collections.Generic;
using System.Threading.Tasks;
using FlowersStore.Core.CoreModels;

namespace FlowersStore.Core.Services
{
    public interface IProductService
    {
        Task<ICollection<Product>> Get();
    }
}