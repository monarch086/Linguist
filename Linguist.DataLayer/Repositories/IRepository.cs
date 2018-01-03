using System.Collections.Generic;

namespace Linguist.DataLayer.Repositories
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();

        int Add(T entity);

        int Edit(T entity);

        int Remove(int entityId);
    }
}
