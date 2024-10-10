using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopClassLibrary.Service
{
    public interface ICrudService<T>
    {
        Task Add(T entity);
        Task Update(T entity);
        Task<T> GetById(long id);
        Task<IEnumerable<T>> GetAll();
        Task Delete(long id);
    }

}
