using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using System.Linq;
using woanware;

namespace snorbert.Configs
{
    /// <summary>
    /// 
    /// </summary>
    public class Sql
    {
        #region Enum
        /// <summary>
        /// 
        /// </summary>
        public enum Query
        {
            SQL_EVENTS,
            SQL_EVENTS_SEARCH,
            SQL_REFERENCES,
            SQL_SIG_NAMES,
            SQL_SIG_PRIORITIES,
            SQL_SIG_CLASS,
            SQL_SENSORS,
            SQL_RULES,
            SQL_RULES_EVENTS,
            SQL_RULES_SRC_IPS,
            SQL_RULES_DST_IPS,
            SQL_RULES_EVENTS_EXPORT,
            SQL_EXCLUDES,
            SQL_EXCLUDE,
            SQL_SENSORS_HOSTNAME,
            SQL_ACKNOWLEDGEMENT,
            SQL_ACKNOWLEDGEMENT_CLASSES
        }
        #endregion

        #region Member Variables
        private const string FOLDER_NAME = "Queries";
        public List<SqlQuery> Queries { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        public Sql()
        {
            Queries = new List<SqlQuery>();
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string Load()
        {
            try
            {
                string path = GetPath();

                if (System.IO.Directory.Exists(path) == false)
                {
                    return string.Empty;
                }

                foreach (string file in System.IO.Directory.GetFiles(path, "*.xml"))
                {
                    SqlQuery sqlQuery = new SqlQuery();
                    string ret = sqlQuery.Load(file);
                    if (ret.Length > 0)
                    {
                        return "Cannot load query: " + ret;
                    }

                    Queries.Add(sqlQuery);
                }

                return string.Empty;
            }
            catch (FileNotFoundException fileNotFoundEx)
            {
                return fileNotFoundEx.Message;
            }
            catch (UnauthorizedAccessException unauthAccessEx)
            {
                return unauthAccessEx.Message;
            }
            catch (IOException ioEx)
            {
                return ioEx.Message;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public string GetQuery(Sql.Query query)
        {
            var temp = (from q in Queries where q.Query == query select q).SingleOrDefault();
            if (temp == null)
            {
                return string.Empty;
            }

            return temp.Data;
        }
        #endregion

        #region Misc Methods
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private string GetPath()
        {
            return System.IO.Path.Combine(Misc.GetApplicationDirectory(), FOLDER_NAME);
        }
        #endregion
    }
}
