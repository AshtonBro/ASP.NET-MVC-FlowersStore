using FlowersStore.Data;
using FlowersStore.Models;
using System;
using System.Linq;

namespace FlowersStore.Services
{
    public class BasketService : IBasketService
    {
        public static readonly StoreDBContext _context = new StoreDBContext();
        public Basket GetBasket(Guid userId)
        {
            if (userId == Guid.Empty) throw new ArgumentException("User id is Empty.");
            
            return _context.Baskets.FirstOrDefault(b => b.User.Id == userId);
        }
    }
}
