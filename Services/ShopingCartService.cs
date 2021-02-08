﻿using FlowersStore.Data;
using FlowersStore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FlowersStore.Services
{
    public class ShopingCartService : ICRUDService<ShopingCart>
    {
        public IEnumerable<ShopingCart> Get(Guid id)
        {
            if (id == Guid.Empty) return null;
            using (StoreDBContext db = new StoreDBContext())
            {
                var basket = db.Baskets.FirstOrDefault(basket => basket.Id == id);
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

        public bool Create(ShopingCart model)
        {
            using (StoreDBContext db = new StoreDBContext())
            {
                model.CartId = Guid.NewGuid();
                model.DateCreated = DateTime.Now;
                db.ShopingCarts.Add(model);            

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
