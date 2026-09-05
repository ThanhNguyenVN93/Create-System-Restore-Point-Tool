namespace WindowsFormsApp1
{
    partial class frm_Create_System_Restore_Point_Tool
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblDescription = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.groupSystemStatus = new System.Windows.Forms.GroupBox();
            this.lblProtectionStatus = new System.Windows.Forms.Label();
            this.lblDiskSpace = new System.Windows.Forms.Label();
            this.lblWindowsVersion = new System.Windows.Forms.Label();
            this.btnCreate = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.lblStatus = new System.Windows.Forms.Label();
            this.groupSystemStatus.SuspendLayout();
            this.SuspendLayout();

            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(0, 120, 215);
            this.lblTitle.Location = new System.Drawing.Point(15, 15);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(368, 32);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "💾 System Restore Point Creator";

            this.lblDescription.AutoSize = true;
            this.lblDescription.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblDescription.Location = new System.Drawing.Point(15, 60);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(187, 19);
            this.lblDescription.TabIndex = 1;
            this.lblDescription.Text = "Custom Description (Optional):";

            this.txtDescription.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.txtDescription.Location = new System.Drawing.Point(20, 82);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(360, 25);
            this.txtDescription.TabIndex = 1;
            this.txtDescription.Text = "Manual Restore Point";

            this.groupSystemStatus.Controls.Add(this.lblWindowsVersion);
            this.groupSystemStatus.Controls.Add(this.lblDiskSpace);
            this.groupSystemStatus.Controls.Add(this.lblProtectionStatus);
            this.groupSystemStatus.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.groupSystemStatus.Location = new System.Drawing.Point(20, 120);
            this.groupSystemStatus.Name = "groupSystemStatus";
            this.groupSystemStatus.Size = new System.Drawing.Size(360, 110);
            this.groupSystemStatus.TabIndex = 2;
            this.groupSystemStatus.TabStop = false;
            this.groupSystemStatus.Text = "📊 System Status";

            this.lblProtectionStatus.AutoSize = true;
            this.lblProtectionStatus.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblProtectionStatus.Location = new System.Drawing.Point(10, 30);
            this.lblProtectionStatus.Name = "lblProtectionStatus";
            this.lblProtectionStatus.Size = new System.Drawing.Size(178, 15);
            this.lblProtectionStatus.TabIndex = 3;
            this.lblProtectionStatus.Text = "✓ System Protection: Checking...";

            this.lblDiskSpace.AutoSize = true;
            this.lblDiskSpace.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblDiskSpace.Location = new System.Drawing.Point(10, 50);
            this.lblDiskSpace.Name = "lblDiskSpace";
            this.lblDiskSpace.Size = new System.Drawing.Size(150, 15);
            this.lblDiskSpace.TabIndex = 4;
            this.lblDiskSpace.Text = "✓ Free Disk Space: Checking...";

            this.lblWindowsVersion.AutoSize = true;
            this.lblWindowsVersion.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblWindowsVersion.Location = new System.Drawing.Point(10, 70);
            this.lblWindowsVersion.Name = "lblWindowsVersion";
            this.lblWindowsVersion.Size = new System.Drawing.Size(142, 15);
            this.lblWindowsVersion.TabIndex = 5;
            this.lblWindowsVersion.Text = "✓ Windows Version: Ready";

            this.btnCreate.BackColor = System.Drawing.Color.FromArgb(0, 120, 215);
            this.btnCreate.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnCreate.ForeColor = System.Drawing.Color.White;
            this.btnCreate.Location = new System.Drawing.Point(20, 240);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(170, 40);
            this.btnCreate.TabIndex = 2;
            this.btnCreate.Text = "✓ Create Restore Point";
            this.btnCreate.UseVisualStyleBackColor = false;
            this.btnCreate.Click += new System.EventHandler(this.button1_Click);

            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.btnCancel.Location = new System.Drawing.Point(210, 240);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(170, 40);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "✕ Exit";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);

            this.progressBar.Location = new System.Drawing.Point(20, 290);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(360, 20);
            this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBar.TabIndex = 6;
            this.progressBar.Visible = false;

            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblStatus.ForeColor = System.Drawing.Color.Gray;
            this.lblStatus.Location = new System.Drawing.Point(20, 315);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(38, 15);
            this.lblStatus.TabIndex = 7;
            this.lblStatus.Text = "Ready";

            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(400, 345);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnCreate);
            this.Controls.Add(this.groupSystemStatus);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = true;
            this.Name = "frm_Create_System_Restore_Point_Tool";
            this.ShowIcon = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "System Restore Point Creator";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupSystemStatus.ResumeLayout(false);
            this.groupSystemStatus.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.GroupBox groupSystemStatus;
        private System.Windows.Forms.Label lblProtectionStatus;
        private System.Windows.Forms.Label lblDiskSpace;
        private System.Windows.Forms.Label lblWindowsVersion;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label lblStatus;
    }
}
