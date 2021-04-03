namespace VersionMaintenance
{
    partial class FormMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.gvSoftwareVersions = new System.Windows.Forms.DataGridView();
            this.colID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colApp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colVersion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDownloadLink = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colReleaseDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDirectDownload = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colMetaData = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDownloadCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.msMain = new System.Windows.Forms.MenuStrip();
            this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAddUpdateAssembly = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuThisAssemblyVersion = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuRefreshDatabaseEntries = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDeleteSelectedEntry = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuGenerateAPIKey = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAddLocalizedData = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuUpdateVersionManually = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTools = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuGenerateFiles = new System.Windows.Forms.ToolStripMenuItem();
            this.odAssembly = new System.Windows.Forms.OpenFileDialog();
            this.tsMain = new System.Windows.Forms.ToolStrip();
            this.tsbDeleteSelectedEntry = new System.Windows.Forms.ToolStripButton();
            this.tsbUpdateEntry = new System.Windows.Forms.ToolStripButton();
            this.tsbRefreshEntries = new System.Windows.Forms.ToolStripButton();
            this.tsbAddLocalizedData = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tslURIDescribtion = new System.Windows.Forms.ToolStripLabel();
            this.tstbLocationURI = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tslAPIKey = new System.Windows.Forms.ToolStripLabel();
            this.tstbAPIKey = new System.Windows.Forms.ToolStripTextBox();
            this.tbGenerateAPIKey = new System.Windows.Forms.ToolStripButton();
            this.lbTimeOutMS = new System.Windows.Forms.Label();
            this.nudTimeOutMS = new System.Windows.Forms.NumericUpDown();
            this.fbdDirectory = new Ookii.Dialogs.WinForms.VistaFolderBrowserDialog();
            this.mnuHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAbout = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.gvSoftwareVersions)).BeginInit();
            this.msMain.SuspendLayout();
            this.tsMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudTimeOutMS)).BeginInit();
            this.SuspendLayout();
            // 
            // gvSoftwareVersions
            // 
            this.gvSoftwareVersions.AllowUserToAddRows = false;
            this.gvSoftwareVersions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gvSoftwareVersions.BackgroundColor = System.Drawing.Color.White;
            this.gvSoftwareVersions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvSoftwareVersions.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colID,
            this.colApp,
            this.colVersion,
            this.colDownloadLink,
            this.colReleaseDate,
            this.colDirectDownload,
            this.colMetaData,
            this.colDownloadCount});
            this.gvSoftwareVersions.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.gvSoftwareVersions.GridColor = System.Drawing.Color.Black;
            this.gvSoftwareVersions.Location = new System.Drawing.Point(14, 60);
            this.gvSoftwareVersions.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.gvSoftwareVersions.Name = "gvSoftwareVersions";
            this.gvSoftwareVersions.Size = new System.Drawing.Size(1079, 384);
            this.gvSoftwareVersions.TabIndex = 3;
            // 
            // colID
            // 
            this.colID.HeaderText = "ID";
            this.colID.Name = "colID";
            this.colID.Visible = false;
            // 
            // colApp
            // 
            this.colApp.HeaderText = "Application";
            this.colApp.Name = "colApp";
            this.colApp.ReadOnly = true;
            // 
            // colVersion
            // 
            this.colVersion.HeaderText = "Version";
            this.colVersion.Name = "colVersion";
            this.colVersion.ReadOnly = true;
            // 
            // colDownloadLink
            // 
            this.colDownloadLink.HeaderText = "Download link";
            this.colDownloadLink.Name = "colDownloadLink";
            this.colDownloadLink.ReadOnly = true;
            // 
            // colReleaseDate
            // 
            this.colReleaseDate.HeaderText = "Release date";
            this.colReleaseDate.Name = "colReleaseDate";
            this.colReleaseDate.ReadOnly = true;
            // 
            // colDirectDownload
            // 
            this.colDirectDownload.HeaderText = "Direct download";
            this.colDirectDownload.Name = "colDirectDownload";
            this.colDirectDownload.ReadOnly = true;
            // 
            // colMetaData
            // 
            this.colMetaData.HeaderText = "Additional release information";
            this.colMetaData.Name = "colMetaData";
            this.colMetaData.ReadOnly = true;
            this.colMetaData.Width = 200;
            // 
            // colDownloadCount
            // 
            this.colDownloadCount.HeaderText = "Downloads";
            this.colDownloadCount.Name = "colDownloadCount";
            this.colDownloadCount.ReadOnly = true;
            this.colDownloadCount.Width = 70;
            // 
            // msMain
            // 
            this.msMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile,
            this.mnuTools,
            this.mnuHelp});
            this.msMain.Location = new System.Drawing.Point(0, 0);
            this.msMain.Name = "msMain";
            this.msMain.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
            this.msMain.Size = new System.Drawing.Size(1107, 24);
            this.msMain.TabIndex = 4;
            this.msMain.Text = "menuStrip1";
            // 
            // mnuFile
            // 
            this.mnuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuAddUpdateAssembly,
            this.mnuThisAssemblyVersion,
            this.mnuRefreshDatabaseEntries,
            this.mnuDeleteSelectedEntry,
            this.mnuGenerateAPIKey,
            this.mnuAddLocalizedData,
            this.mnuUpdateVersionManually});
            this.mnuFile.Name = "mnuFile";
            this.mnuFile.Size = new System.Drawing.Size(37, 20);
            this.mnuFile.Text = "File";
            // 
            // mnuAddUpdateAssembly
            // 
            this.mnuAddUpdateAssembly.Image = global::VersionMaintenance.Properties.Resources.db_update;
            this.mnuAddUpdateAssembly.Name = "mnuAddUpdateAssembly";
            this.mnuAddUpdateAssembly.Size = new System.Drawing.Size(287, 22);
            this.mnuAddUpdateAssembly.Text = "Add/update assembly";
            this.mnuAddUpdateAssembly.Click += new System.EventHandler(this.MnuAddUpdateAssembly_Click);
            // 
            // mnuThisAssemblyVersion
            // 
            this.mnuThisAssemblyVersion.Image = global::VersionMaintenance.Properties.Resources.application_x_ms_dos_executable;
            this.mnuThisAssemblyVersion.Name = "mnuThisAssemblyVersion";
            this.mnuThisAssemblyVersion.Size = new System.Drawing.Size(287, 22);
            this.mnuThisAssemblyVersion.Text = "This assembly version";
            this.mnuThisAssemblyVersion.Click += new System.EventHandler(this.MnuAddUpdateAssembly_Click);
            // 
            // mnuRefreshDatabaseEntries
            // 
            this.mnuRefreshDatabaseEntries.Image = global::VersionMaintenance.Properties.Resources.view_refresh_7;
            this.mnuRefreshDatabaseEntries.Name = "mnuRefreshDatabaseEntries";
            this.mnuRefreshDatabaseEntries.Size = new System.Drawing.Size(287, 22);
            this.mnuRefreshDatabaseEntries.Text = "Refresh database entries";
            this.mnuRefreshDatabaseEntries.Click += new System.EventHandler(this.MnuRefreshDatabaseEntries_Click);
            // 
            // mnuDeleteSelectedEntry
            // 
            this.mnuDeleteSelectedEntry.Image = global::VersionMaintenance.Properties.Resources.edit_delete_6;
            this.mnuDeleteSelectedEntry.Name = "mnuDeleteSelectedEntry";
            this.mnuDeleteSelectedEntry.Size = new System.Drawing.Size(287, 22);
            this.mnuDeleteSelectedEntry.Text = "Delete selected entry";
            this.mnuDeleteSelectedEntry.Click += new System.EventHandler(this.TsbDeleteSelectedEntry_Click);
            // 
            // mnuGenerateAPIKey
            // 
            this.mnuGenerateAPIKey.Image = global::VersionMaintenance.Properties.Resources.database_key;
            this.mnuGenerateAPIKey.Name = "mnuGenerateAPIKey";
            this.mnuGenerateAPIKey.Size = new System.Drawing.Size(287, 22);
            this.mnuGenerateAPIKey.Text = "Generate API key";
            this.mnuGenerateAPIKey.Click += new System.EventHandler(this.TbGenerateAPIKey_Click);
            // 
            // mnuAddLocalizedData
            // 
            this.mnuAddLocalizedData.Image = global::VersionMaintenance.Properties.Resources.education_languages;
            this.mnuAddLocalizedData.Name = "mnuAddLocalizedData";
            this.mnuAddLocalizedData.Size = new System.Drawing.Size(287, 22);
            this.mnuAddLocalizedData.Text = "Add or update localized version changes";
            this.mnuAddLocalizedData.Click += new System.EventHandler(this.mnuAddLocalizedData_Click);
            // 
            // mnuUpdateVersionManually
            // 
            this.mnuUpdateVersionManually.Image = global::VersionMaintenance.Properties.Resources.update_product;
            this.mnuUpdateVersionManually.Name = "mnuUpdateVersionManually";
            this.mnuUpdateVersionManually.Size = new System.Drawing.Size(287, 22);
            this.mnuUpdateVersionManually.Text = "Update version manually";
            this.mnuUpdateVersionManually.Click += new System.EventHandler(this.mnuUpdateVersionManually_Click);
            // 
            // mnuTools
            // 
            this.mnuTools.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuGenerateFiles});
            this.mnuTools.Name = "mnuTools";
            this.mnuTools.Size = new System.Drawing.Size(46, 20);
            this.mnuTools.Text = "Tools";
            // 
            // mnuGenerateFiles
            // 
            this.mnuGenerateFiles.Image = global::VersionMaintenance.Properties.Resources.run_build_install;
            this.mnuGenerateFiles.Name = "mnuGenerateFiles";
            this.mnuGenerateFiles.Size = new System.Drawing.Size(218, 22);
            this.mnuGenerateFiles.Text = "Generate files for a web site";
            this.mnuGenerateFiles.Click += new System.EventHandler(this.MnuGenerateFiles_Click);
            // 
            // odAssembly
            // 
            this.odAssembly.Filter = ".NET Executable|*.exe|.NET Assembly|*.dll";
            // 
            // tsMain
            // 
            this.tsMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbDeleteSelectedEntry,
            this.tsbUpdateEntry,
            this.tsbRefreshEntries,
            this.tsbAddLocalizedData,
            this.toolStripSeparator1,
            this.tslURIDescribtion,
            this.tstbLocationURI,
            this.toolStripSeparator2,
            this.tslAPIKey,
            this.tstbAPIKey,
            this.tbGenerateAPIKey});
            this.tsMain.Location = new System.Drawing.Point(0, 24);
            this.tsMain.Name = "tsMain";
            this.tsMain.Size = new System.Drawing.Size(1107, 25);
            this.tsMain.TabIndex = 5;
            this.tsMain.Text = "toolStrip1";
            // 
            // tsbDeleteSelectedEntry
            // 
            this.tsbDeleteSelectedEntry.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbDeleteSelectedEntry.Image = global::VersionMaintenance.Properties.Resources.edit_delete_6;
            this.tsbDeleteSelectedEntry.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbDeleteSelectedEntry.Name = "tsbDeleteSelectedEntry";
            this.tsbDeleteSelectedEntry.Size = new System.Drawing.Size(23, 22);
            this.tsbDeleteSelectedEntry.Text = "Delete selected entry";
            this.tsbDeleteSelectedEntry.Click += new System.EventHandler(this.TsbDeleteSelectedEntry_Click);
            // 
            // tsbUpdateEntry
            // 
            this.tsbUpdateEntry.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbUpdateEntry.Image = global::VersionMaintenance.Properties.Resources.db_update;
            this.tsbUpdateEntry.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbUpdateEntry.Name = "tsbUpdateEntry";
            this.tsbUpdateEntry.Size = new System.Drawing.Size(23, 22);
            this.tsbUpdateEntry.Text = "Update selected entry";
            this.tsbUpdateEntry.Click += new System.EventHandler(this.MnuAddUpdateAssembly_Click);
            // 
            // tsbRefreshEntries
            // 
            this.tsbRefreshEntries.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbRefreshEntries.Image = global::VersionMaintenance.Properties.Resources.view_refresh_7;
            this.tsbRefreshEntries.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbRefreshEntries.Name = "tsbRefreshEntries";
            this.tsbRefreshEntries.Size = new System.Drawing.Size(23, 22);
            this.tsbRefreshEntries.Text = "Refresh database entries";
            this.tsbRefreshEntries.Click += new System.EventHandler(this.MnuRefreshDatabaseEntries_Click);
            // 
            // tsbAddLocalizedData
            // 
            this.tsbAddLocalizedData.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbAddLocalizedData.Image = global::VersionMaintenance.Properties.Resources.education_languages;
            this.tsbAddLocalizedData.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbAddLocalizedData.Name = "tsbAddLocalizedData";
            this.tsbAddLocalizedData.Size = new System.Drawing.Size(23, 22);
            this.tsbAddLocalizedData.Text = "Add or update localized version changes";
            this.tsbAddLocalizedData.Click += new System.EventHandler(this.mnuAddLocalizedData_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tslURIDescribtion
            // 
            this.tslURIDescribtion.Name = "tslURIDescribtion";
            this.tslURIDescribtion.Size = new System.Drawing.Size(235, 22);
            this.tslURIDescribtion.Text = "The URI where the versions.php in installed:";
            // 
            // tstbLocationURI
            // 
            this.tstbLocationURI.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point);
            this.tstbLocationURI.ForeColor = System.Drawing.Color.Navy;
            this.tstbLocationURI.Name = "tstbLocationURI";
            this.tstbLocationURI.Size = new System.Drawing.Size(349, 25);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // tslAPIKey
            // 
            this.tslAPIKey.Name = "tslAPIKey";
            this.tslAPIKey.Size = new System.Drawing.Size(71, 22);
            this.tslAPIKey.Text = "The API key:";
            // 
            // tstbAPIKey
            // 
            this.tstbAPIKey.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.tstbAPIKey.ForeColor = System.Drawing.Color.IndianRed;
            this.tstbAPIKey.Name = "tstbAPIKey";
            this.tstbAPIKey.Size = new System.Drawing.Size(198, 25);
            this.tstbAPIKey.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TstbAPIKey_KeyDown);
            // 
            // tbGenerateAPIKey
            // 
            this.tbGenerateAPIKey.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbGenerateAPIKey.Image = global::VersionMaintenance.Properties.Resources.database_key;
            this.tbGenerateAPIKey.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbGenerateAPIKey.Name = "tbGenerateAPIKey";
            this.tbGenerateAPIKey.Size = new System.Drawing.Size(23, 22);
            this.tbGenerateAPIKey.Text = "Generate API key";
            this.tbGenerateAPIKey.Click += new System.EventHandler(this.TbGenerateAPIKey_Click);
            // 
            // lbTimeOutMS
            // 
            this.lbTimeOutMS.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbTimeOutMS.AutoSize = true;
            this.lbTimeOutMS.Location = new System.Drawing.Point(14, 453);
            this.lbTimeOutMS.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbTimeOutMS.Name = "lbTimeOutMS";
            this.lbTimeOutMS.Size = new System.Drawing.Size(312, 15);
            this.lbTimeOutMS.TabIndex = 6;
            this.lbTimeOutMS.Text = "Time-out in milliseconds for the HTTP(S) communication:";
            // 
            // nudTimeOutMS
            // 
            this.nudTimeOutMS.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.nudTimeOutMS.Location = new System.Drawing.Point(391, 451);
            this.nudTimeOutMS.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.nudTimeOutMS.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.nudTimeOutMS.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudTimeOutMS.Name = "nudTimeOutMS";
            this.nudTimeOutMS.Size = new System.Drawing.Size(140, 23);
            this.nudTimeOutMS.TabIndex = 7;
            this.nudTimeOutMS.Value = new decimal(new int[] {
            1500,
            0,
            0,
            0});
            // 
            // fbdDirectory
            // 
            this.fbdDirectory.Description = "Select folder for generating files";
            this.fbdDirectory.UseDescriptionForTitle = true;
            // 
            // mnuHelp
            // 
            this.mnuHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuAbout});
            this.mnuHelp.Name = "mnuHelp";
            this.mnuHelp.Size = new System.Drawing.Size(44, 20);
            this.mnuHelp.Text = "Help";
            // 
            // mnuAbout
            // 
            this.mnuAbout.Image = global::VersionMaintenance.Properties.Resources.About;
            this.mnuAbout.Name = "mnuAbout";
            this.mnuAbout.Size = new System.Drawing.Size(107, 22);
            this.mnuAbout.Text = "About";
            this.mnuAbout.Click += new System.EventHandler(this.mnuAbout_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1107, 488);
            this.Controls.Add(this.nudTimeOutMS);
            this.Controls.Add(this.lbTimeOutMS);
            this.Controls.Add(this.tsMain);
            this.Controls.Add(this.gvSoftwareVersions);
            this.Controls.Add(this.msMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.msMain;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "FormMain";
            this.Text = "Version data maintenance";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormMain_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.gvSoftwareVersions)).EndInit();
            this.msMain.ResumeLayout(false);
            this.msMain.PerformLayout();
            this.tsMain.ResumeLayout(false);
            this.tsMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudTimeOutMS)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView gvSoftwareVersions;
        private System.Windows.Forms.MenuStrip msMain;
        private System.Windows.Forms.ToolStripMenuItem mnuFile;
        private System.Windows.Forms.ToolStripMenuItem mnuAddUpdateAssembly;
        private System.Windows.Forms.OpenFileDialog odAssembly;
        private System.Windows.Forms.ToolStrip tsMain;
        private System.Windows.Forms.ToolStripButton tsbDeleteSelectedEntry;
        private System.Windows.Forms.ToolStripButton tsbUpdateEntry;
        private System.Windows.Forms.ToolStripButton tsbRefreshEntries;
        private System.Windows.Forms.ToolStripMenuItem mnuRefreshDatabaseEntries;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel tslURIDescribtion;
        private System.Windows.Forms.ToolStripTextBox tstbLocationURI;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripLabel tslAPIKey;
        private System.Windows.Forms.ToolStripTextBox tstbAPIKey;
        private System.Windows.Forms.ToolStripButton tbGenerateAPIKey;
        private System.Windows.Forms.ToolStripMenuItem mnuDeleteSelectedEntry;
        private System.Windows.Forms.ToolStripMenuItem mnuGenerateAPIKey;
        private System.Windows.Forms.Label lbTimeOutMS;
        private System.Windows.Forms.NumericUpDown nudTimeOutMS;
        private System.Windows.Forms.ToolStripMenuItem mnuTools;
        private System.Windows.Forms.ToolStripMenuItem mnuGenerateFiles;
        private Ookii.Dialogs.WinForms.VistaFolderBrowserDialog fbdDirectory;
        private System.Windows.Forms.ToolStripMenuItem mnuThisAssemblyVersion;
        private System.Windows.Forms.DataGridViewTextBoxColumn colID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colApp;
        private System.Windows.Forms.DataGridViewTextBoxColumn colVersion;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDownloadLink;
        private System.Windows.Forms.DataGridViewTextBoxColumn colReleaseDate;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colDirectDownload;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMetaData;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDownloadCount;
        private System.Windows.Forms.ToolStripButton tsbAddLocalizedData;
        private System.Windows.Forms.ToolStripMenuItem mnuAddLocalizedData;
        private System.Windows.Forms.ToolStripMenuItem mnuUpdateVersionManually;
        private System.Windows.Forms.ToolStripMenuItem mnuHelp;
        private System.Windows.Forms.ToolStripMenuItem mnuAbout;
    }
}

