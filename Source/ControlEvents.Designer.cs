namespace snorbert
{
    partial class ControlEvents
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ControlEvents));
            this.lblPagingEvents = new System.Windows.Forms.Label();
            this.splitter = new System.Windows.Forms.SplitContainer();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.listEvents = new BrightIdeasSoftware.FastObjectListView();
            this.ctxMenuEvent = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ctxMenuCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxMenuCopySourceIp = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxMenuCopySourcePort = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxMenuCopyDestIp = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxMenuCopyDestPort = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxMenuCopyCid = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxMenuCopySid = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxMenuCopySigName = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxMenuCopyTimestamp = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxMenuCopyPayloadAscii = new System.Windows.Forms.ToolStripMenuItem();
            this.btnPagingNextPage = new System.Windows.Forms.Button();
            this.btnPagingPreviousPage = new System.Windows.Forms.Button();
            this.controlEventInfo = new snorbert.ControlEventInfo();
            ((System.ComponentModel.ISupportInitialize)(this.splitter)).BeginInit();
            this.splitter.Panel1.SuspendLayout();
            this.splitter.Panel2.SuspendLayout();
            this.splitter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.listEvents)).BeginInit();
            this.ctxMenuEvent.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblPagingEvents
            // 
            this.lblPagingEvents.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPagingEvents.Location = new System.Drawing.Point(29, 204);
            this.lblPagingEvents.Name = "lblPagingEvents";
            this.lblPagingEvents.Size = new System.Drawing.Size(796, 28);
            this.lblPagingEvents.TabIndex = 4;
            this.lblPagingEvents.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            this.splitter.Panel1.Controls.Add(this.lblPagingEvents);
            this.splitter.Panel1.Controls.Add(this.btnRefresh);
            this.splitter.Panel1.Controls.Add(this.listEvents);
            this.splitter.Panel1.Controls.Add(this.btnPagingNextPage);
            this.splitter.Panel1.Controls.Add(this.btnPagingPreviousPage);
            // 
            // splitter.Panel2
            // 
            this.splitter.Panel2.Controls.Add(this.controlEventInfo);
            this.splitter.Size = new System.Drawing.Size(850, 360);
            this.splitter.SplitterDistance = 233;
            this.splitter.SplitterWidth = 5;
            this.splitter.TabIndex = 0;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefresh.Image = ((System.Drawing.Image)(resources.GetObject("btnRefresh.Image")));
            this.btnRefresh.Location = new System.Drawing.Point(825, -1);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(25, 25);
            this.btnRefresh.TabIndex = 0;
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // listEvents
            // 
            this.listEvents.AlternateRowBackColor = System.Drawing.Color.LightGray;
            this.listEvents.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listEvents.ContextMenuStrip = this.ctxMenuEvent;
            this.listEvents.FullRowSelect = true;
            this.listEvents.HideSelection = false;
            this.listEvents.Location = new System.Drawing.Point(0, 28);
            this.listEvents.Name = "listEvents";
            this.listEvents.ShowGroups = false;
            this.listEvents.Size = new System.Drawing.Size(849, 175);
            this.listEvents.TabIndex = 1;
            this.listEvents.UseAlternatingBackColors = true;
            this.listEvents.UseCompatibleStateImageBehavior = false;
            this.listEvents.View = System.Windows.Forms.View.Details;
            this.listEvents.VirtualMode = true;
            this.listEvents.SelectedIndexChanged += new System.EventHandler(this.listEvents_SelectedIndexChanged);
            // 
            // ctxMenuEvent
            // 
            this.ctxMenuEvent.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ctxMenuCopy});
            this.ctxMenuEvent.Name = "ctxMenuEvent";
            this.ctxMenuEvent.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.ctxMenuEvent.Size = new System.Drawing.Size(103, 26);
            this.ctxMenuEvent.Opening += new System.ComponentModel.CancelEventHandler(this.ctxMenuEvent_Opening);
            // 
            // ctxMenuCopy
            // 
            this.ctxMenuCopy.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ctxMenuCopySourceIp,
            this.ctxMenuCopySourcePort,
            this.ctxMenuCopyDestIp,
            this.ctxMenuCopyDestPort,
            this.ctxMenuCopyCid,
            this.ctxMenuCopySid,
            this.ctxMenuCopySigName,
            this.ctxMenuCopyTimestamp,
            this.ctxMenuCopyPayloadAscii});
            this.ctxMenuCopy.Name = "ctxMenuCopy";
            this.ctxMenuCopy.Size = new System.Drawing.Size(102, 22);
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
            // btnPagingNextPage
            // 
            this.btnPagingNextPage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPagingNextPage.Image = ((System.Drawing.Image)(resources.GetObject("btnPagingNextPage.Image")));
            this.btnPagingNextPage.Location = new System.Drawing.Point(826, 207);
            this.btnPagingNextPage.Name = "btnPagingNextPage";
            this.btnPagingNextPage.Size = new System.Drawing.Size(25, 25);
            this.btnPagingNextPage.TabIndex = 5;
            this.btnPagingNextPage.UseVisualStyleBackColor = true;
            this.btnPagingNextPage.Click += new System.EventHandler(this.btnPagingNextPage_Click);
            // 
            // btnPagingPreviousPage
            // 
            this.btnPagingPreviousPage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnPagingPreviousPage.Image = ((System.Drawing.Image)(resources.GetObject("btnPagingPreviousPage.Image")));
            this.btnPagingPreviousPage.Location = new System.Drawing.Point(0, 207);
            this.btnPagingPreviousPage.Name = "btnPagingPreviousPage";
            this.btnPagingPreviousPage.Size = new System.Drawing.Size(25, 25);
            this.btnPagingPreviousPage.TabIndex = 3;
            this.btnPagingPreviousPage.UseVisualStyleBackColor = true;
            this.btnPagingPreviousPage.Click += new System.EventHandler(this.btnPagingPreviousPage_Click);
            // 
            // controlEventInfo
            // 
            this.controlEventInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.controlEventInfo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.controlEventInfo.Location = new System.Drawing.Point(0, 0);
            this.controlEventInfo.Name = "controlEventInfo";
            this.controlEventInfo.Size = new System.Drawing.Size(850, 122);
            this.controlEventInfo.TabIndex = 0;
            this.controlEventInfo.Load += new System.EventHandler(this.Control_Load);
            // 
            // ControlEvents
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitter);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "ControlEvents";
            this.Size = new System.Drawing.Size(850, 360);
            this.splitter.Panel1.ResumeLayout(false);
            this.splitter.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitter)).EndInit();
            this.splitter.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.listEvents)).EndInit();
            this.ctxMenuEvent.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblPagingEvents;
        private System.Windows.Forms.SplitContainer splitter;
        private System.Windows.Forms.Button btnRefresh;
        private BrightIdeasSoftware.FastObjectListView listEvents;
        private System.Windows.Forms.Button btnPagingNextPage;
        private System.Windows.Forms.Button btnPagingPreviousPage;
        private ControlEventInfo controlEventInfo;
        private System.Windows.Forms.ContextMenuStrip ctxMenuEvent;
        private System.Windows.Forms.ToolStripMenuItem ctxMenuCopy;
        private System.Windows.Forms.ToolStripMenuItem ctxMenuCopySourceIp;
        private System.Windows.Forms.ToolStripMenuItem ctxMenuCopySourcePort;
        private System.Windows.Forms.ToolStripMenuItem ctxMenuCopyDestIp;
        private System.Windows.Forms.ToolStripMenuItem ctxMenuCopyDestPort;
        private System.Windows.Forms.ToolStripMenuItem ctxMenuCopyCid;
        private System.Windows.Forms.ToolStripMenuItem ctxMenuCopySid;
        private System.Windows.Forms.ToolStripMenuItem ctxMenuCopySigName;
        private System.Windows.Forms.ToolStripMenuItem ctxMenuCopyTimestamp;
        private System.Windows.Forms.ToolStripMenuItem ctxMenuCopyPayloadAscii;

    }
}
