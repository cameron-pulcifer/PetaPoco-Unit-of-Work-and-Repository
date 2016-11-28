using System.Collections.Generic;

namespace Data.Repositories.Contracts
{
    public interface IForeignKeyRepository<TEntity>
    {
        IEnumerable<TEntity> GetByForeignKey<TKey>(string columnName, TKey key);
    }
}
