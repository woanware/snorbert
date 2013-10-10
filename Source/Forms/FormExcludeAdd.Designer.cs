using IPAddressControlLib;

namespace snorbert.Forms
{
    partial class FormExcludeAdd
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormExcludeAdd));
            this.ipSource = new IPAddressControlLib.IPAddressControl();
            this.ipDestination = new IPAddressControlLib.IPAddressControl();
            this.lblSourceIp = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblRule = new System.Windows.Forms.Label();
            this.txtRule = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.chkSourceIp = new System.Windows.Forms.CheckBox();
            this.chkDestinationIp = new System.Windows.Forms.CheckBox();
            this.chkRule = new System.Windows.Forms.CheckBox();
            this.lblComment = new System.Windows.Forms.Label();
            this.txtComment = new System.Windows.Forms.TextBox();
            this.chkFalsePositive = new System.Windows.Forms.CheckBox();
            this.txtSourcePort = new System.Windows.Forms.TextBox();
            this.txtDestinationPort = new System.Windows.Forms.TextBox();
            this.chkSrcPort = new System.Windows.Forms.CheckBox();
            this.chkDestPort = new System.Windows.Forms.CheckBox();
            this.lblProtocol = new System.Windows.Forms.Label();
            this.txtProtocol = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // ipSource
            // 
            this.ipSource.AllowInternalTab = false;
            this.ipSource.AutoHeight = true;
            this.ipSource.BackColor = System.Drawing.SystemColors.Window;
            this.ipSource.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ipSource.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.ipSource.Location = new System.Drawing.Point(36, 25);
            this.ipSource.MinimumSize = new System.Drawing.Size(84, 23);
            this.ipSource.Name = "ipSource";
            this.ipSource.ReadOnly = true;
            this.ipSource.Size = new System.Drawing.Size(87, 23);
            this.ipSource.TabIndex = 2;
            this.ipSource.Text = "...";
            // 
            // ipDestination
            // 
            this.ipDestination.AllowInternalTab = false;
            this.ipDestination.AutoHeight = true;
            this.ipDestination.BackColor = System.Drawing.SystemColors.Window;
            this.ipDestination.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ipDestination.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.ipDestination.Location = new System.Drawing.Point(233, 25);
            this.ipDestination.MinimumSize = new System.Drawing.Size(84, 23);
            this.ipDestination.Name = "ipDestination";
            this.ipDestination.ReadOnly = true;
            this.ipDestination.Size = new System.Drawing.Size(87, 23);
            this.ipDestination.TabIndex = 6;
            this.ipDestination.Text = "...";
            // 
            // lblSourceIp
            // 
            this.lblSourceIp.AutoSize = true;
            this.lblSourceIp.Location = new System.Drawing.Point(34, 8);
            this.lblSourceIp.Name = "lblSourceIp";
            this.lblSourceIp.Size = new System.Drawing.Size(43, 15);
            this.lblSourceIp.TabIndex = 0;
            this.lblSourceIp.Text = "Source";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(237, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 15);
            this.label2.TabIndex = 4;
            this.label2.Text = "Destination";
            // 
            // lblRule
            // 
            this.lblRule.AutoSize = true;
            this.lblRule.Location = new System.Drawing.Point(34, 55);
            this.lblRule.Name = "lblRule";
            this.lblRule.Size = new System.Drawing.Size(30, 15);
            this.lblRule.TabIndex = 9;
            this.lblRule.Text = "Rule";
            // 
            // txtRule
            // 
            this.txtRule.Location = new System.Drawing.Point(36, 74);
            this.txtRule.Name = "txtRule";
            this.txtRule.ReadOnly = true;
            this.txtRule.Size = new System.Drawing.Size(471, 23);
            this.txtRule.TabIndex = 11;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(479, 181);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 15;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.Location = new System.Drawing.Point(399, 181);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 14;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // chkSourceIp
            // 
            this.chkSourceIp.AutoSize = true;
            this.chkSourceIp.Location = new System.Drawing.Point(16, 30);
            this.chkSourceIp.Name = "chkSourceIp";
            this.chkSourceIp.Size = new System.Drawing.Size(15, 14);
            this.chkSourceIp.TabIndex = 1;
            this.chkSourceIp.UseVisualStyleBackColor = true;
            this.chkSourceIp.CheckedChanged += new System.EventHandler(this.chkSourceIp_CheckedChanged);
            // 
            // chkDestinationIp
            // 
            this.chkDestinationIp.AutoSize = true;
            this.chkDestinationIp.Location = new System.Drawing.Point(213, 30);
            this.chkDestinationIp.Name = "chkDestinationIp";
            this.chkDestinationIp.Size = new System.Drawing.Size(15, 14);
            this.chkDestinationIp.TabIndex = 5;
            this.chkDestinationIp.UseVisualStyleBackColor = true;
            this.chkDestinationIp.CheckedChanged += new System.EventHandler(this.chkDestinationIp_CheckedChanged);
            // 
            // chkRule
            // 
            this.chkRule.AutoSize = true;
            this.chkRule.Checked = true;
            this.chkRule.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkRule.Location = new System.Drawing.Point(16, 78);
            this.chkRule.Name = "chkRule";
            this.chkRule.Size = new System.Drawing.Size(15, 14);
            this.chkRule.TabIndex = 10;
            this.chkRule.UseVisualStyleBackColor = true;
            this.chkRule.CheckedChanged += new System.EventHandler(this.chkRule_CheckedChanged);
            // 
            // lblComment
            // 
            this.lblComment.AutoSize = true;
            this.lblComment.Location = new System.Drawing.Point(34, 102);
            this.lblComment.Name = "lblComment";
            this.lblComment.Size = new System.Drawing.Size(61, 15);
            this.lblComment.TabIndex = 12;
            this.lblComment.Text = "Comment";
            // 
            // txtComment
            // 
            this.txtComment.Location = new System.Drawing.Point(36, 121);
            this.txtComment.Multiline = true;
            this.txtComment.Name = "txtComment";
            this.txtComment.Size = new System.Drawing.Size(471, 52);
            this.txtComment.TabIndex = 13;
            // 
            // chkFalsePositive
            // 
            this.chkFalsePositive.AutoSize = true;
            this.chkFalsePositive.Checked = true;
            this.chkFalsePositive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkFalsePositive.Location = new System.Drawing.Point(464, 28);
            this.chkFalsePositive.Name = "chkFalsePositive";
            this.chkFalsePositive.Size = new System.Drawing.Size(96, 19);
            this.chkFalsePositive.TabIndex = 8;
            this.chkFalsePositive.Text = "False Positive";
            this.chkFalsePositive.UseVisualStyleBackColor = true;
            // 
            // txtSourcePort
            // 
            this.txtSourcePort.Location = new System.Drawing.Point(148, 25);
            this.txtSourcePort.MaxLength = 5;
            this.txtSourcePort.Name = "txtSourcePort";
            this.txtSourcePort.ReadOnly = true;
            this.txtSourcePort.Size = new System.Drawing.Size(49, 23);
            this.txtSourcePort.TabIndex = 16;
            // 
            // txtDestinationPort
            // 
            this.txtDestinationPort.Location = new System.Drawing.Point(346, 25);
            this.txtDestinationPort.MaxLength = 5;
            this.txtDestinationPort.Name = "txtDestinationPort";
            this.txtDestinationPort.ReadOnly = true;
            this.txtDestinationPort.Size = new System.Drawing.Size(49, 23);
            this.txtDestinationPort.TabIndex = 17;
            // 
            // chkSrcPort
            // 
            this.chkSrcPort.AutoSize = true;
            this.chkSrcPort.Location = new System.Drawing.Point(129, 30);
            this.chkSrcPort.Name = "chkSrcPort";
            this.chkSrcPort.Size = new System.Drawing.Size(15, 14);
            this.chkSrcPort.TabIndex = 18;
            this.chkSrcPort.UseVisualStyleBackColor = true;
            this.chkSrcPort.CheckedChanged += new System.EventHandler(this.chkSrcPort_CheckedChanged);
            // 
            // chkDestPort
            // 
            this.chkDestPort.AutoSize = true;
            this.chkDestPort.Location = new System.Drawing.Point(326, 30);
            this.chkDestPort.Name = "chkDestPort";
            this.chkDestPort.Size = new System.Drawing.Size(15, 14);
            this.chkDestPort.TabIndex = 19;
            this.chkDestPort.UseVisualStyleBackColor = true;
            this.chkDestPort.CheckedChanged += new System.EventHandler(this.chkDestPort_CheckedChanged);
            // 
            // lblProtocol
            // 
            this.lblProtocol.AutoSize = true;
            this.lblProtocol.Location = new System.Drawing.Point(403, 8);
            this.lblProtocol.Name = "lblProtocol";
            this.lblProtocol.Size = new System.Drawing.Size(52, 15);
            this.lblProtocol.TabIndex = 20;
            this.lblProtocol.Text = "Protocol";
            // 
            // txtProtocol
            // 
            this.txtProtocol.Location = new System.Drawing.Point(406, 25);
            this.txtProtocol.MaxLength = 5;
            this.txtProtocol.Name = "txtProtocol";
            this.txtProtocol.ReadOnly = true;
            this.txtProtocol.Size = new System.Drawing.Size(49, 23);
            this.txtProtocol.TabIndex = 21;
            // 
            // FormExcludeAdd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(565, 215);
            this.Controls.Add(this.txtProtocol);
            this.Controls.Add(this.lblProtocol);
            this.Controls.Add(this.chkDestPort);
            this.Controls.Add(this.chkSrcPort);
            this.Controls.Add(this.txtDestinationPort);
            this.Controls.Add(this.txtSourcePort);
            this.Controls.Add(this.chkFalsePositive);
            this.Controls.Add(this.txtComment);
            this.Controls.Add(this.lblComment);
            this.Controls.Add(this.chkRule);
            this.Controls.Add(this.chkDestinationIp);
            this.Controls.Add(this.chkSourceIp);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.txtRule);
            this.Controls.Add(this.lblRule);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblSourceIp);
            this.Controls.Add(this.ipDestination);
            this.Controls.Add(this.ipSource);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormExcludeAdd";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Exclude";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private IPAddressControlLib.IPAddressControl ipSource;
        private IPAddressControlLib.IPAddressControl ipDestination;
        private System.Windows.Forms.Label lblSourceIp;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblRule;
        private System.Windows.Forms.TextBox txtRule;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.CheckBox chkSourceIp;
        private System.Windows.Forms.CheckBox chkDestinationIp;
        private System.Windows.Forms.CheckBox chkRule;
        private System.Windows.Forms.Label lblComment;
        private System.Windows.Forms.TextBox txtComment;
        private System.Windows.Forms.CheckBox chkFalsePositive;
        private System.Windows.Forms.TextBox txtSourcePort;
        private System.Windows.Forms.TextBox txtDestinationPort;
        private System.Windows.Forms.CheckBox chkSrcPort;
        private System.Windows.Forms.CheckBox chkDestPort;
        private System.Windows.Forms.Label lblProtocol;
        private System.Windows.Forms.TextBox txtProtocol;

    }
}