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
using System.Globalization;
using System.Reflection;
using System.Windows.Forms;
using VPKSoft.VersionCheck;
using VPKSoft.VersionCheck.Forms;
using VPKSoft.VersionCheck.UtilityClasses;

namespace AboutTest
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
            VersionCheck.AboutDialogDisplayDownloadDialog = true; // I want to make it fancy..
            VersionCheck.OverrideCultureString = CultureInfo.CurrentUICulture.Name; // I want it localized..
            //VersionCheck.OverrideCultureString = "fi";
            VersionCheck.CacheUpdateHistory = true;
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // ReSharper disable once ObjectCreationAsStatement
            // ReSharper disable once StringLiteralTypo

            using (new FormAbout(this, Assembly.GetEntryAssembly(), "LGPL",
                "http://www.gnu.org/licenses/gpl-3.0.txt", "http://192.168.1.131/admin/ServerSideBase/version.php",
                3000))
            {
                // to dispose of the IDisposable..
            }
        }

        private void MnuCheckNewVersion_Click(object sender, EventArgs e)
        {
            FormCheckVersion.CheckForNewVersion("http://DESKTOP-HFNM7V1/version_check/version.php",
                Assembly.GetEntryAssembly());
        }
    }
}
