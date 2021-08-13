using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using FlowersStore.Core.CoreModels;
using FlowersStore.Core.Repositories;
using AutoMapper;

namespace FlowersStore.DataAccess.MSSQL.Repositories
{
    public class ShopingCartRepository : IShopingCartRepository
    {
        private readonly FlowersStoreDbContext _context;
        private readonly IMapper _mapper;

        public ShopingCartRepository(FlowersStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ShopingCart> Get(Guid shopingCartId)
        {
            if (shopingCartId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(shopingCartId));
            }

            var shopingCart = await _context.ShopingCarts
                .Include(f => f.Product.Category)
                .FirstOrDefaultAsync(f => f.CartId == shopingCartId);

            if (shopingCart is null)
            {
                throw new ArgumentNullException(nameof(shopingCart));
            }

            return _mapper.Map<Entities.ShopingCart, Core.CoreModels.ShopingCart>(shopingCart);
        }

        public async Task<ICollection<ShopingCart>> Get()
        {
            var shopingCarts = await _context.ShopingCarts
                .Include(f => f.Product.Category)
                .ToArrayAsync();

            if (shopingCarts is null)
            {
                throw new ArgumentNullException(nameof(shopingCarts));
            }

            return _mapper.Map<ICollection<Entities.ShopingCart>, ICollection<Core.CoreModels.ShopingCart>>(shopingCarts);
        }

        public async Task<ShopingCart> GetByUserId(Guid userId)
        {
            if (userId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(userId));
            }

            var basket = await _context.Baskets
                .FirstOrDefaultAsync(f => f.Id == userId);

            if (basket is null)
            {
                throw new ArgumentNullException(nameof(basket));
            }

            var shopingCart = await _context.ShopingCarts
                .Where(f => f.BasketId == basket.BasketId)
                .Include(f => f.Product.Category)
                .FirstOrDefaultAsync();

            if (shopingCart is null)
            {
                throw new ArgumentNullException(nameof(shopingCart));
            }

            return _mapper.Map<Entities.ShopingCart, Core.CoreModels.ShopingCart>(shopingCart);
        }

        public async Task<ICollection<ShopingCart>> GetAllByUserId(Guid userId)
        {
            if (userId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(userId));
            }

            var basket = await _context.Baskets
                .FirstOrDefaultAsync(f => f.Id == userId);

            if (basket is null)
            {
                throw new ArgumentNullException(nameof(basket));
            }

            var shopingCarts = await _context.ShopingCarts
                .Where(f => f.BasketId == basket.BasketId)
                .Include(f => f.Product.Category)
                .ToArrayAsync();

            if (shopingCarts is null)
            {
                throw new ArgumentNullException(nameof(shopingCarts));
            }

            return _mapper.Map<ICollection<Entities.ShopingCart>, ICollection<Core.CoreModels.ShopingCart>>(shopingCarts);
        }

        public async Task<bool> Update(ShopingCart shopingCart)
        {

            if (shopingCart is null)
            {
                throw new ArgumentNullException(nameof(shopingCart));
            }

            var existedShopingCart = await _context.ShopingCarts
                .FirstOrDefaultAsync(f => f.CartId == shopingCart.CartId);

            if (existedShopingCart is null)
            {
                throw new ArgumentNullException(nameof(existedShopingCart));
            }

            _context.Entry(existedShopingCart).State = EntityState.Modified;

            existedShopingCart.DateCreated = shopingCart.DateCreated;
            existedShopingCart.Quantity = shopingCart.Quantity;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UpdateAll(ICollection<ShopingCart> shopingCarts)
        {

            if (shopingCarts is null)
            {
                throw new ArgumentNullException(nameof(shopingCarts));
            }

            foreach (var shopingCart in shopingCarts)
            {
                var existedShopingCart = await _context.ShopingCarts.FirstOrDefaultAsync(f => f.CartId == shopingCart.CartId);

                if (existedShopingCart is null)
                {
                    continue;
                }

                _context.Entry(existedShopingCart).State = EntityState.Modified;

                existedShopingCart.DateCreated = shopingCart.DateCreated;
                existedShopingCart.Quantity = shopingCart.Quantity;
            }

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Add(ShopingCart shopingCart)
        {
            if (shopingCart is null)
            {
                throw new ArgumentNullException(nameof(shopingCart));
            }

            var newShopingCart = _mapper.Map<Core.CoreModels.ShopingCart, Entities.ShopingCart>(shopingCart);

            newShopingCart.CartId = Guid.NewGuid();
            newShopingCart.DateCreated = DateTime.Now;

            var result = await _context.ShopingCarts.AddAsync(newShopingCart);

            if (result.State != EntityState.Added)
            {
                return false;
            }

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Delete(Guid shopingCartId)
        {
            if (shopingCartId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(shopingCartId));
            }

            var existedShopingCart = await _context.ShopingCarts.FirstOrDefaultAsync(f => f.CartId == shopingCartId);

            if (existedShopingCart == null)
            {
                throw new ArgumentNullException(nameof(existedShopingCart));
            }

            var result = _context.Remove(existedShopingCart);

            if (result.State != EntityState.Deleted)
            {
                return false;
            }

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAllByUserId(Guid userId)
        {
            if (userId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(userId));
            }

            var basket = await _context.Baskets
                .SingleOrDefaultAsync(f => f.Id == userId);

            var shopingCarts = await _context.ShopingCarts
                .Where(f => f.BasketId == basket.BasketId)
                .ToArrayAsync();

            foreach (var shopingCart in shopingCarts)
            {
                _context.ShopingCarts.Remove(shopingCart);
            }

            await _context.SaveChangesAsync();

            return true;
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

            var user = await _context.Users
                .FirstOrDefaultAsync(f => f.UserName == userNameContext.ToUpper());

            if (user is null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            var basket = await _context.Baskets
                 .FirstOrDefaultAsync(f => f.Id == user.Id);

            if (basket is null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            var existedShopingCart = await _context.ShopingCarts
                .Where(f => f.BasketId == basket.BasketId)
                .Include(f => f.Product.Category)
                .FirstOrDefaultAsync(f => f.ProductId == productId);

            if (existedShopingCart is null)
            {
                var newShopingCart = new ShopingCart()
                {
                    Quantity = quantity,
                    ProductId = productId,
                    BasketId = basket.BasketId
                };

                return await Add(newShopingCart);
            }
            else
            {
                existedShopingCart.Quantity += quantity;

                var existedShopingCartCore = _mapper.Map<Entities.ShopingCart, Core.CoreModels.ShopingCart>(existedShopingCart);

                return await Update(existedShopingCartCore);
            }
        }
    }
}