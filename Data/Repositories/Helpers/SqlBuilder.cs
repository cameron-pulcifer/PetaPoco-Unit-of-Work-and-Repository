using PetaPoco;

namespace Data.Repositories.Helpers
{
    public class SqlBuilder<TEntity>
    {
        public Sql Sql { get; set; }
        public Database Context { get; set; }
    }
}
