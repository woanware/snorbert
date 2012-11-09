using System;
using System.Windows.Forms;
using System.Linq;
using woanware;

namespace snorbert
{
    /// <summary>
    /// 
    /// </summary>
    public partial class FormConnection : Form
    {
        #region Member Variables
        private Connections _connections;
        private Connection _connection;
        #endregion

        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connections"></param>
        public FormConnection()
        {
            InitializeComponent();

            using (new HourGlass(this))
            {
                _connections = new Connections();
                string ret = _connections.Load();
                if (ret.Length > 0)
                {
                    UserInterface.DisplayErrorMessageBox(this, "An error occurred whilst loading the connections");
                    return;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connections"></param>
        public FormConnection(Connection connection) : this()
        {
            _connection = connection;
            txtName.Text = connection.Name;
            txtConnectionString.Text = connection.ConnectionString;
        }
        #endregion

        #region Button Event Handlers
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTest_Click(object sender, EventArgs e)
        {
            if (txtConnectionString.Text.Trim().Length == 0)
            {
                UserInterface.DisplayMessageBox(this, "The connection string must be entered", MessageBoxIcon.Exclamation);
                txtConnectionString.Select();
                return;
            }

            try
            {
                using (MySql.Data.MySqlClient.MySqlConnection connection = new MySql.Data.MySqlClient.MySqlConnection(txtConnectionString.Text))
                {
                    connection.Open();
                }

                UserInterface.DisplayMessageBox(this, "The connection was successful", MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                UserInterface.DisplayErrorMessageBox(this, "The connection was not successful: " + ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOk_Click(object sender, EventArgs e)
        {
            if (txtName.Text.Trim().Length == 0)
            {
                UserInterface.DisplayMessageBox(this, "The name must be entered", MessageBoxIcon.Exclamation);
                txtName.Select();
                return;
            }

            if (txtConnectionString.Text.Trim().Length == 0)
            {
                UserInterface.DisplayMessageBox(this, "The connection string must be entered", MessageBoxIcon.Exclamation);
                txtConnectionString.Select();
                return;
            }

            if (_connection == null)
            {
                var connections = from c in _connections.Data where c.Name == txtName.Text select c;
                if (connections.Count() > 0)
                {
                    UserInterface.DisplayMessageBox(this, "A connection with the same name already exists", MessageBoxIcon.Exclamation);
                    return;
                }

                connections = from c in _connections.Data where c.ConnectionString == txtConnectionString.Text select c;
                if (connections.Count() > 0)
                {
                    UserInterface.DisplayMessageBox(this, "A connection with the same connection string already exists", MessageBoxIcon.Exclamation);
                    return;
                }
            }
            
            if (_connection == null)
            {
                Connection connection = new Connection();
                connection.Name = txtName.Text;
                connection.ConnectionString = txtConnectionString.Text;
                _connections.Data.Add(connection);
            }
            else
            {
                var temp = (from c in _connections.Data where c.Name == _connection.Name & c.ConnectionString == _connection.ConnectionString select c).SingleOrDefault();
                if (temp == null)
                {
                    UserInterface.DisplayErrorMessageBox(this, "Unable to locate connection");
                    return;
                }

                temp.Name = txtName.Text;
                temp.ConnectionString = txtConnectionString.Text;
            }
            
            string ret = _connections.Save();
            if (ret.Length > 0)
            {
                UserInterface.DisplayErrorMessageBox(this, "An error occurred whilst saving the connections: " + ret);
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
    }
}
