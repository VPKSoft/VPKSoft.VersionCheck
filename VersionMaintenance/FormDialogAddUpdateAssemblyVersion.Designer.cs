namespace VersionMaintenance
{
    partial class FormDialogAddUpdateAssemblyVersion
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormDialogAddUpdateAssemblyVersion));
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.lbSoftwareName = new System.Windows.Forms.Label();
            this.tbSoftwareName = new System.Windows.Forms.TextBox();
            this.lbSoftwareVersion = new System.Windows.Forms.Label();
            this.tbSoftwareVersion = new System.Windows.Forms.TextBox();
            this.lbDownloadLink = new System.Windows.Forms.Label();
            this.tbDownloadLink = new System.Windows.Forms.TextBox();
            this.lbReleaseDateTime = new System.Windows.Forms.Label();
            this.tlpReleaseDateTime = new System.Windows.Forms.TableLayoutPanel();
            this.dtpReleaseDate = new System.Windows.Forms.DateTimePicker();
            this.dtpReleaseTime = new System.Windows.Forms.DateTimePicker();
            this.btToUTC = new System.Windows.Forms.Button();
            this.cbDirectDownload = new System.Windows.Forms.CheckBox();
            this.lbMetaData = new System.Windows.Forms.Label();
            this.tbMetaData = new System.Windows.Forms.TextBox();
            this.btOK = new System.Windows.Forms.Button();
            this.btCancel = new System.Windows.Forms.Button();
            this.ttMain = new System.Windows.Forms.ToolTip(this.components);
            this.tlpMain.SuspendLayout();
            this.tlpReleaseDateTime.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpMain
            // 
            this.tlpMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tlpMain.ColumnCount = 2;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Controls.Add(this.lbSoftwareName, 0, 0);
            this.tlpMain.Controls.Add(this.tbSoftwareName, 1, 0);
            this.tlpMain.Controls.Add(this.lbSoftwareVersion, 0, 1);
            this.tlpMain.Controls.Add(this.tbSoftwareVersion, 1, 1);
            this.tlpMain.Controls.Add(this.lbDownloadLink, 0, 2);
            this.tlpMain.Controls.Add(this.tbDownloadLink, 1, 2);
            this.tlpMain.Controls.Add(this.lbReleaseDateTime, 0, 3);
            this.tlpMain.Controls.Add(this.tlpReleaseDateTime, 1, 3);
            this.tlpMain.Controls.Add(this.cbDirectDownload, 0, 4);
            this.tlpMain.Controls.Add(this.lbMetaData, 0, 5);
            this.tlpMain.Controls.Add(this.tbMetaData, 1, 5);
            this.tlpMain.Location = new System.Drawing.Point(12, 12);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 6;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Size = new System.Drawing.Size(664, 339);
            this.tlpMain.TabIndex = 0;
            // 
            // lbSoftwareName
            // 
            this.lbSoftwareName.AutoSize = true;
            this.lbSoftwareName.Location = new System.Drawing.Point(3, 0);
            this.lbSoftwareName.Name = "lbSoftwareName";
            this.lbSoftwareName.Padding = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.lbSoftwareName.Size = new System.Drawing.Size(81, 19);
            this.lbSoftwareName.TabIndex = 0;
            this.lbSoftwareName.Text = "Software name:";
            // 
            // tbSoftwareName
            // 
            this.tbSoftwareName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbSoftwareName.Location = new System.Drawing.Point(163, 3);
            this.tbSoftwareName.Name = "tbSoftwareName";
            this.tbSoftwareName.ReadOnly = true;
            this.tbSoftwareName.Size = new System.Drawing.Size(498, 20);
            this.tbSoftwareName.TabIndex = 1;
            // 
            // lbSoftwareVersion
            // 
            this.lbSoftwareVersion.AutoSize = true;
            this.lbSoftwareVersion.Location = new System.Drawing.Point(3, 26);
            this.lbSoftwareVersion.Name = "lbSoftwareVersion";
            this.lbSoftwareVersion.Padding = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.lbSoftwareVersion.Size = new System.Drawing.Size(89, 19);
            this.lbSoftwareVersion.TabIndex = 2;
            this.lbSoftwareVersion.Text = "Software version:";
            // 
            // tbSoftwareVersion
            // 
            this.tbSoftwareVersion.Location = new System.Drawing.Point(163, 29);
            this.tbSoftwareVersion.Name = "tbSoftwareVersion";
            this.tbSoftwareVersion.ReadOnly = true;
            this.tbSoftwareVersion.Size = new System.Drawing.Size(103, 20);
            this.tbSoftwareVersion.TabIndex = 3;
            // 
            // lbDownloadLink
            // 
            this.lbDownloadLink.AutoSize = true;
            this.lbDownloadLink.Location = new System.Drawing.Point(3, 52);
            this.lbDownloadLink.Name = "lbDownloadLink";
            this.lbDownloadLink.Padding = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.lbDownloadLink.Size = new System.Drawing.Size(77, 19);
            this.lbDownloadLink.TabIndex = 4;
            this.lbDownloadLink.Text = "Download link:";
            // 
            // tbDownloadLink
            // 
            this.tbDownloadLink.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbDownloadLink.Location = new System.Drawing.Point(163, 55);
            this.tbDownloadLink.Name = "tbDownloadLink";
            this.tbDownloadLink.Size = new System.Drawing.Size(498, 20);
            this.tbDownloadLink.TabIndex = 5;
            // 
            // lbReleaseDateTime
            // 
            this.lbReleaseDateTime.AutoSize = true;
            this.lbReleaseDateTime.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lbReleaseDateTime.Location = new System.Drawing.Point(3, 78);
            this.lbReleaseDateTime.Name = "lbReleaseDateTime";
            this.lbReleaseDateTime.Padding = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.lbReleaseDateTime.Size = new System.Drawing.Size(147, 19);
            this.lbReleaseDateTime.TabIndex = 6;
            this.lbReleaseDateTime.Text = "Release date and time (UTC):";
            this.ttMain.SetToolTip(this.lbReleaseDateTime, "Set to the current UTC time");
            this.lbReleaseDateTime.Click += new System.EventHandler(this.LbReleaseDateTime_Click);
            // 
            // tlpReleaseDateTime
            // 
            this.tlpReleaseDateTime.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tlpReleaseDateTime.AutoSize = true;
            this.tlpReleaseDateTime.ColumnCount = 3;
            this.tlpReleaseDateTime.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tlpReleaseDateTime.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tlpReleaseDateTime.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlpReleaseDateTime.Controls.Add(this.dtpReleaseDate, 0, 0);
            this.tlpReleaseDateTime.Controls.Add(this.dtpReleaseTime, 1, 0);
            this.tlpReleaseDateTime.Controls.Add(this.btToUTC, 2, 0);
            this.tlpReleaseDateTime.Location = new System.Drawing.Point(163, 81);
            this.tlpReleaseDateTime.Name = "tlpReleaseDateTime";
            this.tlpReleaseDateTime.RowCount = 1;
            this.tlpReleaseDateTime.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpReleaseDateTime.Size = new System.Drawing.Size(498, 29);
            this.tlpReleaseDateTime.TabIndex = 8;
            // 
            // dtpReleaseDate
            // 
            this.dtpReleaseDate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpReleaseDate.Location = new System.Drawing.Point(3, 3);
            this.dtpReleaseDate.Name = "dtpReleaseDate";
            this.dtpReleaseDate.Size = new System.Drawing.Size(193, 20);
            this.dtpReleaseDate.TabIndex = 7;
            // 
            // dtpReleaseTime
            // 
            this.dtpReleaseTime.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpReleaseTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtpReleaseTime.Location = new System.Drawing.Point(202, 3);
            this.dtpReleaseTime.Name = "dtpReleaseTime";
            this.dtpReleaseTime.ShowUpDown = true;
            this.dtpReleaseTime.Size = new System.Drawing.Size(193, 20);
            this.dtpReleaseTime.TabIndex = 8;
            // 
            // btToUTC
            // 
            this.btToUTC.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btToUTC.Location = new System.Drawing.Point(401, 3);
            this.btToUTC.Name = "btToUTC";
            this.btToUTC.Size = new System.Drawing.Size(94, 23);
            this.btToUTC.TabIndex = 9;
            this.btToUTC.Text = "To UTC";
            this.btToUTC.UseVisualStyleBackColor = true;
            this.btToUTC.Click += new System.EventHandler(this.BtToUTC_Click);
            // 
            // cbDirectDownload
            // 
            this.cbDirectDownload.AutoSize = true;
            this.cbDirectDownload.Location = new System.Drawing.Point(3, 116);
            this.cbDirectDownload.Name = "cbDirectDownload";
            this.cbDirectDownload.Size = new System.Drawing.Size(103, 17);
            this.cbDirectDownload.TabIndex = 9;
            this.cbDirectDownload.Text = "Direct download";
            this.cbDirectDownload.UseVisualStyleBackColor = true;
            // 
            // lbMetaData
            // 
            this.lbMetaData.AutoSize = true;
            this.lbMetaData.Location = new System.Drawing.Point(3, 136);
            this.lbMetaData.Name = "lbMetaData";
            this.lbMetaData.Padding = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.lbMetaData.Size = new System.Drawing.Size(154, 19);
            this.lbMetaData.TabIndex = 10;
            this.lbMetaData.Text = "Meta data (release notes, etc.):";
            // 
            // tbMetaData
            // 
            this.tbMetaData.AcceptsReturn = true;
            this.tbMetaData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbMetaData.Location = new System.Drawing.Point(163, 139);
            this.tbMetaData.Multiline = true;
            this.tbMetaData.Name = "tbMetaData";
            this.tbMetaData.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbMetaData.Size = new System.Drawing.Size(498, 197);
            this.tbMetaData.TabIndex = 11;
            this.tbMetaData.WordWrap = false;
            // 
            // btOK
            // 
            this.btOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btOK.Location = new System.Drawing.Point(12, 357);
            this.btOK.Name = "btOK";
            this.btOK.Size = new System.Drawing.Size(75, 23);
            this.btOK.TabIndex = 1;
            this.btOK.Text = "OK";
            this.btOK.UseVisualStyleBackColor = true;
            // 
            // btCancel
            // 
            this.btCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btCancel.Location = new System.Drawing.Point(601, 357);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(75, 23);
            this.btCancel.TabIndex = 2;
            this.btCancel.Text = "Cancel";
            this.btCancel.UseVisualStyleBackColor = true;
            // 
            // FormDialogAddUpdateAssemblyVersion
            // 
            this.AcceptButton = this.btOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btCancel;
            this.ClientSize = new System.Drawing.Size(688, 392);
            this.Controls.Add(this.btCancel);
            this.Controls.Add(this.btOK);
            this.Controls.Add(this.tlpMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormDialogAddUpdateAssemblyVersion";
            this.ShowInTaskbar = false;
            this.Text = "Add or update assembly version";
            this.tlpMain.ResumeLayout(false);
            this.tlpMain.PerformLayout();
            this.tlpReleaseDateTime.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.Label lbSoftwareName;
        private System.Windows.Forms.TextBox tbSoftwareName;
        private System.Windows.Forms.Label lbSoftwareVersion;
        private System.Windows.Forms.TextBox tbSoftwareVersion;
        private System.Windows.Forms.Label lbDownloadLink;
        private System.Windows.Forms.TextBox tbDownloadLink;
        private System.Windows.Forms.Label lbReleaseDateTime;
        private System.Windows.Forms.TableLayoutPanel tlpReleaseDateTime;
        private System.Windows.Forms.DateTimePicker dtpReleaseDate;
        private System.Windows.Forms.DateTimePicker dtpReleaseTime;
        private System.Windows.Forms.CheckBox cbDirectDownload;
        private System.Windows.Forms.Label lbMetaData;
        private System.Windows.Forms.TextBox tbMetaData;
        private System.Windows.Forms.Button btOK;
        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.Button btToUTC;
        private System.Windows.Forms.ToolTip ttMain;
    }
}