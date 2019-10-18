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
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace VPKSoft.VersionCheck.Forms
{
    /// <summary>
    /// A form to download a file from the internet.
    /// Implements the <see cref="System.Windows.Forms.Form" />
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class FormDialogDownloadFile : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FormDialogDownloadFile"/> class.
        /// </summary>
        public FormDialogDownloadFile()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Gets or sets the localized download text. The default is: 'Download:'.
        /// </summary>
        public static string LocalizedDownloadText { get; set; } = "Download:";

        /// <summary>
        /// Gets or sets the localized download percentage text. The default is: '%'.
        /// </summary>
        public static string LocalizedDownloadPercentageText { get; set; } = "%";

        /// <summary>
        /// The localized short text for megabytes (MB). The default is: 'MB'.
        /// </summary>
        // ReSharper disable once InconsistentNaming
        public static string LocalizedMBShortText = "MB";

        /// <summary>
        /// Gets or sets the localized download speed text in megabytes per seconds. The default is: 'Speed (MB/s):'.
        /// </summary>
        public static string LocalizedDownloadSpeedMBs { get; set; } = "Speed (MB/s):";

        /// <summary>
        /// Gets or sets the download URI.
        /// </summary>
        private string DownloadUri { get; set; }

        /// <summary>
        /// Gets or sets the path to download the file to.
        /// </summary>
        private string PathToDownload { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="System.Net.WebClient"/> class instance used to download the file.
        /// </summary>
        private WebClient WebClient { get; set; }

        /// <summary>
        /// A field to measure download speed.
        /// </summary>
        private DateTime downloadSpeedTime;

        /// <summary>
        /// Shows the dialog and starts a file download from the internet.
        /// </summary>
        /// <param name="downloadUri">The download URI of the file.</param>
        /// <param name="pathToDownload">The path to witch to save the file to download.</param>
        /// <returns><c>true</c> if the file was downloaded successfully, <c>false</c> otherwise.</returns>
        public static bool ShowDialog(string downloadUri, string pathToDownload)
        {
            var form = new FormDialogDownloadFile
            {
                DownloadUri = downloadUri,
                PathToDownload = pathToDownload,

                // (C)::https://social.msdn.microsoft.com/Forums/vstudio/en-US/53c7cd54-df02-44ec-b3ad-c1aaf5db67a5/extract-file-name?forum=csharpgeneral
                lbDownloadFileNameValue = {Text = Path.GetFileName(new Uri(downloadUri).LocalPath)},

                // localize..
                lbDownloadFileNameDescription = {Text = LocalizedDownloadText},
                lbPercentageText = {Text = LocalizedDownloadPercentageText},
                lbMBOfMBText = {Text = LocalizedMBShortText},
                lbSpeedMBsText = {Text = LocalizedDownloadSpeedMBs},
            };

            // set the return value based on the DialogResult value..
            return form.ShowDialog() == DialogResult.OK;
        }

        // the form is shown, so do start the download..
        private void FormDownloadFile_Shown(object sender, EventArgs e)
        {
            try
            {
                // create a web client..
                using (WebClient = new WebClient())
                {
                    // subscribe the web client events..
                    WebClient.DownloadDataCompleted += Client_DownloadDataCompleted;
                    WebClient.DownloadProgressChanged += Client_DownloadProgressChanged;
                    WebClient.DownloadFileCompleted += WebClient_DownloadFileCompleted;

                    // get the current date and time for the download speed calculation..
                    downloadSpeedTime = DateTime.Now;

                    // start the asynchronous file download..
                    WebClient.DownloadFileAsync(new Uri(DownloadUri),
                        Path.Combine(PathToDownload, Path.GetFileName(new Uri(DownloadUri).LocalPath)));
                }
            }
            catch
            {
                // on exception return failure..
                DialogResult = DialogResult.Cancel;
            }
        }

        /// <summary>
        /// Handles the DownloadFileCompleted event of the WebClient control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="AsyncCompletedEventArgs"/> instance containing the event data.</param>
        private void WebClient_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            // return from the dialog based on the value whether the download was cancelled..
            DialogResult = e.Cancelled ? DialogResult.Cancel : DialogResult.OK;
        }

        // the download progress has changed..
        private void Client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            // calculate the download speed in MB/s (this will be cumulative)..
            var speed = (double) e.BytesReceived / 1000000 / (DateTime.Now - downloadSpeedTime).TotalSeconds;

            // display the download speed..
            lbSpeedMBsValue.Text = $@"{speed:0.0}";

            // display tho amount of bytes received and the total amount of bytes in megabytes..
            lbMBOfMBValue.Text = e.BytesReceived / 1000000 + @" / " + e.TotalBytesToReceive / 1000000;

            // display the download progress percentage..
            pbDownloadProgress.Value = e.ProgressPercentage;
            lbPercentageValue.Text = e.ProgressPercentage.ToString();
        }

        /// <summary>
        /// Handles the DownloadDataCompleted event of the Client control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DownloadDataCompletedEventArgs"/> instance containing the event data.</param>
        private void Client_DownloadDataCompleted(object sender, DownloadDataCompletedEventArgs e)
        {
            // return from the dialog based on the value whether the download was cancelled..
            DialogResult = e.Cancelled ? DialogResult.Cancel : DialogResult.OK;
        }

        private void BtCancel_Click(object sender, EventArgs e)
        {
            // a user wishes cancel the download..
            WebClient?.CancelAsync();
        }

        // the form closed, so unsubscribe the events..
        private void FormDialogDownloadFile_FormClosed(object sender, FormClosedEventArgs e)
        {
            // unsubscribe the events..
            if (WebClient != null)
            {
                WebClient.DownloadDataCompleted -= Client_DownloadDataCompleted;
                WebClient.DownloadProgressChanged -= Client_DownloadProgressChanged;
                WebClient.DownloadFileCompleted -= WebClient_DownloadFileCompleted;
            }
        }
    }
}
