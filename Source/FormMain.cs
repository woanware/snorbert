using Be.Windows.Forms;
using BrightIdeasSoftware;
using Microsoft.Isam.Esent.Collections.Generic;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Windows.Forms;
using woanware;

namespace snorbert
{
    /// <summary>
    /// 
    /// </summary>
    public partial class FormMain : Form
    {
        #region Member Variables
        private Sql _sql;
        private Settings _settings = null;
        private Connections _connections;
        //private HourGlass _hourGlass;
        private int _pageLimit = 100;
        private PersistentDictionary<string, string> _rules;
        private RuleImporter _ruleImporter;
        #endregion

        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        public FormMain()
        {
            try
            {
                InitializeComponent();

                this.Show();

                _rules = new PersistentDictionary<string, string>(System.IO.Path.Combine(Misc.GetUserDataDirectory(), "Rules"));

                cboPageLimit.SelectedIndex = 6; // 100

                _sql = new Sql();
                string ret = _sql.Load();
                if (ret.Length > 0)
                {
                    UserInterface.DisplayErrorMessageBox(this, "An error occurred whilst loading the SQL queries, the application cannot continue: " + ret);
                    Misc.WriteToEventLog(Application.ProductName, "An error occurred whilst loading the SQL queries, the application cannot continue: " + ret, System.Diagnostics.EventLogEntryType.Error);

                    Application.Exit();
                }

                controlEvents.SetRules(_rules);
                controlEvents.SetSql(_sql);

                controlRules.SetRules(_rules);
                controlRules.SetSql(_sql);

                controlSearch.SetRules(_rules);
                controlSearch.SetSql(_sql);

                controlSensors.SetSql(_sql);


                LoadConnections();

                _ruleImporter = new RuleImporter();
                _ruleImporter.Complete += OnRuleImporter_Complete;
                _ruleImporter.Error += OnRuleImporter_Error;

                CheckImportFiles();
            }
            catch (TypeLoadException tlEx)
            {
                IO.WriteTextToFile(tlEx.ToString(), "C:\\Error.txt", true);
                //Misc.WriteToEventLog(Application.ProductName, tlEx.ToString(), System.Diagnostics.EventLogEntryType.Error);
            }
            catch (Exception ex)
            {
                IO.WriteTextToFile(ex.ToString(), "C:\\Error.txt", true);
                //Misc.WriteToEventLog(Application.ProductName, ex.ToString(), System.Diagnostics.EventLogEntryType.Error);
            }
        }
        #endregion

        #region Control Event Handlers
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        private void Control_OnMessage(string message)
        {
            UpdateStatusBar(message);
        }
        #endregion

        #region Rule Import Event Handlers
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        private void OnRuleImporter_Error(string message)
        {
            SetProcessingStatus(true);
            UpdateStatusBar(message);
        }

        /// <summary>
        /// 
        /// </summary>
        private void OnRuleImporter_Complete(PersistentDictionary<string, string> rules)
        {
            _rules = rules;
            controlEvents.SetRules(_rules);
            controlRules.SetRules(_rules);
            controlSearch.SetRules(_rules);
            SetProcessingStatus(true);
            UpdateStatusBar("Rule import complete...");
        }
        #endregion

        #region Misc Methods
        /// <summary>
        /// 
        /// </summary>
        private void CheckImportFiles()
        {
            if (Directory.Exists(System.IO.Path.Combine(Misc.GetUserDataDirectory(), "Import")) == false)
            {
                return;
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
                UpdateStatusBar("New import files identified, performing import...");

                _ruleImporter.Import(newFiles.ToArray(), _rules);
                SetProcessingStatus(false);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void LoadConnections()
        {
            using (new HourGlass(this))
            {
                _connections = new Connections();
                string ret = _connections.Load();
                if (ret.Length > 0)
                {
                    UserInterface.DisplayErrorMessageBox(this, "An error occurred whilst loading the connections: " + ret);
                    return;
                }

                cboConnections.Items.Clear();
                cboConnections.ComboBox.DisplayMember = "Name";
                cboConnections.ComboBox.ValueMember = "ConnectionString";
                cboConnections.Items.AddRange(_connections.Data.ToArray());

                if (_connections.Data.Count > 0)
                {
                    cboConnections.SelectedIndex = 0;
                    UpdateConnectionString();
                    controlEvents.SetProcessingStatus(true);
                    controlRules.SetProcessingStatus(true);
                    controlSearch.SetProcessingStatus(true);
                    controlSensors.SetProcessingStatus(true);
                }
                else
                {
                    controlEvents.SetProcessingStatus(false);
                    controlRules.SetProcessingStatus(false);
                    controlSearch.SetProcessingStatus(false);
                    controlSensors.SetProcessingStatus(false);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void UpdateConnectionString()
        {
            if (cboConnections.SelectedIndex == -1)
            {
                if (cboConnections.Items.Count > 0)
                {
                    cboConnections.SelectedIndex = 0;
                    return;
                }

                return;
            }

            Connection connection = (Connection)cboConnections.Items[cboConnections.SelectedIndex];

            // Hack to update the connection string at runtime: http://david.gardiner.net.au/2008/09/programmatically-setting.html
            var settings = ConfigurationManager.ConnectionStrings[0];
            var fi = typeof(ConfigurationElement).GetField("_bReadOnly", BindingFlags.Instance | BindingFlags.NonPublic);
            fi.SetValue(settings, false);

            settings.ConnectionString = connection.ConnectionString;
        }
        #endregion

        #region User Interface Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        private void UpdateStatusBar(string text)
        {
            MethodInvoker methodInvoker = delegate
            {
                statusLabel.Text = text;
            };

            if (this.InvokeRequired == true)
            {
                this.BeginInvoke(methodInvoker);
            }
            else
            {
                methodInvoker.Invoke();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="enabled"></param>
        private void SetProcessingStatus(bool enabled)
        {
            controlEvents.SetProcessingStatus(enabled);
            controlRules.SetProcessingStatus(enabled);
            controlSearch.SetProcessingStatus(enabled);
            controlSearch.SetProcessingStatus(enabled);
        }
        #endregion

        #region Menu Event Handlers
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuFileExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuHelpAbout_Click(object sender, EventArgs e)
        {
            using (FormAbout formAbout = new FormAbout())
            {
                formAbout.ShowDialog(this);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuHelpHelp_Click(object sender, EventArgs e)
        {
            Misc.ShellExecuteFile(System.IO.Path.Combine(Misc.GetApplicationDirectory(), "Help.pdf"));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuToolsConnections_Click(object sender, EventArgs e)
        {
            using (FormConnections formConnections = new FormConnections())
            {
                formConnections.ShowDialog(this);
                LoadConnections();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuToolsImportRules_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Rules|*.rules";
            openFileDialog.Multiselect = true;
            openFileDialog.Title = "Select the rules to import";

            if (openFileDialog.ShowDialog(this) == System.Windows.Forms.DialogResult.Cancel)
            {
                return;
            }

            // Check if any of the files exist in the Import folder, if so then flag to user, if ok then copy to import folder and call CheckXXXX
            List<string> existingFiles = new List<string>();
            foreach (string file in openFileDialog.FileNames)
            {
                string path = System.IO.Path.Combine(Misc.GetUserDataDirectory(), "Import", System.IO.Path.GetFileName(file));
                if (File.Exists(path) == false)
                {
                    continue;
                }

                existingFiles.Add(System.IO.Path.GetFileName(file));
            }

            if (existingFiles.Count > 0)
            {
                DialogResult dialogResult = MessageBox.Show(this,
                                                        "The following files already exist in the Import folder. Do you want to overwrite them? " + Environment.NewLine + Environment.NewLine +
                                                        string.Join(Environment.NewLine, existingFiles.ToArray()),
                                                        Application.ProductName,
                                                        MessageBoxButtons.YesNo,
                                                        MessageBoxIcon.Question);

                if (dialogResult == System.Windows.Forms.DialogResult.No)
                {
                    return;
                }
            }

            List<string> problemFiles = new List<string>();
            using (new HourGlass(this))
            {
                foreach (string file in openFileDialog.FileNames)
                {
                    string path = System.IO.Path.Combine(Misc.GetUserDataDirectory(), "Import", System.IO.Path.GetFileName(file));
                    string ret = woanware.IO.CopyFile(file, path, true);
                    if (ret.Length > 0)
                    {
                        problemFiles.Add(System.IO.Path.GetFileName(file));
                    }
                }
            }
            
            if (problemFiles.Count > 0)
            {
                UserInterface.DisplayErrorMessageBox(this,
                                                     "The following files had errors when being copied to the Import directory: " + Environment.NewLine + Environment.NewLine +
                                                     string.Join(Environment.NewLine, problemFiles.ToArray()));
            }

            CheckImportFiles();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuToolsFalsePositives_Click(object sender, EventArgs e)
        {
            using (FormFalsePositives formFalsePositives = new FormFalsePositives())
            {
                formFalsePositives.ShowDialog(this);
                controlRules.LoadFalsePositives();
            }
        }
        #endregion

        #region Combo Box Event Handlers
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboPageLimit_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboPageLimit.SelectedIndex == -1)
            {
                cboPageLimit.SelectedIndex = 0;
                return;
            }

            _pageLimit = int.Parse(cboPageLimit.Items[cboPageLimit.SelectedIndex].ToString());
            controlEvents.SetPageLimit(_pageLimit);
            controlRules.SetPageLimit(_pageLimit);
            controlSearch.SetPageLimit(_pageLimit);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboConnections_DropDownClosed(object sender, EventArgs e)
        {
            UpdateConnectionString();
        }
        #endregion

        #region Form Event Handlers
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormMain_Load(object sender, EventArgs e)
        {
            _settings = new Settings();
            if (_settings.FileExists == true)
            {
                string ret = _settings.Load();
                if (ret.Length > 0)
                {
                    UserInterface.DisplayErrorMessageBox(this, ret);
                }
                else
                {
                    this.WindowState = _settings.FormState;

                    if (_settings.FormState != FormWindowState.Maximized)
                    {
                        this.Location = _settings.FormLocation;
                        this.Size = _settings.FormSize;
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            _settings.FormLocation = base.Location;
            _settings.FormSize = base.Size;
            _settings.FormState = base.WindowState;
            string ret = _settings.Save();
            if (ret.Length > 0)
            {
                UserInterface.DisplayErrorMessageBox(this, ret);
            }
        }
        #endregion
    }
}
