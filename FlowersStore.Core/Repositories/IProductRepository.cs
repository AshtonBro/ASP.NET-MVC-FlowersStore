using System.Collections.Generic;
using System.Threading.Tasks;
using FlowersStore.Core.CoreModels;

namespace FlowersStore.Core.Repositories
{
    public interface IProductRepository
    {
        Task<ICollection<Product>> Get();
    }
}