using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FlowersStore.Core.CoreModels;

namespace FlowersStore.Core.Repositories
{
    public interface IProductCardRepository
    {
        Task<bool> Add(ProductCard productCard);

        Task<bool> Delete(Guid productCardId);

        Task<ProductCard> Get(Guid productCardId);

        Task<ICollection<ProductCard>> Get();

        Task<ProductCard> GetByUserId(Guid userId);

        Task<ICollection<ProductCard>> GetAllByUserId(Guid userId);

        Task<bool> DeleteAllByUserId(Guid userId);

        Task<bool> Update(ProductCard productCard);

        Task<bool> UpdateAll(ICollection<ProductCard> productCards);

        Task<bool> CreateOrUpdate(string userNameContext, Guid productId, int quantity);
    }
}