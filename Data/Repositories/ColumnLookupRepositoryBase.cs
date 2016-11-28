using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Data.Repositories.Contacts;
using Data.Repositories.Helpers;
using Data.UnitOfWork;
using PetaPoco;
using PetaPoco.Internal;

namespace Data.Repositories
{
    public class ColumnLookupRepositoryBase<TEntity> : RepositoryBase<TEntity>, IColumnLookupRepository<TEntity>
    {
        public ColumnLookupRepositoryBase(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public IEnumerable<TEntity> GetAll<TKey>(string columnName, TKey key)
        {
            return Context.Query<TEntity>(QueryHelper.GetQuery<TEntity>(columnName), key);
        }

        /// <summary>
        /// Example: new { CustomerName = "John", CustomerTypeId = 1 }
        /// </summary>
        /// <param name="columnValuePairs"></param>
        /// <returns></returns>
        public IEnumerable<TEntity> GetAll<TKey>(dynamic columnValuePairs)
        {
            return Context.Query<TEntity>(QueryHelper.GetDynamicQuery<TEntity>((object)columnValuePairs));
        }

        public TEntity First<TKey>(string columnName, TKey key)
        {
            return Context.First<TEntity>(QueryHelper.GetQuery<TEntity>(columnName), key);
        }

        /// <summary>
        /// Example: new { CustomerName = "John", CustomerTypeId = 1 }
        /// </summary>
        /// <param name="columnValuePairs"></param>
        /// <returns></returns>
        public TEntity First(dynamic columnValuePairs)
        {
            return Context.First<TEntity>(QueryHelper.GetDynamicQuery<TEntity>((object)columnValuePairs));
        }

        public TEntity FirstOrDefault<TKey>(string columnName, TKey key)
        {
            return Context.FirstOrDefault<TEntity>(QueryHelper.GetQuery<TEntity>(columnName), key);
        }

        /// <summary>
        /// Example: new { CustomerName = "John", CustomerTypeId = 1 }
        /// </summary>
        /// <param name="columnValuePairs"></param>
        /// <returns></returns>
        public TEntity FirstOrDefault(dynamic columnValuePairs)
        {
            return Context.FirstOrDefault<TEntity>(QueryHelper.GetDynamicQuery<TEntity>((object)columnValuePairs));
        }

        public TEntity Single<TKey>(string columnName, TKey key)
        {
            return Context.Single<TEntity>(QueryHelper.GetQuery<TEntity>(columnName), key);
        }

        /// <summary>
        /// Example: new { CustomerName = "John", CustomerTypeId = 1 }
        /// </summary>
        /// <param name="columnValuePairs"></param>
        /// <returns></returns>
        public TEntity Single(dynamic columnValuePairs)
        {
            return Context.Single<TEntity>(QueryHelper.GetDynamicQuery<TEntity>((object)columnValuePairs));
        }

        public TEntity SingleOrDefault<TKey>(string columnName, TKey key)
        {
            return Context.SingleOrDefault<TEntity>(QueryHelper.GetQuery<TEntity>(columnName), key);
        }

        /// <summary>
        /// Example: new { CustomerName = "John", CustomerTypeId = 1 }
        /// </summary>
        /// <param name="columnValuePairs"></param>
        /// <returns></returns>
        public TEntity SingleOrDefault(dynamic columnValuePairs)
        {
            return Context.SingleOrDefault<TEntity>(QueryHelper.GetDynamicQuery<TEntity>((object)columnValuePairs));
        }

        public SqlBuilder<TEntity> FindByColumns(dynamic columnValuePairs)
        {
            return new SqlBuilder<TEntity>
            {
                Context = this.Context,
                Sql = QueryHelper.GetDynamicQuery<TEntity>((object) columnValuePairs)
            };
        }
    }
}
