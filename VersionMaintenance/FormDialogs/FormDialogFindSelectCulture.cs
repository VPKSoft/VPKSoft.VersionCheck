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
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace VersionMaintenance.FormDialogs
{
    /// <summary>
    /// A dialog form for searching a specific culture.
    /// Implements the <see cref="System.Windows.Forms.Form" />
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class FormDialogFindSelectCulture : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FormDialogFindSelectCulture"/> class.
        /// </summary>
        public FormDialogFindSelectCulture()
        {
            InitializeComponent();
            cultures.AddRange(CultureInfo.GetCultures(CultureTypes.NeutralCultures | CultureTypes.SpecificCultures));
            FilterCultures();
            cbUseNativeNames.Checked = _useNativeCultureNames;
        }

        private static bool _useNativeCultureNames;

        private readonly List<CultureInfo> cultures = new List<CultureInfo>();

        /// <summary>
        /// Filters the cultures with a given search text.
        /// </summary>
        /// <param name="searchText">The search text.</param>
        private void FilterCultures(string searchText = "")
        {
            lbCultures.Items.Clear();
            lbCultures.DisplayMember = cbUseNativeNames.Checked ? "NativeName" : "DisplayName";

            IEnumerable<CultureInfo> foundCultures = cultures;

            if (!string.IsNullOrWhiteSpace(searchText))
            {
                foundCultures =
                    foundCultures.Where(f =>
                        f.DisplayName.IndexOf(searchText, StringComparison.InvariantCultureIgnoreCase) != -1 ||
                        f.NativeName.IndexOf(searchText, StringComparison.InvariantCultureIgnoreCase) != -1 ||
                        f.EnglishName.IndexOf(searchText, StringComparison.InvariantCultureIgnoreCase) != -1 ||
                        f.Name.IndexOf(searchText, StringComparison.InvariantCultureIgnoreCase) != -1);
            }

            foreach (var culture in foundCultures)
            {
                lbCultures.Items.Add(culture);
            }
        }

        /// <summary>
        /// Shows the form as a modal dialog box with the specified owner.
        /// </summary>
        /// <param name="owner">>Any object that implements <see cref="T:System.Windows.Forms.IWin32Window" /> that represents the top-level window that will own the modal dialog box.</param>
        /// <returns>A user selected <see cref="CultureInfo"/> culture if the dialog was accepted; otherwise null.</returns>
        public new static CultureInfo ShowDialog(IWin32Window owner)
        {
            using (var form = new FormDialogFindSelectCulture())
            {
                if (((Form) form).ShowDialog(owner) == DialogResult.OK)
                {
                    return form.lbCultures.SelectedItem as CultureInfo;
                }
            }

            return null;
        }

        private void lbCultures_SelectedValueChanged(object sender, EventArgs e)
        {
            btOK.Enabled = lbCultures.SelectedItem != null;
        }

        private void tbSearchCulture_TextChanged(object sender, EventArgs e)
        {
            var textBox = (TextBox) sender;
            FilterCultures(textBox.Text);
        }

        private void cbUseNativeNames_CheckedChanged(object sender, EventArgs e)
        {
            _useNativeCultureNames = cbUseNativeNames.Checked;
            FilterCultures(tbSearchCulture.Text);
        }

        private void FormDialogFindSelectCulture_Shown(object sender, EventArgs e)
        {
            tbSearchCulture.Focus();
        }
    }
}
