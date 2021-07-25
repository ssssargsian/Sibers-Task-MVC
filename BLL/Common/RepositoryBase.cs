using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Repository.Common
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        private readonly SibersContext _context;

        public RepositoryBase(SibersContext context)
        {
            _context = context;
        }
        public void Create(T item)
        {
            _context.Set<T>().Add(item);
        }

        public void Delete(int id)
        {
            var entity = _context.Set<T>().Find(id);
            if (entity != null)
                _context.Set<T>().Remove(entity);
        }

        public T Get(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public IQueryable<T> GetAll()
        {
            return _context.Set<T>();
        }

        public void Update(T item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }
    }
}
