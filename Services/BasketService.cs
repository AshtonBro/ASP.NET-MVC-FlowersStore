using FlowersStore.Data;
using FlowersStore.Models;
using FlowersStore.Services.ServicesInterfaces;
using System;
using System.Linq;

namespace FlowersStore.Services
{
    public class BasketService : IBasketService
    {
        public Basket GetBasket(Guid userId)
        {
            if (userId == Guid.Empty) throw new ArgumentException("User id is Empty.");
            using StoreDBContext _context = new StoreDBContext();
            return _context.Baskets.FirstOrDefault(b => b.User.Id == userId);
        }

        public bool CreateBasket(Guid userId)
        {
            if (userId == Guid.Empty) throw new ArgumentException("User id is Empty.");
            using StoreDBContext _context = new StoreDBContext();
            Basket newBasket = new Basket()
            {
                Id = userId,
                BasketId = Guid.NewGuid(),
                DateCreated = DateTime.Now
            };
           
            _context.Baskets.Add(newBasket);
            return _context.SaveChanges() >= 1;
        }
    }
}
