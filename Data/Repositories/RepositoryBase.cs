using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Repositories.Contacts;
using Data.UnitOfWork;
using PetaPoco;
using PetaPoco.Internal;

namespace Data.Repositories
{
    public class RepositoryBase<TEntity> : IRepository<TEntity>
    {
        protected readonly Database Context;
        /// <summary>
        /// IUnitOfWork dependency injection
        /// </summary>
        /// <param name="unitOfWork"></param>
        public RepositoryBase(IUnitOfWork unitOfWork)
        {
            Context = unitOfWork.Context;
        }

        public IEnumerable<TEntity> GetAll()
        {
            var pd = PocoData.ForType(typeof(TEntity));
            var sql = "SELECT * FROM " + pd.TableInfo.TableName;
            return Context.Query<TEntity>(sql);
        }

        // GetAll(spec).Where(spec).Execute(); Specification pattern

        public TEntity Get<TKey>(TKey id)
        {
            return Context.SingleOrDefault<TEntity>(id);
        }

        public TKey Add<TKey>(TEntity entity)
        {
            return (TKey) Context.Insert(entity);
        }

        public void Modify(TEntity entity)
        {
            Context.Update(entity);
        }

        public void Remove(TEntity entity)
        {
            Context.Delete(entity);
        }

        public TKey InsertOrUpdate<TKey>(TEntity entity)
        {
            var pd = PocoData.ForType(typeof(TEntity));
            var primaryKey = pd.TableInfo.PrimaryKey;

            var id = entity.GetType().GetProperty(primaryKey).GetValue(entity, null);
            var exists = Context.SingleOrDefault<TEntity>(id);
            
            if (!EqualityComparer<TEntity>.Default.Equals(exists, default(TEntity)))
            {
                Context.Update(entity);
                return (TKey) id;
            }

            return (TKey) Context.Insert(entity);
        }
    }
}
