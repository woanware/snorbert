using System;
using System.Dynamic;
using System.Net;
using System.Windows.Forms;
using System.Linq;
using woanware;

namespace snorbert
{
    /// <summary>
    /// Window to allow the creation of a False Positive record
    /// </summary>
    public partial class FormExcludeEdit : Form
    {
        #region Member Variables
        private long _id = 0;
        private Sql _sql;
        #endregion

        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="id"></param>
        public FormExcludeEdit(Sql sql, long id)
        {
            InitializeComponent();

            _sql = sql;
            _id = id;

            try
            {
                var dbExcludes = new DbExclude();
                var temp = dbExcludes.Query(_sql.GetQuery(Sql.Query.SQL_EXCLUDE), args: new object[] { _id });
                if (temp.Count() == 0)
                {
                    UserInterface.DisplayMessageBox(this, "Unable to locate exclude", MessageBoxIcon.Exclamation);
                    return;
                }

                ipSource.Text = temp.First().ip_src;
                ipDestination.Text = temp.First().ip_dst;
                txtRule.Text = temp.First().sig_name;
                txtComment.Text = temp.First().comment;
                if (temp.First().fp[0] == 48)
                {
                    chkFalsePositive.Checked = false;
                }
                else
                {
                    chkFalsePositive.Checked = true;
                }
            }
            catch (Exception ex)
            {
                UserInterface.DisplayErrorMessageBox("An error occurred whilst loading the exclude: " + ex.Message);
            }
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
                var dbExcludes = new DbExclude();
                var temp = dbExcludes.Single(_id);
                if (temp == null)
                {
                    UserInterface.DisplayMessageBox(this, "Unable to locate exclude", MessageBoxIcon.Exclamation);
                    return;
                }

                if (chkFalsePositive.Checked == true)
                {
                    temp.fp = 1;
                }
                else
                {
                    temp.fp = 0;
                }

                temp.comment = txtComment.Text;

                dbExcludes.Update(temp, temp.id);

                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            catch (Exception ex)
            {
                UserInterface.DisplayErrorMessageBox("An error occurred whilst editing the exclude: " + ex.Message);
            }
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
    }
}
