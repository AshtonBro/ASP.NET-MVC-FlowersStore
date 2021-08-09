using FlowersStore.Core.CoreModels;
using FlowersStore.Core.Services;
using FlowersStore.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace FlowersStore.BusinessLogic
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
