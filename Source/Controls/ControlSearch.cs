using BrightIdeasSoftware;
using snorbert.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using woanware;
using snorbert.Configs;
using snorbert.Data;
using snorbert.Objects;

namespace snorbert.Controls
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ControlSearch : UserControl
    {
        #region Events
        public event Global.MessageEvent Message;
        #endregion

        #region Member Variables
        private string _initials = string.Empty;
        private List<Filter> _filters;
        private long _currentPage = 1;
        private int _pageLimit = 100;
        private bool _moreData;
        private HourGlass _hourGlass;
        private Querier _querier;
        private Sql _sql;
        private Connection _connection;
        private List<AcknowledgmentClass> _acknowledgmentClasses;
        #endregion

        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        public ControlSearch()
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
            Helper.AddListColumn(listEvents, "Signature", "SigName");
            Helper.AddListColumn(listEvents, "Payload (ASCII)", "PayloadAscii");
            Helper.ResizeEventListColumns(listEvents, true);

            Helper.AddListColumn(listFilters, "Field", "Field");
            Helper.AddListColumn(listFilters, "Condition", "Condition");
            Helper.AddListColumn(listFilters, "Value", "Display");

            cboSearch.SelectedIndex = 0;

            _filters = new List<Filter>();

            _querier = new Querier();
            _querier.Error += OnQuerier_Error;
            _querier.Exclamation += OnQuerier_Exclamation;
            _querier.EventQueryComplete += OnQuerier_EventQueryComplete;  
        }
        #endregion

        #region Button Event Handlers
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFilterAdd_Click(object sender, EventArgs e)
        {
            using (FormFilter formFilter = new FormFilter(_sql, _filters))
            {
                using (new HourGlass(this))
                using (NPoco.Database db = new NPoco.Database(Db.GetOpenMySqlConnection()))
                {
                    formFilter.LoadClassifications(db);
                }

                if (formFilter.ShowDialog(this) == System.Windows.Forms.DialogResult.Cancel)
                {
                    return;
                }

                _filters.Add(formFilter.Filter);

                LoadFilters();
                LoadSearch(1);
                SetFilterButtonStatus(true);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFilterEdit_Click(object sender, EventArgs e)
        {
            if (listFilters.SelectedObjects.Count == 0)
            {
                return;
            }

            Filter filter = (Filter)listFilters.SelectedObjects[0];

            using (FormFilter formFilter = new FormFilter(_sql, _filters))
            {
                formFilter.Filter = filter;

                if (formFilter.ShowDialog(this) == System.Windows.Forms.DialogResult.Cancel)
                {
                    return;
                }

                filter.Definition = formFilter.Filter.Definition;
                filter.Condition = formFilter.Filter.Condition;
                filter.Value = formFilter.Filter.Value;

                LoadFilters();
                LoadSearch(1);
                SetFilterButtonStatus(true);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFilterDelete_Click(object sender, EventArgs e)
        {
            if (listFilters.SelectedObjects.Count == 0)
            {
                return;
            }

            Filter filter = (Filter)listFilters.SelectedObjects[0];

            DialogResult dialogResult = MessageBox.Show(this,
                                                        "Are you sure you want to delete the filter? " + Environment.NewLine + Environment.NewLine +
                                                        "Field: " + filter.Field + Environment.NewLine +
                                                        "Condition: " + filter.Condition + Environment.NewLine +
                                                        "Value: " + filter.Display,
                                                        Application.ProductName,
                                                        MessageBoxButtons.YesNo,
                                                        MessageBoxIcon.Question);

            if (dialogResult == System.Windows.Forms.DialogResult.No)
            {
                return;
            }

            _filters.RemoveAll(f => f.Id == filter.Id);

            if (_filters.Count == 0)
            {
                Clear();
            }
            else
            {
                LoadSearch(1);
            }

            LoadFilters();
            SetFilterButtonStatus(true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadSearch(1);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPagingSearchPreviousPage_Click(object sender, EventArgs e)
        {
            LoadSearch(_currentPage - 1);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPagingSearchNextPage_Click(object sender, EventArgs e)
        {
            LoadSearch(_currentPage + 1);
        }
        #endregion

        #region Misc Methods
        /// <summary>
        /// 
        /// </summary>
        private void LoadFilters()
        {
            using (new HourGlass(this))
            {
                listFilters.SetObjects(_filters);

                if (_filters.Any() == true)
                {
                    listFilters.SelectedObject = _filters[0];
                }

                ResizeFilterListColumns();
            }
        }
        #endregion

        #region Query Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        private void LoadSearch(long page)
        {
            if (_filters == null)
            {
                return;
            }

            _currentPage = page;
            _hourGlass = new HourGlass(this);
            SetProcessingStatus(false);
            Clear();

            try
            {
                string condition = string.Empty;
                switch (cboSearch.SelectedIndex)
                {
                    case 0:
                        condition = " OR ";
                        break;
                    case 1:
                        condition = " AND ";
                        break;
                }

                List<object> args = new List<object>();
                string where = "(";
                for (int index = 0; index < _filters.Count; index++)
                {
                    Filter filter = _filters[index];
                    FilterDefinition filterDefinition = filter.Definition;

                    if (index == 0)
                    {
                        where += string.Format(" {0} {1} @{2}", filterDefinition.ColumnName, filter.Condition, index);
                    }
                    else
                    {
                        where += string.Format(" {0} {1} {2} @{3}", condition, filterDefinition.ColumnName, filter.Condition, index);
                    }

                    args.Add(filter.Value);
                }

                where = where + ") LIMIT @" + _filters.Count + ", @" + (_filters.Count + 1);
                args.Add((_currentPage - 1) * _pageLimit);
                args.Add(_pageLimit + 1);

                _querier.QuerySearch(where, args.ToArray());
            }
            catch (Exception ex)
            {
                UserInterface.DisplayErrorMessageBox(this, "An error occurred whilst generating the query: " + ex.Message);
            }
        }
        #endregion

        #region Querier Event Handlers
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

                    if (data.Count > _pageLimit)
                    {
                        data = data.Slice(0, _pageLimit).ToList();
                        _moreData = true;
                    }
                    else
                    {
                        _moreData = false;
                    }

                    listEvents.SetObjects(data);

                    if (data.Any() == true)
                    {
                        listEvents.SelectedObject = data[0];
                        listEvents_SelectedIndexChanged(this, null);
                    }

                    lblPaging.Text = "Page " + _currentPage;
                    OnMessage("Loaded " + data.Count + " results");
                }
                catch (Exception ex)
                {
                    UserInterface.DisplayErrorMessageBox(this, "An error occurred whilst performing the search: " + ex.Message);
                }
                finally
                {
                    Helper.ResizeEventListColumns(listEvents, true);
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

            using (FormAcknowledgment formAcknowledgement = new FormAcknowledgment(_acknowledgmentClasses, list, "N/A", _initials))
            {
                if (formAcknowledgement.ShowDialog(this) == DialogResult.OK)
                {
                    LoadSearch(_currentPage);
                    _initials = formAcknowledgement.Initials;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void SetPagingControlState()
        {           
            btnPagingNextPage.Enabled = _moreData;
            btnPagingPreviousPage.Enabled = (_currentPage != 1);
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
        private void SetFilterButtonStatus(bool addEnabled)
        {
            MethodInvoker methodInvoker = delegate
            {
                if (listFilters.SelectedObjects.Count == 0)
                {
                    btnFilterEdit.Enabled = false;
                    btnFilterDelete.Enabled = false;
                    btnSearch.Enabled = false;
                }
                else
                {
                    btnFilterEdit.Enabled = true;
                    btnFilterDelete.Enabled = true;
                    btnSearch.Enabled = true;
                }

                btnFilterAdd.Enabled = addEnabled;
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
        /// Resizes the filter list's columns
        /// </summary>
        private void ResizeFilterListColumns()
        {
            if (listFilters.Items.Count == 0)
            {
                listFilters.Columns[0].AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize);
                listFilters.Columns[1].AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize);
                listFilters.Columns[2].AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize);
            }
            else
            {
                listFilters.Columns[0].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
                listFilters.Columns[1].AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize);
                listFilters.Columns[2].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
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
        private void listFilters_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetFilterButtonStatus(true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listFilters_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnFilterEdit_Click(this, new EventArgs());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listFilters_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnFilterEdit_Click(this, new EventArgs());
            }
            else if (e.KeyCode == Keys.Delete)
            {
                btnFilterDelete_Click(this, new EventArgs());
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
            controlEventInfo.SetSql(_sql);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connection"></param>
        public void SetConnection(Connection connection)
        {
            _connection = connection;

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

        #region Control Event Handlers
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Control_Load(object sender, EventArgs e)
        {
            Helper.ResizeEventListColumns(listEvents, true);
            ResizeFilterListColumns();
            SetFilterButtonStatus(true);
            SetPagingControlState();
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
        private void ctxMenuCopyCid_Click(object sender, EventArgs e)
        {
            Helper.CopyDataToClipboard(this, listEvents, Global.FieldsEventCopy.Cid);
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
                LoadSearch(_currentPage);
            })).Start();
        }
        #endregion

        #region Combobox Event Handlers
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
             LoadSearch(1);
        }
        #endregion
    }
}
