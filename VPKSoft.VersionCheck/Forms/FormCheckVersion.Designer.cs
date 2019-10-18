namespace VPKSoft.VersionCheck.Forms
{
    partial class FormCheckVersion
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
            this.lbCurrentVersion = new System.Windows.Forms.Label();
            this.tbCurrentVersion = new System.Windows.Forms.TextBox();
            this.tbNewVersion = new System.Windows.Forms.TextBox();
            this.lbNewVersion = new System.Windows.Forms.Label();
            this.lbReleaseDateTime = new System.Windows.Forms.Label();
            this.tbReleaseDateTime = new System.Windows.Forms.TextBox();
            this.tbReleaseNotes = new System.Windows.Forms.TextBox();
            this.lbReleaseNotes = new System.Windows.Forms.Label();
            this.pbLargerIcon = new System.Windows.Forms.PictureBox();
            this.btCancel = new System.Windows.Forms.Button();
            this.btUpdate = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pbLargerIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // lbCurrentVersion
            // 
            this.lbCurrentVersion.AutoSize = true;
            this.lbCurrentVersion.Location = new System.Drawing.Point(12, 15);
            this.lbCurrentVersion.Name = "lbCurrentVersion";
            this.lbCurrentVersion.Size = new System.Drawing.Size(81, 13);
            this.lbCurrentVersion.TabIndex = 0;
            this.lbCurrentVersion.Text = "Current version:";
            // 
            // tbCurrentVersion
            // 
            this.tbCurrentVersion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tbCurrentVersion.Location = new System.Drawing.Point(317, 12);
            this.tbCurrentVersion.Name = "tbCurrentVersion";
            this.tbCurrentVersion.ReadOnly = true;
            this.tbCurrentVersion.Size = new System.Drawing.Size(100, 20);
            this.tbCurrentVersion.TabIndex = 10;
            this.tbCurrentVersion.Text = "v.1.0.0.0";
            // 
            // tbNewVersion
            // 
            this.tbNewVersion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tbNewVersion.Location = new System.Drawing.Point(317, 38);
            this.tbNewVersion.Name = "tbNewVersion";
            this.tbNewVersion.ReadOnly = true;
            this.tbNewVersion.Size = new System.Drawing.Size(100, 20);
            this.tbNewVersion.TabIndex = 12;
            this.tbNewVersion.Text = "v.1.0.0.1";
            // 
            // lbNewVersion
            // 
            this.lbNewVersion.AutoSize = true;
            this.lbNewVersion.Location = new System.Drawing.Point(12, 41);
            this.lbNewVersion.Name = "lbNewVersion";
            this.lbNewVersion.Size = new System.Drawing.Size(69, 13);
            this.lbNewVersion.TabIndex = 11;
            this.lbNewVersion.Text = "New version:";
            // 
            // lbReleaseDateTime
            // 
            this.lbReleaseDateTime.AutoSize = true;
            this.lbReleaseDateTime.Location = new System.Drawing.Point(12, 67);
            this.lbReleaseDateTime.Name = "lbReleaseDateTime";
            this.lbReleaseDateTime.Size = new System.Drawing.Size(97, 13);
            this.lbReleaseDateTime.TabIndex = 13;
            this.lbReleaseDateTime.Text = "Release date/time:";
            // 
            // tbReleaseDateTime
            // 
            this.tbReleaseDateTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tbReleaseDateTime.Location = new System.Drawing.Point(231, 64);
            this.tbReleaseDateTime.Name = "tbReleaseDateTime";
            this.tbReleaseDateTime.ReadOnly = true;
            this.tbReleaseDateTime.Size = new System.Drawing.Size(186, 20);
            this.tbReleaseDateTime.TabIndex = 14;
            this.tbReleaseDateTime.Text = "yyyy/mm/dd HH:mm";
            // 
            // tbReleaseNotes
            // 
            this.tbReleaseNotes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbReleaseNotes.Location = new System.Drawing.Point(12, 116);
            this.tbReleaseNotes.Multiline = true;
            this.tbReleaseNotes.Name = "tbReleaseNotes";
            this.tbReleaseNotes.ReadOnly = true;
            this.tbReleaseNotes.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbReleaseNotes.Size = new System.Drawing.Size(405, 127);
            this.tbReleaseNotes.TabIndex = 15;
            this.tbReleaseNotes.WordWrap = false;
            // 
            // lbReleaseNotes
            // 
            this.lbReleaseNotes.AutoSize = true;
            this.lbReleaseNotes.Location = new System.Drawing.Point(12, 93);
            this.lbReleaseNotes.Name = "lbReleaseNotes";
            this.lbReleaseNotes.Size = new System.Drawing.Size(93, 13);
            this.lbReleaseNotes.TabIndex = 16;
            this.lbReleaseNotes.Text = "Release changes:";
            // 
            // pbLargerIcon
            // 
            this.pbLargerIcon.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pbLargerIcon.Location = new System.Drawing.Point(11, 249);
            this.pbLargerIcon.Name = "pbLargerIcon";
            this.pbLargerIcon.Size = new System.Drawing.Size(100, 50);
            this.pbLargerIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbLargerIcon.TabIndex = 17;
            this.pbLargerIcon.TabStop = false;
            // 
            // btCancel
            // 
            this.btCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btCancel.Location = new System.Drawing.Point(342, 301);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(75, 23);
            this.btCancel.TabIndex = 18;
            this.btCancel.Text = "Cancel";
            this.btCancel.UseVisualStyleBackColor = true;
            // 
            // btUpdate
            // 
            this.btUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btUpdate.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btUpdate.Location = new System.Drawing.Point(129, 301);
            this.btUpdate.Name = "btUpdate";
            this.btUpdate.Size = new System.Drawing.Size(193, 23);
            this.btUpdate.TabIndex = 19;
            this.btUpdate.Text = "Update the software";
            this.btUpdate.UseVisualStyleBackColor = true;
            // 
            // FormCheckVersion
            // 
            this.AcceptButton = this.btUpdate;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btCancel;
            this.ClientSize = new System.Drawing.Size(429, 336);
            this.Controls.Add(this.lbCurrentVersion);
            this.Controls.Add(this.btUpdate);
            this.Controls.Add(this.tbCurrentVersion);
            this.Controls.Add(this.tbReleaseDateTime);
            this.Controls.Add(this.btCancel);
            this.Controls.Add(this.tbReleaseNotes);
            this.Controls.Add(this.lbNewVersion);
            this.Controls.Add(this.lbReleaseDateTime);
            this.Controls.Add(this.pbLargerIcon);
            this.Controls.Add(this.lbReleaseNotes);
            this.Controls.Add(this.tbNewVersion);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormCheckVersion";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FormCheckVersion";
            ((System.ComponentModel.ISupportInitialize)(this.pbLargerIcon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbCurrentVersion;
        private System.Windows.Forms.TextBox tbCurrentVersion;
        private System.Windows.Forms.TextBox tbNewVersion;
        private System.Windows.Forms.Label lbNewVersion;
        private System.Windows.Forms.Label lbReleaseDateTime;
        private System.Windows.Forms.TextBox tbReleaseDateTime;
        private System.Windows.Forms.TextBox tbReleaseNotes;
        private System.Windows.Forms.Label lbReleaseNotes;
        private System.Windows.Forms.PictureBox pbLargerIcon;
        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.Button btUpdate;
    }
}