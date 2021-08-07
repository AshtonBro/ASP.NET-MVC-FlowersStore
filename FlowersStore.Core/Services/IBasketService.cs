using FlowersStore.Core.CoreModels;
using System;

namespace FlowersStore.Core.Services
{
    public interface IBasketService
    {
        Basket GetBasket(Guid userId);
        bool CreateBasket(Guid userId);
    }
}
