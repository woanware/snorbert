using System;
using System.Windows.Forms;
using woanware;

namespace snorbert
{
    /// <summary>
    /// 
    /// </summary>
    public partial class FormFalsePositives : Form
    {
        #region Member Variables
        private FalsePositives _falsePositives;
        #endregion

        /// <summary>
        /// 
        /// </summary>
        public FormFalsePositives()
        {
            InitializeComponent();

            Helper.AddListColumn(listFalsePositives, "SID", "Sid");
            Helper.AddListColumn(listFalsePositives, "Field", "Field");
            Helper.AddListColumn(listFalsePositives, "Condition", "Condition");
            Helper.AddListColumn(listFalsePositives, "Value", "Display");

            LoadFalsePositives();
        }

        /// <summary>
        /// 
        /// </summary>
        private void LoadFalsePositives()
        {
            _falsePositives = new FalsePositives();
            string ret = _falsePositives.Load();
            if (ret.Length > 0)
            {
                UserInterface.DisplayErrorMessageBox(this, "An error occurred whilst loading the false positive data: " + ret);
                Misc.WriteToEventLog(Application.ProductName, "An error occurred whilst loading the false positive data: " + ret, System.Diagnostics.EventLogEntryType.Error);
            }

            listFalsePositives.ClearObjects();
            listFalsePositives.SetObjects(_falsePositives.Data);

            if (_falsePositives.Data.Count > 0)
            {
                listFalsePositives.SelectedObject = _falsePositives.Data[0];
            }

            ResizeFilterListColumns();
            SetButtonState();
        }

        #region Button Event Handlers
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, System.EventArgs e)
        {
            if (listFalsePositives.SelectedObjects.Count != 1)
            {
                return;
            }

            FalsePositive falsePositive = (FalsePositive)listFalsePositives.SelectedObjects[0];

            DialogResult dialogResult = MessageBox.Show(this,
                                                        "Are you sure you want to delete the false positive? " + Environment.NewLine + Environment.NewLine +
                                                        "SID: " + falsePositive.Sid + Environment.NewLine +
                                                        "Field: " + falsePositive.Definition.Field + Environment.NewLine +
                                                        "Condition: " + falsePositive.Condition + Environment.NewLine +
                                                        "Value: " + falsePositive.Display,
                                                        Application.ProductName,
                                                        MessageBoxButtons.YesNo,
                                                        MessageBoxIcon.Question);

            if (dialogResult == System.Windows.Forms.DialogResult.No)
            {
                return;
            }

            int numRemoved = _falsePositives.Data.RemoveAll(f => f.Id == falsePositive.Id);
            if (numRemoved == 0)
            {
                UserInterface.DisplayErrorMessageBox(this, "The false positive could not be removed");
                return;
            }

            string ret = _falsePositives.Save();
            if (ret.Length > 0)
            {
                UserInterface.DisplayErrorMessageBox(this, "An error occurred whilst saving the false positives");
                return;
            }

            LoadFalsePositives();
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
            if (listFalsePositives.Items.Count == 0)
            {
                listFalsePositives.Columns[0].AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize);
                listFalsePositives.Columns[1].AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize);
                listFalsePositives.Columns[2].AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize);
                listFalsePositives.Columns[3].AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize);
            }
            else
            {
                listFalsePositives.Columns[0].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
                listFalsePositives.Columns[1].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
                listFalsePositives.Columns[2].AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize);
                listFalsePositives.Columns[3].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void SetButtonState()
        {
            if (listFalsePositives.Items.Count == 0)
            {
                btnDelete.Enabled = false;
                return;
            }

            if (listFalsePositives.SelectedObjects.Count == 0)
            {
                btnDelete.Enabled = false;
                return;
            }

            if (listFalsePositives.SelectedObjects.Count == 1)
            {
                btnDelete.Enabled = true;
                return;
            }
        }
        #endregion
    }
}
