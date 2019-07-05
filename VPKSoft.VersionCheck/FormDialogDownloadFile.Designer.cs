namespace VPKSoft.VersionCheck
{
    partial class FormDialogDownloadFile
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
            this.pbDownloadProgress = new System.Windows.Forms.ProgressBar();
            this.tlpDownloadStatistics = new System.Windows.Forms.TableLayoutPanel();
            this.lbPercentageValue = new System.Windows.Forms.Label();
            this.lbMBOfMBValue = new System.Windows.Forms.Label();
            this.lbSpeedMBsText = new System.Windows.Forms.Label();
            this.lbSpeedMBsValue = new System.Windows.Forms.Label();
            this.lbPercentageText = new System.Windows.Forms.Label();
            this.lbMBOfMBText = new System.Windows.Forms.Label();
            this.lbDownloadFileNameDescription = new System.Windows.Forms.Label();
            this.lbDownloadFileNameValue = new System.Windows.Forms.Label();
            this.tlpCenterButton = new System.Windows.Forms.TableLayoutPanel();
            this.btCancel = new System.Windows.Forms.Button();
            this.pnMain = new System.Windows.Forms.Panel();
            this.tlpDownloadStatistics.SuspendLayout();
            this.tlpCenterButton.SuspendLayout();
            this.pnMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // pbDownloadProgress
            // 
            this.pbDownloadProgress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pbDownloadProgress.Location = new System.Drawing.Point(9, 59);
            this.pbDownloadProgress.Name = "pbDownloadProgress";
            this.pbDownloadProgress.Size = new System.Drawing.Size(466, 16);
            this.pbDownloadProgress.Step = 1;
            this.pbDownloadProgress.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.pbDownloadProgress.TabIndex = 0;
            // 
            // tlpDownloadStatistics
            // 
            this.tlpDownloadStatistics.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tlpDownloadStatistics.AutoSize = true;
            this.tlpDownloadStatistics.ColumnCount = 11;
            this.tlpDownloadStatistics.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpDownloadStatistics.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tlpDownloadStatistics.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpDownloadStatistics.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpDownloadStatistics.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tlpDownloadStatistics.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpDownloadStatistics.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpDownloadStatistics.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tlpDownloadStatistics.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpDownloadStatistics.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpDownloadStatistics.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpDownloadStatistics.Controls.Add(this.lbPercentageValue, 3, 2);
            this.tlpDownloadStatistics.Controls.Add(this.lbMBOfMBValue, 6, 2);
            this.tlpDownloadStatistics.Controls.Add(this.lbSpeedMBsText, 8, 2);
            this.tlpDownloadStatistics.Controls.Add(this.lbSpeedMBsValue, 9, 2);
            this.tlpDownloadStatistics.Controls.Add(this.lbPercentageText, 2, 2);
            this.tlpDownloadStatistics.Controls.Add(this.lbMBOfMBText, 5, 2);
            this.tlpDownloadStatistics.Controls.Add(this.lbDownloadFileNameDescription, 0, 0);
            this.tlpDownloadStatistics.Controls.Add(this.lbDownloadFileNameValue, 1, 0);
            this.tlpDownloadStatistics.Location = new System.Drawing.Point(9, 13);
            this.tlpDownloadStatistics.Name = "tlpDownloadStatistics";
            this.tlpDownloadStatistics.RowCount = 3;
            this.tlpDownloadStatistics.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpDownloadStatistics.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tlpDownloadStatistics.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpDownloadStatistics.Size = new System.Drawing.Size(466, 40);
            this.tlpDownloadStatistics.TabIndex = 1;
            // 
            // lbPercentageValue
            // 
            this.lbPercentageValue.AutoSize = true;
            this.lbPercentageValue.Location = new System.Drawing.Point(93, 18);
            this.lbPercentageValue.Name = "lbPercentageValue";
            this.lbPercentageValue.Size = new System.Drawing.Size(22, 13);
            this.lbPercentageValue.TabIndex = 1;
            this.lbPercentageValue.Text = "0.0";
            // 
            // lbMBOfMBValue
            // 
            this.lbMBOfMBValue.AutoSize = true;
            this.lbMBOfMBValue.Location = new System.Drawing.Point(155, 18);
            this.lbMBOfMBValue.Name = "lbMBOfMBValue";
            this.lbMBOfMBValue.Size = new System.Drawing.Size(30, 13);
            this.lbMBOfMBValue.TabIndex = 2;
            this.lbMBOfMBValue.Text = "0 / 0";
            // 
            // lbSpeedMBsText
            // 
            this.lbSpeedMBsText.AutoSize = true;
            this.lbSpeedMBsText.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lbSpeedMBsText.Location = new System.Drawing.Point(196, 18);
            this.lbSpeedMBsText.Name = "lbSpeedMBsText";
            this.lbSpeedMBsText.Size = new System.Drawing.Size(76, 13);
            this.lbSpeedMBsText.TabIndex = 3;
            this.lbSpeedMBsText.Text = "Speed (MB/s):";
            // 
            // lbSpeedMBsValue
            // 
            this.lbSpeedMBsValue.AutoSize = true;
            this.lbSpeedMBsValue.Location = new System.Drawing.Point(278, 18);
            this.lbSpeedMBsValue.Name = "lbSpeedMBsValue";
            this.lbSpeedMBsValue.Size = new System.Drawing.Size(22, 13);
            this.lbSpeedMBsValue.TabIndex = 5;
            this.lbSpeedMBsValue.Text = "0.0";
            // 
            // lbPercentageText
            // 
            this.lbPercentageText.AutoSize = true;
            this.lbPercentageText.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lbPercentageText.Location = new System.Drawing.Point(72, 18);
            this.lbPercentageText.Name = "lbPercentageText";
            this.lbPercentageText.Size = new System.Drawing.Size(15, 13);
            this.lbPercentageText.TabIndex = 6;
            this.lbPercentageText.Text = "%";
            // 
            // lbMBOfMBText
            // 
            this.lbMBOfMBText.AutoSize = true;
            this.lbMBOfMBText.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lbMBOfMBText.Location = new System.Drawing.Point(126, 18);
            this.lbMBOfMBText.Name = "lbMBOfMBText";
            this.lbMBOfMBText.Size = new System.Drawing.Size(23, 13);
            this.lbMBOfMBText.TabIndex = 7;
            this.lbMBOfMBText.Text = "MB";
            // 
            // lbDownloadFileNameDescription
            // 
            this.lbDownloadFileNameDescription.AutoSize = true;
            this.lbDownloadFileNameDescription.Location = new System.Drawing.Point(3, 0);
            this.lbDownloadFileNameDescription.Name = "lbDownloadFileNameDescription";
            this.lbDownloadFileNameDescription.Size = new System.Drawing.Size(58, 13);
            this.lbDownloadFileNameDescription.TabIndex = 0;
            this.lbDownloadFileNameDescription.Text = "Download:";
            // 
            // lbDownloadFileNameValue
            // 
            this.lbDownloadFileNameValue.AutoSize = true;
            this.tlpDownloadStatistics.SetColumnSpan(this.lbDownloadFileNameValue, 10);
            this.lbDownloadFileNameValue.Location = new System.Drawing.Point(67, 0);
            this.lbDownloadFileNameValue.Name = "lbDownloadFileNameValue";
            this.lbDownloadFileNameValue.Size = new System.Drawing.Size(37, 13);
            this.lbDownloadFileNameValue.TabIndex = 8;
            this.lbDownloadFileNameValue.Text = "file.ext";
            // 
            // tlpCenterButton
            // 
            this.tlpCenterButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tlpCenterButton.ColumnCount = 3;
            this.tlpCenterButton.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpCenterButton.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpCenterButton.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpCenterButton.Controls.Add(this.btCancel, 1, 0);
            this.tlpCenterButton.Location = new System.Drawing.Point(9, 81);
            this.tlpCenterButton.Name = "tlpCenterButton";
            this.tlpCenterButton.RowCount = 1;
            this.tlpCenterButton.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpCenterButton.Size = new System.Drawing.Size(466, 29);
            this.tlpCenterButton.TabIndex = 2;
            // 
            // btCancel
            // 
            this.btCancel.Location = new System.Drawing.Point(195, 3);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(75, 23);
            this.btCancel.TabIndex = 0;
            this.btCancel.Text = "Cancel";
            this.btCancel.UseVisualStyleBackColor = true;
            this.btCancel.Click += new System.EventHandler(this.BtCancel_Click);
            // 
            // pnMain
            // 
            this.pnMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnMain.BackColor = System.Drawing.SystemColors.Control;
            this.pnMain.Controls.Add(this.tlpDownloadStatistics);
            this.pnMain.Controls.Add(this.tlpCenterButton);
            this.pnMain.Controls.Add(this.pbDownloadProgress);
            this.pnMain.Location = new System.Drawing.Point(2, 2);
            this.pnMain.Name = "pnMain";
            this.pnMain.Size = new System.Drawing.Size(487, 120);
            this.pnMain.TabIndex = 3;
            // 
            // FormDialogDownloadFile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.ClientSize = new System.Drawing.Size(491, 124);
            this.Controls.Add(this.pnMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormDialogDownloadFile";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FormCheckVersion";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormDialogDownloadFile_FormClosed);
            this.Shown += new System.EventHandler(this.FormDownloadFile_Shown);
            this.tlpDownloadStatistics.ResumeLayout(false);
            this.tlpDownloadStatistics.PerformLayout();
            this.tlpCenterButton.ResumeLayout(false);
            this.pnMain.ResumeLayout(false);
            this.pnMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ProgressBar pbDownloadProgress;
        private System.Windows.Forms.TableLayoutPanel tlpDownloadStatistics;
        private System.Windows.Forms.Label lbDownloadFileNameDescription;
        private System.Windows.Forms.Label lbPercentageValue;
        private System.Windows.Forms.Label lbMBOfMBValue;
        private System.Windows.Forms.Label lbSpeedMBsText;
        private System.Windows.Forms.Label lbSpeedMBsValue;
        private System.Windows.Forms.TableLayoutPanel tlpCenterButton;
        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.Label lbPercentageText;
        private System.Windows.Forms.Label lbMBOfMBText;
        private System.Windows.Forms.Label lbDownloadFileNameValue;
        private System.Windows.Forms.Panel pnMain;
    }
}