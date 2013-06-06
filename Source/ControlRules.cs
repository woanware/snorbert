using BrightIdeasSoftware;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using woanware;
using System.ComponentModel;
using System.Drawing;
using System.Threading;

namespace snorbert
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ControlRules : UserControl
    {
        #region Events
        public event Global.MessageEvent Message;
        #endregion

        #region Member Variables
        private string _initials = string.Empty;
        private int _pageLimit = 100;
        private long _currentPage = 1;
        private long _totalPages = 0;
        private long _totalRecords = 0;
        private Querier _querier;
        private Exporter _exporter;
        private HourGlass _hourGlass;
        private Sql _sql;
        private Connection _connection;
        private Commands _commands;
        private List<AcknowledgmentClass> _acknowledgmentClasses;
        #endregion

        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        public ControlRules()
        {
            InitializeComponent();

            Helper.AddListColumn(listEvents, "CID", "Cid");
            Helper.AddListColumn(listEvents, "Src IP", "IpSrcTxt");
            Helper.AddListColumn(listEvents, "Src Port", "SrcPort");
            Helper.AddListColumn(listEvents, "Dst IP", "IpDstTxt");
            Helper.AddListColumn(listEvents, "Dst Port", "DstPort");
            Helper.AddListColumn(listEvents, "Protocol", "Protocol");
            Helper.AddListColumn(listEvents, "Host", "HttpHost");
            Helper.AddListColumn(listEvents, "Timestamp", "Timestamp");
            Helper.AddListColumn(listEvents, "TCP Flags", "TcpFlagsString");
            Helper.AddListColumn(listEvents, "Classification", "AcknowledgmentClass");
            Helper.AddListColumn(listEvents, "Initials", "Initials");
            Helper.AddListColumn(listEvents, "Payload (ASCII)", "PayloadAscii");
            Helper.ResizeEventListColumns(listEvents);

            listEvents.RowFormatter = delegate(OLVListItem olvi)
            {
                Event temp = (Event)olvi.RowObject;
                if (temp != null)
                {
                    if (temp.AcknowledgmentClass.ToLower() == "taken")
                    {
                        olvi.BackColor = Color.FromArgb(255, 246, 127); // Yellow
                    }
                    else if (temp.Initials.Length > 0)
                    {
                        olvi.BackColor = Color.FromArgb(176, 255, 119); // Green
                    }
                }
            };

            cboTimeFrom.SelectedIndex = 0;
            cboTimeTo.SelectedIndex = 0;
            dtpDateTo.Checked = false;

            _querier = new Querier();
            _querier.Error += OnQuerier_Error;
            _querier.Exclamation += OnQuerier_Exclamation;
            _querier.RuleQueryComplete += OnQuerier_RuleQueryComplete;
            _querier.EventQueryComplete += OnQuerier_EventQueryComplete;
            _querier.RuleIpQueryComplete += OnQuerier_RuleIpQueryComplete;

            _exporter = new Exporter();
            _exporter.Complete += OnExporter_Complete;
            _exporter.Error += OnExporter_Error;
            _exporter.Exclamation += OnExporter_Exclamation;

            _commands = new Commands();
            string ret = _commands.Load();
            if (ret.Length > 0)
            {
                UserInterface.DisplayErrorMessageBox(this, "An error occurred whilst loading the commands: " + ret);
            }

            LoadPriorities();
        }
        #endregion

        #region Querier Event Handlers
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        private void OnQuerier_RuleQueryComplete(List<Signature> data)
        {
            MethodInvoker methodInvoker = delegate
            {
                try
                {
                    if (data == null)
                    {
                        UserInterface.DisplayMessageBox(this, "No data retrieved for query", MessageBoxIcon.Exclamation);
                        return;
                    }

                    cboRule.Items.Clear();
                    cboRule.DisplayMember = "Text";
                    cboRule.ValueMember = "Sid";
                    cboRule.Items.AddRange(data.ToArray());

                    if (data.Count == 0)
                    {
                        UserInterface.DisplayMessageBox(this, "No data retrieved for query", MessageBoxIcon.Exclamation);
                    }
                    else
                    {
                        OnMessage("Loaded " + cboRule.Items.Count + " rules");
                    }
                }
                catch (Exception ex)
                {
                    UserInterface.DisplayErrorMessageBox(this, "An error occurred whilst performing the search: " + ex.Message);
                }
                finally
                {
                    Helper.ResizeEventListColumns(listEvents);
                    SetPagingControlState();
                    SetProcessingStatus(true);
                    _hourGlass.Dispose();
                }
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
        /// <param name="data"></param>
        private void OnQuerier_EventQueryComplete(List<Event> data)
        {
            MethodInvoker methodInvoker = delegate
            {
                try
                {
                    if (data == null)
                    {
                        UserInterface.DisplayMessageBox(this, "No data retrieved for query", MessageBoxIcon.Exclamation);
                        return;
                    }

                    Signature rule = (Signature)cboRule.Items[cboRule.SelectedIndex];

                    _totalRecords = rule.Count;

                    // Sometimes there will be more events for a rule, than there originally was 
                    // when the rule list was generated. But since we don't want to requery the 
                    // database to get a more accurate count, then we will just the the count of
                    // the data returned. Though this only works when the amount of data returned 
                    // is less than the Page Size
                    if (rule.Count < data.Count)
                    {
                        _totalRecords = data.Count;
                    }

                    _totalPages = _totalRecords / _pageLimit;
                    if (_totalRecords % _pageLimit != 0)
                    {
                        _totalPages++;
                    }

                    listEvents.SetObjects(data);

                    if (data.Any() == true)
                    {
                        listEvents.SelectedObject = data[0];
                        listEvents_SelectedIndexChanged(this, null);
                    }

                    lblPagingRules.Text = "Page " + _currentPage + " (" + _totalPages + ")";
                    OnMessage("Loaded " + data.Count + " results (Total: " + _totalRecords + ")");
                }
                catch (Exception ex)
                {
                    UserInterface.DisplayErrorMessageBox(this, "An error occurred whilst performing the search: " + ex.Message);
                }
                finally
                {
                    Helper.ResizeEventListColumns(listEvents);
                    SetPagingControlState();
                    SetProcessingStatus(true);
                    _hourGlass.Dispose();
                    listEvents.Select();
                }
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
        /// <param name="data"></param>
        private void OnQuerier_RuleIpQueryComplete(List<string> data)
        {
            MethodInvoker methodInvoker = delegate
            {
                try
                {
                    if (data == null)
                    {
                        UserInterface.DisplayMessageBox(this, "No data retrieved for query", MessageBoxIcon.Exclamation);
                        return;
                    }

                    StringBuilder output = new StringBuilder();
                    foreach (string ip in data)
                    {
                        output.AppendLine(ip);
                    }

                    if (data.Count > 0)
                    {
                        Clipboard.SetText(output.ToString());
                        OnMessage(data.Count + " IP addresses copied to the clipboard");
                    }
                    else
                    {
                        UserInterface.DisplayMessageBox(this, "No data retrieved for query", MessageBoxIcon.Exclamation);
                    }
                }
                catch (Exception ex)
                {
                    UserInterface.DisplayErrorMessageBox(this, "An error occurred whilst performing the search: " + ex.Message);
                }
                finally
                {
                    Helper.ResizeEventListColumns(listEvents);
                    SetPagingControlState();
                    SetProcessingStatus(true);
                    _hourGlass.Dispose();
                }
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
        /// <param name="message"></param>
        private void OnQuerier_Exclamation(string message)
        {
            _hourGlass.Dispose();
            UserInterface.DisplayMessageBox(this, message, MessageBoxIcon.Exclamation);
            SetProcessingStatus(true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        private void OnQuerier_Error(string message)
        {
            _hourGlass.Dispose();
            UserInterface.DisplayErrorMessageBox(this, message);
            SetProcessingStatus(true);
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

        #region Query Methods
        /// <summary>
        /// 
        /// </summary>
        private void LoadRules()
        {
            _hourGlass = new HourGlass(this);
            SetProcessingStatus(false);
            Clear();

            NameValue priority = (NameValue)cboPriority.Items[cboPriority.SelectedIndex];
            NameValue sensor = (NameValue)cboSensor.Items[cboSensor.SelectedIndex];

            if (dtpDateTo.Checked == true)
            {
                if (priority.Name.ToLower() == "all")
                {
                    _querier.QueryRulesToFrom(dtpDateFrom.Value.Date.ToString("yyyy-MM-dd") + " " + cboTimeFrom.Text + ":00",
                                              dtpDateTo.Value.Date.ToString("yyyy-MM-dd") + " " + cboTimeTo.Text + ":00",
                                              sensor.Value);
                }
                else
                {
                    _querier.QueryRulesToFromPriority(dtpDateFrom.Value.Date.ToString("yyyy-MM-dd") + " " + cboTimeFrom.Text + ":00",
                                                      dtpDateTo.Value.Date.ToString("yyyy-MM-dd") + " " + cboTimeTo.Text + ":00",
                                                      priority.Value,
                                                      sensor.Value);
                }
            }
            else
            {
                if (priority.Name.ToLower() == "all")
                {
                    _querier.QueryRulesFrom(dtpDateFrom.Value.Date.ToString("yyyy-MM-dd") + " " + cboTimeFrom.Text + ":00",
                                            sensor.Value);
                }
                else
                {
                    _querier.QueryRulesFromPriority(dtpDateFrom.Value.Date.ToString("yyyy-MM-dd") + " " + cboTimeFrom.Text + ":00",
                                                    priority.Value,
                                                    sensor.Value);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        private void LoadRuleEvents(long page)
        {
            MethodInvoker methodInvoker = delegate
            {
                _currentPage = page;
                _hourGlass = new HourGlass(this);
                SetProcessingStatus(false);
                Clear();

                Signature rule = (Signature)cboRule.Items[cboRule.SelectedIndex];

                if (dtpDateTo.Checked == true)
                {
                    _querier.QueryEventsRulesToFrom(dtpDateFrom.Value.Date.ToString("yyyy-MM-dd") + " " + cboTimeFrom.Text + ":00",
                                                    dtpDateTo.Value.Date.ToString("yyyy-MM-dd") + " " + cboTimeTo.Text + ":00",
                                                    rule.Id.ToString(),
                                                    (_currentPage - 1) * _pageLimit,
                                                    _pageLimit);
                }
                else
                {
                    _querier.QueryEventsRulesFrom(dtpDateFrom.Value.Date.ToString("yyyy-MM-dd") + " " + cboTimeFrom.Text + ":00",
                                                  rule.Id.ToString(),
                                                  (_currentPage - 1) * _pageLimit,
                                                  _pageLimit);
                }
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
        #endregion

        #region User Interface Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="enabled"></param>
        public void SetProcessingStatus(bool enabled)
        {
            MethodInvoker methodInvoker = delegate
            {
                this.Enabled = enabled;
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
        private void SetPagingControlState()
        {
            if (_totalPages == 0)
            {
                btnPagingFirstPage.Enabled = false;
                btnPagingLastPage.Enabled = false;
                btnPagingNextPage.Enabled = false;
                btnPagingPreviousPage.Enabled = false;
            }
            else
            {
                btnPagingFirstPage.Enabled = (_currentPage != 1);
                btnPagingLastPage.Enabled = (_currentPage < _totalPages);
                btnPagingNextPage.Enabled = (_currentPage < _totalPages);
                btnPagingPreviousPage.Enabled = (_currentPage != 1);
            }
        }

        /// <summary>
        /// Resizes the event list's columns
        /// </summary>
        private void ResizeEventListColumns(FastObjectListView list)
        {
            if (list.Items.Count == 0)
            {
                foreach (ColumnHeader column in list.Columns)
                {
                    column.AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize);
                }
            }
            else
            {
                list.Columns[(int)Global.FieldsEvent.Cid].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
                list.Columns[(int)Global.FieldsEvent.SrcIp].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
                list.Columns[(int)Global.FieldsEvent.DstIp].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
                list.Columns[(int)Global.FieldsEvent.Timestamp].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void Clear()
        {
            listEvents.ClearObjects();
            controlEventInfo.ClearControls();
            OnMessage(string.Empty);
        }
        #endregion

        #region Button Event Handlers
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadRules();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPagingFirstPage_Click(object sender, EventArgs e)
        {
            LoadRuleEvents(1);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPagingPreviousPage_Click(object sender, EventArgs e)
        {
            LoadRuleEvents(_currentPage - 1);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPagingNextPage_Click(object sender, EventArgs e)
        {
            LoadRuleEvents(_currentPage + 1); 
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPagingLastPage_Click(object sender, EventArgs e)
        {
            LoadRuleEvents(_totalPages);
        }
        #endregion

        #region Combobox Event Handlers
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboRule_DropDownClosed(object sender, EventArgs e)
        {
            if (cboRule.SelectedIndex == -1)
            {
                return;
            }

            LoadRuleEvents(1);
        }
        #endregion

        #region Date/Time Picker Event Handlers
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dtpDateTo_ValueChanged(object sender, EventArgs e)
        {
            cboTimeTo.Enabled = dtpDateTo.Checked;
        }
        #endregion

        #region Listview Event Handlers
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listEvents_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listEvents.SelectedObjects.Count != 1)
            {
                controlEventInfo.ClearControls();
                return;
            }

            Event temp = (Event)listEvents.SelectedObjects[0];
            controlEventInfo.DisplaySelectedEventDetails(temp);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listEvents_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (listEvents.SelectedObjects.Count != 1)
            {
                return;
            }

            Event temp = (Event)listEvents.SelectedObjects[0];
            if (temp == null)
            {
                UserInterface.DisplayErrorMessageBox(this, "Unable to locate event");
                return;
            }

            using (FormPayload formPayload = new FormPayload(temp))
            {
                formPayload.ShowDialog(this);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listEvents_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ctxMenuPayload_Click(this, new EventArgs());
            }
            else if (e.KeyCode == Keys.F1)
            {
                if (_initials.Length == 0)
                {
                    UserInterface.DisplayMessageBox(this, 
                                                    "The user initials have not been set. Manually classify event to set", 
                                                    MessageBoxIcon.Information);
                    return;
                }

                var events = listEvents.Objects.Cast<Event>().ToList();

                (new Thread(() =>
                {
                    SetProcessingStatus(false);

                    bool acknowledgedPrevious = false;
                    using (new HourGlass(this))
                    {
                        NPoco.Database db = new NPoco.Database(Db.GetOpenMySqlConnection());
                        foreach (Event temp in events)
                        {
                            bool insert = true;
                            var ack = db.Fetch<Acknowledgment>("select * from acknowledgment where cid=@0 and sid=@1", new object[] { temp.Cid, temp.Sid });
                            if (ack.Count() > 0)
                            {
                                if (ack.First().Initials != _initials)
                                {
                                    acknowledgedPrevious = true;
                                    insert = false;
                                }
                                else
                                {
                                    db.Delete(ack.First());
                                }
                            }

                            if (insert == true)
                            {
                                Acknowledgment acknowledgment = new Acknowledgment();
                                acknowledgment.Cid = temp.Cid;
                                acknowledgment.Sid = temp.Sid;
                                acknowledgment.Initials = _initials;
                                acknowledgment.Class = 1;

                                db.Insert(acknowledgment);
                            }
                        }
                    }

                    if (acknowledgedPrevious == true)
                    {
                        UserInterface.DisplayMessageBox(this,
                                                        "Some events were not classified due to being already classified",
                                                        MessageBoxIcon.Exclamation);
                    }

                    SetProcessingStatus(true);
                    LoadRuleEvents(_currentPage);
                })).Start();
            }
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageLimit"></param>
        public void SetPageLimit(int pageLimit)
        {
            _pageLimit = pageLimit;

            _currentPage = 1;
            Clear();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        public void SetSql(Sql sql)
        {
            _sql = sql;
            _querier.SetSql(_sql);
            _exporter.SetSql(_sql);
            controlEventInfo.SetSql(_sql);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connection"></param>
        public void SetConnection(Connection connection)
        {
            _connection = connection;
            UpdateSensors();

            NPoco.Database db = new NPoco.Database(Db.GetOpenMySqlConnection());
            _acknowledgmentClasses = db.Fetch<AcknowledgmentClass>();
            _acknowledgmentClasses = (from a in _acknowledgmentClasses orderby a.Desc select a).ToList();
        }
        #endregion

        #region Event Methods
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

        #region Misc Methods
        /// <summary>
        /// 
        /// </summary>
        public void LoadPriorities()
        {
            if (LicenseManager.UsageMode == LicenseUsageMode.Runtime)
            {
                string[] priorities = System.IO.File.ReadAllLines(System.IO.Path.Combine(Misc.GetApplicationDirectory(), Global.PRIORITIES_FILE));

                List<NameValue> temp = new List<NameValue>();

                NameValue nameValue = new NameValue();
                nameValue.Name = "All";
                nameValue.Value = "All";
                temp.Add(nameValue);

                foreach (string priority in priorities)
                {
                    nameValue = new NameValue();
                    nameValue.Name = priority;
                    nameValue.Value = priority;
                    temp.Add(nameValue);
                }

                cboPriority.Items.Clear();
                cboPriority.DisplayMember = "Name";
                cboPriority.ValueMember = "Value";
                cboPriority.Items.AddRange(temp.ToArray());
                UserInterface.SetDropDownWidth(cboPriority);

                cboPriority.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void UpdateSensors()
        {
            NPoco.Database db = new NPoco.Database(Db.GetOpenMySqlConnection());
            List<Sensor> data = db.Fetch<Sensor>(_sql.GetQuery(Sql.Query.SQL_SENSORS_HOSTNAME));

            List<NameValue> sensors = new List<NameValue>();

            // Add a default 
            NameValue nameValue = new NameValue();
            nameValue.Name = "All";
            nameValue.Value = string.Empty;
            sensors.Add(nameValue);

            foreach (var result in data)
            {
                nameValue = new NameValue();
                nameValue.Name = result.HostName;
                nameValue.Value = result.HostName;
                sensors.Add(nameValue);
            }

            cboSensor.Items.Clear();
            cboSensor.DisplayMember = "Name";
            cboSensor.ValueMember = "Value";
            cboSensor.Items.AddRange(sensors.ToArray());
            UserInterface.SetDropDownWidth(cboSensor);

            if (sensors.Count > 0)
            {
                cboSensor.SelectedIndex = 0;
            }
        }
        #endregion

        #region Control Event Handlers
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Control_Load(object sender, EventArgs e)
        {
            Helper.ResizeEventListColumns(listEvents);

            foreach (NameValue command in _commands.Data)
            {
                ToolStripMenuItem newCommand = new ToolStripMenuItem();
                newCommand.Text = command.Name;
                newCommand.Tag = command.Value;
                newCommand.Click += ctxMenuCommand_Click;
                ctxMenuCommands.DropDownItems.Add(newCommand);
            }
        }
        #endregion

        #region Context Menu Event Handlers
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ctxMenuEvent_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ctxMenuExport.Enabled = true;
            ctxMenuExtractIpInfo.Enabled = true;

            if (listEvents.SelectedObjects.Count == 1)
            {
                ctxMenuCopy.Enabled = true;
                ctxMenuPayload.Enabled = true;
                ctxMenuExclude.Enabled = true;
                ctxMenuNwQuery.Enabled = true;
            }
            else
            {
                ctxMenuCopy.Enabled = false;
                ctxMenuPayload.Enabled = false;
                ctxMenuExclude.Enabled = false;
                ctxMenuNwQuery.Enabled = false;
            }

            if (cboRule.Items.Count == 0)
            {
                ctxMenuExportRules.Enabled = false;
            }
            else
            {
                ctxMenuExportRules.Enabled = true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ctxMenuCopySourceIp_Click(object sender, EventArgs e)
        {
            Helper.CopyDataToClipboard(this, listEvents, Global.FieldsEventCopy.SrcIp);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ctxMenuCopySourcePort_Click(object sender, EventArgs e)
        {
            Helper.CopyDataToClipboard(this, listEvents, Global.FieldsEventCopy.SrcPort);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ctxMenuCopyDestIp_Click(object sender, EventArgs e)
        {
            Helper.CopyDataToClipboard(this, listEvents, Global.FieldsEventCopy.DstIp);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ctxMenuCopyDestPort_Click(object sender, EventArgs e)
        {
            Helper.CopyDataToClipboard(this, listEvents, Global.FieldsEventCopy.DstPort);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ctxMenuCopyCid_Click(object sender, EventArgs e)
        {
            Helper.CopyDataToClipboard(this, listEvents, Global.FieldsEventCopy.Cid);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ctxMenuCopySid_Click(object sender, EventArgs e)
        {
            Helper.CopyDataToClipboard(this, listEvents, Global.FieldsEventCopy.Sid);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ctxMenuCopySigName_Click(object sender, EventArgs e)
        {
            Helper.CopyDataToClipboard(this, listEvents, Global.FieldsEventCopy.SigName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ctxMenuCopyHost_Click(object sender, EventArgs e)
        {
            Helper.CopyDataToClipboard(this, listEvents, Global.FieldsEventCopy.HttpHost);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ctxMenuCopyTimestamp_Click(object sender, EventArgs e)
        {
            Helper.CopyDataToClipboard(this, listEvents, Global.FieldsEventCopy.Timestamp);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ctxMenuCopyPayloadAscii_Click(object sender, EventArgs e)
        {
            Helper.CopyDataToClipboard(this, listEvents, Global.FieldsEventCopy.PayloadAscii);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ctxMenuExclude_Click(object sender, EventArgs e)
        {
            if (listEvents.SelectedObjects.Count != 1)
            {
                return;
            }

            Event temp = (Event)listEvents.SelectedObjects[0];
            if (temp == null)
            {
                UserInterface.DisplayErrorMessageBox(this, "Unable to locate event");
                return;
            }

            Signature rule = (Signature)cboRule.Items[cboRule.SelectedIndex];

            using (FormExcludeAdd formExclude = new FormExcludeAdd(temp.IpSrc, 
                                                             temp.IpDst, 
                                                             rule.Text, 
                                                             rule.Id))
            {
                if (formExclude.ShowDialog(this) == DialogResult.Cancel)
                {
                    return;
                }

                LoadRuleEvents(_currentPage);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ctxMenuExtractIpInfoUniqueSource_Click(object sender, EventArgs e)
        {
            if (listEvents.SelectedObjects.Count != 1)
            {
                return;
            }

            Event temp = (Event)listEvents.SelectedObjects[0];
            if (temp == null)
            {
                UserInterface.DisplayErrorMessageBox(this, "Unable to locate event");
                return;
            }

            _hourGlass = new HourGlass(this);
            SetProcessingStatus(false);

            Signature rule = (Signature)cboRule.Items[cboRule.SelectedIndex];

            if (dtpDateTo.Checked == true)
            {
                _querier.QueryRuleIpsFromTo(rule.Id.ToString(),
                                            dtpDateFrom.Value.Date.ToString("yyyy-MM-dd") + " " + cboTimeFrom.Text + ":00",
                                            dtpDateTo.Value.Date.ToString("yyyy-MM-dd") + " " + cboTimeTo.Text + ":00", 
                                            true);
            }
            else
            {
                _querier.QueryRuleIpsFrom(rule.Id.ToString(),
                                          dtpDateFrom.Value.Date.ToString("yyyy-MM-dd") + " " + cboTimeFrom.Text + ":00",
                                          true);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ctxMenuExtractIpInfoUniqueDestination_Click(object sender, EventArgs e)
        {
            if (listEvents.SelectedObjects.Count != 1)
            {
                return;
            }

            Event temp = (Event)listEvents.SelectedObjects[0];
            if (temp == null)
            {
                UserInterface.DisplayErrorMessageBox(this, "Unable to locate event");
                return;
            }

            _hourGlass = new HourGlass(this);
            SetProcessingStatus(false);

            Signature rule = (Signature)cboRule.Items[cboRule.SelectedIndex];

            if (dtpDateTo.Checked == true)
            {
                _querier.QueryRuleIpsFromTo(rule.Id.ToString(),
                                            dtpDateFrom.Value.Date.ToString("yyyy-MM-dd") + " " + cboTimeFrom.Text + ":00",
                                            dtpDateTo.Value.Date.ToString("yyyy-MM-dd") + " " + cboTimeTo.Text + ":00",
                                            false);
            }
            else
            {
                _querier.QueryRuleIpsFrom(rule.Id.ToString(),
                                          dtpDateFrom.Value.Date.ToString("yyyy-MM-dd") + " " + cboTimeFrom.Text + ":00",
                                          false);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ctxMenuExportCurrent_Click(object sender, EventArgs e)
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

            List<Event> events = (List<Event>)listEvents.Objects.Cast<Event>().ToList();
            _exporter.ExportEventCurrent(events, saveFileDialog.FileName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ctxMenuExportAll_Click(object sender, EventArgs e)
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

            Signature rule = (Signature)cboRule.Items[cboRule.SelectedIndex];

            if (dtpDateTo.Checked == true)
            {
                _exporter.ExportEventsAll(saveFileDialog.FileName,
                                          dtpDateFrom.Value.Date.ToString("yyyy-MM-dd") + " " + cboTimeFrom.Text + ":00",
                                          dtpDateTo.Value.Date.ToString("yyyy-MM-dd") + " " + cboTimeTo.Text + ":00",
                                          rule.Sid);
            }
            else
            {
                _exporter.ExportEventsAll(saveFileDialog.FileName,
                                          dtpDateFrom.Value.Date.ToString("yyyy-MM-dd") + " " + cboTimeFrom.Text + ":00",
                                          rule.Sid);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ctxMenuPayload_Click(object sender, EventArgs e)
        {
            if (listEvents.SelectedObjects.Count != 1)
            {
                return;
            }

            Event temp = (Event)listEvents.SelectedObjects[0];
            if (temp == null)
            {
                UserInterface.DisplayErrorMessageBox(this, "Unable to locate event");
                return;
            }

            using (FormPayload formPayload = new FormPayload(temp))
            {
                formPayload.ShowDialog(this);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ctxMenuSignature_Click(object sender, EventArgs e)
        {
            if (listEvents.SelectedObjects.Count != 1)
            {
                return;
            }

            Event temp = (Event)listEvents.SelectedObjects[0];
            if (temp == null)
            {
                UserInterface.DisplayErrorMessageBox(this, "Unable to locate event");
                return;
            }

            using (FormRule form = new FormRule(controlEventInfo.Signature))
            {
                form.ShowDialog(this);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ctxMenuExportRules_Click(object sender, EventArgs e)
        {
            if (cboRule.Items.Count == 0)
            {
                return;
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "Select the export CSV";
            saveFileDialog.Filter = "TSV Files|*.tsv";
            if (saveFileDialog.ShowDialog(this) == DialogResult.Cancel)
            {
                return;
            }

            _hourGlass = new HourGlass(this);
            SetProcessingStatus(false);

            //Signature rule = (Signature)cboRule.Items[cboRule.SelectedIndex];

            //if (dtpDateTo.Checked == true)
            //{
            //    _exporter.ExportEventsAll(saveFileDialog.FileName,
            //                              dtpDateFrom.Value.Date.ToString("yyyy-MM-dd") + " " + cboTimeFrom.Text + ":00",
            //                              dtpDateTo.Value.Date.ToString("yyyy-MM-dd") + " " + cboTimeTo.Text + ":00",
            //                              rule.Sid);
            //}
            //else
            //{
            //    _exporter.ExportEventsAll(saveFileDialog.FileName,
            //                              dtpDateFrom.Value.Date.ToString("yyyy-MM-dd") + " " + cboTimeFrom.Text + ":00",
            //                              rule.Sid);
            //}


            //Signature rule = (Signature)cboRule.Items[cboRule.SelectedIndex];

            List<Signature> sigs = new List<Signature>();
            foreach (Signature signature in cboRule.Items)
            {
                sigs.Add(signature);
            }

            _exporter.ExportRules(sigs, saveFileDialog.FileName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ctxMenuNwQuerySrcToDst_Click(object sender, EventArgs e)
        {
            if (listEvents.SelectedObjects.Count != 1)
            {
                return;
            }

            Event temp = (Event)listEvents.SelectedObjects[0];
            if (temp == null)
            {
                UserInterface.DisplayErrorMessageBox(this, "Unable to locate event");
                return;
            }

            string query = Helper.ConstructNetWitnessUrl(_connection.ConcentratorIp,
                                                         _connection.CollectionName,
                                                         temp.IpSrcTxt,
                                                         temp.SrcPort.ToString(),
                                                         temp.IpDstTxt,
                                                         temp.DstPort.ToString(),
                                                         temp.Protocol);

            Clipboard.SetText(query);

            OnMessage("NetWitness query copied to the clipboard");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ctxMenuNwQuerySrcPriorTraffic_Click(object sender, EventArgs e)
        {
            if (listEvents.SelectedObjects.Count != 1)
            {
                return;
            }

            Event temp = (Event)listEvents.SelectedObjects[0];
            if (temp == null)
            {
                UserInterface.DisplayErrorMessageBox(this, "Unable to locate event");
                return;
            }

            string query = Helper.ConstructNetWitnessUrl(_connection.ConcentratorIp,
                                                         _connection.CollectionName,
                                                         temp.IpSrcTxt,
                                                         temp.Timestamp);

            Clipboard.SetText(query);

            OnMessage("NetWitness query copied to the clipboard");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ctxMenuNwQueryDstPriorTraffic_Click(object sender, EventArgs e)
        {
            if (listEvents.SelectedObjects.Count != 1)
            {
                return;
            }

            Event temp = (Event)listEvents.SelectedObjects[0];
            if (temp == null)
            {
                UserInterface.DisplayErrorMessageBox(this, "Unable to locate event");
                return;
            }

            string query = Helper.ConstructNetWitnessUrl(_connection.ConcentratorIp,
                                                         _connection.CollectionName,
                                                         temp.IpDstTxt,
                                                         temp.Timestamp);

            Clipboard.SetText(query);

            OnMessage("NetWitness query copied to the clipboard");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ctxMenuCommand_Click(object sender, EventArgs e)
        {
            if (listEvents.SelectedObjects.Count != 1)
            {
                return;
            }

            Event temp = (Event)listEvents.SelectedObjects[0];
            if (temp == null)
            {
                UserInterface.DisplayErrorMessageBox(this, "Unable to locate event");
                return;
            }

            ToolStripMenuItem ctxMenuCommand = (ToolStripMenuItem)sender;

            string command = Helper.ConstructCommand(ctxMenuCommand.Tag.ToString(),
                                                     temp.IpSrcTxt,
                                                     temp.SrcPort.ToString(),
                                                     temp.IpDstTxt,
                                                     temp.DstPort.ToString(),
                                                     temp.Protocol,
                                                     temp.Sid.ToString());

            Misc.ShellExecuteFile(command);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ctxMenuCopyAcknowledgmentSet_Click(object sender, EventArgs e)
        {
            if (listEvents.SelectedObjects.Count == 0)
            {
                return;
            }

            var list = listEvents.SelectedObjects.Cast<Event>().ToList();

            Signature rule = (Signature)cboRule.Items[cboRule.SelectedIndex];
            using (FormAcknowledgment formAcknowledgement = new FormAcknowledgment(_acknowledgmentClasses, list, rule.Text, _initials))
            {
                if (formAcknowledgement.ShowDialog(this) == DialogResult.OK)
                {
                    LoadRuleEvents(_currentPage);
                    _initials = formAcknowledgement.Initials;
                }
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ctxMenuCopyAcknowledgmentClear_Click(object sender, EventArgs e)
        {
            if (listEvents.SelectedObjects.Count == 0)
            {
                return;
            }

            var list = listEvents.SelectedObjects.Cast<Event>().ToList();

            (new Thread(() =>
            {
                SetProcessingStatus(false);

                using (new HourGlass(this))
                {
                    NPoco.Database db = new NPoco.Database(Db.GetOpenMySqlConnection());
                    foreach (Event temp in list)
                    {
                        if (temp.AcknowledgmentId == 0)
                        {
                            continue;
                        }

                        Acknowledgment acknowledgment = new Acknowledgment();
                        acknowledgment.Id = temp.AcknowledgmentId;
                        var ack = db.SingleById<Acknowledgment>(acknowledgment.Id);

                        db.Delete(ack);
                    }
                }

                SetProcessingStatus(true);
                LoadRuleEvents(_currentPage);
            })).Start();
        }
        #endregion
    }
}
