﻿namespace snorbert.Forms
{
    partial class FormExcludeEdit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormExcludeEdit));
            this.ipSource = new IPAddressControlLib.IPAddressControl();
            this.ipDestination = new IPAddressControlLib.IPAddressControl();
            this.lblSourceIp = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblRule = new System.Windows.Forms.Label();
            this.txtRule = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.lblComment = new System.Windows.Forms.Label();
            this.txtComment = new System.Windows.Forms.TextBox();
            this.chkFalsePositive = new System.Windows.Forms.CheckBox();
            this.txtDestinationPort = new System.Windows.Forms.TextBox();
            this.txtSourcePort = new System.Windows.Forms.TextBox();
            this.txtProtocol = new System.Windows.Forms.TextBox();
            this.lblProtocol = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ipSource
            // 
            this.ipSource.AllowInternalTab = false;
            this.ipSource.AutoHeight = true;
            this.ipSource.BackColor = System.Drawing.SystemColors.Window;
            this.ipSource.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ipSource.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.ipSource.Location = new System.Drawing.Point(10, 25);
            this.ipSource.MinimumSize = new System.Drawing.Size(84, 23);
            this.ipSource.Name = "ipSource";
            this.ipSource.ReadOnly = true;
            this.ipSource.Size = new System.Drawing.Size(87, 23);
            this.ipSource.TabIndex = 2;
            this.ipSource.TabStop = false;
            this.ipSource.Text = "...";
            // 
            // ipDestination
            // 
            this.ipDestination.AllowInternalTab = false;
            this.ipDestination.AutoHeight = true;
            this.ipDestination.BackColor = System.Drawing.SystemColors.Window;
            this.ipDestination.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ipDestination.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.ipDestination.Location = new System.Drawing.Point(155, 25);
            this.ipDestination.MinimumSize = new System.Drawing.Size(84, 23);
            this.ipDestination.Name = "ipDestination";
            this.ipDestination.ReadOnly = true;
            this.ipDestination.Size = new System.Drawing.Size(87, 23);
            this.ipDestination.TabIndex = 3;
            this.ipDestination.TabStop = false;
            this.ipDestination.Text = "...";
            // 
            // lblSourceIp
            // 
            this.lblSourceIp.AutoSize = true;
            this.lblSourceIp.Location = new System.Drawing.Point(8, 6);
            this.lblSourceIp.Name = "lblSourceIp";
            this.lblSourceIp.Size = new System.Drawing.Size(43, 15);
            this.lblSourceIp.TabIndex = 0;
            this.lblSourceIp.Text = "Source";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(153, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "Destination";
            // 
            // lblRule
            // 
            this.lblRule.AutoSize = true;
            this.lblRule.Location = new System.Drawing.Point(8, 55);
            this.lblRule.Name = "lblRule";
            this.lblRule.Size = new System.Drawing.Size(30, 15);
            this.lblRule.TabIndex = 5;
            this.lblRule.Text = "Rule";
            // 
            // txtRule
            // 
            this.txtRule.Location = new System.Drawing.Point(10, 74);
            this.txtRule.Name = "txtRule";
            this.txtRule.ReadOnly = true;
            this.txtRule.Size = new System.Drawing.Size(497, 23);
            this.txtRule.TabIndex = 6;
            this.txtRule.TabStop = false;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(433, 181);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.Location = new System.Drawing.Point(353, 181);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 9;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // lblComment
            // 
            this.lblComment.AutoSize = true;
            this.lblComment.Location = new System.Drawing.Point(9, 102);
            this.lblComment.Name = "lblComment";
            this.lblComment.Size = new System.Drawing.Size(61, 15);
            this.lblComment.TabIndex = 7;
            this.lblComment.Text = "Comment";
            // 
            // txtComment
            // 
            this.txtComment.Location = new System.Drawing.Point(10, 121);
            this.txtComment.Multiline = true;
            this.txtComment.Name = "txtComment";
            this.txtComment.Size = new System.Drawing.Size(497, 52);
            this.txtComment.TabIndex = 8;
            // 
            // chkFalsePositive
            // 
            this.chkFalsePositive.AutoSize = true;
            this.chkFalsePositive.Checked = true;
            this.chkFalsePositive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkFalsePositive.Location = new System.Drawing.Point(357, 28);
            this.chkFalsePositive.Name = "chkFalsePositive";
            this.chkFalsePositive.Size = new System.Drawing.Size(96, 19);
            this.chkFalsePositive.TabIndex = 4;
            this.chkFalsePositive.Text = "False Positive";
            this.chkFalsePositive.UseVisualStyleBackColor = true;
            // 
            // txtDestinationPort
            // 
            this.txtDestinationPort.Location = new System.Drawing.Point(247, 25);
            this.txtDestinationPort.Name = "txtDestinationPort";
            this.txtDestinationPort.ReadOnly = true;
            this.txtDestinationPort.Size = new System.Drawing.Size(43, 23);
            this.txtDestinationPort.TabIndex = 11;
            // 
            // txtSourcePort
            // 
            this.txtSourcePort.Location = new System.Drawing.Point(102, 25);
            this.txtSourcePort.Name = "txtSourcePort";
            this.txtSourcePort.ReadOnly = true;
            this.txtSourcePort.Size = new System.Drawing.Size(43, 23);
            this.txtSourcePort.TabIndex = 12;
            // 
            // txtProtocol
            // 
            this.txtProtocol.Location = new System.Drawing.Point(299, 25);
            this.txtProtocol.MaxLength = 5;
            this.txtProtocol.Name = "txtProtocol";
            this.txtProtocol.ReadOnly = true;
            this.txtProtocol.Size = new System.Drawing.Size(49, 23);
            this.txtProtocol.TabIndex = 23;
            // 
            // lblProtocol
            // 
            this.lblProtocol.AutoSize = true;
            this.lblProtocol.Location = new System.Drawing.Point(296, 8);
            this.lblProtocol.Name = "lblProtocol";
            this.lblProtocol.Size = new System.Drawing.Size(52, 15);
            this.lblProtocol.TabIndex = 22;
            this.lblProtocol.Text = "Protocol";
            // 
            // FormExcludeEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(519, 215);
            this.Controls.Add(this.txtProtocol);
            this.Controls.Add(this.lblProtocol);
            this.Controls.Add(this.txtSourcePort);
            this.Controls.Add(this.txtDestinationPort);
            this.Controls.Add(this.chkFalsePositive);
            this.Controls.Add(this.txtComment);
            this.Controls.Add(this.lblComment);
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
            this.Name = "FormExcludeEdit";
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
        private System.Windows.Forms.Label lblComment;
        private System.Windows.Forms.TextBox txtComment;
        private System.Windows.Forms.CheckBox chkFalsePositive;
        private System.Windows.Forms.TextBox txtDestinationPort;
        private System.Windows.Forms.TextBox txtSourcePort;
        private System.Windows.Forms.TextBox txtProtocol;
        private System.Windows.Forms.Label lblProtocol;

    }
}