using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FlowersStore.Core.CoreModels;
using FlowersStore.Core.Repositories;
using AutoMapper;

namespace FlowersStore.DataAccess.MSSQL.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly FlowersStoreDbContext _context;
        private readonly IMapper _mapper;

        public BasketRepository(FlowersStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<bool> Add(Basket newBasket)
        {
            if (newBasket is null)
            {
                throw new ArgumentNullException(nameof(newBasket));
            }

            var basket = _mapper.Map<Core.CoreModels.Basket, Entities.Basket>(newBasket);

            var result = await _context.Baskets.AddAsync(basket);

            if (result.State != EntityState.Added)
            {
                return false;
            }

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<Basket> Get(Guid userId)
        {
            if (userId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(userId));
            }

            var basket = await _context.Baskets
                    .Where(f => f.Id == userId)
                    .FirstOrDefaultAsync();
              
            var shopingCart = _context.ShopingCarts
                    .Where(f => f.BasketId == basket.BasketId)
                    .Include(f => f.Product.Category)
                    .ToArray();

            basket.ShopingCarts = shopingCart;

            return _mapper.Map<Entities.Basket, Core.CoreModels.Basket>(basket);
        }
    }
}

/*
 *
  #1 why doesn't work include()?
    
    var basket = await _context.Baskets
            .Include(f => f.ShopingCarts)
            .Where(f => f.Id == userId)
            .FirstOrDefaultAsync();


 #2 Two queries and collect one Basket model, I think is strange.

    var basket = await _context.Baskets
                .Where(f => f.Id == userId)
                .FirstOrDefaultAsync();

    var shopingCart = _context.ShopingCarts
        .Where(f => f.BasketId == basket.BasketId)
        .Include(f => f.Product.Category)
        .ToArray();

    basket.ShopingCarts = shopingCart;

 #3
 var baskets = from b in _context.Baskets
                join sc in _context.ShopingCarts on b.BasketId equals sc.BasketId
                where b.Id == userId
                select b;
 */