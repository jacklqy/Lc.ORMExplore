using Frameworks;
using Frameworks.SqlFilter;
using Frameworks.SqlMapping;
using Model;
using System;
using System.Data.SqlClient;
using System.Linq;

namespace DAL
{
    /// <summary>
    /// 数据库查询帮助类库---自动生成sql---通用
    /// </summary>
    public class SqlHelper
    {
        /// <summary>
        /// 通用主键查询操作
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public T Find<T>(int id) where T : BaseModel //增加约束
        {
            Type type = typeof(T);
            //string columnsString = string.Join(',', type.GetProperties().Select(p => $"[{p.GetMappingName()}]"));
            //string sql = $"select {columnsString} from [{type.GetMappingName()}] where id={id}";

            string sql = $"{SqlBuilder<T>.GetFindSql()}{id}";
            string connectionStr = SqlConnectionPool.GetConnectionString(SqlConnectionPool.SqlConnectionType.Read);
            Console.WriteLine($"读取数据链接数据库字符串：{connectionStr}");
            using (SqlConnection connection = new SqlConnection(connectionStr))
            {
                SqlCommand command = new SqlCommand(sql, connection);
                connection.Open();
                var reader = command.ExecuteReader();
                if (reader.Read())
                {
                    T t = (T)Activator.CreateInstance(type);
                    foreach (var prop in type.GetProperties())
                    {
                        var propName = prop.GetMappingName();//优化想法，查询时as一下，可以省下一轮
                        prop.SetValue(t, reader[propName] is DBNull ? null : reader[propName]);//可控类型  设置成null而不是数据库查询的值
                    }
                    return t;
                }
                else
                {
                    return default(T);
                }
            }
        }

        public int Insert<T>(T t) where T : BaseModel //增加约束
        {
            Type type = t.GetType();

            //string columnsString = string.Join(',', type.GetPropertiesWithoutKey().Select(p => $"[{p.GetMappingName()}]"));
            //string valuesString = string.Join(',', type.GetPropertiesWithoutKey().Select(p => $"@{p.GetMappingName()}"));
            //string sql = $"insert into [{type.GetMappingName()}] ({columnsString}) values({valuesString})";//不能直接拼装--sql注入问题

            string sql = $"{SqlBuilder<T>.GetInsertSql()}";
            var paraArray = type.GetPropertiesWithoutKey().Select(p => new SqlParameter($"@{p.GetMappingName()}", p.GetValue(t) ?? DBNull.Value)).ToArray();

            string connectionStr = SqlConnectionPool.GetConnectionString(SqlConnectionPool.SqlConnectionType.Write);
            using (SqlConnection connection = new SqlConnection(connectionStr))
            {
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddRange(paraArray);
                connection.Open();
                object oId = command.ExecuteScalar();
                return int.TryParse(oId?.ToString(), out int iId) ? iId : -1;
            }
        }
    }
}
