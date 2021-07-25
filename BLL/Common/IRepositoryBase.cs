using System.Collections.Generic;
using System.Linq;

namespace Repository.Common
{
    public interface IRepositoryBase<T> where T : class
    {
        IQueryable<T> GetAll();
        void Create(T item);
        T Get(int id);
        void Delete(int id);
        void Update(T item);
    }
}
