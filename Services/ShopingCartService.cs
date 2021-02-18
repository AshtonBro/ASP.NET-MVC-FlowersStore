using FlowersStore.Data;
using FlowersStore.Models;
using FlowersStore.Services.ServicesInterfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FlowersStore.Services
{
    public class ShopingCartService : IShopingCartCRUDService<ShopingCart>
    {
        public IEnumerable<ShopingCart> Get(Guid id)
        {
            if (id == Guid.Empty) throw new ArgumentException("User id is empty.");
            using StoreDBContext _context = new StoreDBContext();
            Basket basket = _context.Baskets.FirstOrDefault(basket => basket.Id == id);

            if (basket == null) throw new ArgumentException("Basket is empty.");

            return _context.ShopingCarts.Where(f => f.BasketId == basket.BasketId).Include(f => f.Product.Category).ToArray();
        }

        public ShopingCart GetById(Guid id)
        {
            if (id == Guid.Empty) throw new ArgumentException("ShopingCart id is empty.");
            using StoreDBContext _context = new StoreDBContext();
            return _context.ShopingCarts.Include(f => f.Product.Category).FirstOrDefault(cart => cart.CartId == id);
        }

        public bool Update(ShopingCart model)
        {
            using StoreDBContext _context = new StoreDBContext();
            ShopingCart oldModel = _context.ShopingCarts.FirstOrDefault(cart => cart.CartId == model.CartId);
            if (oldModel == null) return false;

            oldModel.DateCreated = model.DateCreated;
            oldModel.Quantity = model.Quantity;

            return _context.SaveChanges() >= 1;
        }

        public bool Update(IEnumerable<ShopingCart> collection)
        {
            using StoreDBContext _context = new StoreDBContext();
            foreach (var model in collection)
            {
                var oldModel = _context.ShopingCarts.FirstOrDefault(cart => cart.CartId == model.CartId);
                if (oldModel == null) continue;

                oldModel.DateCreated = model.DateCreated;
                oldModel.Quantity = model.Quantity;
            }

            return _context.SaveChanges() >= 1;
        }

        public bool Create(ShopingCart model)
        {
            using StoreDBContext _context = new StoreDBContext();
            model.CartId = Guid.NewGuid();
            model.DateCreated = DateTime.Now;
            _context.ShopingCarts.Add(model);

            return _context.SaveChanges() >= 1;
        }

        public bool Delete(Guid id)
        {
            if (id == Guid.Empty) return false;
            using StoreDBContext _context = new StoreDBContext();
            ShopingCart modelToDelete = _context.ShopingCarts.FirstOrDefault(cart => cart.CartId == id);
            if (modelToDelete == null) return false;

            _context.Remove(modelToDelete);

            return _context.SaveChanges() >= 1;
        }

        public bool DeleteAll(Guid id)
        {
            if (id == null) return false;
            using StoreDBContext _context = new StoreDBContext();
            var shoppingCarts = Get(id);
            foreach (var cart in shoppingCarts)
            {
                _context.ShopingCarts.Remove(cart);
            }
            return _context.SaveChanges() >= 1;
        }
    }
}

