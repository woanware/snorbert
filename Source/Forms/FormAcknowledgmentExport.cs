using System.Windows.Forms;
using snorbert.Configs;
using woanware;

namespace snorbert.Forms
{
    /// <summary>
    /// 
    /// </summary>
    public partial class FormAcknowledgmentExport : Form
    {
        #region Member Variables
        private HourGlass _hourGlass;
        private Exporter _exporter;
        private Sql _sql;
        #endregion

        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        public FormAcknowledgmentExport(Sql sql)
        {
            InitializeComponent();

            cboTimeFrom.SelectedIndex = 0;
            cboTimeTo.SelectedIndex = 0;
            dtpDateTo.Checked = false;

            _sql = sql;

            _exporter = new Exporter();
            _exporter.SetSql(_sql);
            _exporter.Complete += OnExporter_Complete;
            _exporter.Error += OnExporter_Error;
            _exporter.Exclamation += OnExporter_Exclamation;
        }
        #endregion

        #region Exporter Event Handlers
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        private void OnExporter_Exclamation(string message)
        {
            _hourGlass.Dispose();
            UserInterface.DisplayMessageBox(this, message, MessageBoxIcon.Exclamation);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        private void OnExporter_Error(string message)
        {
            _hourGlass.Dispose();
            UserInterface.DisplayErrorMessageBox(this, message);
        }

        /// <summary>
        /// 
        /// </summary>
        private void OnExporter_Complete()
        {
            _hourGlass.Dispose();
            UserInterface.DisplayMessageBox(this, "Export complete", MessageBoxIcon.Information);
        }
        #endregion

        #region Button Event Handlers
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOk_Click(object sender, System.EventArgs e)
        {
            if (txtOutputFile.Text.Trim().Length == 0)
            {
                UserInterface.DisplayMessageBox(this, "The output file must be selected", MessageBoxIcon.Exclamation);
                btnOutputFile.Select();
                return;
            }

            _hourGlass = new HourGlass(this);


            if (dtpDateTo.Checked == true)
            {
                if (txtInitials.Text.Trim().Length == 0)
                {
                     _exporter.ExportAcknowledgmentsFromToAll(txtOutputFile.Text, 
                                                              dtpDateFrom.Value.Date.ToString("yyyy-MM-dd") + " " + cboTimeFrom.Text + ":00",
                                                              dtpDateTo.Value.Date.ToString("yyyy-MM-dd") + " " + cboTimeTo.Text + ":00");
                }
                else
                {
                     _exporter.ExportAcknowledgmentsFromTo(txtOutputFile.Text, 
                                                           dtpDateFrom.Value.Date.ToString("yyyy-MM-dd") + " " + cboTimeFrom.Text + ":00",
                                                           dtpDateTo.Value.Date.ToString("yyyy-MM-dd") + " " + cboTimeTo.Text + ":00",
                                                           txtInitials.Text);
                }
            }
            else
            {
                if (txtInitials.Text.Trim().Length == 0)
                {
                     _exporter.ExportAcknowledgmentsFromAll(txtOutputFile.Text, 
                                                            dtpDateFrom.Value.Date.ToString("yyyy-MM-dd") + " " + cboTimeFrom.Text + ":00");
                }
                else
                {
                     _exporter.ExportAcknowledgmentsFrom(txtOutputFile.Text, 
                                                         dtpDateFrom.Value.Date.ToString("yyyy-MM-dd") + " " + cboTimeFrom.Text + ":00",
                                                         txtInitials.Text);
                }                            
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, System.EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void butOutputFile_Click(object sender, System.EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "Select the export CSV";
            saveFileDialog.Filter = "TSV Files|*.tsv";
            if (saveFileDialog.ShowDialog(this) == DialogResult.Cancel)
            {
                return;
            }

            txtOutputFile.Text = saveFileDialog.FileName;
        }
        #endregion
    }
}
