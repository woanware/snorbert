using System.Windows.Forms;

namespace snorbert
{
    /// <summary>
    /// 
    /// </summary>
    public partial class FormRule : Form
    {
        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        /// <param name="rule"></param>
        public FormRule(string rule)
        {
            InitializeComponent();

            txtRule.Text = rule;
        }
        #endregion

        #region Button Event Handler
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, System.EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }
        #endregion
    }
}
