using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using FlowersStore.Core.CoreModels;
using FlowersStore.Core.Repositories;
using FlowersStore.Core.Services;

namespace FlowersStore.BusinessLogic
{
    public class ProductCardService : IProductCardService
    {
        private readonly IProductCardRepository _productCardRepository;

        public ProductCardService(IProductCardRepository productCardRepository)
        {
            _productCardRepository = productCardRepository;
        }

        public async Task<bool> Create(ProductCard productCard)
        {
            if (productCard == null)
            {
                throw new ArgumentNullException(nameof(productCard));
            }

            return await _productCardRepository.Add(productCard);
        }

        public async Task<bool> Delete(Guid productCardId)
        {
            if (productCardId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(productCardId));
            }

            return await _productCardRepository.Delete(productCardId);
        }

        public async Task<bool> DeleteAllByUserId(Guid userId)
        {
            if (userId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(userId));
            }

            return await _productCardRepository.DeleteAllByUserId(userId);
        }

        public async Task<ProductCard> Get(Guid productCardId)
        {
            if (productCardId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(productCardId));
            }

            var productCard = await _productCardRepository.Get(productCardId);

            if (productCard == null)
            {
                throw new ArgumentNullException(nameof(productCard));
            }

            return productCard;
        }

        public async Task<ICollection<ProductCard>> Get()
        {
            return await _productCardRepository.Get();
        }

        public async Task<ICollection<ProductCard>> GetAllByUserId(Guid userId)
        {
            if (userId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(userId));
            }

            var productCards = await _productCardRepository.GetAllByUserId(userId);

            if (productCards == null)
            {
                throw new ArgumentNullException(nameof(productCards));
            }

            return productCards;
        }

        public async Task<ProductCard> GetByUserId(Guid userId)
        {
            if (userId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(userId));
            }

            var productCard = await _productCardRepository.GetByUserId(userId);

            if (productCard == null)
            {
                throw new ArgumentNullException(nameof(productCard));
            }

            return productCard;
        }

        public async Task<bool> Update(ProductCard productCard)
        {
            if (productCard == null)
            {
                throw new ArgumentNullException(nameof(productCard));
            }

            return await _productCardRepository.Update(productCard);
        }

        public async Task<bool> UpdateAll(ICollection<ProductCard> productCards)
        {
            if (productCards == null)
            {
                throw new ArgumentNullException(nameof(productCards));
            }

            return await _productCardRepository.UpdateAll(productCards);
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

            return await _productCardRepository.CreateOrUpdate(userNameContext, productId, quantity);
        }
    }
}