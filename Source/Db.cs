using System.Configuration;
using System.Data.Common;

namespace snorbert
{
    /// <summary>
    /// 
    /// </summary>
    public class Db
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static DbConnection GetOpenMySqlConnection()
        {
            string connectionString = string.Empty;
            var connection = new MySql.Data.MySqlClient.MySqlConnection(ConfigurationManager.ConnectionStrings[0].ConnectionString);
            connection.Open();

            return connection;
        }
    }
}
