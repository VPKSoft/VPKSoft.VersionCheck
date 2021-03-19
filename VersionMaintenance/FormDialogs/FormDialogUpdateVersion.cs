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
using System.Windows.Forms;
using VersionMaintenance.Annotations;

namespace VersionMaintenance.FormDialogs
{
    /// <summary>
    /// A form to query the user for a version number.
    /// Implements the <see cref="System.Windows.Forms.Form" />
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class FormDialogUpdateVersion : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FormDialogUpdateVersion"/> class.
        /// </summary>
        public FormDialogUpdateVersion()
        {
            InitializeComponent();
        }

        private void tbVersion_TextChanged(object sender, System.EventArgs e)
        {
            btOK.Enabled = Version.TryParse(tbVersion.Text, out _);
        }

        /// <summary>
        /// <summary>Shows the form as a modal dialog box.</summary>
        /// </summary>
        /// <returns>A version if user was able to provide a valid version; null otherwise.</returns>
        [CanBeNull]
        public new static Version ShowDialog()
        {
            var form = new FormDialogUpdateVersion();
            if (form.ShowDialog(null) == DialogResult.OK)
            {
                return  Version.Parse(form.tbVersion.Text);
            }

            return null;
        }
    }
}
