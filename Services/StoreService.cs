using FlowersStore.Data;
using FlowersStore.Models;
using FlowersStore.Services.ServicesInterfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace FlowersStore.Services
{
    public class StoreService : IStoreService
    {
        public IEnumerable<Product> GetProducts()
        {
            using StoreDBContext _context = new StoreDBContext();
            return _context.Products.Include(f => f.Category).ToArray();
        }
    }
}
