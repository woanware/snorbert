namespace snorbert
{
    partial class FormConnection
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormConnection));
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.lblName = new System.Windows.Forms.Label();
            this.lblConnectionString = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtConnectionString = new System.Windows.Forms.TextBox();
            this.btnTest = new System.Windows.Forms.Button();
            this.txtCollectionName = new System.Windows.Forms.TextBox();
            this.txtNetwitnessIp = new System.Windows.Forms.TextBox();
            this.lblCollectionName = new System.Windows.Forms.Label();
            this.lblNetwitnessIp = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(336, 126);
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
            this.btnOk.Location = new System.Drawing.Point(256, 126);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 9;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(75, 14);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(39, 15);
            this.lblName.TabIndex = 0;
            this.lblName.Text = "Name";
            // 
            // lblConnectionString
            // 
            this.lblConnectionString.AutoSize = true;
            this.lblConnectionString.Location = new System.Drawing.Point(11, 42);
            this.lblConnectionString.Name = "lblConnectionString";
            this.lblConnectionString.Size = new System.Drawing.Size(103, 15);
            this.lblConnectionString.TabIndex = 2;
            this.lblConnectionString.Text = "Connection String";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(116, 11);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(294, 23);
            this.txtName.TabIndex = 1;
            // 
            // txtConnectionString
            // 
            this.txtConnectionString.Location = new System.Drawing.Point(116, 39);
            this.txtConnectionString.Name = "txtConnectionString";
            this.txtConnectionString.Size = new System.Drawing.Size(262, 23);
            this.txtConnectionString.TabIndex = 3;
            // 
            // btnTest
            // 
            this.btnTest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTest.Location = new System.Drawing.Point(382, 39);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(28, 23);
            this.btnTest.TabIndex = 4;
            this.btnTest.Text = "...";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // txtCollectionName
            // 
            this.txtCollectionName.Location = new System.Drawing.Point(117, 94);
            this.txtCollectionName.Name = "txtCollectionName";
            this.txtCollectionName.Size = new System.Drawing.Size(294, 23);
            this.txtCollectionName.TabIndex = 8;
            // 
            // txtNetwitnessIp
            // 
            this.txtNetwitnessIp.Location = new System.Drawing.Point(117, 66);
            this.txtNetwitnessIp.Name = "txtNetwitnessIp";
            this.txtNetwitnessIp.Size = new System.Drawing.Size(294, 23);
            this.txtNetwitnessIp.TabIndex = 6;
            // 
            // lblCollectionName
            // 
            this.lblCollectionName.AutoSize = true;
            this.lblCollectionName.Location = new System.Drawing.Point(18, 97);
            this.lblCollectionName.Name = "lblCollectionName";
            this.lblCollectionName.Size = new System.Drawing.Size(96, 15);
            this.lblCollectionName.TabIndex = 7;
            this.lblCollectionName.Text = "Collection Name";
            // 
            // lblNetwitnessIp
            // 
            this.lblNetwitnessIp.AutoSize = true;
            this.lblNetwitnessIp.Location = new System.Drawing.Point(36, 69);
            this.lblNetwitnessIp.Name = "lblNetwitnessIp";
            this.lblNetwitnessIp.Size = new System.Drawing.Size(78, 15);
            this.lblNetwitnessIp.TabIndex = 5;
            this.lblNetwitnessIp.Text = "Netwitness IP";
            // 
            // FormConnection
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(422, 158);
            this.Controls.Add(this.txtCollectionName);
            this.Controls.Add(this.txtNetwitnessIp);
            this.Controls.Add(this.lblCollectionName);
            this.Controls.Add(this.lblNetwitnessIp);
            this.Controls.Add(this.btnTest);
            this.Controls.Add(this.txtConnectionString);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.lblConnectionString);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormConnection";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Connection";
            this.Load += new System.EventHandler(this.FormConnection_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblConnectionString;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtConnectionString;
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.TextBox txtCollectionName;
        private System.Windows.Forms.TextBox txtNetwitnessIp;
        private System.Windows.Forms.Label lblCollectionName;
        private System.Windows.Forms.Label lblNetwitnessIp;
    }
}