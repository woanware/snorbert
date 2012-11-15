namespace snorbert
{
    partial class ControlSensors
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ControlSensors));
            this.listSensors = new BrightIdeasSoftware.ObjectListView();
            this.olvcSensorId = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvcSensorHostName = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvcSensorInterface = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvcSensorLastEvent = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvcSensorEventCount = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvcSensorEventPercentage = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.ctxMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ctxMenuRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.btnRefresh = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.listSensors)).BeginInit();
            this.ctxMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // listSensors
            // 
            this.listSensors.AllColumns.Add(this.olvcSensorId);
            this.listSensors.AllColumns.Add(this.olvcSensorHostName);
            this.listSensors.AllColumns.Add(this.olvcSensorInterface);
            this.listSensors.AllColumns.Add(this.olvcSensorLastEvent);
            this.listSensors.AllColumns.Add(this.olvcSensorEventCount);
            this.listSensors.AllColumns.Add(this.olvcSensorEventPercentage);
            this.listSensors.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listSensors.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvcSensorId,
            this.olvcSensorHostName,
            this.olvcSensorInterface,
            this.olvcSensorLastEvent,
            this.olvcSensorEventCount,
            this.olvcSensorEventPercentage});
            this.listSensors.ContextMenuStrip = this.ctxMenu;
            this.listSensors.FullRowSelect = true;
            this.listSensors.HideSelection = false;
            this.listSensors.Location = new System.Drawing.Point(0, 29);
            this.listSensors.MultiSelect = false;
            this.listSensors.Name = "listSensors";
            this.listSensors.ShowGroups = false;
            this.listSensors.Size = new System.Drawing.Size(652, 433);
            this.listSensors.TabIndex = 1;
            this.listSensors.UseCompatibleStateImageBehavior = false;
            this.listSensors.View = System.Windows.Forms.View.Details;
            // 
            // olvcSensorId
            // 
            this.olvcSensorId.AspectName = "Sid";
            this.olvcSensorId.CellPadding = null;
            this.olvcSensorId.Text = "ID";
            // 
            // olvcSensorHostName
            // 
            this.olvcSensorHostName.AspectName = "HostName";
            this.olvcSensorHostName.CellPadding = null;
            this.olvcSensorHostName.Text = "Host Name";
            // 
            // olvcSensorInterface
            // 
            this.olvcSensorInterface.AspectName = "Interface";
            this.olvcSensorInterface.CellPadding = null;
            this.olvcSensorInterface.Text = "Interface";
            // 
            // olvcSensorLastEvent
            // 
            this.olvcSensorLastEvent.AspectName = "LastEvent";
            this.olvcSensorLastEvent.CellPadding = null;
            this.olvcSensorLastEvent.Text = "Last Event";
            // 
            // olvcSensorEventCount
            // 
            this.olvcSensorEventCount.AspectName = "EventCount";
            this.olvcSensorEventCount.CellPadding = null;
            this.olvcSensorEventCount.Text = "Event Count";
            // 
            // olvcSensorEventPercentage
            // 
            this.olvcSensorEventPercentage.AspectName = "EventPercentage";
            this.olvcSensorEventPercentage.CellPadding = null;
            this.olvcSensorEventPercentage.FillsFreeSpace = true;
            this.olvcSensorEventPercentage.Text = "Event %";
            // 
            // ctxMenu
            // 
            this.ctxMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ctxMenuRefresh});
            this.ctxMenu.Name = "ctxMenu";
            this.ctxMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.ctxMenu.Size = new System.Drawing.Size(114, 26);
            // 
            // ctxMenuRefresh
            // 
            this.ctxMenuRefresh.Name = "ctxMenuRefresh";
            this.ctxMenuRefresh.Size = new System.Drawing.Size(113, 22);
            this.ctxMenuRefresh.Text = "Refresh";
            this.ctxMenuRefresh.Click += new System.EventHandler(this.ctxMenuRefresh_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefresh.Image = ((System.Drawing.Image)(resources.GetObject("btnRefresh.Image")));
            this.btnRefresh.Location = new System.Drawing.Point(628, -1);
            this.btnRefresh.Margin = new System.Windows.Forms.Padding(0);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(25, 25);
            this.btnRefresh.TabIndex = 2;
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // ControlSensors
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.listSensors);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "ControlSensors";
            this.Size = new System.Drawing.Size(653, 462);
            this.Load += new System.EventHandler(this.ControlSensors_Load);
            ((System.ComponentModel.ISupportInitialize)(this.listSensors)).EndInit();
            this.ctxMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private BrightIdeasSoftware.ObjectListView listSensors;
        private BrightIdeasSoftware.OLVColumn olvcSensorId;
        private BrightIdeasSoftware.OLVColumn olvcSensorHostName;
        private BrightIdeasSoftware.OLVColumn olvcSensorInterface;
        private BrightIdeasSoftware.OLVColumn olvcSensorLastEvent;
        private BrightIdeasSoftware.OLVColumn olvcSensorEventCount;
        private BrightIdeasSoftware.OLVColumn olvcSensorEventPercentage;
        private System.Windows.Forms.ContextMenuStrip ctxMenu;
        private System.Windows.Forms.ToolStripMenuItem ctxMenuRefresh;
        private System.Windows.Forms.Button btnRefresh;
    }
}
