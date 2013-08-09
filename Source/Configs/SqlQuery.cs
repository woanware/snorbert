using System;
using System.IO;
using System.Xml.Serialization;

namespace snorbert.Configs
{
    /// <summary>
    /// 
    /// </summary>
    public class SqlQuery
    {
        #region Member Variables
        public Sql.Query Query { get; set; }
        public string Data { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        public SqlQuery()
        {
            Data = string.Empty;
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string Load(string path)
        {
            try
            {
                if (File.Exists(path) == false)
                {
                    return string.Empty;
                }

                XmlSerializer serializer = new XmlSerializer(typeof(SqlQuery));

                FileInfo info = new FileInfo(path);
                using (FileStream stream = info.OpenRead())
                {
                    SqlQuery sqlQuery = (SqlQuery)serializer.Deserialize(stream);
                    this.Query = sqlQuery.Query;
                    this.Data = sqlQuery.Data;
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
    }
}
