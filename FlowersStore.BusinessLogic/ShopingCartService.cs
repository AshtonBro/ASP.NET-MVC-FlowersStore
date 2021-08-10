using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using FlowersStore.Core.CoreModels;
using FlowersStore.Core.Repositories;
using FlowersStore.Core.Services;

namespace FlowersStore.BusinessLogic
{
    public class ShopingCartService : IShopingCartService
    {
        private readonly IShopingCartRepository _shopingCartRepository;
        private readonly IUserService _userService;

        public ShopingCartService(IShopingCartRepository shopingCartRepository, IUserService userService)
        {
            _shopingCartRepository = shopingCartRepository;
            _userService = userService;
        }

        public async Task<bool> Create(ShopingCart shopingCart)
        {
            if (shopingCart is null)
            {
                throw new ArgumentNullException(nameof(shopingCart));
            }

            return await _shopingCartRepository.Add(shopingCart);
        }

        public async Task<bool> Delete(Guid shopingCartId)
        {
            if (shopingCartId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(shopingCartId));
            }

            return await _shopingCartRepository.Delete(shopingCartId);
        }

        public async Task<bool> DeleteAllByUserId(Guid userId)
        {
            if (userId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(userId));
            }

            return await _shopingCartRepository.DeleteAllByUserId(userId);
        }

        public async Task<ShopingCart> Get(Guid shopingCartId)
        {
            if (shopingCartId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(shopingCartId));
            }

            var shopingCart = await _shopingCartRepository.Get(shopingCartId);

            if (shopingCart is null)
            {
                throw new ArgumentNullException(nameof(shopingCart));
            }

            return shopingCart;
        }

        public async Task<ICollection<ShopingCart>> Get()
        {
            return await _shopingCartRepository.Get();
        }

        public async Task<ICollection<ShopingCart>> GetAllByUserId(Guid userId)
        {
            if (userId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(userId));
            }

            var shopingCarts = await _shopingCartRepository.GetAllByUserId(userId);

            if (shopingCarts is null)
            {
                throw new ArgumentNullException(nameof(shopingCarts));
            }

            return shopingCarts;
        }

        public async Task<ShopingCart> GetByUserId(Guid userId)
        {
            if (userId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(userId));
            }

            var shopingCart = await _shopingCartRepository.GetByUserId(userId);

            if (shopingCart is null)
            {
                throw new ArgumentNullException(nameof(shopingCart));
            }

            return shopingCart;
        }

        public async Task<bool> Update(ShopingCart shopingCart)
        {
            if (shopingCart is null)
            {
                throw new ArgumentNullException(nameof(shopingCart));
            }

            return await _shopingCartRepository.Update(shopingCart);
        }

        public async Task<bool> UpdateAll(ICollection<ShopingCart> shopingCarts)
        {
            if (shopingCarts is null)
            {
                throw new ArgumentNullException(nameof(shopingCarts));
            }

            return await _shopingCartRepository.UpdateAll(shopingCarts);
        }

        public async Task<bool> CreateOrUpdate(string userNameContext, Guid productId, int quantity)
        {
            if (productId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(productId));
            }

            if (quantity < 0)
            {
                throw new ArgumentNullException(nameof(quantity));
            }

            if (string.IsNullOrEmpty(userNameContext))
            {
                throw new ArgumentNullException(nameof(userNameContext));
            }

            return await _shopingCartRepository.CreateOrUpdate(userNameContext, productId, quantity);
        }
    }
}

