using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace PillarInterview.Data.Repositories
{
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        private ApplicationDbContext _context;

        private DbSet<T> dbSet
        {
            get { return _context.Set<T>(); }
        }

        public GenericRepository(ApplicationDbContext context)
        {
            this._context = context;
        }

        public T Create(T item)
        {
            dbSet.Add(item);
            return item;
        }

        public void Delete(int id)
        {
            var item = dbSet.Find(id);
            Delete(item);
        }
        public void Delete(T item)
        {
            dbSet.Remove(item);
        }
        public IQueryable<T> Get()
        {
            return dbSet;
        }

        public T Get(int id)
        {
            return dbSet.Find(id);
        }

        public T Update(T item)
        {
            if (_context.Entry(item).State == EntityState.Detached)
            {
                Create(item);
            }
            else
            {
                dbSet.Update(item);
            }
            return item;
        }
    }
}
