using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FlowersStore.Core.CoreModels;

namespace FlowersStore.Core.Repositories
{
    public interface IShopingCartRepository
    {
        Task<bool> Add(ShopingCart shopingCart);

        Task<bool> Delete(Guid shopingCartId);

        Task<ShopingCart> Get(Guid shopingCartId);

        Task<ICollection<ShopingCart>> Get();

        Task<ShopingCart> GetByUserId(Guid userId);

        Task<ICollection<ShopingCart>> GetAllByUserId(Guid userId);

        Task<bool> DeleteAllByUserId(Guid userId);

        Task<bool> Update(ShopingCart shopingCart);

        Task<bool> UpdateAll(ICollection<ShopingCart> shopingCarts);

        Task<bool> CreateOrUpdate(string userNameContext, Guid productId, int quantity);
    }
}