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
        private readonly StoreDBContext _context;
        private readonly IMapper _mapper;

        public BasketRepository(StoreDBContext context, IMapper mapper)
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
                .Include(f => f.User)
                .Include(f => f.ShopingCarts)
                .Where(f => f.Id == userId)
                .FirstOrDefaultAsync();

            return _mapper.Map<Entities.Basket, Core.CoreModels.Basket>(basket);
        }
    }
}