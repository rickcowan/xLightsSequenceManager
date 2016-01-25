namespace xLightsSequenceManager
{
    partial class frmMain
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
            this.tabMain = new System.Windows.Forms.TabControl();
            this.tabPageSplit = new System.Windows.Forms.TabPage();
            this.btnSplit = new System.Windows.Forms.Button();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.txtSequenceFile = new System.Windows.Forms.TextBox();
            this.lblSequenceFile = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.txtStatus = new System.Windows.Forms.TextBox();
            this.chkUpdateMedia = new System.Windows.Forms.CheckBox();
            this.cmbTimingTrack = new System.Windows.Forms.ComboBox();
            this.lblTimingTrack = new System.Windows.Forms.Label();
            this.tabMain.SuspendLayout();
            this.tabPageSplit.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabMain
            // 
            this.tabMain.Controls.Add(this.tabPageSplit);
            this.tabMain.Location = new System.Drawing.Point(13, 13);
            this.tabMain.Name = "tabMain";
            this.tabMain.SelectedIndex = 0;
            this.tabMain.Size = new System.Drawing.Size(539, 441);
            this.tabMain.TabIndex = 0;
            // 
            // tabPageSplit
            // 
            this.tabPageSplit.Controls.Add(this.lblTimingTrack);
            this.tabPageSplit.Controls.Add(this.cmbTimingTrack);
            this.tabPageSplit.Controls.Add(this.chkUpdateMedia);
            this.tabPageSplit.Controls.Add(this.txtStatus);
            this.tabPageSplit.Controls.Add(this.btnSplit);
            this.tabPageSplit.Controls.Add(this.btnBrowse);
            this.tabPageSplit.Controls.Add(this.txtSequenceFile);
            this.tabPageSplit.Controls.Add(this.lblSequenceFile);
            this.tabPageSplit.Location = new System.Drawing.Point(4, 22);
            this.tabPageSplit.Name = "tabPageSplit";
            this.tabPageSplit.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSplit.Size = new System.Drawing.Size(531, 415);
            this.tabPageSplit.TabIndex = 0;
            this.tabPageSplit.Text = "Split";
            this.tabPageSplit.UseVisualStyleBackColor = true;
            // 
            // btnSplit
            // 
            this.btnSplit.Location = new System.Drawing.Point(450, 43);
            this.btnSplit.Name = "btnSplit";
            this.btnSplit.Size = new System.Drawing.Size(75, 23);
            this.btnSplit.TabIndex = 5;
            this.btnSplit.Text = "Split";
            this.btnSplit.UseVisualStyleBackColor = true;
            this.btnSplit.Click += new System.EventHandler(this.btnSplit_Click);
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(450, 14);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(75, 23);
            this.btnBrowse.TabIndex = 2;
            this.btnBrowse.Text = "Browse";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // txtSequenceFile
            // 
            this.txtSequenceFile.Location = new System.Drawing.Point(87, 16);
            this.txtSequenceFile.Name = "txtSequenceFile";
            this.txtSequenceFile.Size = new System.Drawing.Size(357, 20);
            this.txtSequenceFile.TabIndex = 1;
            // 
            // lblSequenceFile
            // 
            this.lblSequenceFile.AutoSize = true;
            this.lblSequenceFile.Location = new System.Drawing.Point(6, 19);
            this.lblSequenceFile.Name = "lblSequenceFile";
            this.lblSequenceFile.Size = new System.Drawing.Size(75, 13);
            this.lblSequenceFile.TabIndex = 0;
            this.lblSequenceFile.Text = "Sequence File";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // txtStatus
            // 
            this.txtStatus.Enabled = false;
            this.txtStatus.Location = new System.Drawing.Point(9, 92);
            this.txtStatus.Multiline = true;
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.Size = new System.Drawing.Size(516, 317);
            this.txtStatus.TabIndex = 6;
            // 
            // chkUpdateMedia
            // 
            this.chkUpdateMedia.AutoSize = true;
            this.chkUpdateMedia.Location = new System.Drawing.Point(87, 69);
            this.chkUpdateMedia.Name = "chkUpdateMedia";
            this.chkUpdateMedia.Size = new System.Drawing.Size(142, 17);
            this.chkUpdateMedia.TabIndex = 7;
            this.chkUpdateMedia.Text = "Update Media File Value";
            this.chkUpdateMedia.UseVisualStyleBackColor = true;
            // 
            // cmbTimingTrack
            // 
            this.cmbTimingTrack.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTimingTrack.FormattingEnabled = true;
            this.cmbTimingTrack.Location = new System.Drawing.Point(87, 42);
            this.cmbTimingTrack.Name = "cmbTimingTrack";
            this.cmbTimingTrack.Size = new System.Drawing.Size(357, 21);
            this.cmbTimingTrack.TabIndex = 8;
            this.cmbTimingTrack.DropDown += new System.EventHandler(this.cmbTimingTrack_DropDown);
            // 
            // lblTimingTrack
            // 
            this.lblTimingTrack.AutoSize = true;
            this.lblTimingTrack.Location = new System.Drawing.Point(6, 48);
            this.lblTimingTrack.Name = "lblTimingTrack";
            this.lblTimingTrack.Size = new System.Drawing.Size(69, 13);
            this.lblTimingTrack.TabIndex = 9;
            this.lblTimingTrack.Text = "Timing Track";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(560, 466);
            this.Controls.Add(this.tabMain);
            this.Name = "frmMain";
            this.Text = "xLights Sequence Manager";
            this.tabMain.ResumeLayout(false);
            this.tabPageSplit.ResumeLayout(false);
            this.tabPageSplit.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabMain;
        private System.Windows.Forms.TabPage tabPageSplit;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.TextBox txtSequenceFile;
        private System.Windows.Forms.Label lblSequenceFile;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button btnSplit;
        private System.Windows.Forms.TextBox txtStatus;
        private System.Windows.Forms.CheckBox chkUpdateMedia;
        private System.Windows.Forms.Label lblTimingTrack;
        private System.Windows.Forms.ComboBox cmbTimingTrack;
    }
}

