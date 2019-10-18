namespace VersionMaintenance.FormDialogs
{
    partial class FormDialogQueryDeleteArchiveEntry
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormDialogQueryDeleteArchiveEntry));
            this.pnAcctionImage = new System.Windows.Forms.Panel();
            this.lbEntryName = new System.Windows.Forms.Label();
            this.tbEntryName = new System.Windows.Forms.TextBox();
            this.cbArchiveData = new System.Windows.Forms.CheckBox();
            this.cbArchiveDataChangeHistory = new System.Windows.Forms.CheckBox();
            this.btOK = new System.Windows.Forms.Button();
            this.btCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // pnAcctionImage
            // 
            this.pnAcctionImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pnAcctionImage.Location = new System.Drawing.Point(12, 9);
            this.pnAcctionImage.Margin = new System.Windows.Forms.Padding(0);
            this.pnAcctionImage.Name = "pnAcctionImage";
            this.pnAcctionImage.Size = new System.Drawing.Size(64, 64);
            this.pnAcctionImage.TabIndex = 0;
            // 
            // lbEntryName
            // 
            this.lbEntryName.AutoSize = true;
            this.lbEntryName.Location = new System.Drawing.Point(85, 15);
            this.lbEntryName.Name = "lbEntryName";
            this.lbEntryName.Size = new System.Drawing.Size(78, 13);
            this.lbEntryName.TabIndex = 1;
            this.lbEntryName.Text = "Software entry:";
            // 
            // tbEntryName
            // 
            this.tbEntryName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbEntryName.Location = new System.Drawing.Point(169, 12);
            this.tbEntryName.Name = "tbEntryName";
            this.tbEntryName.ReadOnly = true;
            this.tbEntryName.Size = new System.Drawing.Size(320, 20);
            this.tbEntryName.TabIndex = 2;
            // 
            // cbArchiveData
            // 
            this.cbArchiveData.AutoSize = true;
            this.cbArchiveData.Checked = true;
            this.cbArchiveData.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbArchiveData.Location = new System.Drawing.Point(88, 40);
            this.cbArchiveData.Name = "cbArchiveData";
            this.cbArchiveData.Size = new System.Drawing.Size(130, 17);
            this.cbArchiveData.TabIndex = 3;
            this.cbArchiveData.Text = "Archive the entry data";
            this.cbArchiveData.UseVisualStyleBackColor = true;
            this.cbArchiveData.CheckedChanged += new System.EventHandler(this.cbArchiveData_CheckedChanged);
            // 
            // cbArchiveDataChangeHistory
            // 
            this.cbArchiveDataChangeHistory.AutoSize = true;
            this.cbArchiveDataChangeHistory.Checked = true;
            this.cbArchiveDataChangeHistory.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbArchiveDataChangeHistory.Location = new System.Drawing.Point(105, 66);
            this.cbArchiveDataChangeHistory.Name = "cbArchiveDataChangeHistory";
            this.cbArchiveDataChangeHistory.Size = new System.Drawing.Size(217, 17);
            this.cbArchiveDataChangeHistory.TabIndex = 6;
            this.cbArchiveDataChangeHistory.Text = "Archive the entry localized history entries";
            this.cbArchiveDataChangeHistory.UseVisualStyleBackColor = true;
            // 
            // btOK
            // 
            this.btOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btOK.Location = new System.Drawing.Point(12, 90);
            this.btOK.Name = "btOK";
            this.btOK.Size = new System.Drawing.Size(75, 23);
            this.btOK.TabIndex = 8;
            this.btOK.Text = "OK";
            this.btOK.UseVisualStyleBackColor = true;
            // 
            // btCancel
            // 
            this.btCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btCancel.Location = new System.Drawing.Point(419, 90);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(75, 23);
            this.btCancel.TabIndex = 9;
            this.btCancel.Text = "Cancel";
            this.btCancel.UseVisualStyleBackColor = true;
            // 
            // FormDialogQueryDeleteArchiveEntry
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(506, 125);
            this.Controls.Add(this.btCancel);
            this.Controls.Add(this.btOK);
            this.Controls.Add(this.cbArchiveDataChangeHistory);
            this.Controls.Add(this.cbArchiveData);
            this.Controls.Add(this.tbEntryName);
            this.Controls.Add(this.lbEntryName);
            this.Controls.Add(this.pnAcctionImage);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormDialogQueryDeleteArchiveEntry";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Really delete selected software entry?";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnAcctionImage;
        private System.Windows.Forms.Label lbEntryName;
        private System.Windows.Forms.TextBox tbEntryName;
        private System.Windows.Forms.CheckBox cbArchiveData;
        private System.Windows.Forms.CheckBox cbArchiveDataChangeHistory;
        private System.Windows.Forms.Button btOK;
        private System.Windows.Forms.Button btCancel;
    }
}