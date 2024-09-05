namespace OLTAB_Manager
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
            this.btnBrowse = new System.Windows.Forms.Button();
            this.txtFolderPath = new System.Windows.Forms.TextBox();
            this.lblFolder = new System.Windows.Forms.Label();
            this.lblSchedule = new System.Windows.Forms.Label();
            this.cboSchedule = new System.Windows.Forms.ComboBox();
            this.timePicker = new System.Windows.Forms.DateTimePicker();
            this.lblTime = new System.Windows.Forms.Label();
            this.btnSchedule = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(360, 30);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(75, 23);
            this.btnBrowse.TabIndex = 0;
            this.btnBrowse.Text = "Browse...";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // txtFolderPath
            // 
            this.txtFolderPath.Location = new System.Drawing.Point(15, 30);
            this.txtFolderPath.Name = "txtFolderPath";
            this.txtFolderPath.Size = new System.Drawing.Size(339, 20);
            this.txtFolderPath.TabIndex = 1;
            // 
            // lblFolder
            // 
            this.lblFolder.AutoSize = true;
            this.lblFolder.Location = new System.Drawing.Point(12, 14);
            this.lblFolder.Name = "lblFolder";
            this.lblFolder.Size = new System.Drawing.Size(101, 13);
            this.lblFolder.TabIndex = 2;
            this.lblFolder.Text = "Select Folder to Back Up:";
            // 
            // lblSchedule
            // 
            this.lblSchedule.AutoSize = true;
            this.lblSchedule.Location = new System.Drawing.Point(12, 70);
            this.lblSchedule.Name = "lblSchedule";
            this.lblSchedule.Size = new System.Drawing.Size(99, 13);
            this.lblSchedule.TabIndex = 3;
            this.lblSchedule.Text = "Backup Schedule:";
            // 
            // cboSchedule
            // 
            this.cboSchedule.FormattingEnabled = true;
            this.cboSchedule.Location = new System.Drawing.Point(15, 87);
            this.cboSchedule.Name = "cboSchedule";
            this.cboSchedule.Size = new System.Drawing.Size(120, 21);
            this.cboSchedule.TabIndex = 4;
            // 
            // timePicker
            // 
            this.timePicker.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.timePicker.Location = new System.Drawing.Point(141, 88);
            this.timePicker.Name = "timePicker";
            this.timePicker.ShowUpDown = true;
            this.timePicker.Size = new System.Drawing.Size(120, 20);
            this.timePicker.TabIndex = 5;
            // 
            // lblTime
            // 
            this.lblTime.AutoSize = true;
            this.lblTime.Location = new System.Drawing.Point(138, 70);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(91, 13);
            this.lblTime.TabIndex = 6;
            this.lblTime.Text = "Time of Backup:";
            // 
            // btnSchedule
            // 
            this.btnSchedule.Location = new System.Drawing.Point(15, 120);
            this.btnSchedule.Name = "btnSchedule";
            this.btnSchedule.Size = new System.Drawing.Size(120, 23);
            this.btnSchedule.TabIndex = 7;
            this.btnSchedule.Text = "Schedule Backup";
            this.btnSchedule.UseVisualStyleBackColor = true;
            this.btnSchedule.Click += new System.EventHandler(this.btnSchedule_Click);
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(450, 160);
            this.Controls.Add(this.btnSchedule);
            this.Controls.Add(this.lblTime);
            this.Controls.Add(this.timePicker);
            this.Controls.Add(this.cboSchedule);
            this.Controls.Add(this.lblSchedule);
            this.Controls.Add(this.lblFolder);
            this.Controls.Add(this.txtFolderPath);
            this.Controls.Add(this.btnBrowse);
            this.Name = "Form1";
            this.Text = "OLTAB - Backup Manager";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.TextBox txtFolderPath;
        private System.Windows.Forms.Label lblFolder;
        private System.Windows.Forms.Label lblSchedule;
        private System.Windows.Forms.ComboBox cboSchedule;
        private System.Windows.Forms.DateTimePicker timePicker;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.Button btnSchedule;
    }
}
