using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;
using System.Linq;
using woanware;
using snorbert.Data;
using System;

namespace snorbert.Forms
{
    /// <summary>
    /// 
    /// </summary>
    public partial class FormAcknowledgment : Form
    {
        private List<Event> _events;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="acknowledgmentClasses"></param>
        /// <param name="events"></param>
        /// <param name="rule"></param>
        /// <param name="initials"></param>
        public FormAcknowledgment(List<AcknowledgmentClass> acknowledgmentClasses,
                                  List<Event> events, 
                                  string rule, 
                                  string initials)
        {
            InitializeComponent();

            cboClassification.Items.Clear();
            cboClassification.DisplayMember = "Desc";
            cboClassification.ValueMember = "Id";
            cboClassification.Items.AddRange(acknowledgmentClasses.ToArray());

            _events = events;
            txtRule.Text = rule;
            txtInitials.Text = initials.ToUpper();

            if (txtInitials.Text.Length == 0)
            {
                txtInitials.Select();
            }
            else
            {
                cboClassification.Select();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Initials
        {
            get
            {
                return txtInitials.Text.ToUpper();
            }
        }

        #region Button Event Handlers
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOk_Click(object sender, System.EventArgs e)
        {
            if (txtInitials.Text.Trim().Length == 0)
            {
                UserInterface.DisplayMessageBox(this, "The initials must be supplied", MessageBoxIcon.Exclamation);
                txtInitials.Select();
                return;
            }

            if (cboClassification.SelectedIndex == -1)
            {
                UserInterface.DisplayMessageBox(this, "The classification must be selected", MessageBoxIcon.Exclamation);
                cboClassification.Select();
                return;
            }

            Process();
        }

        /// <summary>
        /// 
        /// </summary>
        private void Process()
        {
            AcknowledgmentClass acknowledgmentClass = (AcknowledgmentClass)cboClassification.Items[cboClassification.SelectedIndex];
            string initials = txtInitials.Text.ToUpper();
            string notes = txtNotes.Text;

            btnOk.Enabled = false;
            btnCancel.Enabled = false;

            (new Thread(() =>
            {
                bool acknowledgedPrevious = false;
                bool errors = false;
                using (new HourGlass(this))
                using (NPoco.Database db = new NPoco.Database(Db.GetOpenMySqlConnection()))
                {
                    db.BeginTransaction();
                    foreach (Event temp in _events)
                    {
                        try
                        {
                            bool insert = true;
                            var ack = db.Fetch<Acknowledgment>("select * from acknowledgment where cid=@0 and sid=@1", new object[] { temp.Cid, temp.Sid });
                            if (ack.Count() > 0)
                            {
                                if (ack.First().Initials.ToUpper() != initials)
                                {
                                    acknowledgedPrevious = true;
                                    insert = false;
                                }
                                else
                                {
                                    db.Delete(ack.First());
                                }
                            }

                            if (insert == true)
                            {
                                Acknowledgment acknowledgment = new Acknowledgment();
                                acknowledgment.Cid = temp.Cid;
                                acknowledgment.Sid = temp.Sid;
                                acknowledgment.Initials = initials;
                                acknowledgment.Notes = notes;
                                acknowledgment.Class = acknowledgmentClass.Id;
                                acknowledgment.Timestamp = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

                                db.Insert(acknowledgment);
                            }
                        }
                        catch (Exception ex)
                        {
                            errors = true;
                            IO.WriteTextToFile("Acknowledgement Insert Error: " + ex.ToString() + Environment.NewLine, System.IO.Path.Combine(Misc.GetUserDataDirectory(), "Errors.txt"), true); 
                        }
                    }

                    db.CompleteTransaction();
                }

                if (acknowledgedPrevious == true)
                {
                    UserInterface.DisplayMessageBox(this, 
                                                    "Some events were not classified due to being already classified", 
                                                    MessageBoxIcon.Exclamation);
                }

                if (errors == true)
                {
                    UserInterface.DisplayMessageBox(this,
                                                    "Errors occured, check the Errors.txt file",
                                                    MessageBoxIcon.Exclamation);
                }

                this.DialogResult = DialogResult.OK;
            })).Start();
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
        #endregion
    }
}
