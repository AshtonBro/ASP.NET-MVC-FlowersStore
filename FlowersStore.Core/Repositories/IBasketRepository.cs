using System;
using System.Threading.Tasks;
using FlowersStore.Core.CoreModels;

namespace FlowersStore.Core.Repositories
{
    public interface IBasketRepository
    {
        Task<bool> Add(Basket newBasket);

        Task<Basket> Get(Guid userId);
    }
}