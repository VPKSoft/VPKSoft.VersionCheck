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
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using VPKSoft.VersionCheck.UtilityClasses;
using static VPKSoft.VersionCheck.VersionCheck;

namespace VPKSoft.VersionCheck.Forms
{
    /// <summary>
    /// A form to query the user whether the user wants to download a new version of the software.
    /// Implements the <see cref="System.Windows.Forms.Form" />
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class FormCheckVersion : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FormCheckVersion"/> class.
        /// </summary>
        public FormCheckVersion()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Checks for new version of an assembly and if a new version exists a dialog with details is shown.
        /// </summary>
        /// <param name="versionCheckUri">The version check URI.</param>
        /// <param name="assembly">The assembly of which updates to check.</param>
        /// <param name="cultureInfo">The culture information for localization.</param>
        /// <returns><c>true</c> if a new version exists and the user decided to download it, <c>false</c> otherwise.</returns>
        public static bool CheckForNewVersion(string versionCheckUri, Assembly assembly, CultureInfo cultureInfo)
        {
            return CheckForNewVersion(versionCheckUri, assembly, cultureInfo.Name);
        }

        /// <summary>
        /// Checks for new version of an assembly and if a new version exists a dialog with details is shown. This uses the <see cref="VPKSoft.VersionCheck.VersionCheck.OverrideCultureString"/> to be used for localization.
        /// </summary>
        /// <param name="versionCheckUri">The version check URI.</param>
        /// <param name="assembly">The assembly of which updates to check.</param>
        /// <returns><c>true</c> if a new version exists and the user decided to download it, <c>false</c> otherwise.</returns>
        public static bool CheckForNewVersion(string versionCheckUri, Assembly assembly)
        {
            return CheckForNewVersion(versionCheckUri, assembly, OverrideCultureString);
        }

        /// <summary>
        /// Checks for new version of an assembly and if a new version exists a dialog with details is shown.
        /// </summary>
        /// <param name="versionCheckUri">The version check URI.</param>
        /// <param name="assembly">The assembly of which updates to check.</param>
        /// <param name="localeString">The culture name in the format languagecode2-country/regioncode2. languagecode2 is a lowercase two-letter code derived from ISO 639-1. country/regioncode2 is derived from ISO 3166 and usually consists of two uppercase letters, or a BCP-47 language tag.</param>
        /// <returns><c>true</c> if a new version exists and the user decided to download it, <c>false</c> otherwise.</returns>
        public static bool CheckForNewVersion(string versionCheckUri, Assembly assembly, string localeString)
        {
            try
            {
                CheckUri = versionCheckUri;
                var version = GetVersion(assembly, localeString);

                // null validation before usage..
                if (version != null && version.IsLargerVersion(assembly))
                {
                    using (FormCheckVersion checkVersion = new FormCheckVersion())
                    {
                        try
                        {
                            checkVersion.Icon = Icon.ExtractAssociatedIcon(assembly.Location);
                            checkVersion.pbLargerIcon.Image = Icon.ExtractAssociatedIcon(assembly.Location)?.ToBitmap();
                        }
                        catch
                        {
                            // ignored..
                        }

                        var localization = new TabDeliLocalization();
                        localization.GetLocalizedTexts(Properties.Resources.tab_deli_localization);

                        checkVersion.Text = string.Format(localization.GetMessage("txtNewVersionTitle",
                            "A new version of the '{0}' software is available", localeString), version.SoftwareName);

                        checkVersion.lbCurrentVersion.Text = localization.GetMessage("txtCurrentVersion",
                            "Current version:", localeString);

                        checkVersion.tbCurrentVersion.Text = @"v." + assembly.GetName().Version;

                        checkVersion.lbNewVersion.Text = localization.GetMessage("txtNewVersion",
                            "New version:", localeString);

                        checkVersion.tbNewVersion.Text = @"v." + version.SoftwareVersion;

                        checkVersion.lbReleaseDateTime.Text = localization.GetMessage("txtReleaseDateTime",
                            "Release date/time:", localeString);

                        checkVersion.tbReleaseDateTime.Text = version.ReleaseDate.ToLocalTime().ToString("G");

                        checkVersion.lbReleaseNotes.Text = localization.GetMessage("txtReleaseChanges",
                            "Release changes:", localeString);

                        checkVersion.tbReleaseNotes.Text =
                            version.MetaDataLocalized.Replace("* ", "• ").Replace("*", "•");

                        if (!string.IsNullOrEmpty(version.MetaDataLocalized))
                        {
                            checkVersion.tbReleaseNotes.Text =
                                version.MetaDataLocalized.Replace("* ", "• ").Replace("*", "•");
                        }

                        checkVersion.btUpdate.Text =
                            localization.GetMessage("txtUpdateSoftware", "Update the software", localeString);

                        checkVersion.btCancel.Text = localization.GetMessage("txtCancel", "Cancel", localeString);

                        if (checkVersion.ShowDialog() == DialogResult.OK)
                        {
                            if (version.IsDirectDownload)
                            {
                                string tempPath = Path.GetTempPath();
                                if (DownloadFile(version.DownloadLink, tempPath))
                                {
                                    try
                                    {
                                        IncreaseDownloadCount(version.SoftwareName);

                                        Process.Start(Path.Combine(tempPath,
                                            Path.GetFileName(new Uri(version.DownloadLink).LocalPath)));

                                        return true;
                                    }
                                    catch
                                    {
                                        return false;
                                    }
                                }
                            }
                            else
                            {
                                try
                                {
                                    Process.Start(version.DownloadLink);
                                    return true;
                                }
                                catch
                                {
                                    return false;
                                }
                            }
                        }
                    }
                }

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
