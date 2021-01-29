using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlowersStore.Services
{
    public interface ICRUDService<T> where T : class
    {
        IEnumerable<T> Get(Guid userId);
        T GetById(Guid id);
        bool Create(Guid productId, int quantity,Guid userId);
        bool Update(T model);
        bool Update(IEnumerable<T> collection);
        bool Delete(Guid id);
    }
}
