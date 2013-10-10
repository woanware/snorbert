namespace snorbert.Forms
{
    partial class FormLinkedRules
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormLinkedRules));
            this.listLinkedRules = new BrightIdeasSoftware.ObjectListView();
            this.btnClose = new System.Windows.Forms.Button();
            this.olvRule = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            ((System.ComponentModel.ISupportInitialize)(this.listLinkedRules)).BeginInit();
            this.SuspendLayout();
            // 
            // listLinkedRules
            // 
            this.listLinkedRules.AllColumns.Add(this.olvRule);
            this.listLinkedRules.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listLinkedRules.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvRule});
            this.listLinkedRules.FullRowSelect = true;
            this.listLinkedRules.HideSelection = false;
            this.listLinkedRules.Location = new System.Drawing.Point(9, 9);
            this.listLinkedRules.Name = "listLinkedRules";
            this.listLinkedRules.ShowGroups = false;
            this.listLinkedRules.Size = new System.Drawing.Size(576, 330);
            this.listLinkedRules.TabIndex = 0;
            this.listLinkedRules.UseCompatibleStateImageBehavior = false;
            this.listLinkedRules.View = System.Windows.Forms.View.Details;
            this.listLinkedRules.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listLinkedRules_KeyDown);
            this.listLinkedRules.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listLinkedRules_MouseDoubleClick);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(511, 347);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 8;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // olvRule
            // 
            this.olvRule.AspectName = "Data";
            this.olvRule.CellPadding = null;
            this.olvRule.Text = "Rule";
            // 
            // FormLinkedRules
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(594, 377);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.listLinkedRules);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormLinkedRules";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Linked Rules";
            this.Load += new System.EventHandler(this.FormLinkedRules_Load);
            ((System.ComponentModel.ISupportInitialize)(this.listLinkedRules)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private BrightIdeasSoftware.ObjectListView listLinkedRules;
        private System.Windows.Forms.Button btnClose;
        private BrightIdeasSoftware.OLVColumn olvRule;
    }
}