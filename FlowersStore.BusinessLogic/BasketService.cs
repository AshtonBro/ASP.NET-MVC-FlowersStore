using System;
using System.Threading.Tasks;
using FlowersStore.Core.CoreModels;
using FlowersStore.Core.Repositories;
using FlowersStore.Core.Services;

namespace FlowersStore.BusinessLogic
{
    public class BasketService : IBasketService
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IUserService _userService;

        public BasketService(IBasketRepository basketRepository, IUserService userService)
        {
            _basketRepository = basketRepository;
            _userService = userService;
        }

        public async Task<bool> Create(Guid userId)
        {
            if (userId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(userId));
            }

            Basket newBasket = new Basket()
            {
                Id = userId,
                BasketId = Guid.NewGuid(),
                DateCreated = DateTime.Now
            };

            return await _basketRepository.Add(newBasket);
        }

        public async Task<Basket> Get(string userNameContext)
        {
            if (string.IsNullOrEmpty(userNameContext))
            {
                throw new ArgumentNullException(nameof(userNameContext));
            }

            var user = await _userService.Get(userNameContext);

            if (user is null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return await _basketRepository.Get(user.Id);
        }
    }
}