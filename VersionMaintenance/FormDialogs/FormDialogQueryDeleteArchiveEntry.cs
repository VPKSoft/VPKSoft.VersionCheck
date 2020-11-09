#region License
/*
VPKSoft.VersionCheck

A version checker for VPKSoft products.
Copyright © 2020 VPKSoft, Petteri Kautonen

Contact: vpksoft@vpksoft.net

This file is part of VPKSoft.VersionCheck.

VPKSoft.VersionCheck is free software: you can redistribute it and/or modify
it under the terms of the GNU Lesser General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

VPKSoft.VersionCheck is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU Lesser General Public License for more details.

You should have received a copy of the GNU Lesser General Public License
along with VPKSoft.VersionCheck.  If not, see <http://www.gnu.org/licenses/>.
*/
#endregion

using System;
using System.Drawing;
using System.Windows.Forms;

namespace VersionMaintenance.FormDialogs
{
    /// <summary>
    /// A custom dialog to query if a user wishes to delete a software entry from the database.
    /// Implements the <see cref="System.Windows.Forms.Form" />
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class FormDialogQueryDeleteArchiveEntry : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FormDialogQueryDeleteArchiveEntry"/> class.
        /// </summary>
        public FormDialogQueryDeleteArchiveEntry()
        {
            InitializeComponent();
            pnAcctionImage.BackgroundImage = SystemIcons.Question.ToBitmap();
        }

        /// <summary>
        /// Shows the form as a modal dialog box with the specified owner.
        /// </summary>
        /// <param name="owner">Any object that implements <see cref="T:System.Windows.Forms.IWin32Window" /> that represents the top-level window that will own the modal dialog box.</param>
        /// <param name="softwareName">Name of the software to query for deletion and for archiving.</param>
        /// <returns>A named <see cref="Tuple"/> containing the user input.</returns>
        public static (DialogResult dialogResult, bool archive, bool archiveHistory) ShowDialog(IWin32Window owner,
            string softwareName)
        {
            using (var form = new FormDialogQueryDeleteArchiveEntry {tbEntryName = {Text = softwareName}})
            {
                return (form.ShowDialog(owner), form.cbArchiveData.Checked, form.cbArchiveDataChangeHistory.Checked);
            }
        }

        private void cbArchiveData_CheckedChanged(object sender, EventArgs e)
        {
            var checkBox = (CheckBox) sender;

            cbArchiveDataChangeHistory.Checked = checkBox.Checked;
            cbArchiveDataChangeHistory.Enabled = checkBox.Checked;
        }
    }
}
