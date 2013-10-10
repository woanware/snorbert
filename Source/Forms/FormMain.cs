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
using snorbert.Configs;
using snorbert.Objects;

namespace snorbert.Forms
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

                controlRules.SetParent(this);
                controlRules.SetSql(_sql);
                controlRules.Message += Control_OnMessage;

                controlSearch.SetSql(_sql);
                controlSearch.Message += Control_OnMessage;

                controlSensors.SetSql(_sql);
                controlSensors.Message += Control_OnMessage;

                LoadConnections();

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
        private void menuFileExportAcknowledgements_Click(object sender, EventArgs e)
        {
            using (FormAcknowledgmentExport form = new FormAcknowledgmentExport(_sql))
            {
                form.ShowDialog(this);
            }
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Alt | Keys.Control | Keys.T))
            {
                controlRules.SetEventsTaken();
                return true;
            }
            else  if (keyData == (Keys.Alt | Keys.Control | Keys.F))
            {
                controlRules.SetEventsFalsePositive();
                return true;
            }
            else if (keyData == (Keys.Alt | Keys.Control | Keys.P))
            {
                if (tabMain.SelectedTab == tabPageEvents)
                {
                    controlEvents.ShowPayload();
                }
                else if (tabMain.SelectedTab == tabPageRules)
                {
                    controlRules.ShowPayload();
                }
                else if (tabMain.SelectedTab == tabPageSearch)
                {
                    controlSearch.ShowPayload();
                }
                return true;
            }
            else if (keyData == (Keys.Alt | Keys.Control | Keys.S))
            {
                if (tabMain.SelectedTab == tabPageEvents)
                {
                    controlEvents.ShowSignature();
                }
                else if (tabMain.SelectedTab == tabPageRules)
                {
                    controlRules.ShowSignature();
                }
                else if (tabMain.SelectedTab == tabPageSearch)
                {
                    controlSearch.ShowSignature();
                }
                return true;
            }
            else if (keyData == (Keys.Alt | Keys.Control | Keys.A))
            {
                if (tabMain.SelectedTab == tabPageEvents)
                {
                    controlEvents.ShowAcknowledgement();
                }
                else if (tabMain.SelectedTab == tabPageRules)
                {
                    controlRules.ShowAcknowledgement();
                }
                else if (tabMain.SelectedTab == tabPageSearch)
                {
                    controlSearch.ShowAcknowledgement();
                }
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
        #endregion 

        public void SetToFront()
        {
            this.Activate();
            UserInterface.FlashWindow(this.Handle);
        }
    }
}
