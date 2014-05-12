namespace snorbert.Controls
{
    partial class ControlSearch
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ControlSearch));
            this.splitter = new System.Windows.Forms.SplitContainer();
            this.label33 = new System.Windows.Forms.Label();
            this.lblPaging = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.listEvents = new BrightIdeasSoftware.FastObjectListView();
            this.ctxMenuEvent = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ctxMenuCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxMenuCopySourceIp = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxMenuCopySourcePort = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxMenuCopyDestIp = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxMenuCopyDestPort = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxMenuCopyTimestamp = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxMenuCopyPayloadAscii = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxMenuCopyCid = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxMenuCopySid = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxMenuCopySigName = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxMenuAcknowledgment = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxMenuAcknowledgmentSet = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxMenuAcknowledgmentClear = new System.Windows.Forms.ToolStripMenuItem();
            this.btnPagingNextPage = new System.Windows.Forms.Button();
            this.btnPagingPreviousPage = new System.Windows.Forms.Button();
            this.cboSearch = new System.Windows.Forms.ComboBox();
            this.panelTop = new System.Windows.Forms.Panel();
            this.listFilters = new BrightIdeasSoftware.ObjectListView();
            this.btnFilterAdd = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnFilterEdit = new System.Windows.Forms.Button();
            this.btnFilterDelete = new System.Windows.Forms.Button();
            this.panelBottom = new System.Windows.Forms.Panel();
            this.controlEventInfo = new snorbert.Controls.ControlEventInfo();
            ((System.ComponentModel.ISupportInitialize)(this.splitter)).BeginInit();
            this.splitter.Panel1.SuspendLayout();
            this.splitter.Panel2.SuspendLayout();
            this.splitter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.listEvents)).BeginInit();
            this.ctxMenuEvent.SuspendLayout();
            this.panelTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.listFilters)).BeginInit();
            this.panelBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitter
            // 
            this.splitter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitter.Location = new System.Drawing.Point(0, 0);
            this.splitter.Name = "splitter";
            this.splitter.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitter.Panel1
            // 
            this.splitter.Panel1.Controls.Add(this.label33);
            this.splitter.Panel1.Controls.Add(this.lblPaging);
            this.splitter.Panel1.Controls.Add(this.label32);
            this.splitter.Panel1.Controls.Add(this.listEvents);
            this.splitter.Panel1.Controls.Add(this.btnPagingNextPage);
            this.splitter.Panel1.Controls.Add(this.btnPagingPreviousPage);
            // 
            // splitter.Panel2
            // 
            this.splitter.Panel2.Controls.Add(this.controlEventInfo);
            this.splitter.Size = new System.Drawing.Size(792, 267);
            this.splitter.SplitterDistance = 117;
            this.splitter.SplitterWidth = 5;
            this.splitter.TabIndex = 0;
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(545, -33);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(122, 15);
            this.label33.TabIndex = 28;
            this.label33.Text = "of the following rules:";
            // 
            // lblPaging
            // 
            this.lblPaging.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPaging.Location = new System.Drawing.Point(27, 92);
            this.lblPaging.Name = "lblPaging";
            this.lblPaging.Size = new System.Drawing.Size(738, 23);
            this.lblPaging.TabIndex = 22;
            this.lblPaging.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Location = new System.Drawing.Point(434, -33);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(41, 15);
            this.label32.TabIndex = 26;
            this.label32.Text = "Match";
            // 
            // listEvents
            // 
            this.listEvents.AlternateRowBackColor = System.Drawing.Color.LightGray;
            this.listEvents.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listEvents.ContextMenuStrip = this.ctxMenuEvent;
            this.listEvents.FullRowSelect = true;
            this.listEvents.HeaderUsesThemes = false;
            this.listEvents.HideSelection = false;
            this.listEvents.Location = new System.Drawing.Point(0, 5);
            this.listEvents.Name = "listEvents";
            this.listEvents.ShowCommandMenuOnRightClick = true;
            this.listEvents.ShowGroups = false;
            this.listEvents.ShowItemCountOnGroups = true;
            this.listEvents.Size = new System.Drawing.Size(791, 83);
            this.listEvents.TabIndex = 30;
            this.listEvents.UseAlternatingBackColors = true;
            this.listEvents.UseCompatibleStateImageBehavior = false;
            this.listEvents.UseFilterIndicator = true;
            this.listEvents.UseFiltering = true;
            this.listEvents.View = System.Windows.Forms.View.Details;
            this.listEvents.VirtualMode = true;
            this.listEvents.SelectedIndexChanged += new System.EventHandler(this.listEvents_SelectedIndexChanged);
            this.listEvents.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listEvents_MouseDoubleClick);
            // 
            // ctxMenuEvent
            // 
            this.ctxMenuEvent.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ctxMenuCopy,
            this.ctxMenuAcknowledgment});
            this.ctxMenuEvent.Name = "ctxMenuEvent";
            this.ctxMenuEvent.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.ctxMenuEvent.Size = new System.Drawing.Size(169, 48);
            this.ctxMenuEvent.Opening += new System.ComponentModel.CancelEventHandler(this.ctxMenuEvent_Opening);
            // 
            // ctxMenuCopy
            // 
            this.ctxMenuCopy.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ctxMenuCopySourceIp,
            this.ctxMenuCopySourcePort,
            this.ctxMenuCopyDestIp,
            this.ctxMenuCopyDestPort,
            this.ctxMenuCopyTimestamp,
            this.ctxMenuCopyPayloadAscii,
            this.ctxMenuCopyCid,
            this.ctxMenuCopySid,
            this.ctxMenuCopySigName});
            this.ctxMenuCopy.Name = "ctxMenuCopy";
            this.ctxMenuCopy.Size = new System.Drawing.Size(168, 22);
            this.ctxMenuCopy.Text = "Copy";
            // 
            // ctxMenuCopySourceIp
            // 
            this.ctxMenuCopySourceIp.Name = "ctxMenuCopySourceIp";
            this.ctxMenuCopySourceIp.Size = new System.Drawing.Size(159, 22);
            this.ctxMenuCopySourceIp.Text = "Source IP";
            this.ctxMenuCopySourceIp.Click += new System.EventHandler(this.ctxMenuCopySourceIp_Click);
            // 
            // ctxMenuCopySourcePort
            // 
            this.ctxMenuCopySourcePort.Name = "ctxMenuCopySourcePort";
            this.ctxMenuCopySourcePort.Size = new System.Drawing.Size(159, 22);
            this.ctxMenuCopySourcePort.Text = "Source Port";
            this.ctxMenuCopySourcePort.Click += new System.EventHandler(this.ctxMenuCopySourcePort_Click);
            // 
            // ctxMenuCopyDestIp
            // 
            this.ctxMenuCopyDestIp.Name = "ctxMenuCopyDestIp";
            this.ctxMenuCopyDestIp.Size = new System.Drawing.Size(159, 22);
            this.ctxMenuCopyDestIp.Text = "Destination IP";
            this.ctxMenuCopyDestIp.Click += new System.EventHandler(this.ctxMenuCopyDestIp_Click);
            // 
            // ctxMenuCopyDestPort
            // 
            this.ctxMenuCopyDestPort.Name = "ctxMenuCopyDestPort";
            this.ctxMenuCopyDestPort.Size = new System.Drawing.Size(159, 22);
            this.ctxMenuCopyDestPort.Text = "Destination Port";
            this.ctxMenuCopyDestPort.Click += new System.EventHandler(this.ctxMenuCopyDestPort_Click);
            // 
            // ctxMenuCopyTimestamp
            // 
            this.ctxMenuCopyTimestamp.Name = "ctxMenuCopyTimestamp";
            this.ctxMenuCopyTimestamp.Size = new System.Drawing.Size(159, 22);
            this.ctxMenuCopyTimestamp.Text = "Timestamp";
            this.ctxMenuCopyTimestamp.Click += new System.EventHandler(this.ctxMenuCopyTimestamp_Click);
            // 
            // ctxMenuCopyPayloadAscii
            // 
            this.ctxMenuCopyPayloadAscii.Name = "ctxMenuCopyPayloadAscii";
            this.ctxMenuCopyPayloadAscii.Size = new System.Drawing.Size(159, 22);
            this.ctxMenuCopyPayloadAscii.Text = "Payload (ASCII)";
            this.ctxMenuCopyPayloadAscii.Click += new System.EventHandler(this.ctxMenuCopyPayloadAscii_Click);
            // 
            // ctxMenuCopyCid
            // 
            this.ctxMenuCopyCid.Name = "ctxMenuCopyCid";
            this.ctxMenuCopyCid.Size = new System.Drawing.Size(159, 22);
            this.ctxMenuCopyCid.Text = "CID";
            this.ctxMenuCopyCid.Click += new System.EventHandler(this.ctxMenuCopyCid_Click);
            // 
            // ctxMenuCopySid
            // 
            this.ctxMenuCopySid.Name = "ctxMenuCopySid";
            this.ctxMenuCopySid.Size = new System.Drawing.Size(159, 22);
            this.ctxMenuCopySid.Text = "SID";
            this.ctxMenuCopySid.Click += new System.EventHandler(this.ctxMenuCopySid_Click);
            // 
            // ctxMenuCopySigName
            // 
            this.ctxMenuCopySigName.Name = "ctxMenuCopySigName";
            this.ctxMenuCopySigName.Size = new System.Drawing.Size(159, 22);
            this.ctxMenuCopySigName.Text = "Signature Name";
            this.ctxMenuCopySigName.Click += new System.EventHandler(this.ctxMenuCopySigName_Click);
            // 
            // ctxMenuAcknowledgment
            // 
            this.ctxMenuAcknowledgment.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ctxMenuAcknowledgmentSet,
            this.ctxMenuAcknowledgmentClear});
            this.ctxMenuAcknowledgment.Name = "ctxMenuAcknowledgment";
            this.ctxMenuAcknowledgment.Size = new System.Drawing.Size(168, 22);
            this.ctxMenuAcknowledgment.Text = "Acknowledgment";
            // 
            // ctxMenuAcknowledgmentSet
            // 
            this.ctxMenuAcknowledgmentSet.Name = "ctxMenuAcknowledgmentSet";
            this.ctxMenuAcknowledgmentSet.Size = new System.Drawing.Size(101, 22);
            this.ctxMenuAcknowledgmentSet.Text = "Set";
            this.ctxMenuAcknowledgmentSet.Click += new System.EventHandler(this.ctxMenuAcknowledgmentSet_Click);
            // 
            // ctxMenuAcknowledgmentClear
            // 
            this.ctxMenuAcknowledgmentClear.Name = "ctxMenuAcknowledgmentClear";
            this.ctxMenuAcknowledgmentClear.Size = new System.Drawing.Size(101, 22);
            this.ctxMenuAcknowledgmentClear.Text = "Clear";
            this.ctxMenuAcknowledgmentClear.Click += new System.EventHandler(this.ctxMenuAcknowledgmentClear_Click);
            // 
            // btnPagingNextPage
            // 
            this.btnPagingNextPage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPagingNextPage.Image = ((System.Drawing.Image)(resources.GetObject("btnPagingNextPage.Image")));
            this.btnPagingNextPage.Location = new System.Drawing.Point(766, 92);
            this.btnPagingNextPage.Name = "btnPagingNextPage";
            this.btnPagingNextPage.Size = new System.Drawing.Size(25, 25);
            this.btnPagingNextPage.TabIndex = 32;
            this.btnPagingNextPage.UseVisualStyleBackColor = true;
            this.btnPagingNextPage.Click += new System.EventHandler(this.btnPagingSearchNextPage_Click);
            // 
            // btnPagingPreviousPage
            // 
            this.btnPagingPreviousPage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnPagingPreviousPage.Image = ((System.Drawing.Image)(resources.GetObject("btnPagingPreviousPage.Image")));
            this.btnPagingPreviousPage.Location = new System.Drawing.Point(0, 92);
            this.btnPagingPreviousPage.Name = "btnPagingPreviousPage";
            this.btnPagingPreviousPage.Size = new System.Drawing.Size(25, 25);
            this.btnPagingPreviousPage.TabIndex = 31;
            this.btnPagingPreviousPage.UseVisualStyleBackColor = true;
            this.btnPagingPreviousPage.Click += new System.EventHandler(this.btnPagingSearchPreviousPage_Click);
            // 
            // cboSearch
            // 
            this.cboSearch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSearch.FormattingEnabled = true;
            this.cboSearch.Items.AddRange(new object[] {
            "OR",
            "AND"});
            this.cboSearch.Location = new System.Drawing.Point(85, 91);
            this.cboSearch.Name = "cboSearch";
            this.cboSearch.Size = new System.Drawing.Size(60, 23);
            this.cboSearch.TabIndex = 27;
            this.cboSearch.SelectedIndexChanged += new System.EventHandler(this.cboSearch_SelectedIndexChanged);
            // 
            // panelTop
            // 
            this.panelTop.Controls.Add(this.listFilters);
            this.panelTop.Controls.Add(this.btnFilterAdd);
            this.panelTop.Controls.Add(this.cboSearch);
            this.panelTop.Controls.Add(this.btnSearch);
            this.panelTop.Controls.Add(this.btnFilterEdit);
            this.panelTop.Controls.Add(this.btnFilterDelete);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(792, 115);
            this.panelTop.TabIndex = 1;
            // 
            // listFilters
            // 
            this.listFilters.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listFilters.FullRowSelect = true;
            this.listFilters.HideSelection = false;
            this.listFilters.Location = new System.Drawing.Point(0, 0);
            this.listFilters.Name = "listFilters";
            this.listFilters.ShowGroups = false;
            this.listFilters.Size = new System.Drawing.Size(791, 85);
            this.listFilters.TabIndex = 22;
            this.listFilters.UseCompatibleStateImageBehavior = false;
            this.listFilters.View = System.Windows.Forms.View.Details;
            this.listFilters.SelectedIndexChanged += new System.EventHandler(this.listFilters_SelectedIndexChanged);
            this.listFilters.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listFilters_KeyDown);
            this.listFilters.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listFilters_MouseDoubleClick);
            // 
            // btnFilterAdd
            // 
            this.btnFilterAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnFilterAdd.Image = ((System.Drawing.Image)(resources.GetObject("btnFilterAdd.Image")));
            this.btnFilterAdd.Location = new System.Drawing.Point(0, 90);
            this.btnFilterAdd.Name = "btnFilterAdd";
            this.btnFilterAdd.Size = new System.Drawing.Size(25, 25);
            this.btnFilterAdd.TabIndex = 23;
            this.btnFilterAdd.UseVisualStyleBackColor = true;
            this.btnFilterAdd.Click += new System.EventHandler(this.btnFilterAdd_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.Image = ((System.Drawing.Image)(resources.GetObject("btnSearch.Image")));
            this.btnSearch.Location = new System.Drawing.Point(767, 89);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(25, 25);
            this.btnSearch.TabIndex = 29;
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnFilterEdit
            // 
            this.btnFilterEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnFilterEdit.Image = ((System.Drawing.Image)(resources.GetObject("btnFilterEdit.Image")));
            this.btnFilterEdit.Location = new System.Drawing.Point(28, 90);
            this.btnFilterEdit.Name = "btnFilterEdit";
            this.btnFilterEdit.Size = new System.Drawing.Size(25, 25);
            this.btnFilterEdit.TabIndex = 24;
            this.btnFilterEdit.UseVisualStyleBackColor = true;
            this.btnFilterEdit.Click += new System.EventHandler(this.btnFilterEdit_Click);
            // 
            // btnFilterDelete
            // 
            this.btnFilterDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnFilterDelete.Image = ((System.Drawing.Image)(resources.GetObject("btnFilterDelete.Image")));
            this.btnFilterDelete.Location = new System.Drawing.Point(56, 90);
            this.btnFilterDelete.Name = "btnFilterDelete";
            this.btnFilterDelete.Size = new System.Drawing.Size(25, 25);
            this.btnFilterDelete.TabIndex = 25;
            this.btnFilterDelete.UseVisualStyleBackColor = true;
            this.btnFilterDelete.Click += new System.EventHandler(this.btnFilterDelete_Click);
            // 
            // panelBottom
            // 
            this.panelBottom.Controls.Add(this.splitter);
            this.panelBottom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelBottom.Location = new System.Drawing.Point(0, 115);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Size = new System.Drawing.Size(792, 267);
            this.panelBottom.TabIndex = 2;
            // 
            // controlEventInfo
            // 
            this.controlEventInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.controlEventInfo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.controlEventInfo.Location = new System.Drawing.Point(0, 0);
            this.controlEventInfo.Name = "controlEventInfo";
            this.controlEventInfo.Size = new System.Drawing.Size(792, 145);
            this.controlEventInfo.TabIndex = 23;
            // 
            // ControlSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelBottom);
            this.Controls.Add(this.panelTop);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "ControlSearch";
            this.Size = new System.Drawing.Size(792, 382);
            this.Load += new System.EventHandler(this.Control_Load);
            this.splitter.Panel1.ResumeLayout(false);
            this.splitter.Panel1.PerformLayout();
            this.splitter.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitter)).EndInit();
            this.splitter.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.listEvents)).EndInit();
            this.ctxMenuEvent.ResumeLayout(false);
            this.panelTop.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.listFilters)).EndInit();
            this.panelBottom.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitter;
        private System.Windows.Forms.Label lblPaging;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.ComboBox cboSearch;
        private System.Windows.Forms.Label label32;
        private BrightIdeasSoftware.FastObjectListView listEvents;
        private System.Windows.Forms.Button btnPagingNextPage;
        private System.Windows.Forms.Button btnPagingPreviousPage;
        private ControlEventInfo controlEventInfo;
        private System.Windows.Forms.Panel panelTop;
        private BrightIdeasSoftware.ObjectListView listFilters;
        private System.Windows.Forms.Button btnFilterAdd;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnFilterEdit;
        private System.Windows.Forms.Button btnFilterDelete;
        private System.Windows.Forms.Panel panelBottom;
        private System.Windows.Forms.ContextMenuStrip ctxMenuEvent;
        private System.Windows.Forms.ToolStripMenuItem ctxMenuCopy;
        private System.Windows.Forms.ToolStripMenuItem ctxMenuCopySourceIp;
        private System.Windows.Forms.ToolStripMenuItem ctxMenuCopySourcePort;
        private System.Windows.Forms.ToolStripMenuItem ctxMenuCopyDestIp;
        private System.Windows.Forms.ToolStripMenuItem ctxMenuCopyDestPort;
        private System.Windows.Forms.ToolStripMenuItem ctxMenuCopySid;
        private System.Windows.Forms.ToolStripMenuItem ctxMenuCopySigName;
        private System.Windows.Forms.ToolStripMenuItem ctxMenuCopyTimestamp;
        private System.Windows.Forms.ToolStripMenuItem ctxMenuCopyPayloadAscii;
        private System.Windows.Forms.ToolStripMenuItem ctxMenuAcknowledgment;
        private System.Windows.Forms.ToolStripMenuItem ctxMenuAcknowledgmentSet;
        private System.Windows.Forms.ToolStripMenuItem ctxMenuAcknowledgmentClear;
        private System.Windows.Forms.ToolStripMenuItem ctxMenuCopyCid;
    }
}
