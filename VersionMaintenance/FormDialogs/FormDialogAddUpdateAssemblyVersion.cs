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
using System.Reflection;
using System.Windows.Forms;
using VPKSoft.VersionCheck;
using VPKSoft.VersionCheck.APIResponseClasses;

namespace VersionMaintenance.FormDialogs
{
    /// <summary>
    /// A form to set assembly parameters for either update or to a new assembly.
    /// Implements the <see cref="System.Windows.Forms.Form" />
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class FormDialogAddUpdateAssemblyVersion : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FormDialogAddUpdateAssemblyVersion"/> class.
        /// </summary>
        public FormDialogAddUpdateAssemblyVersion()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Shows the dialog with the assembly data.
        /// </summary>
        /// <param name="fileName">Name of the file from which to get the base release date from.</param>
        /// <param name="assembly">The assembly to get the information from.</param>
        /// <param name="archivePreviousEntry">A value indicating if the user wishes to archive the previous <see cref="VersionInfo"/> entry.</param>
        /// <param name="usePreviousLocalizedData">A value indicating whether to use previous localized version data as a base for the new version.</param>
        /// <returns>An instance to <see cref="VersionInfo"/> class if the operation was successful; otherwise null.</returns>
        public static VersionInfo ShowDialog(string fileName, Assembly assembly, out bool archivePreviousEntry,
            out bool usePreviousLocalizedData)
        {
            int applicationId = -1;

            var info = VersionCheck.GetVersion(assembly);
            if (info == null)
            {
                info = VersionInfo.FromAssembly(fileName, assembly);
            }
            else
            {
                info.SoftwareVersion = assembly.GetName().Version.ToString();
                applicationId = info.ID;
            }

            archivePreviousEntry = false;
            usePreviousLocalizedData = false;
            var form = new FormDialogAddUpdateAssemblyVersion
            {
                cbDirectDownload = {Checked = info.IsDirectDownload},
                tbSoftwareName = {Text = info.SoftwareName},
                tbSoftwareVersion = {Text = info.SoftwareVersion},
                tbDownloadLink = {Text = info.DownloadLink},
                tbMetaData = {Text = info.MetaData},
                dtpReleaseDate = {Value = info.ReleaseDate},
                dtpReleaseTime = {Value = info.ReleaseDate},
                cbArchivePreviousVersion = { Enabled = applicationId != -1, Checked = applicationId != -1},
                cbPreservePreviousVersionData = { Enabled = applicationId != -1, Checked = applicationId != -1},
            };


            if (form.ShowDialog() == DialogResult.OK)
            {
                info.MetaData = form.tbMetaData.Text;
                var dt1 = form.dtpReleaseDate.Value;
                var dt2 = form.dtpReleaseTime.Value;
                info.IsDirectDownload = form.cbDirectDownload.Checked;
                info.DownloadLink = form.tbDownloadLink.Text;
                info.ReleaseDate = new DateTime(dt1.Year, dt1.Month, dt1.Day, dt2.Hour, dt2.Minute, dt2.Second, DateTimeKind.Utc);
                if (form.cbArchivePreviousVersion.Checked)
                {
                    archivePreviousEntry = applicationId != -1;
                }
                if (form.cbPreservePreviousVersionData.Checked)
                {
                    usePreviousLocalizedData = applicationId != -1;
                }
                return info;
            }
            return null;
        }

        private void BtToUTC_Click(object sender, EventArgs e)
        {
            DateTime dt = new DateTime(dtpReleaseDate.Value.Year, dtpReleaseDate.Value.Month, dtpReleaseDate.Value.Day,
                dtpReleaseTime.Value.Hour, dtpReleaseTime.Value.Minute, 0);
            dtpReleaseDate.Value = dt.ToUniversalTime();
            dtpReleaseTime.Value = dt.ToUniversalTime();
        }

        private void LbReleaseDateTime_Click(object sender, EventArgs e)
        {
            DateTime dt = DateTime.UtcNow;
            dt = dt.AddSeconds(-dt.Second);
            dtpReleaseDate.Value = dt;
            dtpReleaseTime.Value = dt;
        }
    }
}
