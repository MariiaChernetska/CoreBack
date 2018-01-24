using System.Linq;

namespace PillarInterview.Data.Repositories
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> Get();
        T Get(int id);
        T Create(T item);
        T Update(T item);
        void Delete(int id);
        void Delete(T item);
    }
}
