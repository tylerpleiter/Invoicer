namespace Invoicer2
{
    partial class InvoicerSettings
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
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.prefixTextBox = new System.Windows.Forms.TextBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.locationTextBox = new System.Windows.Forms.TextBox();
            this.jobListPathTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.backupTextBox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.pdfCheckBox = new System.Windows.Forms.CheckBox();
            this.saveButton = new System.Windows.Forms.Button();
            this.gotoFolder = new System.Windows.Forms.Button();
            this.selectJobListButton = new System.Windows.Forms.Button();
            this.comboBoxBackupFreq = new System.Windows.Forms.ComboBox();
            this.backupFolderButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(166, 25);
            this.label1.TabIndex = 1;
            this.label1.Text = "Invoice file prefix: ";
            this.toolTip1.SetToolTip(this.label1, "Inv123");
            // 
            // prefixTextBox
            // 
            this.prefixTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.prefixTextBox.Location = new System.Drawing.Point(184, 12);
            this.prefixTextBox.Name = "prefixTextBox";
            this.prefixTextBox.Size = new System.Drawing.Size(178, 30);
            this.prefixTextBox.TabIndex = 2;
            this.prefixTextBox.Text = "I";
            this.toolTip1.SetToolTip(this.prefixTextBox, "Inv123");
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label2.Location = new System.Drawing.Point(12, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(153, 25);
            this.label2.TabIndex = 3;
            this.label2.Text = "Generate PDFs:";
            this.toolTip1.SetToolTip(this.label2, "Inv123");
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label3.Location = new System.Drawing.Point(12, 87);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(203, 25);
            this.label3.TabIndex = 6;
            this.label3.Text = "Program files location:";
            this.toolTip1.SetToolTip(this.label3, "Inv123");
            // 
            // locationTextBox
            // 
            this.locationTextBox.BackColor = System.Drawing.SystemColors.Control;
            this.locationTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.locationTextBox.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.locationTextBox.Location = new System.Drawing.Point(12, 122);
            this.locationTextBox.Name = "locationTextBox";
            this.locationTextBox.ReadOnly = true;
            this.locationTextBox.Size = new System.Drawing.Size(350, 26);
            this.locationTextBox.TabIndex = 7;
            this.locationTextBox.TabStop = false;
            this.locationTextBox.Text = "C:\\\\";
            this.toolTip1.SetToolTip(this.locationTextBox, "Inv123");
            // 
            // jobListPathTextBox
            // 
            this.jobListPathTextBox.BackColor = System.Drawing.SystemColors.Control;
            this.jobListPathTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.jobListPathTextBox.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.jobListPathTextBox.Location = new System.Drawing.Point(12, 192);
            this.jobListPathTextBox.Name = "jobListPathTextBox";
            this.jobListPathTextBox.ReadOnly = true;
            this.jobListPathTextBox.Size = new System.Drawing.Size(350, 26);
            this.jobListPathTextBox.TabIndex = 10;
            this.jobListPathTextBox.TabStop = false;
            this.jobListPathTextBox.Text = "C:\\\\";
            this.toolTip1.SetToolTip(this.jobListPathTextBox, "Inv123");
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label4.Location = new System.Drawing.Point(12, 157);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(151, 25);
            this.label4.TabIndex = 9;
            this.label4.Text = "Job list location:";
            this.toolTip1.SetToolTip(this.label4, "Inv123");
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label5.Location = new System.Drawing.Point(14, 227);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(137, 25);
            this.label5.TabIndex = 12;
            this.label5.Text = "Backup every:";
            this.toolTip1.SetToolTip(this.label5, "Inv123");
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label6.Location = new System.Drawing.Point(303, 227);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(59, 25);
            this.label6.TabIndex = 13;
            this.label6.Text = "days.";
            this.toolTip1.SetToolTip(this.label6, "Inv123");
            // 
            // backupTextBox
            // 
            this.backupTextBox.BackColor = System.Drawing.SystemColors.Control;
            this.backupTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.backupTextBox.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.backupTextBox.Location = new System.Drawing.Point(12, 304);
            this.backupTextBox.Name = "backupTextBox";
            this.backupTextBox.ReadOnly = true;
            this.backupTextBox.Size = new System.Drawing.Size(350, 26);
            this.backupTextBox.TabIndex = 16;
            this.backupTextBox.TabStop = false;
            this.backupTextBox.Text = "C:\\\\";
            this.toolTip1.SetToolTip(this.backupTextBox, "Inv123");
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label7.Location = new System.Drawing.Point(12, 269);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(156, 25);
            this.label7.TabIndex = 15;
            this.label7.Text = "Backup location:";
            this.toolTip1.SetToolTip(this.label7, "Inv123");
            // 
            // pdfCheckBox
            // 
            this.pdfCheckBox.AutoSize = true;
            this.pdfCheckBox.Checked = true;
            this.pdfCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.pdfCheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pdfCheckBox.Location = new System.Drawing.Point(184, 48);
            this.pdfCheckBox.Name = "pdfCheckBox";
            this.pdfCheckBox.Size = new System.Drawing.Size(43, 29);
            this.pdfCheckBox.TabIndex = 4;
            this.pdfCheckBox.Text = " ";
            this.pdfCheckBox.UseVisualStyleBackColor = true;
            // 
            // saveButton
            // 
            this.saveButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.saveButton.Location = new System.Drawing.Point(12, 380);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(350, 41);
            this.saveButton.TabIndex = 5;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // gotoFolder
            // 
            this.gotoFolder.Location = new System.Drawing.Point(238, 83);
            this.gotoFolder.Margin = new System.Windows.Forms.Padding(0);
            this.gotoFolder.Name = "gotoFolder";
            this.gotoFolder.Size = new System.Drawing.Size(124, 36);
            this.gotoFolder.TabIndex = 8;
            this.gotoFolder.Text = "Open Folder";
            this.gotoFolder.UseVisualStyleBackColor = true;
            this.gotoFolder.Click += new System.EventHandler(this.gotoFolder_Click);
            // 
            // selectJobListButton
            // 
            this.selectJobListButton.Location = new System.Drawing.Point(238, 153);
            this.selectJobListButton.Margin = new System.Windows.Forms.Padding(0);
            this.selectJobListButton.Name = "selectJobListButton";
            this.selectJobListButton.Size = new System.Drawing.Size(124, 36);
            this.selectJobListButton.TabIndex = 11;
            this.selectJobListButton.Text = "Select File";
            this.selectJobListButton.UseVisualStyleBackColor = true;
            this.selectJobListButton.Click += new System.EventHandler(this.selectJobListButton_Click);
            // 
            // comboBoxBackupFreq
            // 
            this.comboBoxBackupFreq.AllowDrop = true;
            this.comboBoxBackupFreq.FormattingEnabled = true;
            this.comboBoxBackupFreq.Items.AddRange(new object[] {
            "0",
            "1",
            "7",
            "14"});
            this.comboBoxBackupFreq.Location = new System.Drawing.Point(239, 228);
            this.comboBoxBackupFreq.Name = "comboBoxBackupFreq";
            this.comboBoxBackupFreq.Size = new System.Drawing.Size(58, 28);
            this.comboBoxBackupFreq.TabIndex = 14;
            this.comboBoxBackupFreq.Text = "0";
            this.comboBoxBackupFreq.SelectedIndexChanged += new System.EventHandler(this.comboBoxBackupFreq_SelectedIndexChanged);
            // 
            // backupFolderButton
            // 
            this.backupFolderButton.Enabled = false;
            this.backupFolderButton.Location = new System.Drawing.Point(238, 265);
            this.backupFolderButton.Margin = new System.Windows.Forms.Padding(0);
            this.backupFolderButton.Name = "backupFolderButton";
            this.backupFolderButton.Size = new System.Drawing.Size(124, 36);
            this.backupFolderButton.TabIndex = 17;
            this.backupFolderButton.Text = "OpenFolder";
            this.backupFolderButton.UseVisualStyleBackColor = true;
            this.backupFolderButton.Click += new System.EventHandler(this.backupFolderButton_Click);
            // 
            // InvoicerSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(374, 433);
            this.Controls.Add(this.backupFolderButton);
            this.Controls.Add(this.backupTextBox);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.comboBoxBackupFreq);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.selectJobListButton);
            this.Controls.Add(this.jobListPathTextBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.gotoFolder);
            this.Controls.Add(this.locationTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.pdfCheckBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.prefixTextBox);
            this.Controls.Add(this.label1);
            this.MaximumSize = new System.Drawing.Size(396, 489);
            this.MinimumSize = new System.Drawing.Size(396, 489);
            this.Name = "InvoicerSettings";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Settings";
            this.Load += new System.EventHandler(this.InvoicerSettings_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox prefixTextBox;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox pdfCheckBox;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox locationTextBox;
        private System.Windows.Forms.Button gotoFolder;
        private System.Windows.Forms.Button selectJobListButton;
        private System.Windows.Forms.TextBox jobListPathTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox comboBoxBackupFreq;
        private System.Windows.Forms.Button backupFolderButton;
        private System.Windows.Forms.TextBox backupTextBox;
        private System.Windows.Forms.Label label7;
    }
}