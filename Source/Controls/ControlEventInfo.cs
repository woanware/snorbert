﻿using Be.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using woanware;
using NPoco;
using snorbert.Forms;
using snorbert.Configs;
using snorbert.Data;

namespace snorbert.Controls
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ControlEventInfo : UserControl
    {
        #region Member Variables
        private snorbert.Configs.Sql _sql;
        #endregion

        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        public ControlEventInfo()
        {
            InitializeComponent();

            tabEvent.TabPages.Remove(tabPageTcpHeader);
            tabEvent.TabPages.Remove(tabPageUdpHeader);

            Helper.AddListColumn(listReferences, "Type", "Type");
            Helper.AddListColumn(listReferences, "Data", "Data");
            ResizeReferenceListColumns();
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// 
        /// </summary> 
        /// <param name="sql"></param>
        public void SetSql(snorbert.Configs.Sql sql)
        {
            _sql = sql;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        public void DisplaySelectedEventDetails(Event temp)
        {
            using (new HourGlass(this))
            {
                if (temp.IpProto == (int)Global.Protocols.Tcp)
                {
                    if (tabEvent.TabPages.Contains(tabPageTcpHeader) == false)
                    {
                        tabEvent.TabPages.Insert(2, tabPageTcpHeader);
                    }

                    if (tabEvent.TabPages.Contains(tabPageUdpHeader) == true)
                    {
                        tabEvent.TabPages.Remove(tabPageUdpHeader);
                    }
                }
                else if (temp.IpProto == (int)Global.Protocols.Udp)
                {
                    if (tabEvent.TabPages.Contains(tabPageUdpHeader) == false)
                    {
                        tabEvent.TabPages.Insert(2, tabPageUdpHeader);
                    }

                    if (tabEvent.TabPages.Contains(tabPageTcpHeader) == true)
                    {
                        tabEvent.TabPages.Remove(tabPageTcpHeader);
                    }
                }
                else
                {
                    tabEvent.TabPages.Remove(tabPageTcpHeader);
                    tabEvent.TabPages.Remove(tabPageUdpHeader);
                }

                // IP Tab
                ipSource.Text = temp.IpSrcTxt.ToString();
                ipDest.Text = temp.IpDstTxt.ToString();
                txtIpCsum.Text = temp.IpCsum.ToString();
                txtIpFlags.Text = temp.IpFlags.ToString();
                txtIpHlen.Text = temp.IpHlen.ToString();
                txtIpId.Text = temp.IpId.ToString();
                txtIpLen.Text = temp.IpLen.ToString();
                txtIpOff.Text = temp.IpOff.ToString();
                txtIpProto.Text = temp.IpProto.ToString();
                txtIpTos.Text = temp.IpTos.ToString();
                txtIpTtl.Text = temp.IpTtl.ToString();
                txtIpVer.Text = temp.IpVer.ToString();

                // Signature Tab
                txtSigCategory.Text = temp.SigClassName;
                txtSigGenId.Text = temp.SigGid.ToString();
                txtSigSigRev.Text = temp.SigRev.ToString();
                txtSigSigId.Text = temp.SigSid.ToString();

                txtRule.Text = temp.Rule;
                if (txtRule.Text.IndexOf("flowbits:isset,", StringComparison.InvariantCultureIgnoreCase) > -1)
                {
                    btnLinkedRules.Enabled = true;
                }
                else
                {
                    btnLinkedRules.Enabled = false;
                }

                // TCP Tab
                txtTcpAck.Text = temp.TcpAck.ToString();
                txtTcpCsum.Text = temp.TcpCsum.ToString();
                txtTcpDstPort.Text = temp.TcpDstPort.ToString();
                txtTcpFlags.Text = temp.TcpFlags.ToString();
                txtTcpOff.Text = temp.TcpOff.ToString();
                txtTcpRes.Text = temp.TcpRes.ToString();
                txtTcpSeq.Text = temp.TcpSeq.ToString();
                txtTcpSrcPrt.Text = temp.TcpSrcPort.ToString();
                txtTcpUrp.Text = temp.TcpUrp.ToString();
                txtTcpWin.Text = temp.TcpWin.ToString();

                // UDP Tab
                txtUdpSrcPort.Text = temp.UdpSrcPort.ToString();
                txtUdpDstPort.Text = temp.TcpDstPort.ToString();
                txtUdpLen.Text = temp.UdpLen.ToString();
                txtUdpCsum.Text = temp.UdpCsum.ToString();

                // References Tab
                using (NPoco.Database dbMySql = new NPoco.Database(Db.GetOpenMySqlConnection()))
                {
                    List<Reference> references = dbMySql.Fetch<Reference>(_sql.GetQuery(snorbert.Configs.Sql.Query.SQL_REFERENCES), new object[] { temp.SigId });
                    listReferences.SetObjects(references);
                }
                
                ResizeReferenceListColumns();

                // Payload Tab (HEX)
                if (temp.PayloadHex != null)
                {
                    DynamicByteProvider dynamicByteProvider = new DynamicByteProvider(temp.PayloadHex);
                    hexEvent.ByteProvider = dynamicByteProvider;
                }
                else
                {
                    DynamicByteProvider dynamicByteProvider = new DynamicByteProvider(new byte[] { });
                    hexEvent.ByteProvider = dynamicByteProvider;
                }

                // Payload Tab (ASCII)
                txtPayloadAscii.Text = temp.PayloadAscii;

                // Misc Tab
                txtEventSid.Text = temp.Sid.ToString();
                txtEventCid.Text = temp.Cid.ToString();
                txtSensorName.Text = temp.SensorName;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void ClearControls()
        {
            // IP Tab
            ipSource.Text = string.Empty;
            ipDest.Text = string.Empty;
            txtIpCsum.Text = string.Empty;
            txtIpFlags.Text = string.Empty;
            txtIpHlen.Text = string.Empty;
            txtIpId.Text = string.Empty;
            txtIpLen.Text = string.Empty;
            txtIpOff.Text = string.Empty;
            txtIpProto.Text = string.Empty;
            txtIpTos.Text = string.Empty;
            txtIpTtl.Text = string.Empty;
            txtIpVer.Text = string.Empty;

            // Signature Tab
            txtSigCategory.Text = string.Empty;
            txtSigGenId.Text = string.Empty;
            txtSigSigRev.Text = string.Empty;
            txtSigSigId.Text = string.Empty;
            txtRule.Text = string.Empty;

            // TCP Tab
            txtTcpAck.Text = string.Empty;
            txtTcpCsum.Text = string.Empty;
            txtTcpDstPort.Text = string.Empty;
            txtTcpFlags.Text = string.Empty;
            txtTcpOff.Text = string.Empty;
            txtTcpRes.Text = string.Empty;
            txtTcpSeq.Text = string.Empty;
            txtTcpSrcPrt.Text = string.Empty;
            txtTcpUrp.Text = string.Empty;
            txtTcpWin.Text = string.Empty;

            // UDP Tab
            txtUdpSrcPort.Text = string.Empty;
            txtUdpDstPort.Text = string.Empty;
            txtUdpLen.Text = string.Empty;
            txtUdpCsum.Text = string.Empty;

            // References Tab
            listReferences.ClearObjects();
            ResizeReferenceListColumns();

            // Payload Tab (HEX)
            DynamicByteProvider dynamicByteProvider = new DynamicByteProvider(new byte[] { });
            hexEvent.ByteProvider = dynamicByteProvider;

            // Payload Tab (ASCII)
            txtPayloadAscii.Text = string.Empty;
        }
        #endregion

        #region Control Methods
        /// <summary>
        /// Resizes the Reference list's columns
        /// </summary>
        private void ResizeReferenceListColumns()
        {
            if (listReferences.Items.Count == 0)
            {
                foreach (ColumnHeader column in listReferences.Columns)
                {
                    column.AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize);
                }
            }
            else
            {
                listReferences.Columns[(int)Global.FieldsReferences.Data].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
                listReferences.Columns[(int)Global.FieldsReferences.Type].Width = 50;
            }
        }
        #endregion

        #region Button Event Handlers
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRule_Click(object sender, EventArgs e)
        {
            using (FormRule formRule = new FormRule(txtRule.Text))
            {
                formRule.ShowDialog(this);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLinkedRules_Click(object sender, EventArgs e)
        {
            using (FormLinkedRules form = new FormLinkedRules(txtRule.Text))
            {
                form.ShowDialog(this);
            }
        }
        #endregion

        #region Context Menu Event Handlers
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ctxMenuHexCopyText_Click(object sender, EventArgs e)
        {
            hexEvent.Copy();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ctxMenuHexCopyHexNoSpaces_Click_1(object sender, EventArgs e)
        {
            hexEvent.CopyHexNoSpace();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ctxMenuHexCopyHexSpaces_Click_1(object sender, EventArgs e)
        {
            hexEvent.CopyHex();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ctxMenuHexSelectAll_Click(object sender, EventArgs e)
        {
            hexEvent.SelectAll();
        }
        #endregion

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public string Signature
        {
            get
            {
                return txtRule.Text;
            }
        }
        #endregion
    }
}
