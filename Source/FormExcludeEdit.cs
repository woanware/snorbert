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
                var data = db.Fetch<Dictionary<string, object>>(_sql.GetQuery(Sql.Query.SQL_EXCLUDES));

                if (data.Count == 0)
                {
                    UserInterface.DisplayMessageBox(this, "Unable to locate exclude", MessageBoxIcon.Exclamation);
                    return;
                }

                ipSource.Text = data[0]["ip_src"].ToString();
                if (data[0]["ip_dst"].ToString() != "0")
                {
                    ipDestination.Text = data[0]["ip_dst"].ToString();
                }
                txtRule.Text = data[0]["sig_name"].ToString();
                txtComment.Text = data[0]["comment"].ToString();

                if (((byte[])data[0]["fp"])[0] == 48)
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
