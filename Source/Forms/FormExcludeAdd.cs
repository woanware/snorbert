using System;
using System.Dynamic;
using System.IO;
using System.Net;
using System.Windows.Forms;
using snorbert.Data;
using woanware;

namespace snorbert.Forms
{
    /// <summary>
    /// Window to allow the creation of a False Positive record
    /// </summary>
    public partial class FormExcludeAdd : Form
    {
        #region Member Variables
        private IPAddress _sourceIp;
        private IPAddress _destinationIp;
        private long _ruleId = 0;
        private uint _ipProto = 0;
        #endregion

        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sourceIp"></param>
        /// <param name="sourcePort"></param>
        /// <param name="destinationIp"></param>
        /// <param name="destinationPort"></param>
        /// <param name="protocol"></param>
        /// <param name="ipProto"></param>
        /// <param name="rule"></param>
        /// <param name="ruleId"></param>
        public FormExcludeAdd(IPAddress sourceIp,
                              int sourcePort,
                              IPAddress destinationIp, 
                              int destinationPort,
                              string protocol,
                              uint ipProto,
                              string rule, 
                              long ruleId)
        {
            InitializeComponent();

            _sourceIp = sourceIp;
            _destinationIp = destinationIp;
            _ruleId = ruleId;
            _ipProto = ipProto;

            ipSource.Text = _sourceIp.ToString();
            txtSourcePort.Text = sourcePort.ToString();
            ipDestination.Text = _destinationIp.ToString();
            txtDestinationPort.Text = destinationPort.ToString();
            txtProtocol.Text = protocol;
            txtRule.Text = rule;
            
        }
        #endregion

        #region Button Event Handlers
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                using (NPoco.Database db = new NPoco.Database(Db.GetOpenMySqlConnection()))
                {
                    Exclude exclude = new Exclude();

                    if (chkRule.Checked == true)
                    {
                        exclude.SigId = _ruleId;
                    }

                    if (chkSourceIp.Checked == true)
                    {
                        byte[] ip = _sourceIp.GetAddressBytes();
                        Array.Reverse(ip);
                        exclude.SourceIp = BitConverter.ToUInt32(ip, 0);
                    }

                    if (chkSrcPort.Checked == true)
                    {
                        exclude.SourcePort = ushort.Parse(txtSourcePort.Text);
                    }

                    if (chkDestinationIp.Checked == true)
                    {
                        byte[] ip = _destinationIp.GetAddressBytes();
                        Array.Reverse(ip);
                        exclude.DestinationIp = BitConverter.ToUInt32(ip, 0);
                    }

                    if (chkDestPort.Checked == true)
                    {
                        exclude.DestinationPort = ushort.Parse(txtDestinationPort.Text);
                    }

                    exclude.IpProto = _ipProto;
                    exclude.Comment = txtComment.Text;
                    exclude.FalsePositive = chkFalsePositive.Checked;
                    exclude.Timestamp = DateTime.Now;

                    db.Insert(exclude);
                }
            }
            catch (Exception ex)
            {
                UserInterface.DisplayErrorMessageBox("An error occurred whilst adding the exclude: " + ex.Message);
                return;
            }

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }
        #endregion

        #region Checkbox Event Handlers
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkSourceIp_CheckedChanged(object sender, EventArgs e)
        {
           // ipSource.Enabled = chkSourceIp.Checked;
            //txtSourcePort.Enabled = chkSourceIp.Checked;
            //chkSrcPort.Enabled = chkSourceIp.Checked;
            if (chkSourceIp.Checked == false)
            {
                chkDestinationIp.Enabled = true;
                chkDestPort.Enabled = true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkDestinationIp_CheckedChanged(object sender, EventArgs e)
        {
           // ipDestination.Enabled = chkDestinationIp.Checked;
            //txtDestinationPort.Enabled = chkDestinationIp.Checked;
            //chkDestPort.Enabled = chkDestinationIp.Checked;
            if (chkDestinationIp.Checked == false)
            {
                chkSourceIp.Enabled = true;
                chkSrcPort.Enabled = true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkRule_CheckedChanged(object sender, EventArgs e)
        {
            txtRule.Enabled = chkRule.Checked;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkSrcPort_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSrcPort.Checked == true)
            {
                chkDestinationIp.Checked = false;
                chkDestinationIp.Enabled = false;
                chkDestPort.Checked = false;
                chkDestPort.Enabled = false;
            }
            else
            {
                chkDestinationIp.Enabled = true;
                chkDestPort.Enabled = true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkDestPort_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDestPort.Checked == true)
            {
                chkSourceIp.Checked = false;
                chkSourceIp.Enabled = false;
               chkSrcPort.Checked = false;
                chkSrcPort.Enabled = false;
            }
            else
            {
                chkSourceIp.Enabled = true;
                chkSrcPort.Enabled = true;
            }
        }
        #endregion
    }
}
