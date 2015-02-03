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
using snorbert.Forms;
using snorbert.Configs;
using snorbert.Data;
using snorbert.Objects;
using System.Drawing.Drawing2D;

namespace snorbert.Controls
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
        private Alerts _alerts;
        private System.Timers.Timer _timerCheck;
        private FormMain _formMain;
        #endregion

        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        public ControlRules()
        {
            InitializeComponent();

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
            Helper.ResizeEventListColumns(listEvents, false);

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
            _querier.RuleCheckQueryComplete += OnQuerier_RuleCheckQueryComplete;
            _querier.EventQueryComplete += OnQuerier_EventQueryComplete;
            _querier.RuleIpCsvQueryComplete += OnQuerier_RuleIpCsvQueryComplete;
            _querier.RuleIpListQueryComplete += OnQuerier_RuleIpListQueryComplete;

            _exporter = new Exporter();
            _exporter.Complete += OnExporter_Complete;
            _exporter.Error += OnExporter_Error;
            _exporter.Exclamation += OnExporter_Exclamation;

            _commands = new Commands();
            if (_commands.FileExists == true)
            {
                string ret1 = _commands.Load();
                if (ret1.Length > 0)
                {
                    UserInterface.DisplayErrorMessageBox(this, "An error occurred whilst loading the commands: " + ret1);
                }
            }
           
            _alerts = new Alerts();
            string ret2 = _alerts.Load();
            if (ret2.Length == 0)
            {
                if (_alerts.Interval > 0)
                {
                    _timerCheck = new System.Timers.Timer();
                    _timerCheck.Elapsed += OnTimerCheck_Elapsed;
                    _timerCheck.Interval = (_alerts.Interval * 60000); // Convert to milliseconds
                    _timerCheck.Enabled = true;
                }
            }

            LoadPriorities();
        }
        #endregion

        #region Timer Event Handlers
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnTimerCheck_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            MethodInvoker methodInvoker = delegate
            {
                if (cboPriority.SelectedIndex == -1)
                {
                    return;
                }

                if (cboSensor.SelectedIndex == -1)
                {
                    return;
                }

                _timerCheck.Enabled = false;

                NameValue priority = (NameValue)cboPriority.Items[cboPriority.SelectedIndex];
                NameValue sensor = (NameValue)cboSensor.Items[cboSensor.SelectedIndex];

                string dateTo = string.Empty;
                if (dtpDateTo.Checked == true)
                {
                    dateTo = dtpDateTo.Value.Date.ToString("yyyy-MM-dd") + " " + cboTimeTo.Text + ":00";
                }

                string dateFrom = dtpDateFrom.Value.Date.ToString("yyyy-MM-dd") + " " + cboTimeFrom.Text + ":00";

                if (cboPriority.SelectedIndex == -1)
                {
                    return;
                }

                if (cboSensor.SelectedIndex == -1)
                {
                    return;
                }

                if (dateTo.Length > 0)
                {
                    if (priority.Name.ToLower() == "all")
                    {
                        _querier.QueryRulesFromTo(dateFrom,
                                                  dateTo,
                                                  sensor.Value,
                                                  chkIncludeAcknowledged.Checked,
                                                  true);
                    }
                    else
                    {
                        _querier.QueryRulesFromToPriority(dateFrom,
                                                          dateTo,
                                                          priority.Value,
                                                          sensor.Value,
                                                          chkIncludeAcknowledged.Checked,
                                                          true);
                    }
                }
                else
                {
                    if (priority.Name.ToLower() == "all")
                    {
                        _querier.QueryRulesFrom(dateFrom,
                                                sensor.Value,
                                                chkIncludeAcknowledged.Checked,
                                                true);
                    }
                    else
                    {
                        _querier.QueryRulesFromPriority(dateFrom,
                                                        priority.Value,
                                                        sensor.Value,
                                                        chkIncludeAcknowledged.Checked,
                                                        true);
                    }
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

        #region Querier Event Handlers
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        private void OnQuerier_RuleCheckQueryComplete(List<Signature> data)
        {
            MethodInvoker methodInvoker = delegate
            {
                try
                {
                    if (data == null)
                    {
                        return;
                    }

                    if (data.Count == 0)
                    {
                        return;
                    }

                    Signature[] temp = new Signature[cboRule.Items.Count];
                    cboRule.Items.CopyTo(temp, 0);

                    List<Signature> oldData = new List<Signature>(temp);

                    // Check all of the alert priorites
                    bool raiseAlert = false;
                    foreach (int p in _alerts.Priorities)
                    {
                        var newRules = from d in data where d.Priority == p.ToString() select d;
                        foreach (Signature s in newRules)
                        {
                            var oldRules = from d in oldData where d.Id == s.Id select d;
                            if (oldRules.Count() == 0)
                            {
                                s.Updated = true;
                                raiseAlert = true;
                                continue;
                            }

                            if (s.Count > oldRules.First().Count)
                            {
                                s.Updated = true;
                                raiseAlert = true;
                                continue;
                            }
                        }
                    }

                    // Check all of the alert keywords
                    foreach (string kw in _alerts.Keywords)
                    {
                        var newRules = from d in data where d.Name.IndexOf(kw, StringComparison.InvariantCultureIgnoreCase) > -1 select d;
                        foreach (Signature s in newRules)
                        {
                            var oldRules = from d in oldData where d.Id == s.Id select d;
                            if (oldRules.Count() == 0)
                            {
                                s.Updated = true;
                                raiseAlert = true;
                                continue;
                            }

                            if (s.Count > oldRules.First().Count)
                            {
                                s.Updated = true;
                                raiseAlert = true;
                                continue;
                            }
                        }
                    }

                    Signature rule = null;
                    if (cboRule.SelectedIndex != -1)
                    {
                        rule = (Signature)cboRule.Items[cboRule.SelectedIndex];
                    }

                    cboRule.Items.Clear();
                    cboRule.DisplayMember = "Text";
                    cboRule.ValueMember = "Sid";
                    cboRule.Items.AddRange(data.ToArray());

                    if (rule != null)
                    {
                        IEnumerable<Signature> query = from Signature sig in cboRule.Items where (sig.Id.Equals(rule.Id)) select sig;
                        cboRule.SelectedItem = query.First();
                    }

                    if (raiseAlert == true)
                    {
                        _formMain.SetToFront();
                        
                        UserInterface.DisplayMessageBox(this, "New alerts have identified. Check the rules", MessageBoxIcon.Exclamation);
                        OnMessage("Loaded " + cboRule.Items.Count + " rules");
                    }
                }
                catch (Exception ex)
                {
                    //UserInterface.DisplayErrorMessageBox(this, "An error occurred whilst performing the search: " + ex.Message);
                }
                finally
                {
                    _timerCheck.Enabled = true;
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
                    Helper.ResizeEventListColumns(listEvents, false);
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

                    if (cboRule.SelectedIndex == -1)
                    {
                        return;
                    }

                    Signature rule = (Signature)cboRule.Items[cboRule.SelectedIndex];

                    if (data.Count == 0)
                    {
                        cboRule.Items.Remove(cboRule.Items[cboRule.SelectedIndex]);
                        if (cboRule.Items.Count > 1)
                        {
                            cboRule.SelectedIndex += 2;
                            cboRule_DropDownClosed(this, new EventArgs());
                        }
                        else if (cboRule.Items.Count == 1)
                        {
                            cboRule.SelectedIndex = 0;
                            cboRule_DropDownClosed(this, new EventArgs());
                        }

                        return;
                    }

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
                    Helper.ResizeEventListColumns(listEvents, false);
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
        /// <param name="csv"></param>
        private void OnQuerier_RuleIpListQueryComplete(List<string> data)
        {
            OnQuerier_RuleIpQueryComplete(data, false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="csv"></param>
        private void OnQuerier_RuleIpCsvQueryComplete(List<string> data)
        {
            OnQuerier_RuleIpQueryComplete(data, true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="csv"></param>
        private void OnQuerier_RuleIpQueryComplete(List<string> data, bool csv)
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

                    string output = string.Empty;
                    if (csv == true)
                    {
                        output = string.Join(",", data);
                    }
                    else
                    {
                        output = string.Join(Environment.NewLine, data);
                    }

                    if (data.Count > 0)
                    {
                        Clipboard.SetText(output);
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
                    Helper.ResizeEventListColumns(listEvents, false);
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
            if (cboPriority.SelectedIndex == -1)
            {
                UserInterface.DisplayMessageBox("The priority must be selected", MessageBoxIcon.Exclamation);
                return;
            }

            if (cboSensor.SelectedIndex == -1)
            {
                UserInterface.DisplayMessageBox("The sensor must be selected", MessageBoxIcon.Exclamation);
                return;
            }

            _hourGlass = new HourGlass(this);
            SetProcessingStatus(false);
            Clear();

            NameValue priority = (NameValue)cboPriority.Items[cboPriority.SelectedIndex];
            NameValue sensor = (NameValue)cboSensor.Items[cboSensor.SelectedIndex];

            if (dtpDateTo.Checked == true)
            {
                if (priority.Name.ToLower() == "all")
                {
                    _querier.QueryRulesFromTo(dtpDateFrom.Value.Date.ToString("yyyy-MM-dd") + " " + cboTimeFrom.Text + ":00",
                                              dtpDateTo.Value.Date.ToString("yyyy-MM-dd") + " " + cboTimeTo.Text + ":00",
                                              sensor.Value,
                                              chkIncludeAcknowledged.Checked,
                                              false);
                }
                else
                {
                    _querier.QueryRulesFromToPriority(dtpDateFrom.Value.Date.ToString("yyyy-MM-dd") + " " + cboTimeFrom.Text + ":00",
                                                      dtpDateTo.Value.Date.ToString("yyyy-MM-dd") + " " + cboTimeTo.Text + ":00",
                                                      priority.Value,
                                                      sensor.Value,
                                                      chkIncludeAcknowledged.Checked,
                                                      false);
                }
            }
            else
            {
                if (priority.Name.ToLower() == "all")
                {
                    _querier.QueryRulesFrom(dtpDateFrom.Value.Date.ToString("yyyy-MM-dd") + " " + cboTimeFrom.Text + ":00",
                                            sensor.Value,
                                            chkIncludeAcknowledged.Checked,
                                            false);
                }
                else
                {
                    _querier.QueryRulesFromPriority(dtpDateFrom.Value.Date.ToString("yyyy-MM-dd") + " " + cboTimeFrom.Text + ":00",
                                                    priority.Value,
                                                    sensor.Value,
                                                    chkIncludeAcknowledged.Checked,
                                                    false);
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

                if (cboRule.SelectedIndex == -1)
                {
                    UserInterface.DisplayMessageBox("No rule selected", MessageBoxIcon.Exclamation);
                    return;
                }

                Signature rule = (Signature)cboRule.Items[cboRule.SelectedIndex];

                if (dtpDateTo.Checked == true)
                {
                    _querier.QueryEventsRulesFromTo(dtpDateFrom.Value.Date.ToString("yyyy-MM-dd") + " " + cboTimeFrom.Text + ":00",
                                                    dtpDateTo.Value.Date.ToString("yyyy-MM-dd") + " " + cboTimeTo.Text + ":00",
                                                    rule.Id.ToString(),
                                                    (_currentPage - 1) * _pageLimit,
                                                    _pageLimit,
                                                    chkIncludeAcknowledged.Checked);
                }
                else
                {
                    _querier.QueryEventsRulesFrom(dtpDateFrom.Value.Date.ToString("yyyy-MM-dd") + " " + cboTimeFrom.Text + ":00",
                                                  rule.Id.ToString(),
                                                  (_currentPage - 1) * _pageLimit,
                                                  _pageLimit,
                                                  chkIncludeAcknowledged.Checked);
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
        private void ShowPayloadWindow()
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
        private void ShowSignatureWindow()
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
        private void ShowAcknowledgementWindow()
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
        protected void cboRule_DrawItem(object sender, System.Windows.Forms.DrawItemEventArgs e)
        {
            if (e.Index == -1)
            {
                return;
            }

            Signature rule = (Signature)cboRule.Items[e.Index];

            // Determine the forecolor based on whether or not the item is selected    
            Brush brush;
            if (rule.Updated == true)
            {
                brush = Brushes.Red;
            }
            else
            {
                brush = Brushes.Black;
            }

            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
            {
                e.Graphics.FillRectangle(new SolidBrush(Color.LightGray), e.Bounds);
            }
            else
            {
                e.Graphics.FillRectangle(new SolidBrush(cboRule.BackColor), e.Bounds);
            }

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.DrawString(rule.Text, cboRule.Font, brush, e.Bounds.X, e.Bounds.Y);

            e.DrawFocusRectangle();
        }

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
                    using (NPoco.Database db = new NPoco.Database(Db.GetOpenMySqlConnection()))
                    {
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

            using (NPoco.Database db = new NPoco.Database(Db.GetOpenMySqlConnection()))
            {
                _acknowledgmentClasses = db.Fetch<AcknowledgmentClass>();
                _acknowledgmentClasses = (from a in _acknowledgmentClasses orderby a.Desc select a).ToList();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void ShowPayload()
        {
            ShowPayloadWindow();
        }

        /// <summary>
        /// 
        /// </summary>
        public void ShowSignature()
        {
            ShowSignatureWindow();
        }

        /// <summary>
        /// 
        /// </summary>
        public void ShowAcknowledgement()
        {
            ShowAcknowledgementWindow();
        }

        /// <summary>
        /// 
        /// </summary>
        public void SetEventsTaken()
        {
            if (listEvents.SelectedObjects.Count == 0)
            {
                return;
            }

            if (_initials.Length == 0)
            {
                ctxMenuAcknowledgmentSet_Click(this, new EventArgs());
                return;
            }

            SetAcknowledgement("taken");
        }

        /// <summary>
        /// 
        /// </summary>
        public void SetEventsFalsePositive()
        {
            if (listEvents.SelectedObjects.Count == 0)
            {
                return;
            }

            if (_initials.Length == 0)
            {
                ctxMenuAcknowledgmentSet_Click(this, new EventArgs());
                return;
            }

            SetAcknowledgement("false positive");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="formMain"></param>
        public void SetParent(FormMain formMain)
        {
            _formMain = formMain;
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
        /// <param name="acknowledgementClass"></param>
        private void SetAcknowledgement(string ackClass)
        {
            var events = listEvents.SelectedObjects.Cast<Event>().ToList();

            if (cboRule.SelectedIndex == -1)
            {
                return;
            }

            Signature rule = (Signature)cboRule.Items[cboRule.SelectedIndex];

            var acknowledgementClass = (from a in _acknowledgmentClasses where a.Desc.ToLower() == ackClass.ToLower() select a).SingleOrDefault();
            if (acknowledgementClass == null)
            {
                UserInterface.DisplayMessageBox(this, "Cannot locate acknowledgement class", MessageBoxIcon.Exclamation);
                return;
            }

            (new Thread(() =>
            {
                try
                {
                    bool errors = false;
                    bool acknowledgedPrevious = false;
                    using (new HourGlass(this))
                    using (NPoco.Database db = new NPoco.Database(Db.GetOpenMySqlConnection()))
                    {
                        db.BeginTransaction();
                        foreach (Event temp in events)
                        {
                            try
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
                                    acknowledgment.Notes = string.Empty;
                                    acknowledgment.Class = acknowledgementClass.Id;
                                    acknowledgment.Timestamp = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

                                    db.Insert(acknowledgment);
                                }
                            }
                            catch (Exception ex)
                            {
                                db.AbortTransaction();
                                errors = true;
                                IO.WriteTextToFile("Acknowledgement Insert Error: " + ex.ToString() + Environment.NewLine, 
                                                   System.IO.Path.Combine(Misc.GetUserDataDirectory(), "Errors.txt"), 
                                                   true);
                                break;
                            }
                        }

                        if (errors == false)
                        {
                            db.CompleteTransaction();
                        }
                    }

                    if (acknowledgedPrevious == true)
                    {
                        UserInterface.DisplayMessageBox(this,
                                                        "Some events were not classified due to being already classified",
                                                        MessageBoxIcon.Exclamation);
                    }

                    if (errors == true)
                    {
                        UserInterface.DisplayMessageBox(this,
                                                        "Errors occured, check the Errors.txt file",
                                                        MessageBoxIcon.Exclamation);
                    }

                    LoadRuleEvents(_currentPage);
                }
                catch (Exception ex)
                {
                    UserInterface.DisplayMessageBox(this,
                                                    "Errors occured, check the Errors.txt file",
                                                     MessageBoxIcon.Exclamation);
                    IO.WriteTextToFile("Acknowledgement Insert Error (" + DateTime.Now + "): " + ex.ToString() + Environment.NewLine,
                                       System.IO.Path.Combine(Misc.GetUserDataDirectory(), "Errors.txt"),
                                       true);
                }

            })).Start();
        }

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
            using (NPoco.Database db = new NPoco.Database(Db.GetOpenMySqlConnection()))
            {
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
            Helper.ResizeEventListColumns(listEvents, false);

            foreach (NameValue command in _commands.Data)
            {
                ToolStripMenuItem newCommand = new ToolStripMenuItem();
                newCommand.Text = command.Name;
                newCommand.Tag = command.Value;
                newCommand.Click += ctxMenuCommand_Click;
                ctxMenuCommands.DropDownItems.Add(newCommand);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ControlRules_Resize(object sender, EventArgs e)
        {
            if (_formMain == null)
            {
                return;
            }

            if (_formMain.WindowState == FormWindowState.Minimized)
            {
                return;
            }

            cboRule.DropDownHeight = (int)Math.Round(this.Parent.Height * 0.75);
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
        private void ctxMenuCopyCid_Click(object sender, EventArgs e)
        {
            Helper.CopyDataToClipboard(this, listEvents, Global.FieldsEventCopy.Cid);
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
                                                                   temp.SrcPort,
                                                                   temp.IpDst, 
                                                                   temp.DstPort,
                                                                   temp.Protocol,
                                                                   (uint)temp.IpProto,
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
        private void ctxMenuExtractIpInfoUniqueSourceList_Click(object sender, EventArgs e)
        {
            _hourGlass = new HourGlass(this);
            SetProcessingStatus(false);

            Signature rule = (Signature)cboRule.Items[cboRule.SelectedIndex];

            if (dtpDateTo.Checked == true)
            {
                _querier.QueryRuleIpsFromTo(rule.Id.ToString(),
                                            dtpDateFrom.Value.Date.ToString("yyyy-MM-dd") + " " + cboTimeFrom.Text + ":00",
                                            dtpDateTo.Value.Date.ToString("yyyy-MM-dd") + " " + cboTimeTo.Text + ":00",
                                            chkIncludeAcknowledged.Checked,
                                            true, 
                                            false);
            }
            else
            {
                _querier.QueryRuleIpsFrom(rule.Id.ToString(),
                                          dtpDateFrom.Value.Date.ToString("yyyy-MM-dd") + " " + cboTimeFrom.Text + ":00",
                                          chkIncludeAcknowledged.Checked,
                                          true,
                                          false);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ctxMenuExtractIpInfoUniqueSourceCsv_Click(object sender, EventArgs e)
        {
            _hourGlass = new HourGlass(this);
            SetProcessingStatus(false);

            Signature rule = (Signature)cboRule.Items[cboRule.SelectedIndex];

            if (dtpDateTo.Checked == true)
            {
                _querier.QueryRuleIpsFromTo(rule.Id.ToString(),
                                            dtpDateFrom.Value.Date.ToString("yyyy-MM-dd") + " " + cboTimeFrom.Text + ":00",
                                            dtpDateTo.Value.Date.ToString("yyyy-MM-dd") + " " + cboTimeTo.Text + ":00",
                                            chkIncludeAcknowledged.Checked,
                                            true,
                                            true);
            }
            else
            {
                _querier.QueryRuleIpsFrom(rule.Id.ToString(),
                                          dtpDateFrom.Value.Date.ToString("yyyy-MM-dd") + " " + cboTimeFrom.Text + ":00",
                                          chkIncludeAcknowledged.Checked,
                                          true,
                                          true);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ctxMenuExtractIpInfoUniqueDestinationList_Click(object sender, EventArgs e)
        {
            _hourGlass = new HourGlass(this);
            SetProcessingStatus(false);

            Signature rule = (Signature)cboRule.Items[cboRule.SelectedIndex];

            if (dtpDateTo.Checked == true)
            {
                _querier.QueryRuleIpsFromTo(rule.Id.ToString(),
                                            dtpDateFrom.Value.Date.ToString("yyyy-MM-dd") + " " + cboTimeFrom.Text + ":00",
                                            dtpDateTo.Value.Date.ToString("yyyy-MM-dd") + " " + cboTimeTo.Text + ":00",
                                            chkIncludeAcknowledged.Checked,
                                            false,
                                            false);
            }
            else
            {
                _querier.QueryRuleIpsFrom(rule.Id.ToString(),
                                          dtpDateFrom.Value.Date.ToString("yyyy-MM-dd") + " " + cboTimeFrom.Text + ":00",
                                          chkIncludeAcknowledged.Checked,
                                          false,
                                          false);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ctxMenuExtractIpInfoUniqueDestinationCsv_Click(object sender, EventArgs e)
        {
            _hourGlass = new HourGlass(this);
            SetProcessingStatus(false);

            Signature rule = (Signature)cboRule.Items[cboRule.SelectedIndex];

            if (dtpDateTo.Checked == true)
            {
                _querier.QueryRuleIpsFromTo(rule.Id.ToString(),
                                            dtpDateFrom.Value.Date.ToString("yyyy-MM-dd") + " " + cboTimeFrom.Text + ":00",
                                            dtpDateTo.Value.Date.ToString("yyyy-MM-dd") + " " + cboTimeTo.Text + ":00",
                                            chkIncludeAcknowledged.Checked,
                                            false,
                                            true);
            }
            else
            {
                _querier.QueryRuleIpsFrom(rule.Id.ToString(),
                                          dtpDateFrom.Value.Date.ToString("yyyy-MM-dd") + " " + cboTimeFrom.Text + ":00",
                                          chkIncludeAcknowledged.Checked,
                                          false,
                                          true);
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
            ShowPayloadWindow();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ctxMenuSignature_Click(object sender, EventArgs e)
        {
            ShowSignatureWindow();
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
        private void ctxMenuNwQuerySrcToDstEventTimestamps_Click(object sender, EventArgs e)
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
                                                         temp.Protocol,
                                                         temp.Timestamp);

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

            double unix = Misc.ConvertToUnixTimestamp(temp.Timestamp);

            string command = Helper.ConstructCommand(ctxMenuCommand.Tag.ToString(),
                                                     temp.IpSrcTxt,
                                                     temp.SrcPort.ToString(),
                                                     temp.IpDstTxt,
                                                     temp.DstPort.ToString(),
                                                     temp.Protocol,
                                                     temp.Sid.ToString(),
                                                     temp.SensorName,
                                                     unix.ToString());

            Misc.ShellExecuteFile(command);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ctxMenuAcknowledgmentSet_Click(object sender, EventArgs e)
        {
            ShowAcknowledgementWindow();
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ctxMenuAcknowledgmentClear_Click(object sender, EventArgs e)
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
                using (NPoco.Database db = new NPoco.Database(Db.GetOpenMySqlConnection()))
                {
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
