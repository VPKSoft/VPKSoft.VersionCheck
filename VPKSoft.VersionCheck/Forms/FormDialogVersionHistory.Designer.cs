namespace VPKSoft.VersionCheck.Forms
{
    partial class FormDialogVersionHistory
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
            this.cmbVersionHistorySelector = new System.Windows.Forms.ComboBox();
            this.lbSelectVersion = new System.Windows.Forms.Label();
            this.tbVersionHistory = new System.Windows.Forms.TextBox();
            this.btOK = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cmbVersionHistorySelector
            // 
            this.cmbVersionHistorySelector.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbVersionHistorySelector.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbVersionHistorySelector.FormattingEnabled = true;
            this.cmbVersionHistorySelector.Location = new System.Drawing.Point(124, 12);
            this.cmbVersionHistorySelector.Name = "cmbVersionHistorySelector";
            this.cmbVersionHistorySelector.Size = new System.Drawing.Size(325, 21);
            this.cmbVersionHistorySelector.TabIndex = 0;
            this.cmbVersionHistorySelector.SelectedIndexChanged += new System.EventHandler(this.cmbVersionHistorySelector_SelectedIndexChanged);
            // 
            // lbSelectVersion
            // 
            this.lbSelectVersion.AutoSize = true;
            this.lbSelectVersion.Location = new System.Drawing.Point(12, 15);
            this.lbSelectVersion.Name = "lbSelectVersion";
            this.lbSelectVersion.Size = new System.Drawing.Size(86, 13);
            this.lbSelectVersion.TabIndex = 1;
            this.lbSelectVersion.Text = "Select a version:";
            // 
            // tbVersionHistory
            // 
            this.tbVersionHistory.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbVersionHistory.Location = new System.Drawing.Point(12, 39);
            this.tbVersionHistory.Multiline = true;
            this.tbVersionHistory.Name = "tbVersionHistory";
            this.tbVersionHistory.ReadOnly = true;
            this.tbVersionHistory.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbVersionHistory.Size = new System.Drawing.Size(437, 212);
            this.tbVersionHistory.TabIndex = 2;
            this.tbVersionHistory.WordWrap = false;
            // 
            // btOK
            // 
            this.btOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btOK.Image = global::VPKSoft.VersionCheck.Properties.Resources.OK;
            this.btOK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btOK.Location = new System.Drawing.Point(363, 257);
            this.btOK.Name = "btOK";
            this.btOK.Size = new System.Drawing.Size(86, 26);
            this.btOK.TabIndex = 12;
            this.btOK.Text = "OK";
            this.btOK.UseVisualStyleBackColor = true;
            // 
            // FormDialogVersionHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(461, 295);
            this.Controls.Add(this.btOK);
            this.Controls.Add(this.tbVersionHistory);
            this.Controls.Add(this.lbSelectVersion);
            this.Controls.Add(this.cmbVersionHistorySelector);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormDialogVersionHistory";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Version history for the \'\' application";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbVersionHistorySelector;
        private System.Windows.Forms.Label lbSelectVersion;
        private System.Windows.Forms.TextBox tbVersionHistory;
        private System.Windows.Forms.Button btOK;
    }
}