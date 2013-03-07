using Be.Windows.Forms;
using System.Windows.Forms;

namespace snorbert
{
    /// <summary>
    /// 
    /// </summary>
    public partial class FormPayload : Form
    {
        #region Member Varaiables
        private FormFind _formFind;
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="temp"></param>
        public FormPayload(Event temp)
        {
            InitializeComponent();

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
        }

        #region Form Event Handlers
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormPayload_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F & e.Modifiers == Keys.Control)
            {
                tabEvent.SelectedTab = tabPageAscii;
                txtPayloadAscii.Select();

                _formFind = new FormFind(this, txtPayloadAscii);
                _formFind.Owner = this;
                _formFind.TopMost = true;
                _formFind.Show();
            }
            else if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
            else if (e.KeyCode == Keys.F3)
            {
                _formFind.DoSearch();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormPayload_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_formFind != null)
            {
                _formFind.Close();
            }    
        }
        #endregion
    }
}
