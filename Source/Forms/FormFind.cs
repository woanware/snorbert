using System;
using System.Windows.Forms;

namespace snorbert.Forms
{
    /// <summary>
    /// 
    /// </summary>
    public partial class FormFind : Form
    {
        #region Member Variables
        private Form _form;
        private RichTextBox _richTextBox = null;
        private int _position;
        #endregion

        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        /// <param name="richTextBox"></param>
        public FormFind(Form form, RichTextBox richTextBox)
        {
            InitializeComponent();
            _form = form;
            _richTextBox = richTextBox;
        }
        #endregion

        #region Button Event Handlers
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFind_Click(object sender, EventArgs e)
        {
            this.DoSearch(true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Find Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="isNewSearch"></param>
        private void DoSearch(bool isNewSearch)
        {
            RichTextBoxFinds richTextBoxFinds = RichTextBoxFinds.None;

            if (chkMatchCase.Checked == true)
            {
                richTextBoxFinds |= RichTextBoxFinds.MatchCase;
            }

            if (chkMatchWholeWord.Checked == true)
            {
                richTextBoxFinds |= RichTextBoxFinds.WholeWord;
            }

            if (chkSearchUpwards.Checked == true)
            {
                richTextBoxFinds |= RichTextBoxFinds.Reverse;
            }

            _richTextBox.Find(txtFind.Text, _position, richTextBoxFinds);

            int temp = _richTextBox.SelectionStart + _richTextBox.SelectionLength;
            _richTextBox.ScrollToCaret();

            if (temp == _position)
            {
                MessageBox.Show(this,
                                "The end of the text has been reached.",
                                Application.ProductName,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);

                if (_position != 0)
                {
                    _position = 0;
                    this.DoSearch(false);
                }
            }
            else
            {
                _position = temp;
            }

            _form.Select();
        }
        #endregion

        #region Form Event Handlers
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormFind_Load(object sender, EventArgs e)
        {
            txtFind.Select();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormFind_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F3)
            {
                this.DoSearch(false);
            }
            else if (e.KeyCode == Keys.Enter)
            {
                this.DoSearch(false);
            }
        }
        #endregion 

        #region Public Methods
        /// <summary>
        /// 
        /// </summary>
        public void DoSearch()
        {
            DoSearch(false);
        }
        #endregion
    }
}
