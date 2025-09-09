namespace uConsole
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.mainStatusStrip = new System.Windows.Forms.StatusStrip();
            this.connectionStateLbl = new System.Windows.Forms.ToolStripStatusLabel();
            this.mainToolStrip = new System.Windows.Forms.ToolStrip();
            this.refreshPortsBtn = new System.Windows.Forms.ToolStripButton();
            this.portCbx = new System.Windows.Forms.ToolStripComboBox();
            this.baudrateCbx = new System.Windows.Forms.ToolStripComboBox();
            this.linkBtn = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.runAnotherInstance = new System.Windows.Forms.ToolStripButton();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.textHistoryTxb = new System.Windows.Forms.RichTextBox();
            this.historyTxbToolStrip = new System.Windows.Forms.ToolStrip();
            this.clearHistoryBtn = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.copyToClipboardBtn = new System.Windows.Forms.ToolStripButton();
            this.saveAsBtn = new System.Windows.Forms.ToolStripButton();
            this.autoscrollBtn = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.saveBinaryBtn = new System.Windows.Forms.ToolStripButton();
            this.sendTxb = new System.Windows.Forms.TextBox();
            this.sendToolStrip = new System.Windows.Forms.ToolStrip();
            this.makeRandomDataBtn = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.sendRandomDataBtn = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.crlfBtn = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.isPingPongModeBtn = new System.Windows.Forms.ToolStripButton();
            this.miscBtn = new System.Windows.Forms.ToolStripDropDownButton();
            this.addNMEA0183ChecksumBtn = new System.Windows.Forms.ToolStripMenuItem();
            this.mainStatusStrip.SuspendLayout();
            this.mainToolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.historyTxbToolStrip.SuspendLayout();
            this.sendToolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainStatusStrip
            // 
            this.mainStatusStrip.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.mainStatusStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.mainStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.connectionStateLbl});
            this.mainStatusStrip.Location = new System.Drawing.Point(0, 499);
            this.mainStatusStrip.Name = "mainStatusStrip";
            this.mainStatusStrip.Size = new System.Drawing.Size(778, 45);
            this.mainStatusStrip.TabIndex = 0;
            this.mainStatusStrip.Text = "statusStrip1";
            // 
            // connectionStateLbl
            // 
            this.connectionStateLbl.Name = "connectionStateLbl";
            this.connectionStateLbl.Size = new System.Drawing.Size(71, 38);
            this.connectionStateLbl.Text = "IDLE";
            // 
            // mainToolStrip
            // 
            this.mainToolStrip.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.mainToolStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.mainToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.refreshPortsBtn,
            this.portCbx,
            this.baudrateCbx,
            this.linkBtn,
            this.toolStripSeparator1,
            this.runAnotherInstance});
            this.mainToolStrip.Location = new System.Drawing.Point(0, 0);
            this.mainToolStrip.Name = "mainToolStrip";
            this.mainToolStrip.Size = new System.Drawing.Size(778, 54);
            this.mainToolStrip.TabIndex = 1;
            this.mainToolStrip.Text = "toolStrip1";
            // 
            // refreshPortsBtn
            // 
            this.refreshPortsBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.refreshPortsBtn.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.refreshPortsBtn.Image = ((System.Drawing.Image)(resources.GetObject("refreshPortsBtn.Image")));
            this.refreshPortsBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.refreshPortsBtn.Name = "refreshPortsBtn";
            this.refreshPortsBtn.Size = new System.Drawing.Size(68, 49);
            this.refreshPortsBtn.Text = "🔄";
            this.refreshPortsBtn.ToolTipText = "Refresh ports (Ctrl + R)";
            this.refreshPortsBtn.Click += new System.EventHandler(this.refreshPortsBtn_Click);
            // 
            // portCbx
            // 
            this.portCbx.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.portCbx.Font = new System.Drawing.Font("Consolas", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.portCbx.Name = "portCbx";
            this.portCbx.Size = new System.Drawing.Size(121, 54);
            this.portCbx.ToolTipText = "Port name";
            // 
            // baudrateCbx
            // 
            this.baudrateCbx.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.baudrateCbx.Font = new System.Drawing.Font("Consolas", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.baudrateCbx.Name = "baudrateCbx";
            this.baudrateCbx.Size = new System.Drawing.Size(200, 54);
            this.baudrateCbx.ToolTipText = "Port baudrate";
            // 
            // linkBtn
            // 
            this.linkBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.linkBtn.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.linkBtn.Image = ((System.Drawing.Image)(resources.GetObject("linkBtn.Image")));
            this.linkBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.linkBtn.Name = "linkBtn";
            this.linkBtn.Size = new System.Drawing.Size(68, 49);
            this.linkBtn.Text = "🔗";
            this.linkBtn.ToolTipText = "Connect (Ctrl + Shift + L) / Disconnect (Ctrl + L)";
            this.linkBtn.Click += new System.EventHandler(this.linkBtn_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 54);
            // 
            // runAnotherInstance
            // 
            this.runAnotherInstance.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.runAnotherInstance.Image = ((System.Drawing.Image)(resources.GetObject("runAnotherInstance.Image")));
            this.runAnotherInstance.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.runAnotherInstance.Name = "runAnotherInstance";
            this.runAnotherInstance.Size = new System.Drawing.Size(112, 49);
            this.runAnotherInstance.Text = "🏃🏃";
            this.runAnotherInstance.ToolTipText = "Run another instance (Ctrl + D)";
            this.runAnotherInstance.Click += new System.EventHandler(this.runAnotherInstance_Click);
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer.Location = new System.Drawing.Point(0, 54);
            this.splitContainer.Name = "splitContainer";
            this.splitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.textHistoryTxb);
            this.splitContainer.Panel1.Controls.Add(this.historyTxbToolStrip);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.sendTxb);
            this.splitContainer.Panel2.Controls.Add(this.sendToolStrip);
            this.splitContainer.Size = new System.Drawing.Size(778, 445);
            this.splitContainer.SplitterDistance = 342;
            this.splitContainer.TabIndex = 2;
            // 
            // textHistoryTxb
            // 
            this.textHistoryTxb.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textHistoryTxb.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textHistoryTxb.Location = new System.Drawing.Point(0, 47);
            this.textHistoryTxb.Name = "textHistoryTxb";
            this.textHistoryTxb.ReadOnly = true;
            this.textHistoryTxb.Size = new System.Drawing.Size(778, 295);
            this.textHistoryTxb.TabIndex = 2;
            this.textHistoryTxb.Text = "";
            this.textHistoryTxb.TextChanged += new System.EventHandler(this.textHistoryTxb_TextChanged);
            // 
            // historyTxbToolStrip
            // 
            this.historyTxbToolStrip.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.historyTxbToolStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.historyTxbToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clearHistoryBtn,
            this.toolStripSeparator2,
            this.copyToClipboardBtn,
            this.saveAsBtn,
            this.autoscrollBtn,
            this.toolStripSeparator6,
            this.saveBinaryBtn});
            this.historyTxbToolStrip.Location = new System.Drawing.Point(0, 0);
            this.historyTxbToolStrip.Name = "historyTxbToolStrip";
            this.historyTxbToolStrip.Size = new System.Drawing.Size(778, 47);
            this.historyTxbToolStrip.TabIndex = 1;
            this.historyTxbToolStrip.Text = "toolStrip1";
            // 
            // clearHistoryBtn
            // 
            this.clearHistoryBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.clearHistoryBtn.Image = ((System.Drawing.Image)(resources.GetObject("clearHistoryBtn.Image")));
            this.clearHistoryBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.clearHistoryBtn.Name = "clearHistoryBtn";
            this.clearHistoryBtn.Size = new System.Drawing.Size(59, 42);
            this.clearHistoryBtn.Text = "🗑️";
            this.clearHistoryBtn.ToolTipText = "Clear history (Ctrl + X)";
            this.clearHistoryBtn.Click += new System.EventHandler(this.clearHistoryBtn_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 47);
            // 
            // copyToClipboardBtn
            // 
            this.copyToClipboardBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.copyToClipboardBtn.Image = ((System.Drawing.Image)(resources.GetObject("copyToClipboardBtn.Image")));
            this.copyToClipboardBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.copyToClipboardBtn.Name = "copyToClipboardBtn";
            this.copyToClipboardBtn.Size = new System.Drawing.Size(59, 42);
            this.copyToClipboardBtn.Text = "📋";
            this.copyToClipboardBtn.ToolTipText = "Copy to clipboard (Ctrl + C)";
            this.copyToClipboardBtn.Click += new System.EventHandler(this.copyToClipboardBtn_Click);
            // 
            // saveAsBtn
            // 
            this.saveAsBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.saveAsBtn.Image = ((System.Drawing.Image)(resources.GetObject("saveAsBtn.Image")));
            this.saveAsBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveAsBtn.Name = "saveAsBtn";
            this.saveAsBtn.Size = new System.Drawing.Size(59, 42);
            this.saveAsBtn.Text = "💾";
            this.saveAsBtn.ToolTipText = "Save history (Ctrl + S)";
            this.saveAsBtn.Click += new System.EventHandler(this.saveAsBtn_Click);
            // 
            // autoscrollBtn
            // 
            this.autoscrollBtn.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.autoscrollBtn.Checked = true;
            this.autoscrollBtn.CheckState = System.Windows.Forms.CheckState.Checked;
            this.autoscrollBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.autoscrollBtn.Image = ((System.Drawing.Image)(resources.GetObject("autoscrollBtn.Image")));
            this.autoscrollBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.autoscrollBtn.Name = "autoscrollBtn";
            this.autoscrollBtn.Size = new System.Drawing.Size(59, 42);
            this.autoscrollBtn.Text = "📜";
            this.autoscrollBtn.ToolTipText = "Autoscroll";
            this.autoscrollBtn.Click += new System.EventHandler(this.autoscrollBtn_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 47);
            // 
            // saveBinaryBtn
            // 
            this.saveBinaryBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.saveBinaryBtn.Font = new System.Drawing.Font("Consolas", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.saveBinaryBtn.Image = ((System.Drawing.Image)(resources.GetObject("saveBinaryBtn.Image")));
            this.saveBinaryBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveBinaryBtn.Name = "saveBinaryBtn";
            this.saveBinaryBtn.Size = new System.Drawing.Size(49, 42);
            this.saveBinaryBtn.Text = "01";
            this.saveBinaryBtn.ToolTipText = "Save as binary";
            this.saveBinaryBtn.Click += new System.EventHandler(this.saveBinaryBtn_Click);
            // 
            // sendTxb
            // 
            this.sendTxb.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sendTxb.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.sendTxb.Location = new System.Drawing.Point(0, 49);
            this.sendTxb.Name = "sendTxb";
            this.sendTxb.Size = new System.Drawing.Size(778, 36);
            this.sendTxb.TabIndex = 1;
            this.sendTxb.KeyDown += new System.Windows.Forms.KeyEventHandler(this.sendTxb_KeyDown);
            // 
            // sendToolStrip
            // 
            this.sendToolStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.sendToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.makeRandomDataBtn,
            this.toolStripSeparator3,
            this.sendRandomDataBtn,
            this.toolStripSeparator5,
            this.crlfBtn,
            this.toolStripSeparator7,
            this.isPingPongModeBtn,
            this.miscBtn});
            this.sendToolStrip.Location = new System.Drawing.Point(0, 0);
            this.sendToolStrip.Name = "sendToolStrip";
            this.sendToolStrip.Size = new System.Drawing.Size(778, 49);
            this.sendToolStrip.TabIndex = 0;
            this.sendToolStrip.Text = "toolStrip1";
            // 
            // makeRandomDataBtn
            // 
            this.makeRandomDataBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.makeRandomDataBtn.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.makeRandomDataBtn.Image = ((System.Drawing.Image)(resources.GetObject("makeRandomDataBtn.Image")));
            this.makeRandomDataBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.makeRandomDataBtn.Name = "makeRandomDataBtn";
            this.makeRandomDataBtn.Size = new System.Drawing.Size(225, 44);
            this.makeRandomDataBtn.Text = "Make random...";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 49);
            // 
            // sendRandomDataBtn
            // 
            this.sendRandomDataBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.sendRandomDataBtn.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.sendRandomDataBtn.Image = ((System.Drawing.Image)(resources.GetObject("sendRandomDataBtn.Image")));
            this.sendRandomDataBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.sendRandomDataBtn.Name = "sendRandomDataBtn";
            this.sendRandomDataBtn.Size = new System.Drawing.Size(219, 44);
            this.sendRandomDataBtn.Text = "Send random...";
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 49);
            // 
            // crlfBtn
            // 
            this.crlfBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.crlfBtn.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.crlfBtn.Image = ((System.Drawing.Image)(resources.GetObject("crlfBtn.Image")));
            this.crlfBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.crlfBtn.Name = "crlfBtn";
            this.crlfBtn.Size = new System.Drawing.Size(90, 44);
            this.crlfBtn.Text = "CR LF";
            this.crlfBtn.ToolTipText = "Send CR LF (Ctrl + Enter)";
            this.crlfBtn.Click += new System.EventHandler(this.crlfBtn_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(6, 49);
            // 
            // isPingPongModeBtn
            // 
            this.isPingPongModeBtn.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.isPingPongModeBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.isPingPongModeBtn.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.isPingPongModeBtn.Image = ((System.Drawing.Image)(resources.GetObject("isPingPongModeBtn.Image")));
            this.isPingPongModeBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.isPingPongModeBtn.Name = "isPingPongModeBtn";
            this.isPingPongModeBtn.Size = new System.Drawing.Size(61, 44);
            this.isPingPongModeBtn.Text = "🏓";
            this.isPingPongModeBtn.ToolTipText = "Ping-pong mode";
            this.isPingPongModeBtn.Click += new System.EventHandler(this.isPingPongModeBtn_Click);
            // 
            // miscBtn
            // 
            this.miscBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.miscBtn.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addNMEA0183ChecksumBtn});
            this.miscBtn.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.miscBtn.Image = ((System.Drawing.Image)(resources.GetObject("miscBtn.Image")));
            this.miscBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.miscBtn.Name = "miscBtn";
            this.miscBtn.Size = new System.Drawing.Size(73, 44);
            this.miscBtn.Text = "👽";
            this.miscBtn.ToolTipText = "Misc tools";
            // 
            // addNMEA0183ChecksumBtn
            // 
            this.addNMEA0183ChecksumBtn.Name = "addNMEA0183ChecksumBtn";
            this.addNMEA0183ChecksumBtn.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.addNMEA0183ChecksumBtn.Size = new System.Drawing.Size(574, 46);
            this.addNMEA0183ChecksumBtn.Text = "Add NMEA0183 Checksum";
            this.addNMEA0183ChecksumBtn.ToolTipText = "The line should start with $ sign and ends with *";
            this.addNMEA0183ChecksumBtn.Click += new System.EventHandler(this.addNMEA0183ChecksumBtn_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 28F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(778, 544);
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.mainToolStrip);
            this.Controls.Add(this.mainStatusStrip);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MainForm";
            this.ShowIcon = false;
            this.Text = "🖵 uConsole";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
            this.mainStatusStrip.ResumeLayout(false);
            this.mainStatusStrip.PerformLayout();
            this.mainToolStrip.ResumeLayout(false);
            this.mainToolStrip.PerformLayout();
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel1.PerformLayout();
            this.splitContainer.Panel2.ResumeLayout(false);
            this.splitContainer.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.historyTxbToolStrip.ResumeLayout(false);
            this.historyTxbToolStrip.PerformLayout();
            this.sendToolStrip.ResumeLayout(false);
            this.sendToolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip mainStatusStrip;
        private System.Windows.Forms.ToolStrip mainToolStrip;
        private System.Windows.Forms.ToolStripButton refreshPortsBtn;
        private System.Windows.Forms.ToolStripComboBox portCbx;
        private System.Windows.Forms.ToolStripComboBox baudrateCbx;
        private System.Windows.Forms.ToolStripButton linkBtn;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.ToolStrip historyTxbToolStrip;
        private System.Windows.Forms.ToolStripButton clearHistoryBtn;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton copyToClipboardBtn;
        private System.Windows.Forms.ToolStripButton saveAsBtn;
        private System.Windows.Forms.ToolStripButton autoscrollBtn;
        private System.Windows.Forms.TextBox sendTxb;
        private System.Windows.Forms.ToolStrip sendToolStrip;
        private System.Windows.Forms.ToolStripDropDownButton makeRandomDataBtn;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton crlfBtn;
        private System.Windows.Forms.ToolStripStatusLabel connectionStateLbl;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripDropDownButton sendRandomDataBtn;
        private System.Windows.Forms.ToolStripButton isPingPongModeBtn;
        private System.Windows.Forms.RichTextBox textHistoryTxb;
        private System.Windows.Forms.ToolStripButton runAnotherInstance;
        private System.Windows.Forms.ToolStripDropDownButton miscBtn;
        private System.Windows.Forms.ToolStripMenuItem addNMEA0183ChecksumBtn;
        private System.Windows.Forms.ToolStripButton saveBinaryBtn;
    }
}