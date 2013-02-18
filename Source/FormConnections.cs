using System;
using System.Windows.Forms;
using woanware;

namespace snorbert
{
    /// <summary>
    /// 
    /// </summary>
    public partial class FormConnections : Form
    {
        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        public FormConnections()
        {
            InitializeComponent();
            LoadConnections();
        }
        #endregion

        #region Button Event Handlers
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (FormConnection formConnection = new FormConnection())
            {
                if (formConnection.ShowDialog(this) == System.Windows.Forms.DialogResult.Cancel)
                {
                    return;
                }

                LoadConnections();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (listConnections.SelectedObjects.Count != 1)
            {
                return;
            }

            Connection connection = (Connection)listConnections.SelectedObjects[0];

            using (FormConnection formConnection = new FormConnection(connection))
            {
                if (formConnection.ShowDialog(this) == System.Windows.Forms.DialogResult.Cancel)
                {
                    return;
                }

                LoadConnections();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (listConnections.SelectedObjects.Count != 1)
            {
                return;
            }

            Connection connection = (Connection)listConnections.SelectedObjects[0];

            Connections connections = new Connections();
            string ret = connections.Load();
            if (ret.Length > 0)
            {
                UserInterface.DisplayErrorMessageBox(this, "An error occurred whilst loading the connections");
                return;
            }

            DialogResult dialogResult = MessageBox.Show(this,
                                                        "Are you sure you want to delete the connection? " + Environment.NewLine + Environment.NewLine +
                                                        "Name: " + connection.Name,
                                                        Application.ProductName,
                                                        MessageBoxButtons.YesNo,
                                                        MessageBoxIcon.Question);

            if (dialogResult == System.Windows.Forms.DialogResult.No)
            {
                return;
            }

            int numRemoved = connections.Data.RemoveAll(c => c.Name == connection.Name & c.ConnectionString == connection.ConnectionString);
            if (numRemoved == 0)
            {
                UserInterface.DisplayErrorMessageBox(this, "The connection could not be removed");
                return;
            }

            ret = connections.Save();
            if (ret.Length > 0)
            {
                UserInterface.DisplayErrorMessageBox(this, "An error occurred whilst saving the connections");
                return;
            }

            LoadConnections();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }
        #endregion

        #region Misc Methods
        /// <summary>
        /// 
        /// </summary>
        private void LoadConnections()
        {
            using (new HourGlass(this))
            {
                Connections connections = new Connections();
                string ret = connections.Load();
                if (ret.Length > 0)
                {
                    UserInterface.DisplayErrorMessageBox(this, "An error occurred whilst loading the connections");
                    return;
                }

                listConnections.SetObjects(connections.Data);
                SetButtonState();

                if (connections.Data.Count > 0)
                {
                    olvcConnectionString.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
                    listConnections.SelectedObject = connections.Data[0];
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void SetButtonState()
        {
            if (listConnections.Items.Count == 0)
            {
                btnEdit.Enabled = false;
                btnDelete.Enabled = false;
                return;
            }

            if (listConnections.SelectedObjects.Count == 0)
            {
                btnEdit.Enabled = false;
                btnDelete.Enabled = false;
                return;
            }

            if (listConnections.SelectedObjects.Count == 1)
            {
                btnEdit.Enabled = true;
                btnDelete.Enabled = true;
                return;
            }
        }
        #endregion

        #region Listview Event Handlers
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listConnections_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetButtonState();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listConnections_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnEdit_Click(this, new EventArgs());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listConnections_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnEdit_Click(this, new EventArgs());
            }
            else if (e.KeyCode == Keys.Delete)
            {
                btnDelete_Click(this, new EventArgs());
            }
        }
        #endregion
    }
}
