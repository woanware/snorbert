using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;
using woanware;

namespace snorbert
{
    /// <summary>
    /// 
    /// </summary>
    public class FalsePositives
    {
        #region Member Variables
        public List<FalsePositive> Data { get; set; }
        private const string FILENAME = "FalsePositives.xml";
        #endregion

        /// <summary>
        /// 
        /// </summary>
        public FalsePositives()
        {
            Data = new List<FalsePositive>();
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

                XmlSerializer serializer = new XmlSerializer(typeof(FalsePositives));
                if (File.Exists(path) == false)
                {
                    return "Cannot locate false positives file: " + path;
                }

                FileInfo info = new FileInfo(path);
                using (FileStream stream = info.OpenRead())
                {
                    FalsePositives falsePositives = (FalsePositives)serializer.Deserialize(stream);

                    Data = falsePositives.Data;

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
                XmlSerializer serializer = new XmlSerializer(typeof(FalsePositives));
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
