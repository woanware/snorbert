using BrightIdeasSoftware;
using Microsoft.Isam.Esent.Collections.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using woanware;

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
        private int _pageLimit = 100;
        private long _currentPage = 1;
        private long _totalPages = 0;
        private long _totalRecords = 0;
        private Querier _querier;
        private Exporter _exporter;
        private HourGlass _hourGlass;
        private Sql _sql;
        private FalsePositives _falsePostives;
        #endregion

        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        public ControlRules()
        {
            InitializeComponent();

            Helper.AddListColumn(listEvents, "CID", "Cid");
            Helper.AddListColumn(listEvents, "Src IP", "IpSrc");
            Helper.AddListColumn(listEvents, "Src Port", "SrcPort");
            Helper.AddListColumn(listEvents, "Dst IP", "IpDst");
            Helper.AddListColumn(listEvents, "Dst Port", "DstPort");
            Helper.AddListColumn(listEvents, "Protocol", "Protocol");
            Helper.AddListColumn(listEvents, "Timestamp", "Timestamp");
            Helper.AddListColumn(listEvents, "TCP Flags", "TcpFlagsString");
            Helper.AddListColumn(listEvents, "Payload (ASCII)", "PayloadAscii");
            Helper.ResizeEventListColumns(listEvents);

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

            LoadFalsePositives();
        }
        #endregion

        #region Querier Event Handlers
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        private void OnQuerier_RuleQueryComplete(List<Rule> data)
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
                        UserInterface.DisplayMessageBox(this, "No rules loaded for defined parameters", MessageBoxIcon.Exclamation);
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

                    Rule rule = (Rule)cboRule.Items[cboRule.SelectedIndex];

                    int preFilterCount = data.Count;
                    _totalRecords = rule.Count;

                    // Sometimes there will be more events for a rule, than there originally was 
                    // when the rule list was generated. But since we don't want to requery the 
                    // database to get a more accurate count, then we will just the the count of
                    // the data returned. Though this only works when the amount of data returned 
                    // is less than the Page Size
                    if (rule.Count < preFilterCount)
                    {
                        _totalRecords = preFilterCount;
                    }

                    _totalPages = _totalRecords / _pageLimit;
                    if (_totalRecords % _pageLimit != 0)
                    {
                        _totalPages++;
                    }

                    listEvents.SetObjects(FilterData(rule.Sid, data));

                    if (data.Any() == true)
                    {
                        listEvents.SelectedObject = data[0];
                        listEvents_SelectedIndexChanged(this, null);
                    }

                    lblPagingRules.Text = "Page " + _currentPage + " (" + _totalPages + ")";
                    OnMessage("Loaded " + data.Count + " results (Hidden: " + (preFilterCount - data.Count) + "/Total: " + _totalRecords + ")");
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

        #region False Positive Filter Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sid"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        private List<Event> FilterData(string sid, List<Event> data)
        {
            // Get all of the FP's relating to this particular rule
            var fps = from f in _falsePostives.Data where f.Sid == sid select f;

            foreach (FalsePositive fp in fps)
            {
                switch (fp.Definition.Field.ToLower())
                {
                    case "source ip":
                        data.RemoveAll(d => CompareFalsePositive(d.IpSrc.ToString(), fp.Display, fp.Condition));
                        break;
                    case "tcp source port":
                        data.RemoveAll(d => CompareFalsePositive(d.TcpSrcPort.ToString(), fp.Value, fp.Condition));
                        break;
                    case "udp source port":
                        data.RemoveAll(d => CompareFalsePositive(d.UdpSrcPort.ToString(), fp.Value, fp.Condition));
                        break;
                    case "destination ip":
                        data.RemoveAll(d => CompareFalsePositive(d.IpDst.ToString(), fp.Display, fp.Condition));
                        break;
                    case "tcp destination port":
                        data.RemoveAll(d => CompareFalsePositive(d.TcpDstPort.ToString(), fp.Value, fp.Condition));
                        break;
                    case "udp destination port":
                        data.RemoveAll(d => CompareFalsePositive(d.UdpDstPort.ToString(), fp.Value, fp.Condition));
                        break;
                    case "protocol":
                        data.RemoveAll(d => CompareFalsePositive(d.IpProto.ToString(), fp.Value, fp.Condition));
                        break;
                    case "payload (ascii)":
                        data.RemoveAll(d => CompareFalsePositive(d.PayloadAscii.ToString(), fp.Display, fp.Condition));
                        break;
                    case "payload (hex)":
                        data.RemoveAll(d => CompareFalsePositive(woanware.Text.ConvertByteArrayToHexString(d.PayloadHex), fp.Value, fp.Condition));
                        break;
                }
            }

            return data;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventData"></param>
        /// <param name="fpData"></param>
        /// <param name="operation"></param>
        /// <returns></returns>
        private bool CompareFalsePositive(string eventData,
                                          string fpData,
                                          string operation)
        {
            switch (operation)
            {
                case "=":
                    return eventData == fpData;
                case "!":
                    return eventData != fpData;
                case "LIKE":
                    return eventData.IndexOf(fpData, StringComparison.InvariantCultureIgnoreCase) > -1;
                case "NOT LIKE":
                    return eventData.IndexOf(fpData, StringComparison.InvariantCultureIgnoreCase) == -1;
                default:
                    return false;
            }
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

            if (dtpDateTo.Checked == true)
            {
                _querier.QueryRulesToFrom(dtpDateFrom.Value.Date.ToString("yyyy-MM-dd") + " " + cboTimeFrom.Text + ":00", 
                                          dtpDateTo.Value.Date.ToString("yyyy-MM-dd") + " " + cboTimeTo.Text + ":00");
            }
            else
            {
                _querier.QueryRulesFrom(dtpDateFrom.Value.Date.ToString("yyyy-MM-dd") + " " + cboTimeFrom.Text + ":00");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        private void LoadRuleEvents(long page)
        {
            _currentPage = page;
            _hourGlass = new HourGlass(this);
            SetProcessingStatus(false);
            Clear();

            Rule rule = (Rule)cboRule.Items[cboRule.SelectedIndex];

            if (dtpDateTo.Checked == true)
            {
                _querier.QueryEventsRulesToFrom(dtpDateFrom.Value.Date.ToString("yyyy-MM-dd") + " " + cboTimeFrom.Text + ":00", 
                                                dtpDateTo.Value.Date.ToString("yyyy-MM-dd") + " " + cboTimeTo.Text + ":00",
                                                rule.Sid,
                                                (_currentPage - 1) * _pageLimit,
                                                _pageLimit);
            }
            else
            {
                _querier.QueryEventsRulesFrom(dtpDateFrom.Value.Date.ToString("yyyy-MM-dd") + " " + cboTimeFrom.Text + ":00",
                                              rule.Sid,
                                              (_currentPage - 1) * _pageLimit,
                                              _pageLimit);
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
        #endregion

        #region Public Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="rules"></param>
        public void SetRules(PersistentDictionary<string, string> rules)
        {
            controlEventInfo.SetRules(rules);
        }

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

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="enabled"></param>
        //public void SetState(bool enabled)
        //{
        //    this.Enabled = enabled;
        //}

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
        public void LoadFalsePositives()
        {
            _falsePostives = new FalsePositives();
            string ret = _falsePostives.Load();
            if (ret.Length > 0)
            {
                UserInterface.DisplayErrorMessageBox(this, "An error occurred whilst loading the false positive data: " + ret);
                Misc.WriteToEventLog(Application.ProductName, "An error occurred whilst loading the false positive data: " + ret, System.Diagnostics.EventLogEntryType.Error);
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
            if (listEvents.SelectedObjects.Count == 0)
            {
                ctxMenuCopy.Enabled = false;
                ctxMenuExtractIpInfo.Enabled = false;
            }
            else
            {
                ctxMenuCopy.Enabled = true;
                ctxMenuExtractIpInfo.Enabled = true;
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
        private void ctxMenuHide_Click(object sender, EventArgs e)
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

            using (FormFalsePositive formFalsePositive = new FormFalsePositive(_falsePostives, temp))
            {
                if (formFalsePositive.ShowDialog(this) == DialogResult.Cancel)
                {
                    return;
                }

                LoadFalsePositives();
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

            Rule rule = (Rule)cboRule.Items[cboRule.SelectedIndex];

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

            Rule rule = (Rule)cboRule.Items[cboRule.SelectedIndex];

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
            _exporter.ExportCurrent(events, saveFileDialog.FileName);
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

            Rule rule = (Rule)cboRule.Items[cboRule.SelectedIndex];

            if (dtpDateTo.Checked == true)
            {
                _exporter.ExportAll(saveFileDialog.FileName,
                                    dtpDateFrom.Value.Date.ToString("yyyy-MM-dd") + " " + cboTimeFrom.Text + ":00",
                                    dtpDateTo.Value.Date.ToString("yyyy-MM-dd") + " " + cboTimeTo.Text + ":00",
                                    rule.Sid);
            }
            else
            {
                _exporter.ExportAll(saveFileDialog.FileName,
                                    dtpDateFrom.Value.Date.ToString("yyyy-MM-dd") + " " + cboTimeFrom.Text + ":00",
                                    rule.Sid);
            }
        }
        #endregion
    }
}
