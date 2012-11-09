using BrightIdeasSoftware;
using Microsoft.Isam.Esent.Collections.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public delegate void MessageEvent(string message);
        public event MessageEvent Message;
        public event MessageEvent Error;
        public event MessageEvent Exclamation;
        #endregion

        #region Member Variables
        private int _pageLimit = 100;
        private long _currentPage = 1;
        private long _totalPages = 0;
        private long _totalRecords = 0;
        private Querier _querier;
        private HourGlass _hourGlass;
        private Sql _sql;
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
                        OnExclamation("No data retrieved for query");
                        return;
                    }

                    cboRule.DisplayMember = "Text";
                    cboRule.ValueMember = "Sid";
                    cboRule.Items.AddRange(data.ToArray());

                    if (data.Count == 0)
                    {
                        OnExclamation("No rules loaded for defined parameters");
                    }
                    else
                    {
                        OnMessage("Loaded " + cboRule.Items.Count + " rules");
                    }
                }
                catch (Exception ex)
                {
                    OnError("An error occurred whilst performing the search: " + ex.Message);
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
                        OnExclamation("No data retrieved for query");
                        return;
                    }

                    Rule rule = (Rule)cboRule.Items[cboRule.SelectedIndex];

                    _totalRecords = rule.Count;
                    _totalPages = rule.Count / _pageLimit;
                    if (rule.Count % _pageLimit != 0)
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
                    OnMessage("Loaded " + data.Count + " results (" + _totalRecords + ")");
                }
                catch (Exception ex)
                {
                    OnError("An error occurred whilst performing the search: " + ex.Message);
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
            OnExclamation(message);
            SetProcessingStatus(true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        private void OnQuerier_Error(string message)
        {
            _hourGlass.Dispose();
            OnError(message);
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="enabled"></param>
        public void SetState(bool enabled)
        {
            this.Enabled = enabled;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        public void SetSql(Sql sql)
        {
            _sql = sql;
            _querier.SetSql(_sql);
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        private void OnExclamation(string message)
        {
            var handler = Exclamation;
            if (handler != null)
            {
                handler(message);
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
            }
            else
            {
                ctxMenuCopy.Enabled = true;
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
        #endregion
    }
}
