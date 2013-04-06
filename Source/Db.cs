using System.Configuration;
using System.Data.Common;
using System.Data.SqlServerCe;
using woanware;

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
            var connection = new MySql.Data.MySqlClient.MySqlConnection(ConfigurationManager.ConnectionStrings[0].ConnectionString);
            connection.Open();
            return connection;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string GetSqlCeConnectionString()
        {
            return string.Format("DataSource=\"{0}\"; Max Database Size=4091", GetSqlCeDbPath());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static DbConnection GetOpenSqlCeConnection()
        {
            var connection = new SqlCeConnection(GetSqlCeConnectionString());
            connection.Open();
            return connection;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string GetSqlCeDbPath()
        {
            return System.IO.Path.Combine(Misc.GetUserDataDirectory(), "Rules", Global.RULES_DB);
        }
    }
}
