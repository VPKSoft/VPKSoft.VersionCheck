using System;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace VPKSoft.VersionCheck
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
                VersionCheck.CheckUri = versionCheckUri;
                var version = VersionCheck.GetVersion(assembly);

                // null validation before usage..
                if (version != null && version.IsLargerVersion(assembly))
                {
                    FormCheckVersion checkVersion = new FormCheckVersion();
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

                    checkVersion.tbReleaseNotes.Text = version.MetaData.Replace("* ", "• ").Replace("*", "•");

                    checkVersion.btUpdate.Text =
                        localization.GetMessage("txtUpdateSoftware", "Update the software", localeString);

                    checkVersion.btCancel.Text = localization.GetMessage("txtCancel", "Cancel", localeString);

                    if (checkVersion.ShowDialog() == DialogResult.OK)
                    {
                        if (version.IsDirectDownload)
                        {
                            string tempPath = Path.GetTempPath();
                            if (VersionCheck.DownloadFile(version.DownloadLink, tempPath))
                            {
                                try
                                {
                                    VersionCheck.IncreaseDownloadCount(version.SoftwareName);

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

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
