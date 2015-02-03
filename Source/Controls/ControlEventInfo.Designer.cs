namespace snorbert.Controls
{
    partial class ControlEventInfo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ControlEventInfo));
            this.tabEvent = new System.Windows.Forms.TabControl();
            this.tabPageIpHeader = new System.Windows.Forms.TabPage();
            this.txtIpCsum = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtIpProto = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtIpTtl = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtIpOff = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtIpFlags = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtIpId = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtIpLen = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtIpTos = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtIpHlen = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtIpVer = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.ipDest = new IPAddressControlLib.IPAddressControl();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.ipSource = new IPAddressControlLib.IPAddressControl();
            this.tabPageSignature = new System.Windows.Forms.TabPage();
            this.btnLinkedRules = new System.Windows.Forms.Button();
            this.btnRule = new System.Windows.Forms.Button();
            this.txtRule = new System.Windows.Forms.TextBox();
            this.label30 = new System.Windows.Forms.Label();
            this.txtSigCategory = new System.Windows.Forms.TextBox();
            this.lblSigCat = new System.Windows.Forms.Label();
            this.txtSigSigRev = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.txtSigSigId = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txtSigGenId = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.tabPageTcpHeader = new System.Windows.Forms.TabPage();
            this.txtTcpUrp = new System.Windows.Forms.TextBox();
            this.label25 = new System.Windows.Forms.Label();
            this.txtTcpCsum = new System.Windows.Forms.TextBox();
            this.label24 = new System.Windows.Forms.Label();
            this.txtTcpWin = new System.Windows.Forms.TextBox();
            this.label23 = new System.Windows.Forms.Label();
            this.txtTcpFlags = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.txtTcpRes = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.txtTcpOff = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.txtTcpAck = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.txtTcpSeq = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.txtTcpDstPort = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.txtTcpSrcPrt = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.tabPageUdpHeader = new System.Windows.Forms.TabPage();
            this.txtUdpCsum = new System.Windows.Forms.TextBox();
            this.label26 = new System.Windows.Forms.Label();
            this.txtUdpLen = new System.Windows.Forms.TextBox();
            this.label27 = new System.Windows.Forms.Label();
            this.txtUdpDstPort = new System.Windows.Forms.TextBox();
            this.label28 = new System.Windows.Forms.Label();
            this.txtUdpSrcPort = new System.Windows.Forms.TextBox();
            this.label29 = new System.Windows.Forms.Label();
            this.tabPageReferences = new System.Windows.Forms.TabPage();
            this.listReferences = new BrightIdeasSoftware.FastObjectListView();
            this.tabPageHex = new System.Windows.Forms.TabPage();
            this.hexEvent = new Be.Windows.Forms.HexBox();
            this.ctxMenuHex = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ctxMenuHexCopyText = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxMenuHexCopyHex = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxMenuHexCopyHexSpaces = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxMenuHexCopyHexNoSpaces = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxMenuHexSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.ctxMenuHexSelectAll = new System.Windows.Forms.ToolStripMenuItem();
            this.tabPageAscii = new System.Windows.Forms.TabPage();
            this.txtPayloadAscii = new System.Windows.Forms.RichTextBox();
            this.tabPageMisc = new System.Windows.Forms.TabPage();
            this.txtSensor = new System.Windows.Forms.TextBox();
            this.lblSensor = new System.Windows.Forms.Label();
            this.txtEventCid = new System.Windows.Forms.TextBox();
            this.lblEventCid = new System.Windows.Forms.Label();
            this.txtEventSid = new System.Windows.Forms.TextBox();
            this.lblEventSid = new System.Windows.Forms.Label();
            this.tabPageDns = new System.Windows.Forms.TabPage();
            this.txtDns = new System.Windows.Forms.RichTextBox();
            this.tabPageAcknowledgement = new System.Windows.Forms.TabPage();
            this.label31 = new System.Windows.Forms.Label();
            this.chkAckSuccessful = new System.Windows.Forms.CheckBox();
            this.txtAckTimestamp = new System.Windows.Forms.TextBox();
            this.lblAckTimestamp = new System.Windows.Forms.Label();
            this.txtAckNotes = new System.Windows.Forms.TextBox();
            this.lblAckNotes = new System.Windows.Forms.Label();
            this.txtAckClassification = new System.Windows.Forms.TextBox();
            this.lblAckClassification = new System.Windows.Forms.Label();
            this.txtAckInitials = new System.Windows.Forms.TextBox();
            this.lblAckInitials = new System.Windows.Forms.Label();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.vS2012LightTheme1 = new WeifenLuo.WinFormsUI.Docking.VS2012LightTheme();
            this.txtPriority = new System.Windows.Forms.TextBox();
            this.lblMiscPriority = new System.Windows.Forms.Label();
            this.tabEvent.SuspendLayout();
            this.tabPageIpHeader.SuspendLayout();
            this.tabPageSignature.SuspendLayout();
            this.tabPageTcpHeader.SuspendLayout();
            this.tabPageUdpHeader.SuspendLayout();
            this.tabPageReferences.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.listReferences)).BeginInit();
            this.tabPageHex.SuspendLayout();
            this.ctxMenuHex.SuspendLayout();
            this.tabPageAscii.SuspendLayout();
            this.tabPageMisc.SuspendLayout();
            this.tabPageDns.SuspendLayout();
            this.tabPageAcknowledgement.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabEvent
            // 
            this.tabEvent.Controls.Add(this.tabPageIpHeader);
            this.tabEvent.Controls.Add(this.tabPageSignature);
            this.tabEvent.Controls.Add(this.tabPageTcpHeader);
            this.tabEvent.Controls.Add(this.tabPageUdpHeader);
            this.tabEvent.Controls.Add(this.tabPageReferences);
            this.tabEvent.Controls.Add(this.tabPageHex);
            this.tabEvent.Controls.Add(this.tabPageAscii);
            this.tabEvent.Controls.Add(this.tabPageMisc);
            this.tabEvent.Controls.Add(this.tabPageDns);
            this.tabEvent.Controls.Add(this.tabPageAcknowledgement);
            this.tabEvent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabEvent.Location = new System.Drawing.Point(0, 0);
            this.tabEvent.Name = "tabEvent";
            this.tabEvent.SelectedIndex = 0;
            this.tabEvent.Size = new System.Drawing.Size(770, 168);
            this.tabEvent.TabIndex = 0;
            // 
            // tabPageIpHeader
            // 
            this.tabPageIpHeader.Controls.Add(this.txtIpCsum);
            this.tabPageIpHeader.Controls.Add(this.label12);
            this.tabPageIpHeader.Controls.Add(this.txtIpProto);
            this.tabPageIpHeader.Controls.Add(this.label11);
            this.tabPageIpHeader.Controls.Add(this.txtIpTtl);
            this.tabPageIpHeader.Controls.Add(this.label10);
            this.tabPageIpHeader.Controls.Add(this.txtIpOff);
            this.tabPageIpHeader.Controls.Add(this.label9);
            this.tabPageIpHeader.Controls.Add(this.txtIpFlags);
            this.tabPageIpHeader.Controls.Add(this.label8);
            this.tabPageIpHeader.Controls.Add(this.txtIpId);
            this.tabPageIpHeader.Controls.Add(this.label7);
            this.tabPageIpHeader.Controls.Add(this.txtIpLen);
            this.tabPageIpHeader.Controls.Add(this.label6);
            this.tabPageIpHeader.Controls.Add(this.txtIpTos);
            this.tabPageIpHeader.Controls.Add(this.label5);
            this.tabPageIpHeader.Controls.Add(this.txtIpHlen);
            this.tabPageIpHeader.Controls.Add(this.label4);
            this.tabPageIpHeader.Controls.Add(this.txtIpVer);
            this.tabPageIpHeader.Controls.Add(this.label3);
            this.tabPageIpHeader.Controls.Add(this.ipDest);
            this.tabPageIpHeader.Controls.Add(this.label2);
            this.tabPageIpHeader.Controls.Add(this.label1);
            this.tabPageIpHeader.Controls.Add(this.ipSource);
            this.tabPageIpHeader.Location = new System.Drawing.Point(4, 24);
            this.tabPageIpHeader.Name = "tabPageIpHeader";
            this.tabPageIpHeader.Size = new System.Drawing.Size(762, 140);
            this.tabPageIpHeader.TabIndex = 2;
            this.tabPageIpHeader.Text = "IP Header Info";
            this.tabPageIpHeader.UseVisualStyleBackColor = true;
            // 
            // txtIpCsum
            // 
            this.txtIpCsum.Location = new System.Drawing.Point(710, 32);
            this.txtIpCsum.Name = "txtIpCsum";
            this.txtIpCsum.ReadOnly = true;
            this.txtIpCsum.Size = new System.Drawing.Size(44, 23);
            this.txtIpCsum.TabIndex = 23;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(706, 13);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(38, 15);
            this.label12.TabIndex = 11;
            this.label12.Text = "Csum";
            // 
            // txtIpProto
            // 
            this.txtIpProto.Location = new System.Drawing.Point(658, 32);
            this.txtIpProto.Name = "txtIpProto";
            this.txtIpProto.ReadOnly = true;
            this.txtIpProto.Size = new System.Drawing.Size(44, 23);
            this.txtIpProto.TabIndex = 22;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(656, 13);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(36, 15);
            this.label11.TabIndex = 10;
            this.label11.Text = "Proto";
            // 
            // txtIpTtl
            // 
            this.txtIpTtl.Location = new System.Drawing.Point(605, 32);
            this.txtIpTtl.Name = "txtIpTtl";
            this.txtIpTtl.ReadOnly = true;
            this.txtIpTtl.Size = new System.Drawing.Size(44, 23);
            this.txtIpTtl.TabIndex = 21;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(603, 13);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(27, 15);
            this.label10.TabIndex = 9;
            this.label10.Text = "TTL";
            // 
            // txtIpOff
            // 
            this.txtIpOff.Location = new System.Drawing.Point(553, 32);
            this.txtIpOff.Name = "txtIpOff";
            this.txtIpOff.ReadOnly = true;
            this.txtIpOff.Size = new System.Drawing.Size(44, 23);
            this.txtIpOff.TabIndex = 20;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(549, 13);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(24, 15);
            this.label9.TabIndex = 8;
            this.label9.Text = "Off";
            // 
            // txtIpFlags
            // 
            this.txtIpFlags.Location = new System.Drawing.Point(500, 32);
            this.txtIpFlags.Name = "txtIpFlags";
            this.txtIpFlags.ReadOnly = true;
            this.txtIpFlags.Size = new System.Drawing.Size(44, 23);
            this.txtIpFlags.TabIndex = 19;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(498, 13);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(34, 15);
            this.label8.TabIndex = 7;
            this.label8.Text = "Flags";
            // 
            // txtIpId
            // 
            this.txtIpId.Location = new System.Drawing.Point(448, 32);
            this.txtIpId.Name = "txtIpId";
            this.txtIpId.ReadOnly = true;
            this.txtIpId.Size = new System.Drawing.Size(44, 23);
            this.txtIpId.TabIndex = 18;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(447, 13);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(18, 15);
            this.label7.TabIndex = 6;
            this.label7.Text = "ID";
            // 
            // txtIpLen
            // 
            this.txtIpLen.Location = new System.Drawing.Point(395, 32);
            this.txtIpLen.Name = "txtIpLen";
            this.txtIpLen.ReadOnly = true;
            this.txtIpLen.Size = new System.Drawing.Size(44, 23);
            this.txtIpLen.TabIndex = 17;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(393, 13);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(26, 15);
            this.label6.TabIndex = 5;
            this.label6.Text = "Len";
            // 
            // txtIpTos
            // 
            this.txtIpTos.Location = new System.Drawing.Point(343, 32);
            this.txtIpTos.Name = "txtIpTos";
            this.txtIpTos.ReadOnly = true;
            this.txtIpTos.Size = new System.Drawing.Size(44, 23);
            this.txtIpTos.TabIndex = 16;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(341, 13);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(26, 15);
            this.label5.TabIndex = 4;
            this.label5.Text = "Tos";
            // 
            // txtIpHlen
            // 
            this.txtIpHlen.Location = new System.Drawing.Point(290, 32);
            this.txtIpHlen.Name = "txtIpHlen";
            this.txtIpHlen.ReadOnly = true;
            this.txtIpHlen.Size = new System.Drawing.Size(44, 23);
            this.txtIpHlen.TabIndex = 15;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(287, 13);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(32, 15);
            this.label4.TabIndex = 3;
            this.label4.Text = "Hlen";
            // 
            // txtIpVer
            // 
            this.txtIpVer.Location = new System.Drawing.Point(238, 32);
            this.txtIpVer.Name = "txtIpVer";
            this.txtIpVer.ReadOnly = true;
            this.txtIpVer.Size = new System.Drawing.Size(44, 23);
            this.txtIpVer.TabIndex = 14;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(234, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(24, 15);
            this.label3.TabIndex = 2;
            this.label3.Text = "Ver";
            // 
            // ipDest
            // 
            this.ipDest.AllowInternalTab = false;
            this.ipDest.AutoHeight = true;
            this.ipDest.BackColor = System.Drawing.SystemColors.Window;
            this.ipDest.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ipDest.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.ipDest.Location = new System.Drawing.Point(127, 32);
            this.ipDest.MinimumSize = new System.Drawing.Size(84, 23);
            this.ipDest.Name = "ipDest";
            this.ipDest.ReadOnly = true;
            this.ipDest.Size = new System.Drawing.Size(88, 23);
            this.ipDest.TabIndex = 13;
            this.ipDest.Text = "...";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(124, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "Destination IP";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Source IP";
            // 
            // ipSource
            // 
            this.ipSource.AllowInternalTab = false;
            this.ipSource.AutoHeight = true;
            this.ipSource.BackColor = System.Drawing.SystemColors.Window;
            this.ipSource.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ipSource.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.ipSource.Location = new System.Drawing.Point(15, 32);
            this.ipSource.MinimumSize = new System.Drawing.Size(84, 23);
            this.ipSource.Name = "ipSource";
            this.ipSource.ReadOnly = true;
            this.ipSource.Size = new System.Drawing.Size(89, 23);
            this.ipSource.TabIndex = 12;
            this.ipSource.Text = "...";
            // 
            // tabPageSignature
            // 
            this.tabPageSignature.Controls.Add(this.btnLinkedRules);
            this.tabPageSignature.Controls.Add(this.btnRule);
            this.tabPageSignature.Controls.Add(this.txtRule);
            this.tabPageSignature.Controls.Add(this.label30);
            this.tabPageSignature.Controls.Add(this.txtSigCategory);
            this.tabPageSignature.Controls.Add(this.lblSigCat);
            this.tabPageSignature.Controls.Add(this.txtSigSigRev);
            this.tabPageSignature.Controls.Add(this.label15);
            this.tabPageSignature.Controls.Add(this.txtSigSigId);
            this.tabPageSignature.Controls.Add(this.label14);
            this.tabPageSignature.Controls.Add(this.txtSigGenId);
            this.tabPageSignature.Controls.Add(this.label13);
            this.tabPageSignature.Location = new System.Drawing.Point(4, 24);
            this.tabPageSignature.Name = "tabPageSignature";
            this.tabPageSignature.Size = new System.Drawing.Size(762, 140);
            this.tabPageSignature.TabIndex = 3;
            this.tabPageSignature.Text = "Signature Info";
            this.tabPageSignature.UseVisualStyleBackColor = true;
            // 
            // btnLinkedRules
            // 
            this.btnLinkedRules.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLinkedRules.Image = ((System.Drawing.Image)(resources.GetObject("btnLinkedRules.Image")));
            this.btnLinkedRules.Location = new System.Drawing.Point(727, 93);
            this.btnLinkedRules.Name = "btnLinkedRules";
            this.btnLinkedRules.Size = new System.Drawing.Size(26, 26);
            this.btnLinkedRules.TabIndex = 11;
            this.btnLinkedRules.UseVisualStyleBackColor = true;
            this.btnLinkedRules.Click += new System.EventHandler(this.btnLinkedRules_Click);
            // 
            // btnRule
            // 
            this.btnRule.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRule.Image = ((System.Drawing.Image)(resources.GetObject("btnRule.Image")));
            this.btnRule.Location = new System.Drawing.Point(699, 93);
            this.btnRule.Name = "btnRule";
            this.btnRule.Size = new System.Drawing.Size(26, 26);
            this.btnRule.TabIndex = 10;
            this.btnRule.UseVisualStyleBackColor = true;
            this.btnRule.Click += new System.EventHandler(this.btnRule_Click);
            // 
            // txtRule
            // 
            this.txtRule.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRule.Location = new System.Drawing.Point(13, 95);
            this.txtRule.Name = "txtRule";
            this.txtRule.ReadOnly = true;
            this.txtRule.Size = new System.Drawing.Size(681, 23);
            this.txtRule.TabIndex = 9;
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(12, 73);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(30, 15);
            this.label30.TabIndex = 8;
            this.label30.Text = "Rule";
            // 
            // txtSigCategory
            // 
            this.txtSigCategory.Location = new System.Drawing.Point(217, 33);
            this.txtSigCategory.Name = "txtSigCategory";
            this.txtSigCategory.ReadOnly = true;
            this.txtSigCategory.Size = new System.Drawing.Size(361, 23);
            this.txtSigCategory.TabIndex = 7;
            // 
            // lblSigCat
            // 
            this.lblSigCat.AutoSize = true;
            this.lblSigCat.Location = new System.Drawing.Point(215, 13);
            this.lblSigCat.Name = "lblSigCat";
            this.lblSigCat.Size = new System.Drawing.Size(55, 15);
            this.lblSigCat.TabIndex = 3;
            this.lblSigCat.Text = "Category";
            // 
            // txtSigSigRev
            // 
            this.txtSigSigRev.Location = new System.Drawing.Point(156, 33);
            this.txtSigSigRev.Name = "txtSigSigRev";
            this.txtSigSigRev.ReadOnly = true;
            this.txtSigSigRev.Size = new System.Drawing.Size(44, 23);
            this.txtSigSigRev.TabIndex = 6;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(154, 13);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(48, 15);
            this.label15.TabIndex = 2;
            this.label15.Text = "Sig. Rev";
            // 
            // txtSigSigId
            // 
            this.txtSigSigId.Location = new System.Drawing.Point(65, 33);
            this.txtSigSigId.Name = "txtSigSigId";
            this.txtSigSigId.ReadOnly = true;
            this.txtSigSigId.Size = new System.Drawing.Size(83, 23);
            this.txtSigSigId.TabIndex = 5;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(63, 13);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(40, 15);
            this.label14.TabIndex = 1;
            this.label14.Text = "Sig. ID";
            // 
            // txtSigGenId
            // 
            this.txtSigGenId.Location = new System.Drawing.Point(14, 33);
            this.txtSigGenId.Name = "txtSigGenId";
            this.txtSigGenId.ReadOnly = true;
            this.txtSigGenId.Size = new System.Drawing.Size(44, 23);
            this.txtSigGenId.TabIndex = 4;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(10, 13);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(45, 15);
            this.label13.TabIndex = 0;
            this.label13.Text = "Gen. ID";
            // 
            // tabPageTcpHeader
            // 
            this.tabPageTcpHeader.Controls.Add(this.txtTcpUrp);
            this.tabPageTcpHeader.Controls.Add(this.label25);
            this.tabPageTcpHeader.Controls.Add(this.txtTcpCsum);
            this.tabPageTcpHeader.Controls.Add(this.label24);
            this.tabPageTcpHeader.Controls.Add(this.txtTcpWin);
            this.tabPageTcpHeader.Controls.Add(this.label23);
            this.tabPageTcpHeader.Controls.Add(this.txtTcpFlags);
            this.tabPageTcpHeader.Controls.Add(this.label22);
            this.tabPageTcpHeader.Controls.Add(this.txtTcpRes);
            this.tabPageTcpHeader.Controls.Add(this.label21);
            this.tabPageTcpHeader.Controls.Add(this.txtTcpOff);
            this.tabPageTcpHeader.Controls.Add(this.label20);
            this.tabPageTcpHeader.Controls.Add(this.txtTcpAck);
            this.tabPageTcpHeader.Controls.Add(this.label19);
            this.tabPageTcpHeader.Controls.Add(this.txtTcpSeq);
            this.tabPageTcpHeader.Controls.Add(this.label18);
            this.tabPageTcpHeader.Controls.Add(this.txtTcpDstPort);
            this.tabPageTcpHeader.Controls.Add(this.label17);
            this.tabPageTcpHeader.Controls.Add(this.txtTcpSrcPrt);
            this.tabPageTcpHeader.Controls.Add(this.label16);
            this.tabPageTcpHeader.Location = new System.Drawing.Point(4, 24);
            this.tabPageTcpHeader.Name = "tabPageTcpHeader";
            this.tabPageTcpHeader.Size = new System.Drawing.Size(762, 140);
            this.tabPageTcpHeader.TabIndex = 4;
            this.tabPageTcpHeader.Text = "TCP Header";
            this.tabPageTcpHeader.UseVisualStyleBackColor = true;
            // 
            // txtTcpUrp
            // 
            this.txtTcpUrp.Location = new System.Drawing.Point(720, 33);
            this.txtTcpUrp.Name = "txtTcpUrp";
            this.txtTcpUrp.ReadOnly = true;
            this.txtTcpUrp.Size = new System.Drawing.Size(44, 23);
            this.txtTcpUrp.TabIndex = 19;
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(717, 13);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(29, 15);
            this.label25.TabIndex = 9;
            this.label25.Text = "URP";
            // 
            // txtTcpCsum
            // 
            this.txtTcpCsum.Location = new System.Drawing.Point(665, 33);
            this.txtTcpCsum.Name = "txtTcpCsum";
            this.txtTcpCsum.ReadOnly = true;
            this.txtTcpCsum.Size = new System.Drawing.Size(44, 23);
            this.txtTcpCsum.TabIndex = 18;
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(660, 13);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(38, 15);
            this.label24.TabIndex = 8;
            this.label24.Text = "Csum";
            // 
            // txtTcpWin
            // 
            this.txtTcpWin.Location = new System.Drawing.Point(610, 33);
            this.txtTcpWin.Name = "txtTcpWin";
            this.txtTcpWin.ReadOnly = true;
            this.txtTcpWin.Size = new System.Drawing.Size(44, 23);
            this.txtTcpWin.TabIndex = 17;
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(607, 13);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(28, 15);
            this.label23.TabIndex = 7;
            this.label23.Text = "Win";
            // 
            // txtTcpFlags
            // 
            this.txtTcpFlags.Location = new System.Drawing.Point(555, 33);
            this.txtTcpFlags.Name = "txtTcpFlags";
            this.txtTcpFlags.ReadOnly = true;
            this.txtTcpFlags.Size = new System.Drawing.Size(44, 23);
            this.txtTcpFlags.TabIndex = 16;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(553, 13);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(34, 15);
            this.label22.TabIndex = 6;
            this.label22.Text = "Flags";
            // 
            // txtTcpRes
            // 
            this.txtTcpRes.Location = new System.Drawing.Point(500, 33);
            this.txtTcpRes.Name = "txtTcpRes";
            this.txtTcpRes.ReadOnly = true;
            this.txtTcpRes.Size = new System.Drawing.Size(44, 23);
            this.txtTcpRes.TabIndex = 15;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(499, 13);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(25, 15);
            this.label21.TabIndex = 5;
            this.label21.Text = "Res";
            // 
            // txtTcpOff
            // 
            this.txtTcpOff.Location = new System.Drawing.Point(446, 33);
            this.txtTcpOff.Name = "txtTcpOff";
            this.txtTcpOff.ReadOnly = true;
            this.txtTcpOff.Size = new System.Drawing.Size(44, 23);
            this.txtTcpOff.TabIndex = 14;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(444, 13);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(24, 15);
            this.label20.TabIndex = 4;
            this.label20.Text = "Off";
            // 
            // txtTcpAck
            // 
            this.txtTcpAck.Location = new System.Drawing.Point(307, 33);
            this.txtTcpAck.Name = "txtTcpAck";
            this.txtTcpAck.ReadOnly = true;
            this.txtTcpAck.Size = new System.Drawing.Size(128, 23);
            this.txtTcpAck.TabIndex = 13;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(306, 13);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(27, 15);
            this.label19.TabIndex = 3;
            this.label19.Text = "Ack";
            // 
            // txtTcpSeq
            // 
            this.txtTcpSeq.Location = new System.Drawing.Point(168, 33);
            this.txtTcpSeq.Name = "txtTcpSeq";
            this.txtTcpSeq.ReadOnly = true;
            this.txtTcpSeq.Size = new System.Drawing.Size(128, 23);
            this.txtTcpSeq.TabIndex = 12;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(167, 13);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(26, 15);
            this.label18.TabIndex = 2;
            this.label18.Text = "Seq";
            // 
            // txtTcpDstPort
            // 
            this.txtTcpDstPort.Location = new System.Drawing.Point(91, 33);
            this.txtTcpDstPort.Name = "txtTcpDstPort";
            this.txtTcpDstPort.ReadOnly = true;
            this.txtTcpDstPort.Size = new System.Drawing.Size(66, 23);
            this.txtTcpDstPort.TabIndex = 11;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(89, 13);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(49, 15);
            this.label17.TabIndex = 1;
            this.label17.Text = "Dst Port";
            // 
            // txtTcpSrcPrt
            // 
            this.txtTcpSrcPrt.Location = new System.Drawing.Point(14, 33);
            this.txtTcpSrcPrt.Name = "txtTcpSrcPrt";
            this.txtTcpSrcPrt.ReadOnly = true;
            this.txtTcpSrcPrt.Size = new System.Drawing.Size(66, 23);
            this.txtTcpSrcPrt.TabIndex = 10;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(13, 13);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(48, 15);
            this.label16.TabIndex = 0;
            this.label16.Text = "Src Port";
            // 
            // tabPageUdpHeader
            // 
            this.tabPageUdpHeader.Controls.Add(this.txtUdpCsum);
            this.tabPageUdpHeader.Controls.Add(this.label26);
            this.tabPageUdpHeader.Controls.Add(this.txtUdpLen);
            this.tabPageUdpHeader.Controls.Add(this.label27);
            this.tabPageUdpHeader.Controls.Add(this.txtUdpDstPort);
            this.tabPageUdpHeader.Controls.Add(this.label28);
            this.tabPageUdpHeader.Controls.Add(this.txtUdpSrcPort);
            this.tabPageUdpHeader.Controls.Add(this.label29);
            this.tabPageUdpHeader.Location = new System.Drawing.Point(4, 24);
            this.tabPageUdpHeader.Name = "tabPageUdpHeader";
            this.tabPageUdpHeader.Size = new System.Drawing.Size(762, 140);
            this.tabPageUdpHeader.TabIndex = 6;
            this.tabPageUdpHeader.Text = "UDP Header";
            this.tabPageUdpHeader.UseVisualStyleBackColor = true;
            // 
            // txtUdpCsum
            // 
            this.txtUdpCsum.Location = new System.Drawing.Point(223, 33);
            this.txtUdpCsum.Name = "txtUdpCsum";
            this.txtUdpCsum.ReadOnly = true;
            this.txtUdpCsum.Size = new System.Drawing.Size(44, 23);
            this.txtUdpCsum.TabIndex = 7;
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(219, 13);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(38, 15);
            this.label26.TabIndex = 3;
            this.label26.Text = "Csum";
            // 
            // txtUdpLen
            // 
            this.txtUdpLen.Location = new System.Drawing.Point(168, 33);
            this.txtUdpLen.Name = "txtUdpLen";
            this.txtUdpLen.ReadOnly = true;
            this.txtUdpLen.Size = new System.Drawing.Size(44, 23);
            this.txtUdpLen.TabIndex = 6;
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(166, 13);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(26, 15);
            this.label27.TabIndex = 2;
            this.label27.Text = "Len";
            // 
            // txtUdpDstPort
            // 
            this.txtUdpDstPort.Location = new System.Drawing.Point(91, 33);
            this.txtUdpDstPort.Name = "txtUdpDstPort";
            this.txtUdpDstPort.ReadOnly = true;
            this.txtUdpDstPort.Size = new System.Drawing.Size(66, 23);
            this.txtUdpDstPort.TabIndex = 5;
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(89, 13);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(49, 15);
            this.label28.TabIndex = 1;
            this.label28.Text = "Dst Port";
            // 
            // txtUdpSrcPort
            // 
            this.txtUdpSrcPort.Location = new System.Drawing.Point(14, 33);
            this.txtUdpSrcPort.Name = "txtUdpSrcPort";
            this.txtUdpSrcPort.ReadOnly = true;
            this.txtUdpSrcPort.Size = new System.Drawing.Size(66, 23);
            this.txtUdpSrcPort.TabIndex = 4;
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(13, 13);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(48, 15);
            this.label29.TabIndex = 0;
            this.label29.Text = "Src Port";
            // 
            // tabPageReferences
            // 
            this.tabPageReferences.Controls.Add(this.listReferences);
            this.tabPageReferences.Location = new System.Drawing.Point(4, 24);
            this.tabPageReferences.Name = "tabPageReferences";
            this.tabPageReferences.Size = new System.Drawing.Size(762, 140);
            this.tabPageReferences.TabIndex = 5;
            this.tabPageReferences.Text = "References";
            this.tabPageReferences.UseVisualStyleBackColor = true;
            // 
            // listReferences
            // 
            this.listReferences.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listReferences.FullRowSelect = true;
            this.listReferences.HideSelection = false;
            this.listReferences.Location = new System.Drawing.Point(0, 0);
            this.listReferences.MultiSelect = false;
            this.listReferences.Name = "listReferences";
            this.listReferences.ShowGroups = false;
            this.listReferences.Size = new System.Drawing.Size(762, 140);
            this.listReferences.TabIndex = 0;
            this.listReferences.UseCompatibleStateImageBehavior = false;
            this.listReferences.View = System.Windows.Forms.View.Details;
            this.listReferences.VirtualMode = true;
            // 
            // tabPageHex
            // 
            this.tabPageHex.Controls.Add(this.hexEvent);
            this.tabPageHex.Location = new System.Drawing.Point(4, 24);
            this.tabPageHex.Name = "tabPageHex";
            this.tabPageHex.Size = new System.Drawing.Size(762, 140);
            this.tabPageHex.TabIndex = 0;
            this.tabPageHex.Text = "Payload (Hex)";
            this.tabPageHex.UseVisualStyleBackColor = true;
            // 
            // hexEvent
            // 
            this.hexEvent.ContextMenuStrip = this.ctxMenuHex;
            this.hexEvent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hexEvent.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hexEvent.InfoForeColor = System.Drawing.Color.Gray;
            this.hexEvent.LineInfoVisible = true;
            this.hexEvent.Location = new System.Drawing.Point(0, 0);
            this.hexEvent.Name = "hexEvent";
            this.hexEvent.ReadOnly = true;
            this.hexEvent.ShadowSelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(60)))), ((int)(((byte)(188)))), ((int)(((byte)(255)))));
            this.hexEvent.Size = new System.Drawing.Size(762, 140);
            this.hexEvent.StringViewVisible = true;
            this.hexEvent.TabIndex = 0;
            this.hexEvent.UseFixedBytesPerLine = true;
            this.hexEvent.VScrollBarVisible = true;
            // 
            // ctxMenuHex
            // 
            this.ctxMenuHex.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ctxMenuHexCopyText,
            this.ctxMenuHexCopyHex,
            this.ctxMenuHexSep1,
            this.ctxMenuHexSelectAll});
            this.ctxMenuHex.Name = "ctxMenuHex";
            this.ctxMenuHex.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.ctxMenuHex.Size = new System.Drawing.Size(128, 76);
            // 
            // ctxMenuHexCopyText
            // 
            this.ctxMenuHexCopyText.Name = "ctxMenuHexCopyText";
            this.ctxMenuHexCopyText.Size = new System.Drawing.Size(127, 22);
            this.ctxMenuHexCopyText.Text = "Copy Text";
            this.ctxMenuHexCopyText.Click += new System.EventHandler(this.ctxMenuHexCopyText_Click);
            // 
            // ctxMenuHexCopyHex
            // 
            this.ctxMenuHexCopyHex.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ctxMenuHexCopyHexSpaces,
            this.ctxMenuHexCopyHexNoSpaces});
            this.ctxMenuHexCopyHex.Name = "ctxMenuHexCopyHex";
            this.ctxMenuHexCopyHex.Size = new System.Drawing.Size(127, 22);
            this.ctxMenuHexCopyHex.Text = "Copy Hex";
            // 
            // ctxMenuHexCopyHexSpaces
            // 
            this.ctxMenuHexCopyHexSpaces.Name = "ctxMenuHexCopyHexSpaces";
            this.ctxMenuHexCopyHexSpaces.Size = new System.Drawing.Size(138, 22);
            this.ctxMenuHexCopyHexSpaces.Text = "With Spaces";
            this.ctxMenuHexCopyHexSpaces.Click += new System.EventHandler(this.ctxMenuHexCopyHexSpaces_Click_1);
            // 
            // ctxMenuHexCopyHexNoSpaces
            // 
            this.ctxMenuHexCopyHexNoSpaces.Name = "ctxMenuHexCopyHexNoSpaces";
            this.ctxMenuHexCopyHexNoSpaces.Size = new System.Drawing.Size(138, 22);
            this.ctxMenuHexCopyHexNoSpaces.Text = "No Spaces";
            this.ctxMenuHexCopyHexNoSpaces.Click += new System.EventHandler(this.ctxMenuHexCopyHexNoSpaces_Click_1);
            // 
            // ctxMenuHexSep1
            // 
            this.ctxMenuHexSep1.Name = "ctxMenuHexSep1";
            this.ctxMenuHexSep1.Size = new System.Drawing.Size(124, 6);
            // 
            // ctxMenuHexSelectAll
            // 
            this.ctxMenuHexSelectAll.Name = "ctxMenuHexSelectAll";
            this.ctxMenuHexSelectAll.Size = new System.Drawing.Size(127, 22);
            this.ctxMenuHexSelectAll.Text = "Select All";
            this.ctxMenuHexSelectAll.Click += new System.EventHandler(this.ctxMenuHexSelectAll_Click);
            // 
            // tabPageAscii
            // 
            this.tabPageAscii.Controls.Add(this.txtPayloadAscii);
            this.tabPageAscii.Location = new System.Drawing.Point(4, 24);
            this.tabPageAscii.Name = "tabPageAscii";
            this.tabPageAscii.Size = new System.Drawing.Size(762, 140);
            this.tabPageAscii.TabIndex = 1;
            this.tabPageAscii.Text = "Payload (ASCII)";
            this.tabPageAscii.UseVisualStyleBackColor = true;
            // 
            // txtPayloadAscii
            // 
            this.txtPayloadAscii.DetectUrls = false;
            this.txtPayloadAscii.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtPayloadAscii.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPayloadAscii.Location = new System.Drawing.Point(0, 0);
            this.txtPayloadAscii.Name = "txtPayloadAscii";
            this.txtPayloadAscii.ReadOnly = true;
            this.txtPayloadAscii.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedBoth;
            this.txtPayloadAscii.Size = new System.Drawing.Size(762, 140);
            this.txtPayloadAscii.TabIndex = 0;
            this.txtPayloadAscii.Text = "";
            // 
            // tabPageMisc
            // 
            this.tabPageMisc.Controls.Add(this.txtPriority);
            this.tabPageMisc.Controls.Add(this.lblMiscPriority);
            this.tabPageMisc.Controls.Add(this.txtSensor);
            this.tabPageMisc.Controls.Add(this.lblSensor);
            this.tabPageMisc.Controls.Add(this.txtEventCid);
            this.tabPageMisc.Controls.Add(this.lblEventCid);
            this.tabPageMisc.Controls.Add(this.txtEventSid);
            this.tabPageMisc.Controls.Add(this.lblEventSid);
            this.tabPageMisc.Location = new System.Drawing.Point(4, 24);
            this.tabPageMisc.Name = "tabPageMisc";
            this.tabPageMisc.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageMisc.Size = new System.Drawing.Size(762, 140);
            this.tabPageMisc.TabIndex = 7;
            this.tabPageMisc.Text = "Misc";
            this.tabPageMisc.UseVisualStyleBackColor = true;
            // 
            // txtSensor
            // 
            this.txtSensor.Location = new System.Drawing.Point(322, 27);
            this.txtSensor.Name = "txtSensor";
            this.txtSensor.ReadOnly = true;
            this.txtSensor.Size = new System.Drawing.Size(233, 23);
            this.txtSensor.TabIndex = 14;
            // 
            // lblSensor
            // 
            this.lblSensor.AutoSize = true;
            this.lblSensor.Location = new System.Drawing.Point(319, 8);
            this.lblSensor.Name = "lblSensor";
            this.lblSensor.Size = new System.Drawing.Size(42, 15);
            this.lblSensor.TabIndex = 13;
            this.lblSensor.Text = "Sensor";
            // 
            // txtEventCid
            // 
            this.txtEventCid.Location = new System.Drawing.Point(73, 27);
            this.txtEventCid.Name = "txtEventCid";
            this.txtEventCid.ReadOnly = true;
            this.txtEventCid.Size = new System.Drawing.Size(238, 23);
            this.txtEventCid.TabIndex = 12;
            // 
            // lblEventCid
            // 
            this.lblEventCid.AutoSize = true;
            this.lblEventCid.Location = new System.Drawing.Point(71, 8);
            this.lblEventCid.Name = "lblEventCid";
            this.lblEventCid.Size = new System.Drawing.Size(58, 15);
            this.lblEventCid.TabIndex = 11;
            this.lblEventCid.Text = "Event CID";
            // 
            // txtEventSid
            // 
            this.txtEventSid.Location = new System.Drawing.Point(14, 27);
            this.txtEventSid.Name = "txtEventSid";
            this.txtEventSid.ReadOnly = true;
            this.txtEventSid.Size = new System.Drawing.Size(48, 23);
            this.txtEventSid.TabIndex = 10;
            // 
            // lblEventSid
            // 
            this.lblEventSid.AutoSize = true;
            this.lblEventSid.Location = new System.Drawing.Point(11, 8);
            this.lblEventSid.Name = "lblEventSid";
            this.lblEventSid.Size = new System.Drawing.Size(56, 15);
            this.lblEventSid.TabIndex = 9;
            this.lblEventSid.Text = "Event SID";
            // 
            // tabPageDns
            // 
            this.tabPageDns.Controls.Add(this.txtDns);
            this.tabPageDns.Location = new System.Drawing.Point(4, 24);
            this.tabPageDns.Name = "tabPageDns";
            this.tabPageDns.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageDns.Size = new System.Drawing.Size(762, 140);
            this.tabPageDns.TabIndex = 9;
            this.tabPageDns.Text = "DNS";
            this.tabPageDns.UseVisualStyleBackColor = true;
            // 
            // txtDns
            // 
            this.txtDns.DetectUrls = false;
            this.txtDns.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDns.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDns.Location = new System.Drawing.Point(3, 3);
            this.txtDns.Name = "txtDns";
            this.txtDns.ReadOnly = true;
            this.txtDns.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedBoth;
            this.txtDns.Size = new System.Drawing.Size(756, 134);
            this.txtDns.TabIndex = 1;
            this.txtDns.Text = "";
            // 
            // tabPageAcknowledgement
            // 
            this.tabPageAcknowledgement.Controls.Add(this.label31);
            this.tabPageAcknowledgement.Controls.Add(this.chkAckSuccessful);
            this.tabPageAcknowledgement.Controls.Add(this.txtAckTimestamp);
            this.tabPageAcknowledgement.Controls.Add(this.lblAckTimestamp);
            this.tabPageAcknowledgement.Controls.Add(this.txtAckNotes);
            this.tabPageAcknowledgement.Controls.Add(this.lblAckNotes);
            this.tabPageAcknowledgement.Controls.Add(this.txtAckClassification);
            this.tabPageAcknowledgement.Controls.Add(this.lblAckClassification);
            this.tabPageAcknowledgement.Controls.Add(this.txtAckInitials);
            this.tabPageAcknowledgement.Controls.Add(this.lblAckInitials);
            this.tabPageAcknowledgement.Location = new System.Drawing.Point(4, 24);
            this.tabPageAcknowledgement.Name = "tabPageAcknowledgement";
            this.tabPageAcknowledgement.Size = new System.Drawing.Size(762, 140);
            this.tabPageAcknowledgement.TabIndex = 8;
            this.tabPageAcknowledgement.Text = "Acknowledgement";
            this.tabPageAcknowledgement.UseVisualStyleBackColor = true;
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(517, 10);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(62, 15);
            this.label31.TabIndex = 21;
            this.label31.Text = "Successful";
            // 
            // chkAckSuccessful
            // 
            this.chkAckSuccessful.AutoSize = true;
            this.chkAckSuccessful.Enabled = false;
            this.chkAckSuccessful.Location = new System.Drawing.Point(541, 30);
            this.chkAckSuccessful.Name = "chkAckSuccessful";
            this.chkAckSuccessful.Size = new System.Drawing.Size(15, 14);
            this.chkAckSuccessful.TabIndex = 20;
            this.chkAckSuccessful.UseVisualStyleBackColor = true;
            // 
            // txtAckTimestamp
            // 
            this.txtAckTimestamp.Location = new System.Drawing.Point(307, 27);
            this.txtAckTimestamp.Name = "txtAckTimestamp";
            this.txtAckTimestamp.ReadOnly = true;
            this.txtAckTimestamp.Size = new System.Drawing.Size(205, 23);
            this.txtAckTimestamp.TabIndex = 19;
            // 
            // lblAckTimestamp
            // 
            this.lblAckTimestamp.AutoSize = true;
            this.lblAckTimestamp.Location = new System.Drawing.Point(305, 7);
            this.lblAckTimestamp.Name = "lblAckTimestamp";
            this.lblAckTimestamp.Size = new System.Drawing.Size(67, 15);
            this.lblAckTimestamp.TabIndex = 18;
            this.lblAckTimestamp.Text = "Timestamp";
            // 
            // txtAckNotes
            // 
            this.txtAckNotes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAckNotes.Location = new System.Drawing.Point(13, 75);
            this.txtAckNotes.Multiline = true;
            this.txtAckNotes.Name = "txtAckNotes";
            this.txtAckNotes.ReadOnly = true;
            this.txtAckNotes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtAckNotes.Size = new System.Drawing.Size(738, 55);
            this.txtAckNotes.TabIndex = 17;
            // 
            // lblAckNotes
            // 
            this.lblAckNotes.AutoSize = true;
            this.lblAckNotes.Location = new System.Drawing.Point(12, 56);
            this.lblAckNotes.Name = "lblAckNotes";
            this.lblAckNotes.Size = new System.Drawing.Size(38, 15);
            this.lblAckNotes.TabIndex = 16;
            this.lblAckNotes.Text = "Notes";
            // 
            // txtAckClassification
            // 
            this.txtAckClassification.Location = new System.Drawing.Point(91, 27);
            this.txtAckClassification.Name = "txtAckClassification";
            this.txtAckClassification.ReadOnly = true;
            this.txtAckClassification.Size = new System.Drawing.Size(205, 23);
            this.txtAckClassification.TabIndex = 15;
            // 
            // lblAckClassification
            // 
            this.lblAckClassification.AutoSize = true;
            this.lblAckClassification.Location = new System.Drawing.Point(89, 7);
            this.lblAckClassification.Name = "lblAckClassification";
            this.lblAckClassification.Size = new System.Drawing.Size(77, 15);
            this.lblAckClassification.TabIndex = 13;
            this.lblAckClassification.Text = "Classification";
            // 
            // txtAckInitials
            // 
            this.txtAckInitials.Location = new System.Drawing.Point(14, 27);
            this.txtAckInitials.Name = "txtAckInitials";
            this.txtAckInitials.ReadOnly = true;
            this.txtAckInitials.Size = new System.Drawing.Size(66, 23);
            this.txtAckInitials.TabIndex = 14;
            // 
            // lblAckInitials
            // 
            this.lblAckInitials.AutoSize = true;
            this.lblAckInitials.Location = new System.Drawing.Point(13, 7);
            this.lblAckInitials.Name = "lblAckInitials";
            this.lblAckInitials.Size = new System.Drawing.Size(41, 15);
            this.lblAckInitials.TabIndex = 12;
            this.lblAckInitials.Text = "Initials";
            // 
            // txtPriority
            // 
            this.txtPriority.Location = new System.Drawing.Point(565, 27);
            this.txtPriority.Name = "txtPriority";
            this.txtPriority.ReadOnly = true;
            this.txtPriority.Size = new System.Drawing.Size(48, 23);
            this.txtPriority.TabIndex = 16;
            // 
            // lblMiscPriority
            // 
            this.lblMiscPriority.AutoSize = true;
            this.lblMiscPriority.Location = new System.Drawing.Point(562, 8);
            this.lblMiscPriority.Name = "lblMiscPriority";
            this.lblMiscPriority.Size = new System.Drawing.Size(45, 15);
            this.lblMiscPriority.TabIndex = 15;
            this.lblMiscPriority.Text = "Priority";
            // 
            // ControlEventInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabEvent);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "ControlEventInfo";
            this.Size = new System.Drawing.Size(770, 168);
            this.tabEvent.ResumeLayout(false);
            this.tabPageIpHeader.ResumeLayout(false);
            this.tabPageIpHeader.PerformLayout();
            this.tabPageSignature.ResumeLayout(false);
            this.tabPageSignature.PerformLayout();
            this.tabPageTcpHeader.ResumeLayout(false);
            this.tabPageTcpHeader.PerformLayout();
            this.tabPageUdpHeader.ResumeLayout(false);
            this.tabPageUdpHeader.PerformLayout();
            this.tabPageReferences.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.listReferences)).EndInit();
            this.tabPageHex.ResumeLayout(false);
            this.ctxMenuHex.ResumeLayout(false);
            this.tabPageAscii.ResumeLayout(false);
            this.tabPageMisc.ResumeLayout(false);
            this.tabPageMisc.PerformLayout();
            this.tabPageDns.ResumeLayout(false);
            this.tabPageAcknowledgement.ResumeLayout(false);
            this.tabPageAcknowledgement.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabEvent;
        private System.Windows.Forms.TabPage tabPageIpHeader;
        private System.Windows.Forms.TextBox txtIpCsum;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtIpProto;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtIpTtl;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtIpOff;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtIpFlags;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtIpId;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtIpLen;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtIpTos;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtIpHlen;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtIpVer;
        private System.Windows.Forms.Label label3;
        private IPAddressControlLib.IPAddressControl ipDest;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private IPAddressControlLib.IPAddressControl ipSource;
        private System.Windows.Forms.TabPage tabPageSignature;
        private System.Windows.Forms.Button btnRule;
        private System.Windows.Forms.TextBox txtRule;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.TextBox txtSigCategory;
        private System.Windows.Forms.Label lblSigCat;
        private System.Windows.Forms.TextBox txtSigSigRev;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtSigSigId;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtSigGenId;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TabPage tabPageTcpHeader;
        private System.Windows.Forms.TextBox txtTcpUrp;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.TextBox txtTcpCsum;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.TextBox txtTcpWin;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.TextBox txtTcpFlags;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.TextBox txtTcpRes;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TextBox txtTcpOff;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox txtTcpAck;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox txtTcpSeq;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox txtTcpDstPort;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox txtTcpSrcPrt;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TabPage tabPageUdpHeader;
        private System.Windows.Forms.TextBox txtUdpCsum;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.TextBox txtUdpLen;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.TextBox txtUdpDstPort;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.TextBox txtUdpSrcPort;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.TabPage tabPageReferences;
        private BrightIdeasSoftware.FastObjectListView listReferences;
        private System.Windows.Forms.TabPage tabPageHex;
        private Be.Windows.Forms.HexBox hexEvent;
        private System.Windows.Forms.TabPage tabPageAscii;
        private System.Windows.Forms.RichTextBox txtPayloadAscii;
        private System.Windows.Forms.ContextMenuStrip ctxMenuHex;
        private System.Windows.Forms.ToolStripMenuItem ctxMenuHexCopyText;
        private System.Windows.Forms.ToolStripMenuItem ctxMenuHexCopyHex;
        private System.Windows.Forms.ToolStripSeparator ctxMenuHexSep1;
        private System.Windows.Forms.ToolStripMenuItem ctxMenuHexSelectAll;
        private System.Windows.Forms.ToolStripMenuItem ctxMenuHexCopyHexSpaces;
        private System.Windows.Forms.ToolStripMenuItem ctxMenuHexCopyHexNoSpaces;
        private System.Windows.Forms.TabPage tabPageMisc;
        private System.Windows.Forms.TextBox txtEventCid;
        private System.Windows.Forms.Label lblEventCid;
        private System.Windows.Forms.TextBox txtEventSid;
        private System.Windows.Forms.Label lblEventSid;
        private System.Windows.Forms.Button btnLinkedRules;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.TabPage tabPageAcknowledgement;
        private System.Windows.Forms.TextBox txtAckClassification;
        private System.Windows.Forms.Label lblAckClassification;
        private System.Windows.Forms.TextBox txtAckInitials;
        private System.Windows.Forms.Label lblAckInitials;
        private System.Windows.Forms.TextBox txtAckNotes;
        private System.Windows.Forms.Label lblAckNotes;
        private System.Windows.Forms.TextBox txtAckTimestamp;
        private System.Windows.Forms.Label lblAckTimestamp;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.CheckBox chkAckSuccessful;
        private System.Windows.Forms.TextBox txtSensor;
        private System.Windows.Forms.Label lblSensor;
        private System.Windows.Forms.TabPage tabPageDns;
        private WeifenLuo.WinFormsUI.Docking.VS2012LightTheme vS2012LightTheme1;
        private System.Windows.Forms.RichTextBox txtDns;
        private System.Windows.Forms.TextBox txtPriority;
        private System.Windows.Forms.Label lblMiscPriority;
    }
}
