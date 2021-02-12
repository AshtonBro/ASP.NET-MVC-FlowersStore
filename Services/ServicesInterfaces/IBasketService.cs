using FlowersStore.Models;
using System;

namespace FlowersStore.Services.ServicesInterfaces
{
    public interface IBasketService
    {
        Basket GetBasket(Guid userId);
        bool CreateBasket(Guid userId);
    }
}
