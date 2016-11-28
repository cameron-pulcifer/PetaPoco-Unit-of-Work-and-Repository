using System.Collections.Generic;
using Data.Repositories.Helpers;

namespace Data.Repositories.Contracts
{
    public interface IColumnLookupRepository<TEntity>
    {
        IEnumerable<TEntity> GetAll<TKey>(string columnName, TKey key);
        IEnumerable<TEntity> GetAll<TKey>(dynamic columnValuePairs);
        TEntity First<TKey>(string columnName, TKey key);
        TEntity First(dynamic columnValuePairs);
        TEntity FirstOrDefault<TKey>(string columnName, TKey key);
        TEntity FirstOrDefault(dynamic columnValuePairs);
        TEntity Single<TKey>(string columnName, TKey key);
        TEntity Single(dynamic columnValuePairs);
        TEntity SingleOrDefault<TKey>(string columnName, TKey key);
        TEntity SingleOrDefault(dynamic columnValuePairs);
        SqlBuilder<TEntity> FindByColumns(dynamic columnValuePairs);
    }
}
