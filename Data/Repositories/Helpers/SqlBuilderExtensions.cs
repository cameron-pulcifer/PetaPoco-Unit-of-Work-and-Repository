using System.Collections.Generic;

namespace Data.Repositories.Helpers
{
    public static class SqlBuilderExtensions
    {

        public static TEntity First<TEntity>(this SqlBuilder<TEntity> builder)
        {
            return builder.Context.First<TEntity>(builder.Sql);
        }

        public static TEntity FirstOrDefault<TEntity>(this SqlBuilder<TEntity> builder)
        {
            return builder.Context.FirstOrDefault<TEntity>(builder.Sql);
        }

        public static TEntity Single<TEntity>(this SqlBuilder<TEntity> builder)
        {
            return builder.Context.Single<TEntity>(builder.Sql);
        }

        public static TEntity SingleOrDefault<TEntity>(this SqlBuilder<TEntity> builder)
        {
            return builder.Context.SingleOrDefault<TEntity>(builder.Sql);
        }

        public static IEnumerable<TEntity> GetAll<TEntity>(this SqlBuilder<TEntity> builder)
        {
            return builder.Context.Query<TEntity>(builder.Sql);
        }

    }
}
