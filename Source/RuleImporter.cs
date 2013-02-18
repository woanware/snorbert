using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Linq;
using woanware;
using System.Data.SqlServerCe;
using System.Data.Common;
using System.Collections.Generic;
using NPoco;
using System.Windows.Forms;

namespace snorbert
{
    /// <summary>
    /// 
    /// </summary>
    internal class RuleImporter
    {
        #region SQL_TABLE_RULES
        private const string SQL_TABLE_RULES = @"CREATE TABLE [Rules] (
  [Id] bigint NOT NULL IDENTITY (1,1)
, [Sid] nvarchar(25)
, [Rule] ntext
);";
        #endregion

        #region SQL_TABLE_RULES_PK
        private const string SQL_TABLE_RULES_PK = @"ALTER TABLE [Rules] ADD CONSTRAINT [Id_Rules] PRIMARY KEY ([Id]);";
        #endregion

        #region SQL_ADD_INDEX
        private const string SQL_ADD_INDEX = @"CREATE NONCLUSTERED INDEX [IX_Sid] ON [Rules]
(
	[Sid] ASC
)";
        #endregion

        #region SQL_DROP_INDEX
        private const string SQL_DROP_INDEX = @"DROP INDEX [Rules].[IX_Sid]";
        #endregion

        #region Events
        public event Global.DefaultEvent Complete;
        public event Global.MessageEvent Error;
        public event Global.MessageEvent Message;
        #endregion

        #region Member Variables
        private readonly object _lock = new object();
        private bool _isRunning;
        private Thread _thread;
        private System.Threading.Mutex _mutex = null;
        private Settings _settings = null;
        #endregion

        #region Constants
        private readonly string MUTEX = "snorbert";
        #endregion

        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        /// <param name="settings"></param>
        public RuleImporter(Settings settings)
        {
            _settings = settings;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// 
        /// </summary>
        public void Check()
        {
            if (_isRunning == true)
            {
                return;
            }

            _thread = new Thread(() => PerformImport());
            _thread.IsBackground = true;
            _thread.Start();
        }
        #endregion

        #region Import Methods
        /// <summary>
        /// 
        /// </summary>
        private void PerformImport()
        {
            try
            {
                using (Mutex mutex = new Mutex(false, "Global\\" + MUTEX))
                {
                    if (!mutex.WaitOne(0, false))
                    {
                        UserInterface.DisplayMessageBox("MUTEX", System.Windows.Forms.MessageBoxIcon.Information);
                        return;
                    }

                    _isRunning = true;

                    if (File.Exists(Db.GetSqlCeDbPath()) == false)
                    {
                        string ret = CreateDatabase();
                        if (ret.Length > 0)
                        {
                            OnError("An error occurred whilst creating the rule database: " + ret);
                            return;
                        }
                    }

                    List<string> files = GetChangedFiles();
                    if (files == null)
                    {
                        return;
                    }

                    DropIndex();

                    NPoco.Database db = new NPoco.Database(Db.GetOpenSqlCeConnection(), DatabaseType.SQLCe);

                    Regex regex = new Regex(@"sid\:(.*?);", RegexOptions.IgnoreCase);
                    foreach (string file in files)
                    {
                        foreach (var line in File.ReadAllLines(file))
                        {
                            Match match = regex.Match(line);
                            if (match.Success == false)
                            {
                                continue;
                            }

                            Rule rule = db.SingleOrDefault<Rule>("SELECT * FROM Rules WHERE Sid = @0", new object[] { match.Groups[1].Value.Trim() });
                            if (rule != null)
                            {
                                rule.Data = line;
                                db.Update(rule);
                            }
                            else
                            {
                                rule = new Rule();
                                rule.Sid = match.Groups[1].Value.Trim();
                                rule.Data = line;
                                db.Insert(rule);
                            }
                        }
                    }

                    AddIndex();
                    OnComplete();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                OnError("An error occurred whilst importing the rules: " + ex.Message);
            }
            finally
            {
                _isRunning = false;
                lock (_lock)
                {
                    if (_mutex != null)
                    {
                        _mutex.ReleaseMutex();
                    } 
                } 
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private List<string> GetChangedFiles()
        {
            if (Directory.Exists(System.IO.Path.Combine(Misc.GetUserDataDirectory(), "Import")) == false)
            {
                return null;
            }

            List<string> newFiles = new List<string>();
            DirectoryInfo directoryInfo = new DirectoryInfo(System.IO.Path.Combine(Misc.GetUserDataDirectory(), "Import"));
            FileInfo[] fileInfos = directoryInfo.GetFiles("*.rules");
            foreach (FileInfo fileInfo in fileInfos)
            {
                var file = from f in _settings.RuleFiles where f.FileName == fileInfo.Name & f.ModifiedTimestamp >= fileInfo.LastWriteTime select f;
                if (file.Any() == true)
                {
                    continue;
                }

                newFiles.Add(fileInfo.FullName);

                // Remove any existing RuleFiles from Settings, so we don't end up with multiple entries of the same file name
                _settings.RuleFiles.RemoveAll(rf => rf.FileName == fileInfo.Name);

                // Now add a new RuleFile that contains the appropriate info
                RuleFile ruleFile = new RuleFile();
                ruleFile.FileName = fileInfo.Name;
                ruleFile.ModifiedTimestamp = fileInfo.LastWriteTime;
                _settings.RuleFiles.Add(ruleFile);
            }

            if (newFiles.Any() == true)
            {
                _settings.Save();
                OnMessage("New import files identified, performing import...");
            }

            return newFiles; 
        }
        #endregion

        #region Database Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private string CreateDatabase()
        {
            try
            {
                SqlCeEngine sqlCeEngine = new SqlCeEngine(Db.GetSqlCeConnectionString());
                sqlCeEngine.CreateDatabase();

                using (SqlCeConnection connection = new SqlCeConnection(Db.GetSqlCeConnectionString()))
                {
                    connection.Open();

                    using (SqlCeCommand command = new SqlCeCommand(SQL_TABLE_RULES, connection))
                    {
                        command.ExecuteNonQuery();
                    }

                    using (SqlCeCommand command = new SqlCeCommand(SQL_TABLE_RULES_PK, connection))
                    {
                        command.ExecuteNonQuery();
                    }

                    using (SqlCeCommand command = new SqlCeCommand(SQL_ADD_INDEX, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }

                return string.Empty;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void AddIndex()
        {
            try
            {
                SqlCeEngine sqlCeEngine = new SqlCeEngine(Db.GetSqlCeConnectionString());
                using (SqlCeConnection connection = new SqlCeConnection(Db.GetSqlCeConnectionString()))
                {
                    connection.Open();

                    using (SqlCeCommand command = new SqlCeCommand(SQL_ADD_INDEX, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void DropIndex()
        {
            try
            {
                SqlCeEngine sqlCeEngine = new SqlCeEngine(Db.GetSqlCeConnectionString());
                using (SqlCeConnection connection = new SqlCeConnection(Db.GetSqlCeConnectionString()))
                {
                    connection.Open();

                    using (SqlCeCommand command = new SqlCeCommand(SQL_DROP_INDEX, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            { 
            }
        }
        #endregion

        #region Event Methods
        /// <summary>
        /// 
        /// </summary>
        private void OnComplete()
        {
            var handler = Complete;
            if (handler != null)
            {
                handler();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        private void OnError(string message)
        {
            var handler = Error;
            if (handler != null)
            {
                handler(message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        private void OnMessage(string message)
        {
            var handler = Message;
            if (handler != null)
            {
                handler(message);
            }
        }
        #endregion
    }
}
