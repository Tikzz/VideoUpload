namespace VideoUpload
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.cutUploadButton = new System.Windows.Forms.Button();
            this.fromLabel = new System.Windows.Forms.Label();
            this.outputConsole = new System.Windows.Forms.TextBox();
            this.toLabel = new System.Windows.Forms.Label();
            this.fileTextBox = new System.Windows.Forms.TextBox();
            this.browseButton = new System.Windows.Forms.Button();
            this.fromHours = new System.Windows.Forms.NumericUpDown();
            this.fromMinutes = new System.Windows.Forms.NumericUpDown();
            this.fromSeconds = new System.Windows.Forms.NumericUpDown();
            this.toSeconds = new System.Windows.Forms.NumericUpDown();
            this.toMinutes = new System.Windows.Forms.NumericUpDown();
            this.toHours = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.URLtextBox = new System.Windows.Forms.TextBox();
            this.titleTextBox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.UploadProgressBar = new System.Windows.Forms.ProgressBar();
            this.deleteCheckBox = new System.Windows.Forms.CheckBox();
            this.uploadCheckBox = new System.Windows.Forms.CheckBox();
            this.codecComboBox = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.fromHours)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fromMinutes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fromSeconds)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.toSeconds)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.toMinutes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.toHours)).BeginInit();
            this.SuspendLayout();
            // 
            // cutUploadButton
            // 
            this.cutUploadButton.Enabled = false;
            this.cutUploadButton.Location = new System.Drawing.Point(351, 101);
            this.cutUploadButton.Name = "cutUploadButton";
            this.cutUploadButton.Size = new System.Drawing.Size(119, 33);
            this.cutUploadButton.TabIndex = 8;
            this.cutUploadButton.Text = "Cut and Upload";
            this.cutUploadButton.UseVisualStyleBackColor = true;
            this.cutUploadButton.Click += new System.EventHandler(this.cutUploadButton_Click);
            // 
            // fromLabel
            // 
            this.fromLabel.AutoSize = true;
            this.fromLabel.Location = new System.Drawing.Point(9, 74);
            this.fromLabel.Name = "fromLabel";
            this.fromLabel.Size = new System.Drawing.Size(33, 13);
            this.fromLabel.TabIndex = 4;
            this.fromLabel.Text = "From:";
            // 
            // outputConsole
            // 
            this.outputConsole.BackColor = System.Drawing.Color.Black;
            this.outputConsole.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.outputConsole.ForeColor = System.Drawing.Color.White;
            this.outputConsole.Location = new System.Drawing.Point(11, 140);
            this.outputConsole.Multiline = true;
            this.outputConsole.Name = "outputConsole";
            this.outputConsole.ReadOnly = true;
            this.outputConsole.Size = new System.Drawing.Size(458, 241);
            this.outputConsole.TabIndex = 9;
            // 
            // toLabel
            // 
            this.toLabel.AutoSize = true;
            this.toLabel.Location = new System.Drawing.Point(266, 74);
            this.toLabel.Name = "toLabel";
            this.toLabel.Size = new System.Drawing.Size(23, 13);
            this.toLabel.TabIndex = 13;
            this.toLabel.Text = "To:";
            // 
            // fileTextBox
            // 
            this.fileTextBox.AllowDrop = true;
            this.fileTextBox.Location = new System.Drawing.Point(12, 10);
            this.fileTextBox.Name = "fileTextBox";
            this.fileTextBox.ReadOnly = true;
            this.fileTextBox.Size = new System.Drawing.Size(377, 20);
            this.fileTextBox.TabIndex = 14;
            this.fileTextBox.Text = "Drag and drop a file here";
            this.fileTextBox.TextChanged += new System.EventHandler(this.fileTextBox_TextChanged);
            this.fileTextBox.DragDrop += new System.Windows.Forms.DragEventHandler(this.fileTextBox_DragDrop);
            this.fileTextBox.DragOver += new System.Windows.Forms.DragEventHandler(this.fileTextBox_DragOver);
            // 
            // browseButton
            // 
            this.browseButton.Location = new System.Drawing.Point(395, 10);
            this.browseButton.Name = "browseButton";
            this.browseButton.Size = new System.Drawing.Size(75, 20);
            this.browseButton.TabIndex = 0;
            this.browseButton.Text = "Browse";
            this.browseButton.UseVisualStyleBackColor = true;
            this.browseButton.Click += new System.EventHandler(this.browsebutton_Click);
            // 
            // fromHours
            // 
            this.fromHours.Location = new System.Drawing.Point(48, 72);
            this.fromHours.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
            this.fromHours.Name = "fromHours";
            this.fromHours.Size = new System.Drawing.Size(35, 20);
            this.fromHours.TabIndex = 2;
            this.fromHours.Click += new System.EventHandler(this.numeric_SelectAll);
            this.fromHours.Enter += new System.EventHandler(this.numeric_SelectAll);
            // 
            // fromMinutes
            // 
            this.fromMinutes.Location = new System.Drawing.Point(108, 72);
            this.fromMinutes.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
            this.fromMinutes.Name = "fromMinutes";
            this.fromMinutes.Size = new System.Drawing.Size(35, 20);
            this.fromMinutes.TabIndex = 3;
            this.fromMinutes.Click += new System.EventHandler(this.numeric_SelectAll);
            this.fromMinutes.Enter += new System.EventHandler(this.numeric_SelectAll);
            // 
            // fromSeconds
            // 
            this.fromSeconds.Location = new System.Drawing.Point(170, 72);
            this.fromSeconds.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
            this.fromSeconds.Name = "fromSeconds";
            this.fromSeconds.Size = new System.Drawing.Size(35, 20);
            this.fromSeconds.TabIndex = 4;
            this.fromSeconds.Click += new System.EventHandler(this.numeric_SelectAll);
            this.fromSeconds.Enter += new System.EventHandler(this.numeric_SelectAll);
            // 
            // toSeconds
            // 
            this.toSeconds.Location = new System.Drawing.Point(417, 72);
            this.toSeconds.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
            this.toSeconds.Name = "toSeconds";
            this.toSeconds.Size = new System.Drawing.Size(35, 20);
            this.toSeconds.TabIndex = 7;
            this.toSeconds.Click += new System.EventHandler(this.numeric_SelectAll);
            this.toSeconds.Enter += new System.EventHandler(this.numeric_SelectAll);
            // 
            // toMinutes
            // 
            this.toMinutes.Location = new System.Drawing.Point(355, 72);
            this.toMinutes.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
            this.toMinutes.Name = "toMinutes";
            this.toMinutes.Size = new System.Drawing.Size(35, 20);
            this.toMinutes.TabIndex = 6;
            this.toMinutes.Click += new System.EventHandler(this.numeric_SelectAll);
            this.toMinutes.Enter += new System.EventHandler(this.numeric_SelectAll);
            // 
            // toHours
            // 
            this.toHours.Location = new System.Drawing.Point(295, 72);
            this.toHours.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
            this.toHours.Name = "toHours";
            this.toHours.Size = new System.Drawing.Size(35, 20);
            this.toHours.TabIndex = 5;
            this.toHours.Click += new System.EventHandler(this.numeric_SelectAll);
            this.toHours.Enter += new System.EventHandler(this.numeric_SelectAll);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(89, 74);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(13, 13);
            this.label1.TabIndex = 22;
            this.label1.Text = "h";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(149, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(15, 13);
            this.label2.TabIndex = 23;
            this.label2.Text = "m";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(211, 74);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(12, 13);
            this.label3.TabIndex = 24;
            this.label3.Text = "s";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(458, 74);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(12, 13);
            this.label4.TabIndex = 27;
            this.label4.Text = "s";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(396, 74);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(15, 13);
            this.label5.TabIndex = 26;
            this.label5.Text = "m";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(336, 74);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(13, 13);
            this.label6.TabIndex = 25;
            this.label6.Text = "h";
            // 
            // URLtextBox
            // 
            this.URLtextBox.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.URLtextBox.Location = new System.Drawing.Point(12, 101);
            this.URLtextBox.Name = "URLtextBox";
            this.URLtextBox.ReadOnly = true;
            this.URLtextBox.Size = new System.Drawing.Size(333, 33);
            this.URLtextBox.TabIndex = 28;
            this.URLtextBox.Visible = false;
            this.URLtextBox.Click += new System.EventHandler(this.URLtextBox_Click);
            // 
            // titleTextBox
            // 
            this.titleTextBox.Location = new System.Drawing.Point(48, 40);
            this.titleTextBox.Name = "titleTextBox";
            this.titleTextBox.Size = new System.Drawing.Size(421, 20);
            this.titleTextBox.TabIndex = 1;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(9, 43);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(30, 13);
            this.label7.TabIndex = 30;
            this.label7.Text = "Title:";
            // 
            // UploadProgressBar
            // 
            this.UploadProgressBar.Location = new System.Drawing.Point(12, 101);
            this.UploadProgressBar.Name = "UploadProgressBar";
            this.UploadProgressBar.Size = new System.Drawing.Size(333, 33);
            this.UploadProgressBar.TabIndex = 31;
            this.UploadProgressBar.Visible = false;
            // 
            // deleteCheckBox
            // 
            this.deleteCheckBox.AutoSize = true;
            this.deleteCheckBox.Checked = true;
            this.deleteCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.deleteCheckBox.Location = new System.Drawing.Point(152, 394);
            this.deleteCheckBox.Name = "deleteCheckBox";
            this.deleteCheckBox.Size = new System.Drawing.Size(134, 17);
            this.deleteCheckBox.TabIndex = 32;
            this.deleteCheckBox.Text = "Delete cut after upload";
            this.deleteCheckBox.UseVisualStyleBackColor = true;
            // 
            // uploadCheckBox
            // 
            this.uploadCheckBox.AutoSize = true;
            this.uploadCheckBox.Checked = true;
            this.uploadCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.uploadCheckBox.Location = new System.Drawing.Point(12, 394);
            this.uploadCheckBox.Name = "uploadCheckBox";
            this.uploadCheckBox.Size = new System.Drawing.Size(115, 17);
            this.uploadCheckBox.TabIndex = 33;
            this.uploadCheckBox.Text = "Upload to lag.party";
            this.uploadCheckBox.UseVisualStyleBackColor = true;
            this.uploadCheckBox.CheckedChanged += new System.EventHandler(this.uploadCheckBox_CheckedChanged);
            // 
            // codecComboBox
            // 
            this.codecComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.codecComboBox.FormattingEnabled = true;
            this.codecComboBox.Items.AddRange(new object[] {
            "VP9 (slow)",
            "x264 (fast)"});
            this.codecComboBox.Location = new System.Drawing.Point(348, 392);
            this.codecComboBox.Name = "codecComboBox";
            this.codecComboBox.Size = new System.Drawing.Size(121, 21);
            this.codecComboBox.TabIndex = 34;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(482, 421);
            this.Controls.Add(this.codecComboBox);
            this.Controls.Add(this.uploadCheckBox);
            this.Controls.Add(this.deleteCheckBox);
            this.Controls.Add(this.UploadProgressBar);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.titleTextBox);
            this.Controls.Add(this.URLtextBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.toSeconds);
            this.Controls.Add(this.toMinutes);
            this.Controls.Add(this.toHours);
            this.Controls.Add(this.fromSeconds);
            this.Controls.Add(this.fromMinutes);
            this.Controls.Add(this.fromHours);
            this.Controls.Add(this.browseButton);
            this.Controls.Add(this.fileTextBox);
            this.Controls.Add(this.toLabel);
            this.Controls.Add(this.outputConsole);
            this.Controls.Add(this.fromLabel);
            this.Controls.Add(this.cutUploadButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Upload to lag.party";
            ((System.ComponentModel.ISupportInitialize)(this.fromHours)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fromMinutes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fromSeconds)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.toSeconds)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.toMinutes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.toHours)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Button cutUploadButton;
        private System.Windows.Forms.Label fromLabel;
        private System.Windows.Forms.TextBox outputConsole;
        private System.Windows.Forms.Label toLabel;
        private System.Windows.Forms.TextBox fileTextBox;
        private System.Windows.Forms.Button browseButton;
        private System.Windows.Forms.NumericUpDown fromHours;
        private System.Windows.Forms.NumericUpDown fromMinutes;
        private System.Windows.Forms.NumericUpDown fromSeconds;
        private System.Windows.Forms.NumericUpDown toSeconds;
        private System.Windows.Forms.NumericUpDown toMinutes;
        private System.Windows.Forms.NumericUpDown toHours;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        public System.Windows.Forms.ProgressBar UploadProgressBar;
        public System.Windows.Forms.TextBox URLtextBox;
        private System.Windows.Forms.TextBox titleTextBox;
        private System.Windows.Forms.CheckBox deleteCheckBox;
        private System.Windows.Forms.CheckBox uploadCheckBox;
        private System.Windows.Forms.ComboBox codecComboBox;
    }
}

