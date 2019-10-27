using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using VPKSoft.VersionCheck.UtilityClasses;
using static VPKSoft.VersionCheck.VersionCheck;

namespace VPKSoft.VersionCheck.Forms
{
    /// <summary>
    /// A dialog form to display version history for the software.
    /// Implements the <see cref="System.Windows.Forms.Form" />
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class FormDialogVersionHistory : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FormDialogVersionHistory"/> class.
        /// <param name="icon">A <see cref="Icon"/> to use with the form.</param>
        /// <param name="programName">Name of the program which version history to show.</param>
        /// </summary>
        public FormDialogVersionHistory(Icon icon, string programName)
        {
            InitializeComponent();
            Icon = icon;

            versionHistory = 
                VersionDataCache.GetVersionHistoryFromCache(programName);

            var localization = new TabDeliLocalization();
            localization.GetLocalizedTexts(Properties.Resources.version_history_localization);

            btOK.Text = localization.GetMessage("txtOK", "OK", OverrideCultureString);
            lbSelectVersion.Text = localization.GetMessage("txtSelectVersion", "Select a version:", OverrideCultureString);

            // ReSharper disable once VirtualMemberCallInConstructor
            Text = localization.GetMessage("txtCaption", "Version history for the '{0}' application", OverrideCultureString, programName);
            allVersionsText = localization.GetMessage("txtAllVersions", "All versions", OverrideCultureString);

            cmbVersionHistorySelector.Items.Add(allVersionsText);
            foreach (var value in versionHistory)
            {
                cmbVersionHistorySelector.Items.Add(value.Key.ToString());
            }

            cmbVersionHistorySelector.SelectedIndex = 0;
        }

        private readonly string allVersionsText;

        private readonly List<KeyValuePair<Version, string>> versionHistory;

        /// <summary>
        /// Shows the form as a modal dialog box.
        /// </summary>
        /// <param name="owner">Any object that implements <see cref="T:System.Windows.Forms.IWin32Window" /> that represents the top-level window that will own the modal dialog box.</param>
        /// <param name="programName">Name of the program which version history to show.</param>
        /// <param name="icon">A <see cref="Icon"/> to use with the form.</param>
        /// <returns>One of the <see cref="T:System.Windows.Forms.DialogResult"/> values.</returns>
        public static DialogResult ShowDialog(IWin32Window owner, string programName, Icon icon)
        {
            var form = new FormDialogVersionHistory(icon, programName);
            return form.ShowDialog(owner);
        }

        private void cmbVersionHistorySelector_SelectedIndexChanged(object sender, EventArgs e)
        {
            var comboBox = (ComboBox) sender;
            if (comboBox.SelectedIndex != -1)
            {
                var version = comboBox.Items[comboBox.SelectedIndex].ToString();

                string versionText = string.Empty;
                tbVersionHistory.Clear();

                if (version == allVersionsText)
                {
                    foreach (var value in versionHistory)
                    {
                        versionText +=
                            string.Join(Environment.NewLine,
                                value.Key.ToString(),
                                value.Value.Replace("\n", Environment.NewLine), "");
                    }
                }
                else
                {
                    var value = 
                        versionHistory.
                            FirstOrDefault(f => f.Key.CompareTo(new Version(version)) == 0);
                    versionText = 
                        string.Join(Environment.NewLine,
                            value.Key.ToString(),
                            value.Value.Replace("\n", Environment.NewLine), "");
                }

                tbVersionHistory.Text = versionText;
            }
        }
    }
}
