namespace snorbert
{
    partial class ControlRules
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ControlRules));
            this.lblPagingRules = new System.Windows.Forms.Label();
            this.splitter = new System.Windows.Forms.SplitContainer();
            this.btnPagingLastPage = new System.Windows.Forms.Button();
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
            this.ctxMenuSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.ctxMenuHide = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxMenuSep2 = new System.Windows.Forms.ToolStripSeparator();
            this.ctxMenuExtractIpInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.btnPagingPreviousPage = new System.Windows.Forms.Button();
            this.btnPagingNextPage = new System.Windows.Forms.Button();
            this.btnPagingFirstPage = new System.Windows.Forms.Button();
            this.panelTop = new System.Windows.Forms.Panel();
            this.label31 = new System.Windows.Forms.Label();
            this.cboTimeTo = new System.Windows.Forms.ComboBox();
            this.dtpDateTo = new System.Windows.Forms.DateTimePicker();
            this.lblDate = new System.Windows.Forms.Label();
            this.lblRule = new System.Windows.Forms.Label();
            this.cboRule = new System.Windows.Forms.ComboBox();
            this.cboTimeFrom = new System.Windows.Forms.ComboBox();
            this.dtpDateFrom = new System.Windows.Forms.DateTimePicker();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.controlEventInfo = new snorbert.ControlEventInfo();
            this.ctxMenuExtractIpInfoUniqueSource = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxMenuExtractIpInfoUniqueDestination = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.splitter)).BeginInit();
            this.splitter.Panel1.SuspendLayout();
            this.splitter.Panel2.SuspendLayout();
            this.splitter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.listEvents)).BeginInit();
            this.ctxMenuEvent.SuspendLayout();
            this.panelTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblPagingRules
            // 
            this.lblPagingRules.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPagingRules.Location = new System.Drawing.Point(56, 285);
            this.lblPagingRules.Name = "lblPagingRules";
            this.lblPagingRules.Size = new System.Drawing.Size(637, 24);
            this.lblPagingRules.TabIndex = 20;
            this.lblPagingRules.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            this.splitter.Panel1.Controls.Add(this.btnPagingLastPage);
            this.splitter.Panel1.Controls.Add(this.listEvents);
            this.splitter.Panel1.Controls.Add(this.btnPagingPreviousPage);
            this.splitter.Panel1.Controls.Add(this.btnPagingNextPage);
            this.splitter.Panel1.Controls.Add(this.btnPagingFirstPage);
            this.splitter.Panel1.Controls.Add(this.lblPagingRules);
            this.splitter.Panel1.Controls.Add(this.panelTop);
            // 
            // splitter.Panel2
            // 
            this.splitter.Panel2.Controls.Add(this.controlEventInfo);
            this.splitter.Size = new System.Drawing.Size(748, 623);
            this.splitter.SplitterDistance = 310;
            this.splitter.TabIndex = 21;
            // 
            // btnPagingLastPage
            // 
            this.btnPagingLastPage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPagingLastPage.Image = ((System.Drawing.Image)(resources.GetObject("btnPagingLastPage.Image")));
            this.btnPagingLastPage.Location = new System.Drawing.Point(723, 285);
            this.btnPagingLastPage.Name = "btnPagingLastPage";
            this.btnPagingLastPage.Size = new System.Drawing.Size(25, 25);
            this.btnPagingLastPage.TabIndex = 25;
            this.btnPagingLastPage.UseVisualStyleBackColor = true;
            this.btnPagingLastPage.Click += new System.EventHandler(this.btnPagingLastPage_Click);
            // 
            // listEvents
            // 
            this.listEvents.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listEvents.ContextMenuStrip = this.ctxMenuEvent;
            this.listEvents.FullRowSelect = true;
            this.listEvents.HideSelection = false;
            this.listEvents.Location = new System.Drawing.Point(0, 58);
            this.listEvents.Name = "listEvents";
            this.listEvents.ShowGroups = false;
            this.listEvents.Size = new System.Drawing.Size(747, 224);
            this.listEvents.TabIndex = 21;
            this.listEvents.UseCompatibleStateImageBehavior = false;
            this.listEvents.View = System.Windows.Forms.View.Details;
            this.listEvents.VirtualMode = true;
            this.listEvents.SelectedIndexChanged += new System.EventHandler(this.listEvents_SelectedIndexChanged);
            // 
            // ctxMenuEvent
            // 
            this.ctxMenuEvent.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ctxMenuCopy,
            this.ctxMenuSep1,
            this.ctxMenuHide,
            this.ctxMenuSep2,
            this.ctxMenuExtractIpInfo});
            this.ctxMenuEvent.Name = "ctxMenuEvent";
            this.ctxMenuEvent.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.ctxMenuEvent.Size = new System.Drawing.Size(189, 104);
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
            this.ctxMenuCopy.Size = new System.Drawing.Size(188, 22);
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
            // ctxMenuSep1
            // 
            this.ctxMenuSep1.Name = "ctxMenuSep1";
            this.ctxMenuSep1.Size = new System.Drawing.Size(185, 6);
            // 
            // ctxMenuHide
            // 
            this.ctxMenuHide.Name = "ctxMenuHide";
            this.ctxMenuHide.Size = new System.Drawing.Size(188, 22);
            this.ctxMenuHide.Text = "Hide";
            this.ctxMenuHide.Click += new System.EventHandler(this.ctxMenuHide_Click);
            // 
            // ctxMenuSep2
            // 
            this.ctxMenuSep2.Name = "ctxMenuSep2";
            this.ctxMenuSep2.Size = new System.Drawing.Size(185, 6);
            // 
            // ctxMenuExtractIpInfo
            // 
            this.ctxMenuExtractIpInfo.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ctxMenuExtractIpInfoUniqueSource,
            this.ctxMenuExtractIpInfoUniqueDestination});
            this.ctxMenuExtractIpInfo.Name = "ctxMenuExtractIpInfo";
            this.ctxMenuExtractIpInfo.Size = new System.Drawing.Size(188, 22);
            this.ctxMenuExtractIpInfo.Text = "Extract IP Information";
            // 
            // btnPagingPreviousPage
            // 
            this.btnPagingPreviousPage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnPagingPreviousPage.Image = ((System.Drawing.Image)(resources.GetObject("btnPagingPreviousPage.Image")));
            this.btnPagingPreviousPage.Location = new System.Drawing.Point(27, 285);
            this.btnPagingPreviousPage.Name = "btnPagingPreviousPage";
            this.btnPagingPreviousPage.Size = new System.Drawing.Size(25, 25);
            this.btnPagingPreviousPage.TabIndex = 23;
            this.btnPagingPreviousPage.UseVisualStyleBackColor = true;
            this.btnPagingPreviousPage.Click += new System.EventHandler(this.btnPagingPreviousPage_Click);
            // 
            // btnPagingNextPage
            // 
            this.btnPagingNextPage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPagingNextPage.Image = ((System.Drawing.Image)(resources.GetObject("btnPagingNextPage.Image")));
            this.btnPagingNextPage.Location = new System.Drawing.Point(695, 285);
            this.btnPagingNextPage.Name = "btnPagingNextPage";
            this.btnPagingNextPage.Size = new System.Drawing.Size(25, 25);
            this.btnPagingNextPage.TabIndex = 24;
            this.btnPagingNextPage.UseVisualStyleBackColor = true;
            this.btnPagingNextPage.Click += new System.EventHandler(this.btnPagingNextPage_Click);
            // 
            // btnPagingFirstPage
            // 
            this.btnPagingFirstPage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnPagingFirstPage.Image = ((System.Drawing.Image)(resources.GetObject("btnPagingFirstPage.Image")));
            this.btnPagingFirstPage.Location = new System.Drawing.Point(-1, 285);
            this.btnPagingFirstPage.Name = "btnPagingFirstPage";
            this.btnPagingFirstPage.Size = new System.Drawing.Size(25, 25);
            this.btnPagingFirstPage.TabIndex = 22;
            this.btnPagingFirstPage.UseVisualStyleBackColor = true;
            this.btnPagingFirstPage.Click += new System.EventHandler(this.btnPagingFirstPage_Click);
            // 
            // panelTop
            // 
            this.panelTop.Controls.Add(this.label31);
            this.panelTop.Controls.Add(this.cboTimeTo);
            this.panelTop.Controls.Add(this.dtpDateTo);
            this.panelTop.Controls.Add(this.lblDate);
            this.panelTop.Controls.Add(this.lblRule);
            this.panelTop.Controls.Add(this.cboRule);
            this.panelTop.Controls.Add(this.cboTimeFrom);
            this.panelTop.Controls.Add(this.dtpDateFrom);
            this.panelTop.Controls.Add(this.btnRefresh);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(748, 57);
            this.panelTop.TabIndex = 20;
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(250, 3);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(21, 15);
            this.label31.TabIndex = 28;
            this.label31.Text = "To";
            // 
            // cboTimeTo
            // 
            this.cboTimeTo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTimeTo.FormattingEnabled = true;
            this.cboTimeTo.Items.AddRange(new object[] {
            "00:00",
            "01:00",
            "02:00",
            "03:00",
            "04:00",
            "05:00",
            "06:00",
            "07:00",
            "08:00",
            "09:00",
            "10:00",
            "11:00",
            "12:00",
            "13:00",
            "14:00",
            "15:00",
            "16:00",
            "17:00",
            "18:00",
            "19:00",
            "20:00",
            "21:00",
            "22:00",
            "23:00"});
            this.cboTimeTo.Location = new System.Drawing.Point(407, 0);
            this.cboTimeTo.Name = "cboTimeTo";
            this.cboTimeTo.Size = new System.Drawing.Size(66, 23);
            this.cboTimeTo.TabIndex = 27;
            // 
            // dtpDateTo
            // 
            this.dtpDateTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDateTo.Location = new System.Drawing.Point(278, 0);
            this.dtpDateTo.Name = "dtpDateTo";
            this.dtpDateTo.ShowCheckBox = true;
            this.dtpDateTo.Size = new System.Drawing.Size(122, 23);
            this.dtpDateTo.TabIndex = 26;
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.Location = new System.Drawing.Point(7, 3);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(35, 15);
            this.lblDate.TabIndex = 25;
            this.lblDate.Text = "From";
            // 
            // lblRule
            // 
            this.lblRule.AutoSize = true;
            this.lblRule.Location = new System.Drawing.Point(8, 34);
            this.lblRule.Name = "lblRule";
            this.lblRule.Size = new System.Drawing.Size(30, 15);
            this.lblRule.TabIndex = 22;
            this.lblRule.Text = "Rule";
            // 
            // cboRule
            // 
            this.cboRule.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboRule.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboRule.FormattingEnabled = true;
            this.cboRule.Location = new System.Drawing.Point(49, 29);
            this.cboRule.Name = "cboRule";
            this.cboRule.Size = new System.Drawing.Size(698, 23);
            this.cboRule.TabIndex = 21;
            this.cboRule.DropDownClosed += new System.EventHandler(this.cboRule_DropDownClosed);
            // 
            // cboTimeFrom
            // 
            this.cboTimeFrom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTimeFrom.FormattingEnabled = true;
            this.cboTimeFrom.Items.AddRange(new object[] {
            "00:00",
            "01:00",
            "02:00",
            "03:00",
            "04:00",
            "05:00",
            "06:00",
            "07:00",
            "08:00",
            "09:00",
            "10:00",
            "11:00",
            "12:00",
            "13:00",
            "14:00",
            "15:00",
            "16:00",
            "17:00",
            "18:00",
            "19:00",
            "20:00",
            "21:00",
            "22:00",
            "23:00"});
            this.cboTimeFrom.Location = new System.Drawing.Point(169, 0);
            this.cboTimeFrom.Name = "cboTimeFrom";
            this.cboTimeFrom.Size = new System.Drawing.Size(66, 23);
            this.cboTimeFrom.TabIndex = 20;
            // 
            // dtpDateFrom
            // 
            this.dtpDateFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDateFrom.Location = new System.Drawing.Point(50, 0);
            this.dtpDateFrom.Name = "dtpDateFrom";
            this.dtpDateFrom.Size = new System.Drawing.Size(111, 23);
            this.dtpDateFrom.TabIndex = 19;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefresh.Image = ((System.Drawing.Image)(resources.GetObject("btnRefresh.Image")));
            this.btnRefresh.Location = new System.Drawing.Point(723, -1);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(25, 25);
            this.btnRefresh.TabIndex = 12;
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // controlEventInfo
            // 
            this.controlEventInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.controlEventInfo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.controlEventInfo.Location = new System.Drawing.Point(0, 0);
            this.controlEventInfo.Name = "controlEventInfo";
            this.controlEventInfo.Size = new System.Drawing.Size(748, 309);
            this.controlEventInfo.TabIndex = 1;
            // 
            // ctxMenuExtractIpInfoUniqueSource
            // 
            this.ctxMenuExtractIpInfoUniqueSource.Name = "ctxMenuExtractIpInfoUniqueSource";
            this.ctxMenuExtractIpInfoUniqueSource.Size = new System.Drawing.Size(175, 22);
            this.ctxMenuExtractIpInfoUniqueSource.Text = "Unique Source";
            this.ctxMenuExtractIpInfoUniqueSource.Click += new System.EventHandler(this.ctxMenuExtractIpInfoUniqueSource_Click);
            // 
            // ctxMenuExtractIpInfoUniqueDestination
            // 
            this.ctxMenuExtractIpInfoUniqueDestination.Name = "ctxMenuExtractIpInfoUniqueDestination";
            this.ctxMenuExtractIpInfoUniqueDestination.Size = new System.Drawing.Size(175, 22);
            this.ctxMenuExtractIpInfoUniqueDestination.Text = "Unique Destination";
            this.ctxMenuExtractIpInfoUniqueDestination.Click += new System.EventHandler(this.ctxMenuExtractIpInfoUniqueDestination_Click);
            // 
            // ControlRules
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitter);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "ControlRules";
            this.Size = new System.Drawing.Size(748, 623);
            this.Load += new System.EventHandler(this.Control_Load);
            this.splitter.Panel1.ResumeLayout(false);
            this.splitter.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitter)).EndInit();
            this.splitter.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.listEvents)).EndInit();
            this.ctxMenuEvent.ResumeLayout(false);
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblPagingRules;
        private System.Windows.Forms.SplitContainer splitter;
        private System.Windows.Forms.Button btnPagingLastPage;
        private BrightIdeasSoftware.FastObjectListView listEvents;
        private System.Windows.Forms.Button btnPagingPreviousPage;
        private System.Windows.Forms.Button btnPagingNextPage;
        private System.Windows.Forms.Button btnPagingFirstPage;
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.ComboBox cboTimeTo;
        private System.Windows.Forms.DateTimePicker dtpDateTo;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.Label lblRule;
        private System.Windows.Forms.ComboBox cboRule;
        private System.Windows.Forms.ComboBox cboTimeFrom;
        private System.Windows.Forms.DateTimePicker dtpDateFrom;
        private System.Windows.Forms.Button btnRefresh;
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
        private System.Windows.Forms.ToolStripSeparator ctxMenuSep1;
        private System.Windows.Forms.ToolStripMenuItem ctxMenuHide;
        private System.Windows.Forms.ToolStripSeparator ctxMenuSep2;
        private System.Windows.Forms.ToolStripMenuItem ctxMenuExtractIpInfo;
        private System.Windows.Forms.ToolStripMenuItem ctxMenuExtractIpInfoUniqueSource;
        private System.Windows.Forms.ToolStripMenuItem ctxMenuExtractIpInfoUniqueDestination;
    }
}
