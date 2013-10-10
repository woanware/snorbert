using snorbert.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
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
    public partial class ControlEvents : UserControl
    {
        #region Events
        public event Global.MessageEvent Message;
        #endregion

        #region Member Variables
        private string _initials = string.Empty;
        private long _currentPage = 1;
        private HourGlass _hourGlass;
        private bool _moreData;
        private int _pageLimit = 100;
        private Querier _querier;
        private Sql _sql;
        private Connection _connection;
        private List<AcknowledgmentClass> _acknowledgmentClasses;
        #endregion

        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        public ControlEvents()
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

            _querier = new Querier();
            _querier.Error += OnQuerier_Error;
            _querier.Exclamation += OnQuerier_Exclamation;
            _querier.EventQueryComplete += OnQuerier_EventQueryComplete;           
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

                    lblPagingEvents.Text = "Page " + _currentPage;
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

        #region Query Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        private void LoadEvents(long page)
        {
            _currentPage = page;
            _hourGlass = new HourGlass(this);
            SetProcessingStatus(false);
            Clear();

            _querier.QueryEvents((_currentPage - 1) * _pageLimit, _pageLimit + 1);
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
                    LoadEvents(_currentPage);
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
            btnPagingNextPage.Enabled = _moreData;
            btnPagingPreviousPage.Enabled = (_currentPage != 1);

            //if (_totalPages == 0)
            //{
            //    btnPagingFirstPage.Enabled = false;
            //    btnPagingLastPage.Enabled = false;
            //    btnPagingNextPage.Enabled = false;
            //    btnPagingPreviousPage.Enabled = false;
            //}
            //else
            //{
            //    btnPagingFirstPage.Enabled = (_currentPage != 1);
            //    btnPagingLastPage.Enabled = (_currentPage < _totalPages);
            //    btnPagingNextPage.Enabled = (_currentPage < _totalPages);
            //    btnPagingPreviousPage.Enabled = (_currentPage != 1);
            //}
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
        #endregion

        #region Button Event Handlers
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadEvents(1);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPagingPreviousPage_Click(object sender, EventArgs e)
        {
            LoadEvents(_currentPage - 1);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPagingNextPage_Click(object sender, EventArgs e)
        {
            LoadEvents(_currentPage + 1);
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
            SetPagingControlState();
            Helper.ResizeEventListColumns(listEvents, true);
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
        #endregion
    }
}
