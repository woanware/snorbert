using System;
using System.Dynamic;
using System.Net;
using System.Windows.Forms;
using System.Linq;
using woanware;
using System.Collections.Generic;

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
                NPoco.Database db = new NPoco.Database(Db.GetOpenMySqlConnection());
                List<Exclude> excludes = db.Fetch<Exclude>(_sql.GetQuery(Sql.Query.SQL_EXCLUDE), new object[] { _id });

                if (excludes.Count == 0)
                {
                    UserInterface.DisplayMessageBox(this, "Unable to locate exclude", MessageBoxIcon.Exclamation);
                    return;
                }

                ipSource.Text = excludes[0].SourceIpText;
                ipDestination.Text = excludes[0].DestinationIpText;
                txtRule.Text = excludes[0].Rule;
                txtComment.Text = excludes[0].Comment;
                chkFalsePositive.Checked = excludes[0].FalsePositive;
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
                NPoco.Database db = new NPoco.Database(Db.GetOpenMySqlConnection());
                Exclude exclude = db.SingleOrDefaultById<Exclude>(_id);
                if (exclude == null)
                {
                    UserInterface.DisplayMessageBox(this, "Unable to locate exclude", MessageBoxIcon.Exclamation);
                    return;
                }

                if (chkFalsePositive.Checked == true)
                {   
                    exclude.FalsePositive = true;
                }
                else
                {
                    exclude.FalsePositive = false;
                }

                exclude.Comment = txtComment.Text;
                db.Update(exclude);

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
