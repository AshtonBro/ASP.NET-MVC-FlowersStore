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
        public IEnumerable<ShopingCart> Get(Guid userId)
        {
            if (userId == Guid.Empty) return null;
            using (StoreDBContext db = new StoreDBContext())
            {
                var basket = db.Baskets.FirstOrDefault(basket => basket.UserId == userId);
                if (basket == null) return null;

                return db.ShopingCarts.Where(f => f.BasketId == basket.BasketId).Include(f => f.Product.Category).ToArray();
            }
        }

        public ShopingCart GetById(Guid id)
        {
            using (StoreDBContext db = new StoreDBContext())
            {
                return db.ShopingCarts.Include(f => f.Product.Category).FirstOrDefault(cart => cart.CartId == id);
            }
        }

        public bool Update(ShopingCart model)
        {
            using (StoreDBContext db = new StoreDBContext())
            {
                var oldModel = db.ShopingCarts.FirstOrDefault(cart => cart.CartId == model.CartId);
                if (oldModel == null) return false;

                oldModel.DateCreated = model.DateCreated;
                oldModel.Quantity = model.Quantity;

                return db.SaveChanges() >= 1;
            }
        }

        public bool Update(IEnumerable<ShopingCart> collection)
        {
            using (StoreDBContext db = new StoreDBContext())
            {
                foreach(var model in collection)
                {
                    var oldModel = db.ShopingCarts.FirstOrDefault(cart => cart.CartId == model.CartId);
                    if (oldModel == null) continue;

                    oldModel.DateCreated = model.DateCreated;
                    oldModel.Quantity = model.Quantity;
                }
               
                return db.SaveChanges() >= 1;
            }
        }

        public bool Create(Guid productId, int quantity, Guid userId)
        {
            using (StoreDBContext db = new StoreDBContext())
            {
                var basket = db.Baskets.FirstOrDefault(b => b.UserId == userId);
                if (basket == null) return false;

                var isShopingCartExist = db.ShopingCarts.FirstOrDefault(f => f.BasketId == basket.BasketId && f.ProductId == productId);

                if (isShopingCartExist == null)
                {
                    ShopingCart shoppingCart = new ShopingCart
                    {
                        CartId = Guid.NewGuid(),
                        DateCreated = DateTime.Now,
                        Product = db.Products.FirstOrDefault(f => f.ProductId == productId),
                        Quantity = quantity,
                        Basket = basket
                    };

                    db.ShopingCarts.Add(shoppingCart);
                }
                else
                {
                    isShopingCartExist.Quantity += quantity;
                   return Update(isShopingCartExist);
                }

                return db.SaveChanges() >= 1;
            }
        }

        public bool Delete(Guid id)
        {
            using (StoreDBContext db = new StoreDBContext())
            {
                var modelToDelete = db.ShopingCarts.FirstOrDefault(cart => cart.CartId == id);
                if (modelToDelete == null) return false;

                db.Remove(modelToDelete);

                return db.SaveChanges() >= 1;           
            }
        }
    }
}
