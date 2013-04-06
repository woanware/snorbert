using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using System.Linq;
using woanware;

namespace snorbert
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
            SQL_EVENTS = 0,
            SQL_RULES_FROM = 1,
            SQL_RULES_FROM_TO = 2,
            SQL_RULES_FROM_PRIORITY = 3,
            SQL_RULES_FROM_TO_PRIORITY = 4,
            SQL_EVENTS_RULES_FROM = 5,
            SQL_EVENTS_RULES_FROM_TO = 6,
            SQL_EVENTS_SEARCH = 7,
            SQL_REFERENCES = 8,
            SQL_SIG_NAMES = 9,
            SQL_SIG_PRIORITIES = 10,
            SQL_SIG_CLASS = 11,
            SQL_SENSORS = 12,
            SQL_RULES_SRC_IPS_FROM = 13,
            SQL_RULES_SRC_IPS_FROM_TO = 14,
            SQL_RULES_DST_IPS_FROM = 15,
            SQL_RULES_DST_IPS_FROM_TO = 16,
            SQL_EVENTS_RULES_FROM_EXPORT = 17,
            SQL_EVENTS_RULES_FROM_TO_EXPORT = 17,
            SQL_EXCLUDES = 18,
            SQL_EXCLUDE = 19,
            SQL_SENSORS_HOSTNAME = 20,
            SQL_RULES_FROM_ALL = 21,
            SQL_RULES_FROM_TO_ALL = 22,
            SQL_RULES_FROM_PRIORITY_ALL = 23,
            SQL_RULES_FROM_TO_PRIORITY_ALL = 24
        }
        #endregion

        #region Member Variables
        private const string FILENAME = "Sql.xml";
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

                if (File.Exists(path) == false)
                {
                    return string.Empty;
                }

                XmlSerializer serializer = new XmlSerializer(typeof(Sql));
                if (File.Exists(path) == false)
                {
                    return "Cannot locate settings file: " + path;
                }

                FileInfo info = new FileInfo(path);
                using (FileStream stream = info.OpenRead())
                {
                    Sql sql = (Sql)serializer.Deserialize(stream);
                    Queries = sql.Queries;
                    return string.Empty;
                }
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
        /// <returns></returns>
        public string Save()
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Sql));
                using (StreamWriter writer = new StreamWriter(GetPath(), false))
                {
                    serializer.Serialize((TextWriter)writer, this);
                    return string.Empty;
                }
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

        /// <summary>
        /// 
        /// </summary>
        public bool FileExists
        {
            get
            {
                return File.Exists(GetPath());
            }
        }
        #endregion

        #region Misc Methods
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private string GetPath()
        {
            return System.IO.Path.Combine(Misc.GetApplicationDirectory(), FILENAME);
        }
        #endregion
    }
}
