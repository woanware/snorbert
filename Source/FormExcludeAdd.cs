using System;
using System.Dynamic;
using System.Net;
using System.Windows.Forms;
using woanware;

namespace snorbert
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
        #endregion

        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sourceIp"></param>
        /// <param name="destinationIp"></param>
        /// <param name="rule"></param>
        /// <param name="ruleId"></param>
        public FormExcludeAdd(IPAddress sourceIp,
                              IPAddress destinationIp, 
                              string rule, 
                              long ruleId)
        {
            InitializeComponent();

            _sourceIp = sourceIp;
            _destinationIp = destinationIp;

            ipSource.Text = _sourceIp.ToString();
            ipDestination.Text = _destinationIp.ToString();
            txtRule.Text = rule;
            _ruleId = ruleId;
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
                var dbExclude = new DbExclude();

                dynamic exclude = new ExpandoObject();

                if (chkRule.Checked == true)
                {
                    exclude.sig_id = _ruleId;
                }

                if (chkSourceIp.Checked == true)
                {
                    byte[] ip = _sourceIp.GetAddressBytes();
                    Array.Reverse(ip);
                    exclude.ip_src = BitConverter.ToUInt32(ip, 0);
                }

                if (chkDestinationIp.Checked == true)
                {
                    byte[] ip = _destinationIp.GetAddressBytes();
                    Array.Reverse(ip);
                    exclude.ip_dst = BitConverter.ToUInt32(ip, 0); 
                }
                
                exclude.comment = txtComment.Text;
                exclude.fp = chkFalsePositive.Checked;
                exclude.timeadded = DateTime.Now;

                dbExclude.Insert(exclude);
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
            ipSource.Enabled = chkSourceIp.Checked;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkDestinationIp_CheckedChanged(object sender, EventArgs e)
        {
            ipDestination.Enabled = chkDestinationIp.Checked;
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
        #endregion
    }
}
