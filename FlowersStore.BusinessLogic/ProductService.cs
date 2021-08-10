using System.Threading.Tasks;
using FlowersStore.Core.CoreModels;
using FlowersStore.Core.Services;
using System.Collections.Generic;
using FlowersStore.Core.Repositories;

namespace FlowersStore.BusinessLogic
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<ICollection<Product>> Get()
        {
            return await _productRepository.Get();
        }
    }
}