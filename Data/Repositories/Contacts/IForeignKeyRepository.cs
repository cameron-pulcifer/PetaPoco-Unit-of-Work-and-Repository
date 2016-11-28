using System.Collections.Generic;

namespace Data.Repositories.Contacts
{
    public interface IForeignKeyRepository<TEntity>
    {
        IEnumerable<TEntity> GetByForeignKey<TKey>(string columnName, TKey key);
    }
}
