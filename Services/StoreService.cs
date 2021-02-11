using FlowersStore.Data;
using FlowersStore.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace FlowersStore.Services
{
    public class StoreService : IStoreService
    {
        public static readonly StoreDBContext _context = new StoreDBContext();

        public IEnumerable<Product> GetProducts()
        {
            return _context.Products.Include(f => f.Category).ToArray();
        }
    }
}
