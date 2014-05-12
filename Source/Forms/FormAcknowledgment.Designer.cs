namespace snorbert.Forms
{
    partial class FormAcknowledgment
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAcknowledgment));
            this.lblInitials = new System.Windows.Forms.Label();
            this.lblClassification = new System.Windows.Forms.Label();
            this.lblRule = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.txtRule = new System.Windows.Forms.TextBox();
            this.txtInitials = new System.Windows.Forms.TextBox();
            this.cboClassification = new System.Windows.Forms.ComboBox();
            this.txtNotes = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblInitials
            // 
            this.lblInitials.AutoSize = true;
            this.lblInitials.Location = new System.Drawing.Point(12, 51);
            this.lblInitials.Name = "lblInitials";
            this.lblInitials.Size = new System.Drawing.Size(41, 15);
            this.lblInitials.TabIndex = 2;
            this.lblInitials.Text = "Initials";
            // 
            // lblClassification
            // 
            this.lblClassification.AutoSize = true;
            this.lblClassification.Location = new System.Drawing.Point(12, 181);
            this.lblClassification.Name = "lblClassification";
            this.lblClassification.Size = new System.Drawing.Size(77, 15);
            this.lblClassification.TabIndex = 6;
            this.lblClassification.Text = "Classification";
            // 
            // lblRule
            // 
            this.lblRule.AutoSize = true;
            this.lblRule.Location = new System.Drawing.Point(12, 9);
            this.lblRule.Name = "lblRule";
            this.lblRule.Size = new System.Drawing.Size(30, 15);
            this.lblRule.TabIndex = 0;
            this.lblRule.Text = "Rule";
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(372, 232);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.Location = new System.Drawing.Point(292, 232);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 8;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // txtRule
            // 
            this.txtRule.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRule.Location = new System.Drawing.Point(12, 26);
            this.txtRule.Name = "txtRule";
            this.txtRule.ReadOnly = true;
            this.txtRule.Size = new System.Drawing.Size(433, 23);
            this.txtRule.TabIndex = 1;
            // 
            // txtInitials
            // 
            this.txtInitials.Location = new System.Drawing.Point(12, 68);
            this.txtInitials.MaxLength = 3;
            this.txtInitials.Name = "txtInitials";
            this.txtInitials.Size = new System.Drawing.Size(48, 23);
            this.txtInitials.TabIndex = 3;
            // 
            // cboClassification
            // 
            this.cboClassification.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboClassification.FormattingEnabled = true;
            this.cboClassification.Location = new System.Drawing.Point(12, 200);
            this.cboClassification.Name = "cboClassification";
            this.cboClassification.Size = new System.Drawing.Size(434, 23);
            this.cboClassification.TabIndex = 7;
            // 
            // txtNotes
            // 
            this.txtNotes.Location = new System.Drawing.Point(12, 110);
            this.txtNotes.MaxLength = 500;
            this.txtNotes.Multiline = true;
            this.txtNotes.Name = "txtNotes";
            this.txtNotes.Size = new System.Drawing.Size(434, 68);
            this.txtNotes.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 93);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 15);
            this.label1.TabIndex = 4;
            this.label1.Text = "Notes";
            // 
            // FormAcknowledgment
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(459, 266);
            this.Controls.Add(this.txtNotes);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cboClassification);
            this.Controls.Add(this.txtInitials);
            this.Controls.Add(this.txtRule);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.lblRule);
            this.Controls.Add(this.lblClassification);
            this.Controls.Add(this.lblInitials);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormAcknowledgment";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Acknowledgment";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblInitials;
        private System.Windows.Forms.Label lblClassification;
        private System.Windows.Forms.Label lblRule;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.TextBox txtRule;
        private System.Windows.Forms.TextBox txtInitials;
        private System.Windows.Forms.ComboBox cboClassification;
        private System.Windows.Forms.TextBox txtNotes;
        private System.Windows.Forms.Label label1;
    }
}