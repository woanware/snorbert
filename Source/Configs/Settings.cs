using System;
using System.Collections.Generic;
using System.Drawing;
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
    public class Settings
    {
        #region Member Variables
        public Point FormLocation { get; set; }
        public Size FormSize { get; set; }
        public FormWindowState FormState { get; set; }
        public int EventsRefresh { get; set; } // Minutes
        private const string FILENAME = "Settings.xml";
        #endregion

        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        public Settings()
        {
            EventsRefresh = 5;
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

                XmlSerializer serializer = new XmlSerializer(typeof(Settings));

                FileInfo info = new FileInfo(path);
                using (FileStream stream = info.OpenRead())
                {
                    Settings settings = (Settings)serializer.Deserialize(stream);

                    FormLocation = settings.FormLocation;
                    FormSize = settings.FormSize;
                    FormState = settings.FormState;
                    EventsRefresh = settings.EventsRefresh;
                   
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
                string path = GetPath();

                if (System.IO.Directory.Exists(Misc.GetUserDataDirectory()) == false)
                {
                    System.IO.Directory.CreateDirectory(Misc.GetUserDataDirectory());
                }

                XmlSerializer serializer = new XmlSerializer(typeof(Settings));
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
        public bool FileExists
        {
            get
            {
                //string path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), @"woanware\" + Application.ProductName + @"\");
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
            return System.IO.Path.Combine(Misc.GetUserDataDirectory(), FILENAME);
        }
        #endregion
    }
}
