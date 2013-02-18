namespace snorbert
{
    partial class FormConnections
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormConnections));
            this.listConnections = new BrightIdeasSoftware.ObjectListView();
            this.olvcName = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvcConnectionString = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.olvcConcentratorIp = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvcCollectionName = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            ((System.ComponentModel.ISupportInitialize)(this.listConnections)).BeginInit();
            this.SuspendLayout();
            // 
            // listConnections
            // 
            this.listConnections.AllColumns.Add(this.olvcName);
            this.listConnections.AllColumns.Add(this.olvcConcentratorIp);
            this.listConnections.AllColumns.Add(this.olvcCollectionName);
            this.listConnections.AllColumns.Add(this.olvcConnectionString);
            this.listConnections.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listConnections.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvcName,
            this.olvcConcentratorIp,
            this.olvcCollectionName,
            this.olvcConnectionString});
            this.listConnections.FullRowSelect = true;
            this.listConnections.HideSelection = false;
            this.listConnections.Location = new System.Drawing.Point(10, 10);
            this.listConnections.MultiSelect = false;
            this.listConnections.Name = "listConnections";
            this.listConnections.ShowGroups = false;
            this.listConnections.Size = new System.Drawing.Size(478, 141);
            this.listConnections.TabIndex = 2;
            this.listConnections.UseCompatibleStateImageBehavior = false;
            this.listConnections.View = System.Windows.Forms.View.Details;
            this.listConnections.SelectedIndexChanged += new System.EventHandler(this.listConnections_SelectedIndexChanged);
            this.listConnections.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listConnections_KeyDown);
            this.listConnections.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listConnections_MouseDoubleClick);
            // 
            // olvcName
            // 
            this.olvcName.AspectName = "Name";
            this.olvcName.CellPadding = null;
            this.olvcName.Text = "Name";
            // 
            // olvcConnectionString
            // 
            this.olvcConnectionString.AspectName = "ConnectionString";
            this.olvcConnectionString.CellPadding = null;
            this.olvcConnectionString.Text = "Connection String";
            this.olvcConnectionString.Width = 133;
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAdd.Image = ((System.Drawing.Image)(resources.GetObject("btnAdd.Image")));
            this.btnAdd.Location = new System.Drawing.Point(9, 158);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(27, 27);
            this.btnAdd.TabIndex = 3;
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnEdit.Image = ((System.Drawing.Image)(resources.GetObject("btnEdit.Image")));
            this.btnEdit.Location = new System.Drawing.Point(39, 158);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(27, 27);
            this.btnEdit.TabIndex = 4;
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDelete.Image = ((System.Drawing.Image)(resources.GetObject("btnDelete.Image")));
            this.btnDelete.Location = new System.Drawing.Point(69, 158);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(27, 27);
            this.btnDelete.TabIndex = 5;
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(412, 163);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // olvcConcentratorIp
            // 
            this.olvcConcentratorIp.AspectName = "ConcentratorIp";
            this.olvcConcentratorIp.CellPadding = null;
            this.olvcConcentratorIp.Text = "Concentrator IP";
            this.olvcConcentratorIp.Width = 110;
            // 
            // olvcCollectionName
            // 
            this.olvcCollectionName.AspectName = "CollectionName";
            this.olvcCollectionName.CellPadding = null;
            this.olvcCollectionName.Text = "Collection Name";
            this.olvcCollectionName.Width = 128;
            // 
            // FormConnections
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(498, 194);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.listConnections);
            this.Controls.Add(this.btnClose);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormConnections";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Connections";
            ((System.ComponentModel.ISupportInitialize)(this.listConnections)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private BrightIdeasSoftware.ObjectListView listConnections;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnDelete;
        private BrightIdeasSoftware.OLVColumn olvcName;
        private BrightIdeasSoftware.OLVColumn olvcConnectionString;
        private System.Windows.Forms.Button btnClose;
        private BrightIdeasSoftware.OLVColumn olvcConcentratorIp;
        private BrightIdeasSoftware.OLVColumn olvcCollectionName;
    }
}