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
    public class ProductCardRepository : IProductCardRepository
    {
        private readonly FlowersStoreDbContext _context;
        private readonly IMapper _mapper;

        public ProductCardRepository(FlowersStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ProductCard> Get(Guid productCardId)
        {
            if (productCardId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(productCardId));
            }

            var productCard = await _context.ProductCards
                .Include(f => f.Product.Category)
                .FirstOrDefaultAsync(f => f.Id == productCardId);

            if (productCard == null)
            {
                throw new ArgumentNullException(nameof(productCard));
            }

            return _mapper.Map<Entities.ProductCard, Core.CoreModels.ProductCard>(productCard);
        }

        public async Task<ICollection<ProductCard>> Get()
        {
            var productCards = await _context.ProductCards
                .Include(f => f.Product.Category)
                .ToArrayAsync();

            if (productCards == null)
            {
                throw new ArgumentNullException(nameof(productCards));
            }

            return _mapper.Map<ICollection<Entities.ProductCard>, ICollection<Core.CoreModels.ProductCard>>(productCards);
        }

        public async Task<ProductCard> GetByUserId(Guid userId)
        {
            if (userId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(userId));
            }

            var basket = await _context.Baskets
                .FirstOrDefaultAsync(f => f.UserId == userId);

            if (basket == null)
            {
                throw new ArgumentNullException(nameof(basket));
            }

            var productCard = await _context.ProductCards
                .Where(f => f.BasketId == basket.Id)
                .Include(f => f.Product.Category)
                .FirstOrDefaultAsync();

            if (productCard == null)
            {
                throw new ArgumentNullException(nameof(productCard));
            }

            return _mapper.Map<Entities.ProductCard, Core.CoreModels.ProductCard>(productCard);
        }

        public async Task<ICollection<ProductCard>> GetAllByUserId(Guid userId)
        {
            if (userId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(userId));
            }

            var basket = await _context.Baskets
                .FirstOrDefaultAsync(f => f.UserId == userId);

            if (basket == null)
            {
                throw new ArgumentNullException(nameof(basket));
            }

            var productCards = await _context.ProductCards
                .Where(f => f.BasketId == basket.Id)
                .Include(f => f.Product.Category)
                .ToArrayAsync();

            if (productCards == null)
            {
                throw new ArgumentNullException(nameof(productCards));
            }

            return _mapper.Map<ICollection<Entities.ProductCard>, ICollection<Core.CoreModels.ProductCard>>(productCards);
        }

        public async Task<bool> Update(ProductCard productCard)
        {

            if (productCard == null)
            {
                throw new ArgumentNullException(nameof(productCard));
            }

            var existedProductCard = await _context.ProductCards
                .FirstOrDefaultAsync(f => f.Id == productCard.Id);

            if (existedProductCard == null)
            {
                throw new ArgumentNullException(nameof(existedProductCard));
            }

            _context.Entry(existedProductCard).State = EntityState.Modified;

            existedProductCard.DateCreated = productCard.DateCreated;
            existedProductCard.Quantity = productCard.Quantity;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UpdateAll(ICollection<ProductCard> productCards)
        {

            if (productCards == null)
            {
                throw new ArgumentNullException(nameof(productCards));
            }

            foreach (var productCard in productCards)
            {
                var existedProductCard = await _context.ProductCards
                    .FirstOrDefaultAsync(f => f.Id == productCard.Id);

                if (existedProductCard == null)
                {
                    continue;
                }

                _context.Entry(existedProductCard).State = EntityState.Modified;

                existedProductCard.DateCreated = productCard.DateCreated;
                existedProductCard.Quantity = productCard.Quantity;
            }

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Add(ProductCard productCard)
        {
            if (productCard == null)
            {
                throw new ArgumentNullException(nameof(productCard));
            }

            var newProductCard = _mapper.Map<Core.CoreModels.ProductCard, Entities.ProductCard>(productCard);

            newProductCard.Id = Guid.NewGuid();
            newProductCard.DateCreated = DateTime.Now;

            var result = await _context.ProductCards.AddAsync(newProductCard);

            if (result.State != EntityState.Added)
            {
                return false;
            }

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Delete(Guid productCardId)
        {
            if (productCardId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(productCardId));
            }

            var existedProductCard = await _context.ProductCards
                .FirstOrDefaultAsync(f => f.Id == productCardId);

            if (existedProductCard == null)
            {
                throw new ArgumentNullException(nameof(existedProductCard));
            }

            var result = _context.Remove(existedProductCard);

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
                .SingleOrDefaultAsync(f => f.UserId == userId);

            var productCards = await _context.ProductCards
                .Where(f => f.BasketId == basket.Id)
                .ToArrayAsync();

            foreach (var productCard in productCards)
            {
                _context.ProductCards.Remove(productCard);
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

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            var basket = await _context.Baskets
                 .FirstOrDefaultAsync(f => f.UserId == user.Id);

            if (basket == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            var existedProductCard = await _context.ProductCards
                .Where(f => f.BasketId == basket.Id)
                .Include(f => f.Product.Category)
                .FirstOrDefaultAsync(f => f.ProductId == productId);

            if (existedProductCard == null)
            {
                var newProductCard = new ProductCard()
                {
                    Quantity = quantity,
                    ProductId = productId,
                    BasketId = basket.Id
                };

                return await Add(newProductCard);
            }
            else
            {
                existedProductCard.Quantity += quantity;

                var existedProductCardCore = _mapper.Map<Entities.ProductCard, Core.CoreModels.ProductCard>(existedProductCard);

                return await Update(existedProductCardCore);
            }
        }
    }
}