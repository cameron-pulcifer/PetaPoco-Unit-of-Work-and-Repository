using System.Collections.Generic;

namespace Data.Repositories.Contacts
{
    public interface IRepository<TEntity>
    {
        IEnumerable<TEntity> GetAll();
        TEntity Get<TKey>(TKey id);
        TKey Add<TKey>(TEntity entity);
        void Modify(TEntity entity);
        void Remove(TEntity entity);
        TKey InsertOrUpdate<TKey>(TEntity entity);
    }
}
