using Be.Windows.Forms;
using BrightIdeasSoftware;
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
        private HourGlass _hourGlass;
        private int _pageLimit = 100;
        private RuleImporter _ruleImporter;
        private Exporter _exporter;
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

                cboPageLimit.SelectedIndex = 6; // 100

                _sql = new Sql();
                string ret = _sql.Load();
                if (ret.Length > 0)
                {
                    UserInterface.DisplayErrorMessageBox(this, "An error occurred whilst loading the SQL queries, the application cannot continue: " + ret);
                    Misc.WriteToEventLog(Application.ProductName, "An error occurred whilst loading the SQL queries, the application cannot continue: " + ret, System.Diagnostics.EventLogEntryType.Error);

                    Application.Exit();
                }

                controlEvents.SetSql(_sql);
                controlEvents.Message += Control_OnMessage;

                controlRules.SetSql(_sql);
                controlRules.Message += Control_OnMessage;

                controlSearch.SetSql(_sql);
                controlSearch.Message += Control_OnMessage;

                controlSensors.SetSql(_sql);
                controlSensors.Message += Control_OnMessage;

                LoadConnections();

                _ruleImporter = new RuleImporter(_settings);
                _ruleImporter.Complete += OnRuleImporter_Complete;
                _ruleImporter.Error += OnRuleImporter_Error;
                _ruleImporter.Message += OnRuleImporter_Message;
                _ruleImporter.Check();

                _exporter = new Exporter();
                _exporter.SetSql(_sql);
                _exporter.Complete += OnExporter_Complete;
                _exporter.Error += OnExporter_Error;
                _exporter.Exclamation += OnExporter_Exclamation;
            }
            catch (Exception ex)
            {
                Misc.WriteToEventLog(Application.ProductName, ex.ToString(), System.Diagnostics.EventLogEntryType.Error);
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
        /// <param name="message"></param>
        private void OnRuleImporter_Message(string message)
        {
            UpdateStatusBar(message);
        }

        /// <summary>
        /// 
        /// </summary>
        private void OnRuleImporter_Complete()
        {
            SetProcessingStatus(true);
            UpdateStatusBar("Rule import complete...");
        }
        #endregion

        #region Exporter Event Handlers
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        private void OnExporter_Exclamation(string message)
        {
            _hourGlass.Dispose();
            UserInterface.DisplayMessageBox(this, message, MessageBoxIcon.Exclamation);
            SetProcessingStatus(true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        private void OnExporter_Error(string message)
        {
            _hourGlass.Dispose();
            UserInterface.DisplayErrorMessageBox(this, message);
            SetProcessingStatus(true);
        }

        /// <summary>
        /// 
        /// </summary>
        private void OnExporter_Complete()
        {
            _hourGlass.Dispose();
            UserInterface.DisplayMessageBox(this, "Export complete", MessageBoxIcon.Information);
            SetProcessingStatus(true);
        }
        #endregion

        #region Misc Methods
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

            controlEvents.SetConnection(connection);
            controlRules.SetConnection(connection);
            controlSearch.SetConnection(connection);
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

            _ruleImporter.Check();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuToolsExcludeConfiguration_Click(object sender, EventArgs e)
        {
            using (FormExcludes formExcludes = new FormExcludes(_sql))
            {
                formExcludes.ShowDialog(this);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuToolsExcludesExport_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "Select the export CSV";
            saveFileDialog.Filter = "TSV Files|*.tsv";
            if (saveFileDialog.ShowDialog(this) == DialogResult.Cancel)
            {
                return;
            }

            _hourGlass = new HourGlass(this);
            SetProcessingStatus(false);

            _exporter.ExportExcludes(saveFileDialog.FileName);
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
