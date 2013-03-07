namespace snorbert
{
    partial class FormPayload
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPayload));
            this.tabEvent = new System.Windows.Forms.TabControl();
            this.tabPageHex = new System.Windows.Forms.TabPage();
            this.hexEvent = new Be.Windows.Forms.HexBox();
            this.tabPageAscii = new System.Windows.Forms.TabPage();
            this.txtPayloadAscii = new System.Windows.Forms.RichTextBox();
            this.tabEvent.SuspendLayout();
            this.tabPageHex.SuspendLayout();
            this.tabPageAscii.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabEvent
            // 
            this.tabEvent.Controls.Add(this.tabPageHex);
            this.tabEvent.Controls.Add(this.tabPageAscii);
            this.tabEvent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabEvent.Location = new System.Drawing.Point(0, 0);
            this.tabEvent.Name = "tabEvent";
            this.tabEvent.SelectedIndex = 0;
            this.tabEvent.Size = new System.Drawing.Size(480, 460);
            this.tabEvent.TabIndex = 1;
            // 
            // tabPageHex
            // 
            this.tabPageHex.Controls.Add(this.hexEvent);
            this.tabPageHex.Location = new System.Drawing.Point(4, 24);
            this.tabPageHex.Name = "tabPageHex";
            this.tabPageHex.Size = new System.Drawing.Size(472, 432);
            this.tabPageHex.TabIndex = 0;
            this.tabPageHex.Text = "Payload (Hex)";
            this.tabPageHex.UseVisualStyleBackColor = true;
            // 
            // hexEvent
            // 
            this.hexEvent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hexEvent.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hexEvent.InfoForeColor = System.Drawing.Color.Gray;
            this.hexEvent.LineInfoVisible = true;
            this.hexEvent.Location = new System.Drawing.Point(0, 0);
            this.hexEvent.Name = "hexEvent";
            this.hexEvent.ReadOnly = true;
            this.hexEvent.ShadowSelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(60)))), ((int)(((byte)(188)))), ((int)(((byte)(255)))));
            this.hexEvent.Size = new System.Drawing.Size(472, 432);
            this.hexEvent.StringViewVisible = true;
            this.hexEvent.TabIndex = 0;
            this.hexEvent.UseFixedBytesPerLine = true;
            this.hexEvent.VScrollBarVisible = true;
            // 
            // tabPageAscii
            // 
            this.tabPageAscii.Controls.Add(this.txtPayloadAscii);
            this.tabPageAscii.Location = new System.Drawing.Point(4, 24);
            this.tabPageAscii.Name = "tabPageAscii";
            this.tabPageAscii.Size = new System.Drawing.Size(472, 432);
            this.tabPageAscii.TabIndex = 1;
            this.tabPageAscii.Text = "Payload (ASCII)";
            this.tabPageAscii.UseVisualStyleBackColor = true;
            // 
            // txtPayloadAscii
            // 
            this.txtPayloadAscii.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtPayloadAscii.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPayloadAscii.Location = new System.Drawing.Point(0, 0);
            this.txtPayloadAscii.Name = "txtPayloadAscii";
            this.txtPayloadAscii.ReadOnly = true;
            this.txtPayloadAscii.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedBoth;
            this.txtPayloadAscii.Size = new System.Drawing.Size(472, 432);
            this.txtPayloadAscii.TabIndex = 0;
            this.txtPayloadAscii.Text = "";
            // 
            // FormPayload
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(480, 460);
            this.Controls.Add(this.tabEvent);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MinimumSize = new System.Drawing.Size(334, 325);
            this.Name = "FormPayload";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Payload";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormPayload_FormClosing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormPayload_KeyDown);
            this.tabEvent.ResumeLayout(false);
            this.tabPageHex.ResumeLayout(false);
            this.tabPageAscii.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabEvent;
        private System.Windows.Forms.TabPage tabPageHex;
        private Be.Windows.Forms.HexBox hexEvent;
        private System.Windows.Forms.TabPage tabPageAscii;
        private System.Windows.Forms.RichTextBox txtPayloadAscii;
    }
}