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
using System.Data.SQLite;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using VersionMaintenance.FormDialogs;
using VPKSoft.VersionCheck;

namespace VersionMaintenance
{
    /// <summary>
    /// The main form of the maintenance application.
    /// Implements the <see cref="System.Windows.Forms.Form" />
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class FormMain : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FormMain"/> class.
        /// </summary>
        public FormMain()
        {
            InitializeComponent();
            settings = new Settings();

            if (Debugger.IsAttached && !overrideDebugCheck) // different settings when debugging..
            {
                VersionCheck.ApiKey = settings.ApiKeyTest; // a random string and a random file on the server...
                VersionCheck.CheckUri = settings.CheckUriTest;
            }
            else
            {
                VersionCheck.ApiKey = settings.ApiKey; // a random string and a random file on the server...
                VersionCheck.CheckUri = settings.CheckUri;
            }
            VersionCheck.TimeOutMs = settings.TimeOutMs;

            tstbLocationURI.Text = VersionCheck.CheckUri;
            tstbAPIKey.Text = VersionCheck.ApiKey;
            nudTimeOutMS.Value = settings.TimeOutMs;

            // update the database to the newest version..
            VersionCheck.UpdateDatabase();

            // the application dead-locks if the "self-assembly" check is made in the debug mode..
            mnuThisAssemblyVersion.Enabled = !Debugger.IsAttached;
            mnuThisAssemblyVersion.Visible = !Debugger.IsAttached;

            ListVersions();
        }

        // settings for this piece of software..
        private readonly Settings settings;

        // toggle this to override the Debugger.IsAttached check..
        private static readonly bool overrideDebugCheck = false;

        /// <summary>
        /// Lists the versions in a remote SQLite database if one exists and is accessible.
        /// </summary>
        private void ListVersions()
        {
            gvSoftwareVersions.Rows.Clear();
            var versions = VersionCheck.GetSoftwareVersions();

            // avoid an exception..
            if (versions == null)
            {
                return;
            }

            // loop through the versions..
            foreach (var versionInfo in versions)
            {
                gvSoftwareVersions.Rows.Add(versionInfo.ID,
                    versionInfo.SoftwareName,
                    versionInfo.SoftwareVersion,
                    versionInfo.DownloadLink,
                    versionInfo.ReleaseDate,
                    versionInfo.IsDirectDownload,
                    versionInfo.MetaData,
                    versionInfo.DownloadCount);
            }
        }

        // a user wants to add or update a version information entry within the remote database..
        private void MnuAddUpdateAssembly_Click(object sender, EventArgs e)
        {
            Assembly assembly = null;
            if ((sender.Equals(mnuAddUpdateAssembly) || sender.Equals(tsbUpdateEntry)) 
                && odAssembly.ShowDialog() == DialogResult.OK)
            {
                assembly = Assembly.LoadFile(odAssembly.FileName);
            }
            else if (sender.Equals(mnuThisAssemblyVersion))
            {
                // ..just use the entry assembly..
                assembly = Assembly.GetEntryAssembly();
            }

            if (assembly != null)
            {
                var info = FormDialogAddUpdateAssemblyVersion.ShowDialog(assembly.Location, assembly,
                    out bool archivePreviousEntry, out bool previousLocalizedData);

                var version = gvSoftwareVersions.CurrentRow?.Cells[colVersion.Index].Value.ToString();

                if (info != null)
                {
                    if (info.ID != -1)
                    {
                        if (archivePreviousEntry)
                        {
                            VersionCheck.ArchiveVersion(info.ID);
                            if (previousLocalizedData && version != null)
                            {
                                VersionCheck.PreservePreviousVersionData(version, info.SoftwareVersion, info.ID);
                            }
                            else
                            {
                                VersionCheck.ArchiveVersionHistoryByApplicationId(info.ID, true);
                            }
                        }
                        else
                        {
                            VersionCheck.DeleteVersionHistoryByApplicationId(info.ID);
                        }
                    }

                    VersionCheck.UpdateVersion(assembly, info.DownloadLink, info.IsDirectDownload, info.ReleaseDate,
                        info.MetaData);
                    ListVersions();
                }
            }
        }

        // a user wants to delete a selected entry in the grid from the remote database..
        private void TsbDeleteSelectedEntry_Click(object sender, EventArgs e)
        {
            if (gvSoftwareVersions.CurrentRow != null)
            {
                // verify that the user wants to delete a software entry from the server's database..
                var (dialogResult, archive, archiveHistory) = FormDialogQueryDeleteArchiveEntry.ShowDialog(this,
                    gvSoftwareVersions.CurrentRow.Cells[colApp.Index].Value.ToString());

                var name = gvSoftwareVersions.CurrentRow.Cells[colApp.Index].Value.ToString();

                var id = Convert.ToInt32(gvSoftwareVersions.CurrentRow.Cells[colID.Index].Value);

                if (dialogResult == DialogResult.OK)
                {
                    if (archive)
                    {
                        VersionCheck.ArchiveVersion(id, true);
                    }
                    else
                    {
                        VersionCheck.DeleteSoftwareEntry(name);
                    }

                    if (archiveHistory)
                    {
                        VersionCheck.ArchiveVersionHistoryByApplicationId(id, true);
                    }
                    else
                    {
                        VersionCheck.DeleteVersionHistoryByApplicationId(id);
                    }

                    //VersionCheck.DeleteSoftwareEntry(name);
                }
            }

            ListVersions();
        }

        // a user wants to re-list the version information from the remote database..
        private void MnuRefreshDatabaseEntries_Click(object sender, EventArgs e)
        {
            ListVersions();
        }

        // a string containing valid characters for a file name..
        private const string CharString = @"abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-_";

        // a random number generator for a new API key..
        private readonly Random random = new Random();

        // generates a random string of 20 characters of length..
        private void TbGenerateAPIKey_Click(object sender, EventArgs e)
        {
            if (tstbAPIKey.Text != string.Empty)
            {
                // verify that the user wants to generate a new API key..
                if (MessageBox.Show(@"Override the current API key?", @"Confirm", MessageBoxButtons.YesNo,
                        MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) == DialogResult.No)
                {
                    // ..no so do return..
                    return;
                }
            }

            string apiKey = string.Empty;
            for (int i = 0; i < 20; i++)
            {
                apiKey += CharString[random.Next(0, CharString.Length - 1)];
            }

            tstbAPIKey.Text = apiKey;
        }


        private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        // the user is not allowed to write some "stupid" non-random API key..
        private void TstbAPIKey_KeyDown(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;
        }

        private void MnuGenerateFiles_Click(object sender, EventArgs e)
        {
            bool randomizeDatabaseName =
                MessageBox.Show(
                    $@"Randomize the database file name for increased security?",
                    @"Question", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes;

            if (fbdDirectory.ShowDialog() == DialogResult.OK)
            {
                var softwarePath = Path.GetDirectoryName(Assembly.GetEntryAssembly()?.Location);
                if (softwarePath != null)
                {
                    using (File.Create(Path.Combine(fbdDirectory.SelectedPath, tstbAPIKey.Text)))
                    {
                        // create an empty file..
                    }

                    // ReSharper disable once StringLiteralTypo
                    var databaseFilename = "version.sqlite";

                    if (randomizeDatabaseName)
                    {
                        databaseFilename = FormDialogGenerateRandomFilename.ShowDialogQueryFilename(this);
                    }


                    // copy the existing files..
                    File.Copy(Path.Combine(softwarePath, "ServerSideBase", "index.html"),
                        Path.Combine(fbdDirectory.SelectedPath, "index.html"), true);

                    File.Copy(Path.Combine(softwarePath, "ServerSideBase", "version.php"),
                        Path.Combine(fbdDirectory.SelectedPath, "version.php"), true);

                    // ReSharper disable once StringLiteralTypo
                    File.Copy(Path.Combine(softwarePath, "ServerSideBase", "addupdate_functions.php"),
                        Path.Combine(fbdDirectory.SelectedPath, "addupdate_functions.php"), true);

                    File.Copy(Path.Combine(softwarePath, "ServerSideBase", "archive_functions.php"),
                        Path.Combine(fbdDirectory.SelectedPath, "archive_functions.php"), true);

                    File.Copy(Path.Combine(softwarePath, "ServerSideBase", "database_update.php"),
                        Path.Combine(fbdDirectory.SelectedPath, "database_update.php"), true);

                    File.Copy(Path.Combine(softwarePath, "ServerSideBase", "delete_functions.php"),
                        Path.Combine(fbdDirectory.SelectedPath, "delete_functions.php"), true);

                    File.Copy(Path.Combine(softwarePath, "ServerSideBase", "query_functions.php"),
                        Path.Combine(fbdDirectory.SelectedPath, "query_functions.php"), true);

                    // ReSharper disable once StringLiteralTypo
                    File.Copy(Path.Combine(softwarePath, "ServerSideBase", "empty.sqlite"),
                        Path.Combine(fbdDirectory.SelectedPath, "empty.sqlite"), true);

                    var fileContents = File.ReadAllText(Path.Combine(softwarePath, "ServerSideBase", "functions.php"));

                    fileContents = fileContents.Replace("define(\"dbname\", \"version.sqlite\");",
                        $"define(\"dbname\", \"{databaseFilename}\");");

                    File.WriteAllText(Path.Combine(fbdDirectory.SelectedPath, "functions.php"), fileContents);

                    // create an empty SQLite database..
                    using (SQLiteConnection connection = new SQLiteConnection(
                        "Data Source=" + Path.Combine(fbdDirectory.SelectedPath,
                            // ReSharper disable once StringLiteralTypo
                            databaseFilename) + ";Pooling=true;FailIfMissing=false;")) 
                    {
                        connection.Open();

                        using (SQLiteCommand command = new SQLiteCommand(File.ReadAllText(Path.Combine(softwarePath, "ServerSideBase", "version.sql")), connection))
                        {
                            command.ExecuteNonQuery();
                        }

                        connection.Close();
                    }

                    Process.Start("explorer.exe", fbdDirectory.SelectedPath);
                }
            }
        }

        private void mnuAddLocalizedData_Click(object sender, EventArgs e)
        {
            if (gvSoftwareVersions.CurrentRow != null)
            {
                FormDialogAddUpdateVersionChangeTextLocalized.ShowDialog(this,
                    gvSoftwareVersions.CurrentRow.Cells[colApp.Index].Value.ToString(),
                    gvSoftwareVersions.CurrentRow.Cells[colVersion.Index].Value.ToString(),
                    Convert.ToInt32(gvSoftwareVersions.CurrentRow.Cells[colID.Index].Value));
            }
        }

        // the form is about to close, so do save the settings..
        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            using (settings)
            {
                // save the settings..
                if (Debugger.IsAttached && !overrideDebugCheck)
                {
                    settings.CheckUriTest = tstbLocationURI.Text;
                    settings.ApiKeyTest = tstbAPIKey.Text;
                }
                else
                {
                    settings.CheckUri = tstbLocationURI.Text;
                    settings.ApiKey = tstbAPIKey.Text;
                }

                settings.TimeOutMs = (int)nudTimeOutMS.Value;

                using (settings)
                {
                    // ..and dispose of class instance..
                }
            }
        }
    }
}
