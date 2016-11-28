using System.Linq;
using System.Text;
using PetaPoco;
using PetaPoco.Internal;

namespace Data.Repositories.Helpers
{
    public static class QueryHelper
    {
        public static string GetQuery<TEntity>(string columnName)
        {
            var pd = PocoData.ForType(typeof(TEntity));
            return "select * from " + pd.TableInfo.TableName + " where " + columnName + " = @0";
        }

        public static Sql GetDynamicQuery<TEntity>(object values)
        {
            var pd = PocoData.ForType(typeof(TEntity));
            var sb = new StringBuilder()
                .Append("select * from " + pd.TableInfo.TableName);

            var dict = new DynamicDictionary(values);

            var index = 0;
            foreach (var d in dict)
            {
                if (index == 0)
                    sb.Append(" where " + d.Key);
                else
                    sb.Append(" and " + d.Key);

                sb.Append(" = @" + index);
                index++;
            }

            var query = Sql.Builder.Append(sb.ToString(), dict.Values.Cast<object>().ToArray());

            return query;
        }
    }
}
