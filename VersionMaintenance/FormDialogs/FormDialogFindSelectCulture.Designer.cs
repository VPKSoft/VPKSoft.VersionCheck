namespace VersionMaintenance.FormDialogs
{
    partial class FormDialogFindSelectCulture
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormDialogFindSelectCulture));
            this.btOK = new System.Windows.Forms.Button();
            this.btCancel = new System.Windows.Forms.Button();
            this.lbSearchCulture = new System.Windows.Forms.Label();
            this.tbSearchCulture = new System.Windows.Forms.TextBox();
            this.lbCultures = new System.Windows.Forms.ListBox();
            this.cbUseNativeNames = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // btOK
            // 
            this.btOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btOK.Enabled = false;
            this.btOK.Location = new System.Drawing.Point(12, 290);
            this.btOK.Name = "btOK";
            this.btOK.Size = new System.Drawing.Size(75, 23);
            this.btOK.TabIndex = 0;
            this.btOK.Text = "OK";
            this.btOK.UseVisualStyleBackColor = true;
            // 
            // btCancel
            // 
            this.btCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btCancel.Location = new System.Drawing.Point(232, 290);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(75, 23);
            this.btCancel.TabIndex = 1;
            this.btCancel.Text = "Cancel";
            this.btCancel.UseVisualStyleBackColor = true;
            // 
            // lbSearchCulture
            // 
            this.lbSearchCulture.AutoSize = true;
            this.lbSearchCulture.Location = new System.Drawing.Point(9, 15);
            this.lbSearchCulture.Name = "lbSearchCulture";
            this.lbSearchCulture.Size = new System.Drawing.Size(41, 13);
            this.lbSearchCulture.TabIndex = 2;
            this.lbSearchCulture.Text = "Search";
            // 
            // tbSearchCulture
            // 
            this.tbSearchCulture.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbSearchCulture.Location = new System.Drawing.Point(84, 12);
            this.tbSearchCulture.Name = "tbSearchCulture";
            this.tbSearchCulture.Size = new System.Drawing.Size(223, 20);
            this.tbSearchCulture.TabIndex = 3;
            this.tbSearchCulture.TextChanged += new System.EventHandler(this.tbSearchCulture_TextChanged);
            // 
            // lbCultures
            // 
            this.lbCultures.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbCultures.FormattingEnabled = true;
            this.lbCultures.IntegralHeight = false;
            this.lbCultures.Location = new System.Drawing.Point(12, 61);
            this.lbCultures.Name = "lbCultures";
            this.lbCultures.Size = new System.Drawing.Size(295, 223);
            this.lbCultures.TabIndex = 5;
            this.lbCultures.SelectedValueChanged += new System.EventHandler(this.lbCultures_SelectedValueChanged);
            // 
            // cbUseNativeNames
            // 
            this.cbUseNativeNames.AutoSize = true;
            this.cbUseNativeNames.Location = new System.Drawing.Point(12, 38);
            this.cbUseNativeNames.Name = "cbUseNativeNames";
            this.cbUseNativeNames.Size = new System.Drawing.Size(146, 17);
            this.cbUseNativeNames.TabIndex = 6;
            this.cbUseNativeNames.Text = "Use native culture names";
            this.cbUseNativeNames.UseVisualStyleBackColor = true;
            this.cbUseNativeNames.CheckedChanged += new System.EventHandler(this.cbUseNativeNames_CheckedChanged);
            // 
            // FormDialogFindSelectCulture
            // 
            this.AcceptButton = this.btOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btCancel;
            this.ClientSize = new System.Drawing.Size(319, 325);
            this.Controls.Add(this.cbUseNativeNames);
            this.Controls.Add(this.lbCultures);
            this.Controls.Add(this.tbSearchCulture);
            this.Controls.Add(this.lbSearchCulture);
            this.Controls.Add(this.btCancel);
            this.Controls.Add(this.btOK);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormDialogFindSelectCulture";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Select a culture";
            this.Shown += new System.EventHandler(this.FormDialogFindSelectCulture_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btOK;
        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.Label lbSearchCulture;
        private System.Windows.Forms.TextBox tbSearchCulture;
        private System.Windows.Forms.ListBox lbCultures;
        private System.Windows.Forms.CheckBox cbUseNativeNames;
    }
}