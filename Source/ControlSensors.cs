using System;
using System.Collections.Generic;
using System.Windows.Forms;
using woanware;

namespace snorbert
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ControlSensors : UserControl
    {
        #region Events
        public delegate void MessageEvent(string message);
        public event MessageEvent Message;
        public event MessageEvent Error;
        public event MessageEvent Exclamation;
        #endregion

        #region Member Variables
        private HourGlass _hourGlass;
        private Querier _querier;
        private Sql _sql;
        #endregion

        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        public ControlSensors()
        {
            InitializeComponent();

            _querier = new Querier();
            _querier.Error += OnQuerier_Error;
            _querier.Exclamation += OnQuerier_Exclamation;
            _querier.SensorQueryComplete += OnQuerier_SensorQueryComplete;  
        }
        #endregion

        #region Control Event Handlers
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ControlSensors_Load(object sender, EventArgs e)
        {
        }
        #endregion

        #region Querier Event Handlers
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        private void OnQuerier_SensorQueryComplete(List<Sensor> data)
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

                    listSensors.SetObjects(data);
                }
                catch (Exception ex)
                {
                    OnError("An error occurred whilst performing the search: " + ex.Message);
                }
                finally
                {
                    ResizeListColumns();
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
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        private void OnQuerier_Error(string message)
        {
            _hourGlass.Dispose();
            OnError(message);
        }
        #endregion

        #region Query Methods
        /// <summary>
        /// 
        /// </summary>
        private void LoadSensors()
        {
            _hourGlass = new HourGlass(this);
            SetProcessingStatus(false);
            listSensors.ClearObjects();

            _querier.QuerySensors();
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="enabled"></param>
        public void SetState(bool enabled)
        {
            this.Enabled = enabled;
            if (enabled == true)
            {
                LoadSensors();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        public void SetSql(Sql sql)
        {
            _sql = sql;
            _querier.SetSql(sql);
        }
        #endregion

        #region User Interface Methods
        /// <summary>
        /// 
        /// </summary>
        private void ResizeListColumns()
        {
            if (listSensors.Items.Count == 0)
            {
                foreach (ColumnHeader column in listSensors.Columns)
                {
                    column.AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize);
                }
            }
            else
            {
                listSensors.Columns[0].AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize);
                listSensors.Columns[1].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
                listSensors.Columns[2].AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize);
                listSensors.Columns[3].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
                listSensors.Columns[4].AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize);
                listSensors.Columns[5].AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize);
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

        #region Context Menu Event Handlers
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ctxMenuRefresh_Click(object sender, EventArgs e)
        {
            LoadSensors();
        }
        #endregion
    }
}
