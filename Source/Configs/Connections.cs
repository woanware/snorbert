using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;
using snorbert.Objects;
using woanware;

namespace snorbert.Configs
{
    /// <summary>
    /// 
    /// </summary>
    public class Connections
    {
        #region Member Variables
        public List<Connection> Data { get; set; }
        private const string FILENAME = "Connections.xml";
        #endregion

        /// <summary>
        /// 
        /// </summary>
        public Connections()
        {
            Data = new List<Connection>();
        }

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

                XmlSerializer serializer = new XmlSerializer(typeof(Connections));
                if (File.Exists(path) == false)
                {
                    return "Cannot locate connections file: " + path;
                }

                FileInfo info = new FileInfo(path);
                using (FileStream stream = info.OpenRead())
                {
                    Connections connections = (Connections)serializer.Deserialize(stream);

                    Data = connections.Data;

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
                XmlSerializer serializer = new XmlSerializer(typeof(Connections));
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
        /// <returns></returns>
        private string GetPath()
        {
            return System.IO.Path.Combine(Misc.GetUserDataDirectory(), FILENAME);
        }
    }
}
