using System;
using System.Threading.Tasks;
using FlowersStore.Core.CoreModels;

namespace FlowersStore.Core.Services
{
    public interface IBasketService
    {
        Task<bool> Create(Guid userId);

        Task<Basket> Get(string userNameContext);
    }
}