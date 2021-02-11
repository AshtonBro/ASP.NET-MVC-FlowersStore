using FlowersStore.Data;
using FlowersStore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FlowersStore.Services
{
    public class ShopingCartService : ICRUDService<ShopingCart>
    {
        public static readonly StoreDBContext _context = new StoreDBContext();
        public IEnumerable<ShopingCart> Get(Guid id)
        {
            if (id == Guid.Empty) return null;
            
            var basket = _context.Baskets.FirstOrDefault(basket => basket.Id == id);
            if (basket == null) return null;

            return _context.ShopingCarts.Where(f => f.BasketId == basket.BasketId).Include(f => f.Product.Category).ToArray();
            
        }

        public ShopingCart GetById(Guid id)
        {
            return _context.ShopingCarts.Include(f => f.Product.Category).FirstOrDefault(cart => cart.CartId == id);
        }

        public bool Update(ShopingCart model)
        {
            var oldModel = _context.ShopingCarts.FirstOrDefault(cart => cart.CartId == model.CartId);
            if (oldModel == null) return false;

            oldModel.DateCreated = model.DateCreated;
            oldModel.Quantity = model.Quantity;

            return _context.SaveChanges() >= 1;
        }

        public bool Update(IEnumerable<ShopingCart> collection)
        {
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
            model.CartId = Guid.NewGuid();
            model.DateCreated = DateTime.Now;
            _context.ShopingCarts.Add(model);

            return _context.SaveChanges() >= 1;
        }

        public bool Delete(Guid id)
        {
            var modelToDelete = _context.ShopingCarts.FirstOrDefault(cart => cart.CartId == id);
            if (modelToDelete == null) return false;

            _context.Remove(modelToDelete);

            return _context.SaveChanges() >= 1;
        }
    }
}

