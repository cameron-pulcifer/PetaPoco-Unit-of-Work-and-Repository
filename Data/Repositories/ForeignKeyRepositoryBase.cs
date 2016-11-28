using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Repositories.Contracts;
using Data.UnitOfWork;
using PetaPoco.Internal;

namespace Data.Repositories
{
    public class ForeignKeyRepositoryBase<TEntity> : RepositoryBase<TEntity>, IForeignKeyRepository<TEntity>
    {
        public ForeignKeyRepositoryBase(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public IEnumerable<TEntity> GetByForeignKey<TKey>(string columnName, TKey key)
        {
            var pd = PocoData.ForType(typeof(TEntity));
            var sql = "select * from " + pd.TableInfo.TableName + " where " + columnName + " = @0";
            return Context.Query<TEntity>(sql, key);
        }
    }
}
