namespace VersionMaintenance.FormDialogs
{
    partial class FormDialogGenerateRandomFilename
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormDialogGenerateRandomFilename));
            this.lbRandomFileNameDescription = new System.Windows.Forms.Label();
            this.tbRandomFileNameValue = new System.Windows.Forms.TextBox();
            this.pnGenerateNew = new System.Windows.Forms.Panel();
            this.btCancel = new System.Windows.Forms.Button();
            this.btOK = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lbRandomFileNameDescription
            // 
            this.lbRandomFileNameDescription.AutoSize = true;
            this.lbRandomFileNameDescription.Location = new System.Drawing.Point(12, 15);
            this.lbRandomFileNameDescription.Name = "lbRandomFileNameDescription";
            this.lbRandomFileNameDescription.Size = new System.Drawing.Size(146, 13);
            this.lbRandomFileNameDescription.TabIndex = 0;
            this.lbRandomFileNameDescription.Text = "Generate a random file name:";
            // 
            // tbRandomFileNameValue
            // 
            this.tbRandomFileNameValue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbRandomFileNameValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbRandomFileNameValue.ForeColor = System.Drawing.Color.IndianRed;
            this.tbRandomFileNameValue.Location = new System.Drawing.Point(173, 12);
            this.tbRandomFileNameValue.Name = "tbRandomFileNameValue";
            this.tbRandomFileNameValue.Size = new System.Drawing.Size(420, 20);
            this.tbRandomFileNameValue.TabIndex = 1;
            this.tbRandomFileNameValue.TextChanged += new System.EventHandler(this.tbRandomFileNameValue_TextChanged);
            // 
            // pnGenerateNew
            // 
            this.pnGenerateNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pnGenerateNew.BackgroundImage = global::VersionMaintenance.Properties.Resources.database_key;
            this.pnGenerateNew.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pnGenerateNew.Location = new System.Drawing.Point(596, 12);
            this.pnGenerateNew.Margin = new System.Windows.Forms.Padding(0);
            this.pnGenerateNew.Name = "pnGenerateNew";
            this.pnGenerateNew.Size = new System.Drawing.Size(20, 20);
            this.pnGenerateNew.TabIndex = 2;
            this.pnGenerateNew.Click += new System.EventHandler(this.pnGenerateNew_Click);
            // 
            // btCancel
            // 
            this.btCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btCancel.Location = new System.Drawing.Point(541, 38);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(75, 23);
            this.btCancel.TabIndex = 12;
            this.btCancel.Text = "Cancel";
            this.btCancel.UseVisualStyleBackColor = true;
            // 
            // btOK
            // 
            this.btOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btOK.Location = new System.Drawing.Point(12, 38);
            this.btOK.Name = "btOK";
            this.btOK.Size = new System.Drawing.Size(75, 23);
            this.btOK.TabIndex = 11;
            this.btOK.Text = "OK";
            this.btOK.UseVisualStyleBackColor = true;
            // 
            // FormDialogGenerateRandomFilename
            // 
            this.AcceptButton = this.btOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btCancel;
            this.ClientSize = new System.Drawing.Size(628, 73);
            this.Controls.Add(this.btCancel);
            this.Controls.Add(this.btOK);
            this.Controls.Add(this.pnGenerateNew);
            this.Controls.Add(this.tbRandomFileNameValue);
            this.Controls.Add(this.lbRandomFileNameDescription);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormDialogGenerateRandomFilename";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Generate a random file name";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbRandomFileNameDescription;
        private System.Windows.Forms.TextBox tbRandomFileNameValue;
        private System.Windows.Forms.Panel pnGenerateNew;
        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.Button btOK;
    }
}