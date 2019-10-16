namespace VersionMaintenance
{
    partial class FormDialogAddUpdateVersionChangeTextLocalized
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormDialogAddUpdateVersionChangeTextLocalized));
            this.lbLocale = new System.Windows.Forms.Label();
            this.cmbLocale = new System.Windows.Forms.ComboBox();
            this.cbUseNativeNames = new System.Windows.Forms.CheckBox();
            this.lbCultureISOValue = new System.Windows.Forms.Label();
            this.lbCultureISODesc = new System.Windows.Forms.Label();
            this.lbSoftwareChangeHistoryName = new System.Windows.Forms.Label();
            this.tbSoftwareChangeHistoryName = new System.Windows.Forms.TextBox();
            this.tbVersionValue = new System.Windows.Forms.TextBox();
            this.lbVersionDescription = new System.Windows.Forms.Label();
            this.btOK = new System.Windows.Forms.Button();
            this.btCancel = new System.Windows.Forms.Button();
            this.lbChangeDescription = new System.Windows.Forms.Label();
            this.tbChangesDescription = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // lbLocale
            // 
            this.lbLocale.AutoSize = true;
            this.lbLocale.Location = new System.Drawing.Point(12, 67);
            this.lbLocale.Name = "lbLocale";
            this.lbLocale.Size = new System.Drawing.Size(43, 13);
            this.lbLocale.TabIndex = 0;
            this.lbLocale.Text = "Culture:";
            // 
            // cmbLocale
            // 
            this.cmbLocale.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbLocale.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cmbLocale.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbLocale.FormattingEnabled = true;
            this.cmbLocale.Location = new System.Drawing.Point(82, 64);
            this.cmbLocale.Name = "cmbLocale";
            this.cmbLocale.Size = new System.Drawing.Size(504, 21);
            this.cmbLocale.TabIndex = 1;
            this.cmbLocale.SelectedIndexChanged += new System.EventHandler(this.cmbLocale_SelectedIndexChanged);
            // 
            // cbUseNativeNames
            // 
            this.cbUseNativeNames.AutoSize = true;
            this.cbUseNativeNames.Location = new System.Drawing.Point(12, 93);
            this.cbUseNativeNames.Name = "cbUseNativeNames";
            this.cbUseNativeNames.Size = new System.Drawing.Size(146, 17);
            this.cbUseNativeNames.TabIndex = 2;
            this.cbUseNativeNames.Text = "Use native culture names";
            this.cbUseNativeNames.UseVisualStyleBackColor = true;
            this.cbUseNativeNames.CheckedChanged += new System.EventHandler(this.cbUseNativeNames_CheckedChanged);
            // 
            // lbCultureISOValue
            // 
            this.lbCultureISOValue.AutoSize = true;
            this.lbCultureISOValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbCultureISOValue.ForeColor = System.Drawing.Color.IndianRed;
            this.lbCultureISOValue.Location = new System.Drawing.Point(492, 94);
            this.lbCultureISOValue.Name = "lbCultureISOValue";
            this.lbCultureISOValue.Size = new System.Drawing.Size(42, 13);
            this.lbCultureISOValue.TabIndex = 3;
            this.lbCultureISOValue.Text = "en-US";
            // 
            // lbCultureISODesc
            // 
            this.lbCultureISODesc.AutoSize = true;
            this.lbCultureISODesc.Location = new System.Drawing.Point(372, 94);
            this.lbCultureISODesc.Name = "lbCultureISODesc";
            this.lbCultureISODesc.Size = new System.Drawing.Size(114, 13);
            this.lbCultureISODesc.TabIndex = 4;
            this.lbCultureISODesc.Text = "Selected culture (ISO):";
            // 
            // lbSoftwareChangeHistoryName
            // 
            this.lbSoftwareChangeHistoryName.AutoSize = true;
            this.lbSoftwareChangeHistoryName.Location = new System.Drawing.Point(12, 15);
            this.lbSoftwareChangeHistoryName.Name = "lbSoftwareChangeHistoryName";
            this.lbSoftwareChangeHistoryName.Size = new System.Drawing.Size(156, 13);
            this.lbSoftwareChangeHistoryName.TabIndex = 5;
            this.lbSoftwareChangeHistoryName.Text = "Change history for the software:";
            // 
            // tbSoftwareChangeHistoryName
            // 
            this.tbSoftwareChangeHistoryName.Location = new System.Drawing.Point(174, 12);
            this.tbSoftwareChangeHistoryName.Name = "tbSoftwareChangeHistoryName";
            this.tbSoftwareChangeHistoryName.ReadOnly = true;
            this.tbSoftwareChangeHistoryName.Size = new System.Drawing.Size(412, 20);
            this.tbSoftwareChangeHistoryName.TabIndex = 6;
            // 
            // tbVersionValue
            // 
            this.tbVersionValue.Location = new System.Drawing.Point(174, 38);
            this.tbVersionValue.Name = "tbVersionValue";
            this.tbVersionValue.ReadOnly = true;
            this.tbVersionValue.Size = new System.Drawing.Size(215, 20);
            this.tbVersionValue.TabIndex = 7;
            // 
            // lbVersionDescription
            // 
            this.lbVersionDescription.AutoSize = true;
            this.lbVersionDescription.Location = new System.Drawing.Point(12, 41);
            this.lbVersionDescription.Name = "lbVersionDescription";
            this.lbVersionDescription.Size = new System.Drawing.Size(45, 13);
            this.lbVersionDescription.TabIndex = 8;
            this.lbVersionDescription.Text = "Version:";
            // 
            // btOK
            // 
            this.btOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btOK.Location = new System.Drawing.Point(12, 344);
            this.btOK.Name = "btOK";
            this.btOK.Size = new System.Drawing.Size(75, 23);
            this.btOK.TabIndex = 9;
            this.btOK.Text = "OK";
            this.btOK.UseVisualStyleBackColor = true;
            // 
            // btCancel
            // 
            this.btCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btCancel.Location = new System.Drawing.Point(511, 344);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(75, 23);
            this.btCancel.TabIndex = 10;
            this.btCancel.Text = "Cancel";
            this.btCancel.UseVisualStyleBackColor = true;
            // 
            // lbChangeDescription
            // 
            this.lbChangeDescription.AutoSize = true;
            this.lbChangeDescription.Location = new System.Drawing.Point(12, 122);
            this.lbChangeDescription.Name = "lbChangeDescription";
            this.lbChangeDescription.Size = new System.Drawing.Size(151, 13);
            this.lbChangeDescription.TabIndex = 11;
            this.lbChangeDescription.Text = "Describe the version changes:";
            // 
            // tbChangesDescription
            // 
            this.tbChangesDescription.AcceptsReturn = true;
            this.tbChangesDescription.AcceptsTab = true;
            this.tbChangesDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbChangesDescription.Location = new System.Drawing.Point(12, 138);
            this.tbChangesDescription.Multiline = true;
            this.tbChangesDescription.Name = "tbChangesDescription";
            this.tbChangesDescription.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbChangesDescription.Size = new System.Drawing.Size(574, 200);
            this.tbChangesDescription.TabIndex = 12;
            this.tbChangesDescription.WordWrap = false;
            this.tbChangesDescription.TextChanged += new System.EventHandler(this.tbChangesDescription_TextChanged);
            // 
            // FormDialogAddUpdateVersionChangeTextLocalized
            // 
            this.AcceptButton = this.btOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btCancel;
            this.ClientSize = new System.Drawing.Size(598, 379);
            this.Controls.Add(this.tbChangesDescription);
            this.Controls.Add(this.lbChangeDescription);
            this.Controls.Add(this.btCancel);
            this.Controls.Add(this.btOK);
            this.Controls.Add(this.lbVersionDescription);
            this.Controls.Add(this.tbVersionValue);
            this.Controls.Add(this.tbSoftwareChangeHistoryName);
            this.Controls.Add(this.lbSoftwareChangeHistoryName);
            this.Controls.Add(this.lbCultureISODesc);
            this.Controls.Add(this.lbCultureISOValue);
            this.Controls.Add(this.cbUseNativeNames);
            this.Controls.Add(this.cmbLocale);
            this.Controls.Add(this.lbLocale);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormDialogAddUpdateVersionChangeTextLocalized";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Add or update localized version changes";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbLocale;
        private System.Windows.Forms.ComboBox cmbLocale;
        private System.Windows.Forms.CheckBox cbUseNativeNames;
        private System.Windows.Forms.Label lbCultureISOValue;
        private System.Windows.Forms.Label lbCultureISODesc;
        private System.Windows.Forms.Label lbSoftwareChangeHistoryName;
        private System.Windows.Forms.TextBox tbSoftwareChangeHistoryName;
        private System.Windows.Forms.TextBox tbVersionValue;
        private System.Windows.Forms.Label lbVersionDescription;
        private System.Windows.Forms.Button btOK;
        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.Label lbChangeDescription;
        private System.Windows.Forms.TextBox tbChangesDescription;
    }
}