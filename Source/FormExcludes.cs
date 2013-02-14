using System;
using System.Collections.Generic;
using System.Windows.Forms;
using woanware;

namespace snorbert
{
    /// <summary>
    /// 
    /// </summary>
    public partial class FormExcludes : Form
    {
        #region Member Variables
        private Sql _sql;
        #endregion

        /// <summary>
        /// 
        /// </summary>
        public FormExcludes(Sql sql)
        {
            InitializeComponent();

            _sql = sql;

            Helper.AddListColumn(listExcludes, "Source IP", "SourceIp");
            Helper.AddListColumn(listExcludes, "Destination IP", "DestinationIp");
            Helper.AddListColumn(listExcludes, "FP", "FalsePositive");
            Helper.AddListColumn(listExcludes, "Timestamp", "Timestamp");
            Helper.AddListColumn(listExcludes, "Rule", "Rule");
            Helper.AddListColumn(listExcludes, "Comment", "Comment");

            LoadExcludes();
        }

        /// <summary>
        /// 
        /// </summary>
        private void LoadExcludes()
        {
            try
            {
                using (new HourGlass(this))
                {
                    listExcludes.ClearObjects();

                    var dbExcludes = new DbExclude();
                    var query = dbExcludes.Query(_sql.GetQuery(Sql.Query.SQL_EXCLUDES));

                    List<Exclude> excludes = new List<Exclude>();
                    foreach (var temp in query)
                    {
                        Exclude exclude = new Exclude();
                        exclude.Id = temp.id;
                        exclude.SigId = temp.sig_id;
                        exclude.SourceIp = temp.ip_src;
                        exclude.DestinationIp = temp.ip_dst;

                        if (temp.fp[0] == 48)
                        {
                            exclude.FalsePositive = false;
                        }
                        else
                        {
                            exclude.FalsePositive = true;
                        }

                        exclude.Comment = temp.comment;
                        exclude.Rule = temp.sig_name;
                        exclude.Timestamp = temp.timestamp;

                        excludes.Add(exclude);
                    }

                    listExcludes.SetObjects(excludes.ToArray());

                    if (excludes.Count > 0)
                    {
                        listExcludes.SelectedObject = excludes[0];
                    }
                }

                ResizeFilterListColumns();
                SetButtonState();
            }
            catch (Exception ex)
            {
                UserInterface.DisplayErrorMessageBox("An error occurred whilst loading the excludes" + ex.Message);
            }
        }

        #region Button Event Handlers
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (listExcludes.SelectedObjects.Count != 1)
            {
                return;
            }

            Exclude exclude = (Exclude)listExcludes.SelectedObjects[0];

            using (FormExcludeEdit formExcludeEdit = new FormExcludeEdit(_sql, exclude.Id))
            {
                if (formExcludeEdit.ShowDialog(this) == System.Windows.Forms.DialogResult.Cancel)
                {
                    return;
                }

                LoadExcludes();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, System.EventArgs e)
        {
            if (listExcludes.SelectedObjects.Count != 1)
            {
                return;
            }

            Exclude exclude = (Exclude)listExcludes.SelectedObjects[0];

            DialogResult dialogResult = MessageBox.Show(this,
                                                        "Are you sure you want to delete the exclude?",
                                                        Application.ProductName,
                                                        MessageBoxButtons.YesNo,
                                                        MessageBoxIcon.Question);

            if (dialogResult == System.Windows.Forms.DialogResult.No)
            {
                return;
            }

            try
            {
                var dbExcludes = new DbExclude();
                var temp = dbExcludes.Single(exclude.Id);
                if (temp == null)
                {
                    UserInterface.DisplayMessageBox(this, "Unable to locate exclude", MessageBoxIcon.Exclamation);
                    return;
                }

                int ret = dbExcludes.Delete(temp);
                if (ret != 1)
                {
                    UserInterface.DisplayErrorMessageBox(this, "The exclude could not be deleted");
                    return;
                }

                LoadExcludes();
            }
            catch (Exception ex)
            {
                UserInterface.DisplayErrorMessageBox("An error occurred whilst deleting the exclude" + ex.Message);
            }
        }
        #endregion

        #region ListView Event Handlers
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listFalsePositives_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            SetButtonState();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listFalsePositives_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                btnDelete_Click(this, new EventArgs());
            }
        }
        #endregion

        #region User Interface Methods
        /// <summary>
        /// Resizes the filter list's columns
        /// </summary>
        private void ResizeFilterListColumns()
        {
            if (listExcludes.Items.Count == 0)
            {
                listExcludes.Columns[0].AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize);
                listExcludes.Columns[1].AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize);
                listExcludes.Columns[2].AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize);
                listExcludes.Columns[3].AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize);
            }
            else
            {
                listExcludes.Columns[0].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
                listExcludes.Columns[1].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
                listExcludes.Columns[2].AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize);
                listExcludes.Columns[3].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void SetButtonState()
        {
            if (listExcludes.Items.Count == 0)
            {
                btnDelete.Enabled = false;
                return;
            }

            if (listExcludes.SelectedObjects.Count == 0)
            {
                btnDelete.Enabled = false;
                return;
            }

            if (listExcludes.SelectedObjects.Count == 1)
            {
                btnDelete.Enabled = true;
                return;
            }
        }
        #endregion
    }
}
