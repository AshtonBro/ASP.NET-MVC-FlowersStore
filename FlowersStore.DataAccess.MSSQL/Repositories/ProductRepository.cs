using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FlowersStore.Core.CoreModels;
using FlowersStore.Core.Repositories;
using AutoMapper;

namespace FlowersStore.DataAccess.MSSQL.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly StoreDBContext _context;
        private readonly IMapper _mapper;

        public ProductRepository(StoreDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ICollection<Product>> Get()
        {
            var products = await _context.Products.Include(f => f.Category).ToArrayAsync();

            return _mapper.Map<ICollection<Entities.Product>, ICollection<Core.CoreModels.Product>>(products);
        }
    }
}
