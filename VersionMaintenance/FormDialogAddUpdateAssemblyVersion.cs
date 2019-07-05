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
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using VPKSoft.VersionCheck;

namespace VersionMaintenance
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
        /// <returns>An instance to <see cref="VersionCheck.VersionInfo"/> class if the operation was successful; otherwise null.</returns>
        public static VersionCheck.VersionInfo ShowDialog(string fileName, Assembly assembly)
        {
            var info = VersionCheck.GetVersion(assembly);
            if (info == null)
            {
                info = VersionCheck.VersionInfo.FromAssembly(fileName, assembly);
            }
            else
            {
                info.SoftwareVersion = assembly.GetName().Version.ToString();
                info.ReleaseDate = new FileInfo(fileName).LastWriteTimeUtc;
            }

            var form = new FormDialogAddUpdateAssemblyVersion
            {
                cbDirectDownload = {Checked = info.IsDirectDownload},
                tbSoftwareName = {Text = info.SoftwareName},
                tbSoftwareVersion = {Text = info.SoftwareVersion},
                tbDownloadLink = {Text = info.DownloadLink},
                tbMetaData = {Text = info.MetaData},
                dtpReleaseDate = {Value = info.ReleaseDate},
                dtpReleaseTime = {Value = info.ReleaseDate}
            };


            if (form.ShowDialog() == DialogResult.OK)
            {
                info.MetaData = form.tbMetaData.Text;
                var dt1 = form.dtpReleaseDate.Value;
                var dt2 = form.dtpReleaseTime.Value;
                info.IsDirectDownload = form.cbDirectDownload.Checked;
                info.ReleaseDate = new DateTime(dt1.Year, dt1.Month, dt1.Day, dt2.Hour, dt2.Minute, dt2.Second, DateTimeKind.Utc);
                return info;
            }

            return null;
        }
    }
}
