using snorbert.Controls;
namespace snorbert.Forms
{
    partial class FormMain
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.menuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.menuFileExport = new System.Windows.Forms.ToolStripMenuItem();
            this.menuFileExportAcknowledgements = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.menuFileExit = new System.Windows.Forms.ToolStripMenuItem();
            this.menuTools = new System.Windows.Forms.ToolStripMenuItem();
            this.menuToolsConnections = new System.Windows.Forms.ToolStripMenuItem();
            this.menuToolsSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.menuToolsImportRules = new System.Windows.Forms.ToolStripMenuItem();
            this.menuToolsSep2 = new System.Windows.Forms.ToolStripSeparator();
            this.menuToolsExcludes = new System.Windows.Forms.ToolStripMenuItem();
            this.menuToolsExcludeConfiguration = new System.Windows.Forms.ToolStripMenuItem();
            this.menuToolsExcludesExport = new System.Windows.Forms.ToolStripMenuItem();
            this.menuHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.menuHelpHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.menuHelpSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.menuHelpAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.cboConnections = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.cboPageLimit = new System.Windows.Forms.ToolStripComboBox();
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
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.panelControl = new System.Windows.Forms.Panel();
            this.tabMain = new System.Windows.Forms.TabControl();
            this.tabPageEvents = new System.Windows.Forms.TabPage();
            this.controlEvents = new snorbert.Controls.ControlEvents();
            this.tabPageRules = new System.Windows.Forms.TabPage();
            this.controlRules = new snorbert.Controls.ControlRules();
            this.tabPageSearch = new System.Windows.Forms.TabPage();
            this.controlSearch = new snorbert.Controls.ControlSearch();
            this.tabPageSensors = new System.Windows.Forms.TabPage();
            this.controlSensors = new snorbert.Controls.ControlSensors();
            this.menuStrip.SuspendLayout();
            this.toolStrip.SuspendLayout();
            this.ctxMenuEvent.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.panelControl.SuspendLayout();
            this.tabMain.SuspendLayout();
            this.tabPageEvents.SuspendLayout();
            this.tabPageRules.SuspendLayout();
            this.tabPageSearch.SuspendLayout();
            this.tabPageSensors.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuFile,
            this.menuTools,
            this.menuHelp});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
            this.menuStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.menuStrip.Size = new System.Drawing.Size(867, 24);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "menuStrip1";
            // 
            // menuFile
            // 
            this.menuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuFileExport,
            this.toolStripMenuItem1,
            this.menuFileExit});
            this.menuFile.Name = "menuFile";
            this.menuFile.Size = new System.Drawing.Size(37, 20);
            this.menuFile.Text = "&File";
            // 
            // menuFileExport
            // 
            this.menuFileExport.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuFileExportAcknowledgements});
            this.menuFileExport.Name = "menuFileExport";
            this.menuFileExport.Size = new System.Drawing.Size(107, 22);
            this.menuFileExport.Text = "Export";
            // 
            // menuFileExportAcknowledgements
            // 
            this.menuFileExportAcknowledgements.Name = "menuFileExportAcknowledgements";
            this.menuFileExportAcknowledgements.Size = new System.Drawing.Size(179, 22);
            this.menuFileExportAcknowledgements.Text = "Acknowledgements";
            this.menuFileExportAcknowledgements.Click += new System.EventHandler(this.menuFileExportAcknowledgements_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(104, 6);
            // 
            // menuFileExit
            // 
            this.menuFileExit.Name = "menuFileExit";
            this.menuFileExit.Size = new System.Drawing.Size(107, 22);
            this.menuFileExit.Text = "&Exit";
            this.menuFileExit.Click += new System.EventHandler(this.menuFileExit_Click);
            // 
            // menuTools
            // 
            this.menuTools.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuToolsConnections,
            this.menuToolsSep1,
            this.menuToolsImportRules,
            this.menuToolsSep2,
            this.menuToolsExcludes});
            this.menuTools.Name = "menuTools";
            this.menuTools.Size = new System.Drawing.Size(48, 20);
            this.menuTools.Text = "&Tools";
            // 
            // menuToolsConnections
            // 
            this.menuToolsConnections.Name = "menuToolsConnections";
            this.menuToolsConnections.Size = new System.Drawing.Size(141, 22);
            this.menuToolsConnections.Text = "Connections";
            this.menuToolsConnections.Click += new System.EventHandler(this.menuToolsConnections_Click);
            // 
            // menuToolsSep1
            // 
            this.menuToolsSep1.Name = "menuToolsSep1";
            this.menuToolsSep1.Size = new System.Drawing.Size(138, 6);
            // 
            // menuToolsSep2
            // 
            this.menuToolsSep2.Name = "menuToolsSep2";
            this.menuToolsSep2.Size = new System.Drawing.Size(138, 6);
            // 
            // menuToolsExcludes
            // 
            this.menuToolsExcludes.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuToolsExcludeConfiguration,
            this.menuToolsExcludesExport});
            this.menuToolsExcludes.Name = "menuToolsExcludes";
            this.menuToolsExcludes.Size = new System.Drawing.Size(141, 22);
            this.menuToolsExcludes.Text = "&Excludes";
            // 
            // menuToolsExcludeConfiguration
            // 
            this.menuToolsExcludeConfiguration.Name = "menuToolsExcludeConfiguration";
            this.menuToolsExcludeConfiguration.Size = new System.Drawing.Size(148, 22);
            this.menuToolsExcludeConfiguration.Text = "Configuration";
            this.menuToolsExcludeConfiguration.Click += new System.EventHandler(this.menuToolsExcludeConfiguration_Click);
            // 
            // menuToolsExcludesExport
            // 
            this.menuToolsExcludesExport.Name = "menuToolsExcludesExport";
            this.menuToolsExcludesExport.Size = new System.Drawing.Size(148, 22);
            this.menuToolsExcludesExport.Text = "Export";
            this.menuToolsExcludesExport.Click += new System.EventHandler(this.menuToolsExcludesExport_Click);
            // 
            // menuHelp
            // 
            this.menuHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuHelpHelp,
            this.menuHelpSep1,
            this.menuHelpAbout});
            this.menuHelp.Name = "menuHelp";
            this.menuHelp.Size = new System.Drawing.Size(44, 20);
            this.menuHelp.Text = "&Help";
            // 
            // menuHelpHelp
            // 
            this.menuHelpHelp.Name = "menuHelpHelp";
            this.menuHelpHelp.Size = new System.Drawing.Size(107, 22);
            this.menuHelpHelp.Text = "Help";
            this.menuHelpHelp.Click += new System.EventHandler(this.menuHelpHelp_Click);
            // 
            // menuHelpSep1
            // 
            this.menuHelpSep1.Name = "menuHelpSep1";
            this.menuHelpSep1.Size = new System.Drawing.Size(104, 6);
            // 
            // menuHelpAbout
            // 
            this.menuHelpAbout.Name = "menuHelpAbout";
            this.menuHelpAbout.Size = new System.Drawing.Size(107, 22);
            this.menuHelpAbout.Text = "&About";
            this.menuHelpAbout.Click += new System.EventHandler(this.menuHelpAbout_Click);
            // 
            // toolStrip
            // 
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.cboConnections,
            this.toolStripLabel2,
            this.cboPageLimit});
            this.toolStrip.Location = new System.Drawing.Point(0, 24);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip.Size = new System.Drawing.Size(867, 25);
            this.toolStrip.TabIndex = 1;
            this.toolStrip.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(69, 22);
            this.toolStripLabel1.Text = "Connection";
            // 
            // cboConnections
            // 
            this.cboConnections.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboConnections.Name = "cboConnections";
            this.cboConnections.Size = new System.Drawing.Size(200, 25);
            this.cboConnections.DropDownClosed += new System.EventHandler(this.cboConnections_DropDownClosed);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(63, 22);
            this.toolStripLabel2.Text = "Page Limit";
            // 
            // cboPageLimit
            // 
            this.cboPageLimit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPageLimit.Items.AddRange(new object[] {
            "10",
            "20",
            "30",
            "40",
            "50",
            "75",
            "100",
            "200",
            "300",
            "400",
            "500",
            "1000",
            "2000",
            "3000",
            "4000",
            "5000",
            "10000",
            "20000",
            "30000",
            "40000",
            "50000"});
            this.cboPageLimit.Name = "cboPageLimit";
            this.cboPageLimit.Size = new System.Drawing.Size(121, 25);
            this.cboPageLimit.SelectedIndexChanged += new System.EventHandler(this.cboPageLimit_SelectedIndexChanged);
            // 
            // ctxMenuEvent
            // 
            this.ctxMenuEvent.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ctxMenuCopy});
            this.ctxMenuEvent.Name = "ctxMenuEvent";
            this.ctxMenuEvent.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.ctxMenuEvent.Size = new System.Drawing.Size(103, 26);
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
            // 
            // ctxMenuCopySourcePort
            // 
            this.ctxMenuCopySourcePort.Name = "ctxMenuCopySourcePort";
            this.ctxMenuCopySourcePort.Size = new System.Drawing.Size(159, 22);
            this.ctxMenuCopySourcePort.Text = "Source Port";
            // 
            // ctxMenuCopyDestIp
            // 
            this.ctxMenuCopyDestIp.Name = "ctxMenuCopyDestIp";
            this.ctxMenuCopyDestIp.Size = new System.Drawing.Size(159, 22);
            this.ctxMenuCopyDestIp.Text = "Destination IP";
            // 
            // ctxMenuCopyDestPort
            // 
            this.ctxMenuCopyDestPort.Name = "ctxMenuCopyDestPort";
            this.ctxMenuCopyDestPort.Size = new System.Drawing.Size(159, 22);
            this.ctxMenuCopyDestPort.Text = "Destination Port";
            // 
            // ctxMenuCopyCid
            // 
            this.ctxMenuCopyCid.Name = "ctxMenuCopyCid";
            this.ctxMenuCopyCid.Size = new System.Drawing.Size(159, 22);
            this.ctxMenuCopyCid.Text = "CID";
            // 
            // ctxMenuCopySid
            // 
            this.ctxMenuCopySid.Name = "ctxMenuCopySid";
            this.ctxMenuCopySid.Size = new System.Drawing.Size(159, 22);
            this.ctxMenuCopySid.Text = "SID";
            // 
            // ctxMenuCopySigName
            // 
            this.ctxMenuCopySigName.Name = "ctxMenuCopySigName";
            this.ctxMenuCopySigName.Size = new System.Drawing.Size(159, 22);
            this.ctxMenuCopySigName.Text = "Signature Name";
            // 
            // ctxMenuCopyTimestamp
            // 
            this.ctxMenuCopyTimestamp.Name = "ctxMenuCopyTimestamp";
            this.ctxMenuCopyTimestamp.Size = new System.Drawing.Size(159, 22);
            this.ctxMenuCopyTimestamp.Text = "Timestamp";
            // 
            // ctxMenuCopyPayloadAscii
            // 
            this.ctxMenuCopyPayloadAscii.Name = "ctxMenuCopyPayloadAscii";
            this.ctxMenuCopyPayloadAscii.Size = new System.Drawing.Size(159, 22);
            this.ctxMenuCopyPayloadAscii.Text = "Payload (ASCII)";
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 530);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Padding = new System.Windows.Forms.Padding(1, 0, 16, 0);
            this.statusStrip.Size = new System.Drawing.Size(867, 22);
            this.statusStrip.TabIndex = 3;
            this.statusStrip.Text = "statusStrip1";
            // 
            // statusLabel
            // 
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // panelControl
            // 
            this.panelControl.Controls.Add(this.tabMain);
            this.panelControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl.Location = new System.Drawing.Point(0, 49);
            this.panelControl.Name = "panelControl";
            this.panelControl.Size = new System.Drawing.Size(867, 481);
            this.panelControl.TabIndex = 4;
            // 
            // tabMain
            // 
            this.tabMain.Controls.Add(this.tabPageEvents);
            this.tabMain.Controls.Add(this.tabPageRules);
            this.tabMain.Controls.Add(this.tabPageSearch);
            this.tabMain.Controls.Add(this.tabPageSensors);
            this.tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabMain.Location = new System.Drawing.Point(0, 0);
            this.tabMain.Name = "tabMain";
            this.tabMain.SelectedIndex = 0;
            this.tabMain.Size = new System.Drawing.Size(867, 481);
            this.tabMain.TabIndex = 0;
            // 
            // tabPageEvents
            // 
            this.tabPageEvents.Controls.Add(this.controlEvents);
            this.tabPageEvents.Location = new System.Drawing.Point(4, 24);
            this.tabPageEvents.Name = "tabPageEvents";
            this.tabPageEvents.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageEvents.Size = new System.Drawing.Size(798, 453);
            this.tabPageEvents.TabIndex = 0;
            this.tabPageEvents.Text = "Events";
            this.tabPageEvents.UseVisualStyleBackColor = true;
            // 
            // controlEvents
            // 
            this.controlEvents.ContextMenuStrip = this.ctxMenuEvent;
            this.controlEvents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.controlEvents.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.controlEvents.Location = new System.Drawing.Point(3, 3);
            this.controlEvents.Margin = new System.Windows.Forms.Padding(0);
            this.controlEvents.Name = "controlEvents";
            this.controlEvents.Size = new System.Drawing.Size(792, 447);
            this.controlEvents.TabIndex = 0;
            // 
            // tabPageRules
            // 
            this.tabPageRules.Controls.Add(this.controlRules);
            this.tabPageRules.Location = new System.Drawing.Point(4, 24);
            this.tabPageRules.Name = "tabPageRules";
            this.tabPageRules.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageRules.Size = new System.Drawing.Size(859, 453);
            this.tabPageRules.TabIndex = 1;
            this.tabPageRules.Text = "Rules";
            this.tabPageRules.UseVisualStyleBackColor = true;
            // 
            // controlRules
            // 
            this.controlRules.Dock = System.Windows.Forms.DockStyle.Fill;
            this.controlRules.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.controlRules.Location = new System.Drawing.Point(3, 3);
            this.controlRules.Name = "controlRules";
            this.controlRules.Size = new System.Drawing.Size(853, 447);
            this.controlRules.TabIndex = 0;
            // 
            // tabPageSearch
            // 
            this.tabPageSearch.Controls.Add(this.controlSearch);
            this.tabPageSearch.Location = new System.Drawing.Point(4, 24);
            this.tabPageSearch.Name = "tabPageSearch";
            this.tabPageSearch.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSearch.Size = new System.Drawing.Size(798, 453);
            this.tabPageSearch.TabIndex = 2;
            this.tabPageSearch.Text = "Search";
            this.tabPageSearch.UseVisualStyleBackColor = true;
            // 
            // controlSearch
            // 
            this.controlSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.controlSearch.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.controlSearch.Location = new System.Drawing.Point(3, 3);
            this.controlSearch.Margin = new System.Windows.Forms.Padding(0);
            this.controlSearch.Name = "controlSearch";
            this.controlSearch.Size = new System.Drawing.Size(792, 447);
            this.controlSearch.TabIndex = 0;
            // 
            // tabPageSensors
            // 
            this.tabPageSensors.Controls.Add(this.controlSensors);
            this.tabPageSensors.Location = new System.Drawing.Point(4, 24);
            this.tabPageSensors.Name = "tabPageSensors";
            this.tabPageSensors.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSensors.Size = new System.Drawing.Size(798, 453);
            this.tabPageSensors.TabIndex = 3;
            this.tabPageSensors.Text = "Sensors";
            this.tabPageSensors.UseVisualStyleBackColor = true;
            // 
            // controlSensors
            // 
            this.controlSensors.Dock = System.Windows.Forms.DockStyle.Fill;
            this.controlSensors.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.controlSensors.Location = new System.Drawing.Point(3, 3);
            this.controlSensors.Margin = new System.Windows.Forms.Padding(0);
            this.controlSensors.Name = "controlSensors";
            this.controlSensors.Size = new System.Drawing.Size(792, 447);
            this.controlSensors.TabIndex = 0;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(867, 552);
            this.Controls.Add(this.panelControl);
            this.Controls.Add(this.toolStrip);
            this.Controls.Add(this.menuStrip);
            this.Controls.Add(this.statusStrip);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip;
            this.MinimumSize = new System.Drawing.Size(883, 590);
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "snorbert";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.ctxMenuEvent.ResumeLayout(false);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.panelControl.ResumeLayout(false);
            this.tabMain.ResumeLayout(false);
            this.tabPageEvents.ResumeLayout(false);
            this.tabPageRules.ResumeLayout(false);
            this.tabPageSearch.ResumeLayout(false);
            this.tabPageSensors.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel statusLabel;
        private System.Windows.Forms.ToolStripMenuItem menuFile;
        private System.Windows.Forms.ToolStripMenuItem menuFileExit;
        private System.Windows.Forms.ToolStripMenuItem menuHelp;
        private System.Windows.Forms.ToolStripMenuItem menuHelpAbout;
        private System.Windows.Forms.ToolStripMenuItem menuTools;
        private System.Windows.Forms.ToolStripMenuItem menuToolsConnections;
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
        private System.Windows.Forms.ToolStripSeparator menuToolsSep1;
        private System.Windows.Forms.ToolStripMenuItem menuToolsImportRules;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripComboBox cboConnections;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripComboBox cboPageLimit;
        private System.Windows.Forms.Panel panelControl;
        private System.Windows.Forms.TabControl tabMain;
        private System.Windows.Forms.TabPage tabPageEvents;
        private System.Windows.Forms.TabPage tabPageRules;
        private System.Windows.Forms.TabPage tabPageSearch;
        private ControlEvents controlEvents;
        private ControlRules controlRules;
        private ControlSearch controlSearch;
        private System.Windows.Forms.TabPage tabPageSensors;
        private ControlSensors controlSensors;
        private System.Windows.Forms.ToolStripMenuItem menuHelpHelp;
        private System.Windows.Forms.ToolStripSeparator menuHelpSep1;
        private System.Windows.Forms.ToolStripSeparator menuToolsSep2;
        private System.Windows.Forms.ToolStripMenuItem menuToolsExcludes;
        private System.Windows.Forms.ToolStripMenuItem menuToolsExcludeConfiguration;
        private System.Windows.Forms.ToolStripMenuItem menuToolsExcludesExport;
        private System.Windows.Forms.ToolStripMenuItem menuFileExport;
        private System.Windows.Forms.ToolStripMenuItem menuFileExportAcknowledgements;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
    }
}

