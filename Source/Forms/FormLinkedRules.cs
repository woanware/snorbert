using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using NPoco;
using snorbert.Data;
using woanware;

namespace snorbert.Forms
{
    /// <summary>
    /// 
    /// </summary>
    public partial class FormLinkedRules : Form
    {
        private string _rule;

        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        /// <param name="rule"></param>
        public FormLinkedRules(string rule)
        {
            InitializeComponent();
            _rule = rule;
        }
        #endregion

        #region Button Event Handlers
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, System.EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }
        #endregion

        #region Listview Event Handlers
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listLinkedRules_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ShowRule();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listLinkedRules_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ShowRule();
            }
        }
        #endregion

        #region Misc Methods
        /// <summary>
        /// 
        /// </summary>
        private void ShowRule()
        {
            if (listLinkedRules.SelectedItems.Count != 1)
            {
                return;
            }

            using (Form form = new FormRule(listLinkedRules.SelectedItems[0].Text))
            {
                form.ShowDialog(this);
            }
        }
        #endregion

        #region Form Event Handlers
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormLinkedRules_Load(object sender, System.EventArgs e)
        {
            Regex regex = new Regex("flowbits:isset,(.*?);", RegexOptions.IgnoreCase);
            Match match = regex.Match(_rule);
            if (match.Success == false)
            {
                UserInterface.DisplayMessageBox(this, "The rule does not contain the flowbits:set parameter", MessageBoxIcon.Exclamation);
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                return;
            }

            using (NPoco.Database db = new NPoco.Database(Db.GetOpenMySqlConnection(), DatabaseType.MySQL))
            {
                List<Rule> temp = db.Fetch<Rule>("SELECT * FROM rule WHERE rule LIKE @0", new object[] { string.Format("%flowbits:set,{0};%", match.Groups[1].Value.Trim()) });
                listLinkedRules.SetObjects(temp);

                if (temp.Count > 0)
                {
                    listLinkedRules.SelectedObject = temp[0];
                    olvRule.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
                    listLinkedRules.Select();
                }
            }
        }
        #endregion
    }
}
