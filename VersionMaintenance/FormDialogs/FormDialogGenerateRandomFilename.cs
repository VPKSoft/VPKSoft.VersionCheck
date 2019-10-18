#region License
/*
VPKSoft.VersionCheck

A version checker for VPKSoft products.
Copyright © 2019 VPKSoft, Petteri Kautonen

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

namespace VersionMaintenance.FormDialogs
{
    /// <summary>
    /// A dialog form to generate a random name for the SQLite database.
    /// Implements the <see cref="System.Windows.Forms.Form" />
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class FormDialogGenerateRandomFilename : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FormDialogGenerateRandomFilename"/> class.
        /// </summary>
        public FormDialogGenerateRandomFilename()
        {
            InitializeComponent();
            GenerateNewFilename();
        }

        // a string containing valid characters for a file name..
        private const string CharString = @"abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-_";

        // a random number generator for a new API key..
        private readonly Random random = new Random();

        private void pnGenerateNew_Click(object sender, EventArgs e)
        {
            GenerateNewFilename();
        }

        private void GenerateNewFilename()
        {
            string randomSqLiteDatabaseName = string.Empty;
            for (int i = 0; i < 15; i++)
            {
                randomSqLiteDatabaseName += CharString[random.Next(0, CharString.Length - 1)];
            }

            // ReSharper disable once StringLiteralTypo
            tbRandomFileNameValue.Text = randomSqLiteDatabaseName + @".sqlite";
        }

        private void tbRandomFileNameValue_TextChanged(object sender, EventArgs e)
        {
            btOK.Enabled = !string.IsNullOrWhiteSpace(tbRandomFileNameValue.Text);
        }

        /// <summary>
        /// Shows the form as a modal dialog box with the specified owner.
        /// </summary>
        /// <param name="owner">Any object that implements <see cref="T:System.Windows.Forms.IWin32Window" /> that represents the top-level window that will own the modal dialog box.</param>
        /// <returns>A user given or randomized file name for a SQLite database file if successful; otherwise null.</returns>
        public static string ShowDialogQueryFilename(IWin32Window owner)
        {
            var form = new FormDialogGenerateRandomFilename();
            var result = form.ShowDialog(owner);
            if (result == DialogResult.OK)
            {
                return form.tbRandomFileNameValue.Text;
            }

            return null;
        }
    }
}
