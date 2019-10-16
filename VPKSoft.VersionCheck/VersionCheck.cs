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
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Script.Serialization;
using System.Reflection;
using System.Web;
using System.Windows.Forms;
using VPKSoft.VersionCheck.APIResponseClasses;
using VPKSoft.VersionCheck.Enumerations;

namespace VPKSoft.VersionCheck
{
    /// <summary>
    /// A class to check a latest program / assembly version.
    /// </summary>
    public class VersionCheck
    {
        /// <summary>
        /// Downloads a file from the internet showing a dialog box with a progress.
        /// </summary>
        /// <param name="downloadUri">The download URI of the file.</param>
        /// <param name="pathToDownload">The path to witch to save the file to download.</param>
        /// <returns><c>true</c> if the file was downloaded successfully, <c>false</c> otherwise.</returns>
        public static bool DownloadFile(string downloadUri, string pathToDownload)
        {
            return FormDialogDownloadFile.ShowDialog(downloadUri, pathToDownload);
        }

        /// <summary>
        /// Gets or sets the localized download text for the download dialog. The default is: 'Download:'.
        /// </summary>
        public static string LocalizedDownloadText
        {
            get => FormDialogDownloadFile.LocalizedDownloadText;
            set => FormDialogDownloadFile.LocalizedDownloadText = value;
        }

        /// <summary>
        /// Gets or sets the localized download percentage text for the download dialog. The default is: '%'.
        /// </summary>
        public static string LocalizedDownloadPercentageText
        {
            get => FormDialogDownloadFile.LocalizedDownloadPercentageText;
            set => FormDialogDownloadFile.LocalizedDownloadPercentageText = value;
        }

        /// <summary>
        /// Gets or sets the image for the about dialog. The default size is 310x70 pixels and the image is centered on the picture box.
        /// </summary>
        public static Image AboutDialogImage { get; set; } = Properties.Resources.VPKSoftLogo_App;

        /// <summary>
        /// Gets or sets the publisher web site URL which is navigated to when the user clicks the <see cref="AboutDialogImage"/>.
        /// </summary>
        public static string AboutDialogPublisherWebSiteUrl { get; set; } = "http://www.vpksoft.net";

        /// <summary>
        /// Gets or set the image size mode for the about dialog.
        /// </summary>
        public static PictureBoxSizeMode AboutDialogImageSizeMode { get; set; } = PictureBoxSizeMode.CenterImage;

        /// <summary>
        /// The localized short text for megabytes (MB) for the download dialog. The default is: 'MB'.
        /// </summary>
        // ReSharper disable once InconsistentNaming
        public static string LocalizedMBShortText
        {
            get => FormDialogDownloadFile.LocalizedMBShortText;
            set => FormDialogDownloadFile.LocalizedMBShortText = value;
        }

        /// <summary>
        /// Gets or sets the localized download speed text in megabytes per seconds for the download dialog. The default is: 'Speed (MB/s):'.
        /// </summary>
        public static string LocalizedDownloadSpeedMBs
        {
            get => FormDialogDownloadFile.LocalizedDownloadSpeedMBs;
            set => FormDialogDownloadFile.LocalizedDownloadSpeedMBs = value;
        }

        /// <summary>
        /// Gets or sets the post methods with their combined <see cref="PostMethod"/> enumeration value.
        /// </summary>
        internal static List<KeyValuePair<PostMethod, string>> PostMethods { get; set; } =
            new List<KeyValuePair<PostMethod, string>>(new[]
                {
                    new KeyValuePair<PostMethod, string>(PostMethod.QueryVersion, "Query_Version"),
                    new KeyValuePair<PostMethod, string>(PostMethod.GetSoftwareList, "GetSoftwareList"),
                    new KeyValuePair<PostMethod, string>(PostMethod.UpdateDatabase, "Update_Database"),
                    new KeyValuePair<PostMethod, string>(PostMethod.IncreaseDownloadCount, "Increase_DownloadCount"),
                    new KeyValuePair<PostMethod, string>(PostMethod.UpdateInsertVersion, "UpdateInsert_Version"),
                    new KeyValuePair<PostMethod, string>(PostMethod.DeleteVersion, "Delete_Version"),
                    new KeyValuePair<PostMethod, string>(PostMethod.AddVersionChanges, "AddVersionChanges"),
                    new KeyValuePair<PostMethod, string>(PostMethod.GetVersionChanges, "GetVersionChanges"),
                    new KeyValuePair<PostMethod, string>(PostMethod.DeleteVersionChange, "DeleteVersionChange"),
                    new KeyValuePair<PostMethod, string>(PostMethod.ArchiveVersion, "ArchiveVersion"),
                    new KeyValuePair<PostMethod, string>(PostMethod.ArchiveVersionHistory, "ArchiveVersionHistory"),
                }
            );

        /// <summary>
        /// An Uri to check the version from.
        /// </summary>
        public static string CheckUri { get; set; } = "http://localhost//version.php";

        /// <summary>
        /// Gets or sets the time out in milliseconds for the HTTP(S) communication.
        /// </summary>
        public static int TimeOutMs { get; set; } = 1500;

        /// <summary>
        /// Gets or sets the API key to validate update and deletion methods.
        /// </summary>
        public static string ApiKey { get; set; } = "abcd1234";

        /// <summary>
        /// Gets a VersionInfo class instance for a given assembly
        /// </summary>
        /// <param name="assembly">An Assembly class instance to get the latest version for.</param>
        /// <returns>A VersionInfo class instance if the operation was successful, otherwise null.</returns>
        public static VersionInfo GetVersion(Assembly assembly)
        {
            return GetVersion(assembly.GetName().Name);
        }

        /// <summary>
        /// Gets a VersionInfo class instance for a given assembly
        /// </summary>
        /// <param name="assembly">An Assembly class instance to get the latest version for.</param>
        /// <param name="culture">To primary culture to use to get the change history.</param>
        /// <returns>A VersionInfo class instance if the operation was successful, otherwise null.</returns>
        public static VersionInfo GetVersion(Assembly assembly, CultureInfo culture)
        {
            return GetVersion(assembly.GetName().Name, culture.Name);
        }

        /// <summary>
        /// Gets a VersionInfo class instance for a given assembly
        /// </summary>
        /// <param name="assembly">An Assembly class instance to get the latest version for.</param>
        /// <param name="localeString">To primary culture (in the format languagecode2-country/regioncode2) to use to get the change history.</param>
        /// <returns>A VersionInfo class instance if the operation was successful, otherwise null.</returns>
        public static VersionInfo GetVersion(Assembly assembly, string localeString)
        {
            return GetVersion(assembly.GetName().Name);
        }

        /// <summary>
        /// Gets a VersionInfo class instance for a given assembly name
        /// </summary>
        /// <param name="programName">An assembly name to get the latest version for.</param>
        /// <param name="culture">To primary culture to use to get the change history.</param>
        /// <returns>A VersionInfo class instance if the operation was successful, otherwise null.</returns>
        public static VersionInfo GetVersion(string programName, CultureInfo culture)
        {
            return GetVersion(programName, culture.Name);
        }

        /// <summary>
        /// Gets a VersionInfo class instance for a given assembly name
        /// </summary>
        /// <param name="programName">An assembly name to get the latest version for.</param>
        /// <returns>A VersionInfo class instance if the operation was successful, otherwise null.</returns>
        public static VersionInfo GetVersion(string programName)
        {
            return GetVersion(programName, CultureInfo.CurrentCulture);
        }


        /// <summary>
        /// Gets a VersionInfo class instance for a given assembly name
        /// </summary>
        /// <param name="programName">An assembly name to get the latest version for.</param>
        /// <param name="localeString">To primary culture (in the format languagecode2-country/regioncode2) to use to get the change history.</param>
        /// <returns>A VersionInfo class instance if the operation was successful, otherwise null.</returns>
        public static VersionInfo GetVersion(string programName, string localeString)
        {
            try
            {
                WebResponse webResponse = 
                    GetResponse(PostMethod.QueryVersion, false, programName, localeString);

                var result =  SerializeResponse<VersionInfo>(webResponse);

                return result;
            }
            catch (Exception ex)
            {
                // log the exception..
                ExceptionAction?.Invoke(ex);
                return null;
            }
        }

        /// <summary>
        /// Gets the localized version data for a an application with a given Id number.
        /// </summary>
        /// <param name="applicationId">The application identifier number.</param>
        /// <returns>A list of <see cref="LocalizeChangeHistoryResponse"/> class instances containing the localized version history entries for the application.</returns>
        public static List<LocalizeChangeHistoryResponse> GetVersionDataLocalized(int applicationId)
        {
            try
            {
                WebResponse webResponse = 
                    GetResponse(PostMethod.GetVersionChanges, 
                        true, 
                        applicationId);

                var result = SerializeResponse<List<LocalizeChangeHistoryResponse>>(webResponse);

                return result;
            }
            catch (Exception ex)
            {
                // log the exception..
                ExceptionAction?.Invoke(ex);
                return null;
            }
        }

        /// <summary>
        /// Deletes the localized version history data from the database.
        /// </summary>
        /// <param name="id">The identifier for the version history entry to delete.</param>
        /// <returns>A <see cref="GeneralResponse"/> class instance containing data of the API call.</returns>
        public static GeneralResponse DeleteLocalizedVersionData(int id)
        {
            try
            {
                WebResponse webResponse = 
                    GetResponse(PostMethod.DeleteVersionChange, 
                        true, 
                        id);

                var result = SerializeResponse<GeneralResponse>(webResponse);

                return result;
            }
            catch (Exception ex)
            {
                // log the exception..
                ExceptionAction?.Invoke(ex);
                return new GeneralResponse {Error = true, ErrorCode = -1, Message = ex.Message};
            }
        }

        /// <summary>
        /// Deletes the software entry from the remote database with a given name.
        /// </summary>
        /// <param name="applicationName">Name of the program to remove from the database.</param>
        /// <returns>A <see cref="GeneralResponse"/> class instance containing data of the API call.</returns>
        public static GeneralResponse DeleteSoftwareEntry(string applicationName)
        {
            try
            {
                WebResponse webResponse = 
                    GetResponse(PostMethod.DeleteVersion, 
                        false, 
                        applicationName, ApiKey);

                var result = SerializeResponse<GeneralResponse>(webResponse);

                return result;
            }
            catch (Exception ex)
            {
                // log the exception..
                ExceptionAction?.Invoke(ex);
                return new GeneralResponse {Error = true, ErrorCode = -1, Message = ex.Message};
            }
        }

        /// <summary>
        /// Adds the version changes as a localized text to a given <paramref name="culture"/>.
        /// </summary>
        /// <param name="assembly">The assembly of which changes to add.</param>
        /// <param name="softwareId">The software identifier.</param>
        /// <param name="culture">The culture of the software change text.</param>
        /// <param name="metaDataTextLocalized">The localized change history.</param>
        /// <returns>A <see cref="GeneralResponse"/> class instance containing data of the API call.</returns>
        public static GeneralResponse AddVersionChanges(Assembly assembly, int softwareId,
            CultureInfo culture, string metaDataTextLocalized)
        {
            try
            {
                WebResponse webResponse =
                    GetResponse(
                        PostMethod.AddVersionChanges,
                        true,
                        softwareId,
                        assembly.GetName().Name,
                        assembly.GetName().Version,
                        metaDataTextLocalized,
                        culture.Name);

                var result = SerializeResponse<GeneralResponse>(webResponse);

                return result;
            }
            catch (Exception ex)
            {
                // log the exception..
                ExceptionAction?.Invoke(ex);
                return new GeneralResponse {Error = true, ErrorCode = -1, Message = ex.Message};
            }
        }

        /// <summary>
        /// Deletes the software entry from the remote database with a name gotten from the <paramref name="assembly"/>.
        /// </summary>
        /// <param name="assembly">The assembly of which name to use.</param>
        /// <returns>A <see cref="GeneralResponse"/> class instance containing data of the API call.</returns>
        public static GeneralResponse DeleteSoftwareEntry(Assembly assembly)
        {
            return DeleteSoftwareEntry(assembly.GetName().Name);
        }

        /// <summary>
        /// Inserts or updates the software version into the remote database.
        /// </summary>
        /// <param name="assembly">The assembly to get the software data from.</param>
        /// <param name="downloadLink">The download link for the software.</param>
        /// <param name="isDirectDownload">if set to <c>true</c> the <paramref name="downloadLink"/> is a link to a binary file or to an installer executable.</param>
        /// <param name="releaseDateTime">The release date and time of the software.</param>
        /// <param name="metaDataText">The meta data text. I.e. version changes with the software.</param>Delete_Version
        /// <returns>A <see cref="GeneralResponse"/> class instance containing data of the API call.</returns>
        public static GeneralResponse UpdateVersion(Assembly assembly, string downloadLink, bool isDirectDownload,
            DateTime releaseDateTime, string metaDataText = "")
        {
            try
            {
                WebResponse webResponse =
                    GetResponse(
                        PostMethod.UpdateInsertVersion,
                        false,
                        assembly.GetName().Name,
                        assembly.GetName().Version,
                        downloadLink,
                        releaseDateTime.ToString("yyyy-MM-dd HH':'mm':'ss", CultureInfo.InvariantCulture),
                        isDirectDownload.ToString(),
                        metaDataText,
                        ApiKey);


                var result = SerializeResponse<GeneralResponse>(webResponse);

                return result;
            }
            catch (Exception ex)
            {
                // log the exception..
                ExceptionAction?.Invoke(ex);
                return new GeneralResponse {Error = true, ErrorCode = -1, Message = ex.Message};
            }
        }

        /// <summary>
        /// Increases the download count of a given application name.
        /// </summary>
        /// <param name="applicationName">Name of the application which download count to increase.</param>
        /// <returns>A <see cref="GeneralResponse"/> class instance containing data of the API call.</returns>
        public static GeneralResponse IncreaseDownloadCount(string applicationName)
        {
            try
            {
                WebResponse webResponse = GetResponse(PostMethod.IncreaseDownloadCount, false, applicationName);

                var result = SerializeResponse<GeneralResponse>(webResponse);

                return result;
            }
            catch (Exception ex)
            {
                // log the exception..
                ExceptionAction?.Invoke(ex);
                return new GeneralResponse {Error = true, ErrorCode = -1, Message = ex.Message};
            }
        }

        /// <summary>
        /// Archives a version entry with a given Id number.
        /// </summary>
        /// <param name="id">The identifier number for the version entry to archive.</param>
        /// <param name="delete">if set to <c>true</c> the entry is deleted after archiving.</param>
        /// <returns>A <see cref="GeneralResponse"/> class instance containing data of the API call.</returns>
        public static GeneralResponse ArchiveVersion(int id, bool delete = false)
        {
            try
            {
                WebResponse webResponse = 
                    GetResponse(PostMethod.ArchiveVersion, true, id, delete ? 1 : 0);

                var result = SerializeResponse<GeneralResponse>(webResponse);

                return result;
            }
            catch (Exception ex)
            {
                // log the exception..
                ExceptionAction?.Invoke(ex);
                return new GeneralResponse {Error = true, ErrorCode = -1, Message = ex.Message};
            }
        }

        /// <summary>
        /// Archives a given <see cref="VersionInfo"/> using it's Id number.
        /// </summary>
        /// <param name="versionInfo">The <see cref="VersionInfo"/> class instance to archive by it's Id number.</param>
        /// <param name="delete">if set to <c>true</c> the entry is deleted after archiving.</param>
        /// <returns>A <see cref="GeneralResponse"/> class instance containing data of the API call.</returns>
        public static GeneralResponse ArchiveVersion(VersionInfo versionInfo, bool delete = false)
        {
            return ArchiveVersion(versionInfo.ID, delete);
        }

        /// <summary>
        /// Archives a version history entry with a given Id number.
        /// </summary>
        /// <param name="id">The identifier number for the version history entry to archive.</param>
        /// <param name="delete">if set to <c>true</c> the entry is deleted after archiving.</param>
        /// <returns>A <see cref="GeneralResponse"/> class instance containing data of the API call.</returns>
        public static GeneralResponse ArchiveVersionHistory(int id, bool delete = false)
        {
            try
            {
                WebResponse webResponse = 
                    GetResponse(PostMethod.ArchiveVersionHistory, true, id, delete ? 1 : 0);

                var result = SerializeResponse<GeneralResponse>(webResponse);

                return result;
            }
            catch (Exception ex)
            {
                // log the exception..
                ExceptionAction?.Invoke(ex);
                return new GeneralResponse {Error = true, ErrorCode = -1, Message = ex.Message};
            }
        }

        /// <summary>
        /// Archives a given <see cref="LocalizeChangeHistoryResponse"/> using it's Id number.
        /// </summary>
        /// <param name="changeHistory">The <see cref="LocalizeChangeHistoryResponse"/> class instance to archive by it's Id number.</param>
        /// <param name="delete">if set to <c>true</c> the entry is deleted after archiving.</param>
        /// <returns>A <see cref="GeneralResponse"/> class instance containing data of the API call.</returns>
        public static GeneralResponse ArchiveVersionHistory(LocalizeChangeHistoryResponse changeHistory, bool delete = false)
        {
            try
            {
                return ArchiveVersionHistory(int.Parse(changeHistory.ID), delete);
            }
            catch (Exception ex)
            {
                // log the exception..
                ExceptionAction?.Invoke(ex);
                return new GeneralResponse {Error = true, ErrorCode = -1, Message = ex.Message};
            }
        }

        /// <summary>
        /// Increases the download count of the software.
        /// </summary>
        /// <param name="assembly">The assembly to get the software data from.</param>
        /// <returns>A <see cref="GeneralResponse"/> class instance containing data of the API call.</returns>
        public static GeneralResponse IncreaseDownloadCount(Assembly assembly)
        {
            return IncreaseDownloadCount(assembly.GetName().Name);
        }

        /// <summary>
        /// Updates the database structure to the newest version.
        /// </summary>
        /// <returns>A <see cref="GeneralResponse"/> class instance containing data of the API call.</returns>
        public static GeneralResponse UpdateDatabase()
        {
            try
            {
                WebResponse webResponse = GetResponse(PostMethod.UpdateDatabase, true);

                var result = SerializeResponse<GeneralResponse>(webResponse);

                return result;
            }
            catch (Exception ex)
            {
                // log the exception..
                ExceptionAction?.Invoke(ex);
                return new GeneralResponse {Error = true, ErrorCode = -1, Message = ex.Message};
            }
        }

        internal static WebResponse GetResponse(PostMethod postMethod, bool apiKeyRequired, params object[] arguments)
        {
            // See: https://docs.microsoft.com/en-us/dotnet/api/system.net.servicepointmanager.expect100continue?f1url=https%3A%2F%2Fmsdn.microsoft.com%2Fquery%2Fdev15.query%3FappId%3DDev15IDEF1%26l%3DEN-US%26k%3Dk(System.Net.ServicePointManager.Expect100Continue);k(TargetFrameworkMoniker-.NETFramework,Version%3Dv4.5);k(DevLang-csharp)%26rd%3Dtrue%26f%3D255%26MSPPError%3D-2147217396&view=netframework-4.8
            ServicePointManager.Expect100Continue = true;

            // set all the security protocols..
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 |
                                                   SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(CheckUri);
            request.Timeout = TimeOutMs;

            request.Method = "POST";

            var recordData = apiKeyRequired ? ApiKey : string.Empty;

            if (arguments != null && arguments.Length > 0)
            {
                recordData += (recordData == string.Empty ? "" : ";") + string.Join(";", arguments);
            }

            // no empty values because dev doesn't know or wishes to test whether
            // PHP: isset($_POST["xxx"]) is true when empty data is given..
            if (recordData == string.Empty)
            {
                recordData = "1";
            }

            recordData = HttpUtility.UrlEncode(recordData);

            recordData = PostMethods.FirstOrDefault(f => f.Key == postMethod).Value + "=" + recordData;

            byte[] data =
                Encoding.UTF8.GetBytes(recordData);

            request.ContentLength = data.Length;
            request.ContentType = "application/x-www-form-urlencoded";

            using (Stream rs = request.GetRequestStream())
            {
                rs.Write(data, 0, data.Length);
            }

            return request.GetResponse();
        }

        /// <summary>
        /// Serializes a see <see cref="WebResponse"/> response stream <see cref="Stream"/> to a class of type of <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The type of the response.</typeparam>
        /// <param name="response">The <see cref="WebResponse"/> class instance to serialize into an object.</param>
        /// <returns>An object of type of <typeparamref name="T"/> serialized from the response stream if successful; otherwise default(T).</returns>
        internal static T SerializeResponse<T>(WebResponse response)
        {
            try
            {
                using (Stream stream = response.GetResponseStream())
                {
                    if (stream != null && stream.CanRead)
                    {
                        using (StreamReader sr = new StreamReader(stream, Encoding.UTF8))
                        {
                            JavaScriptSerializer js = new JavaScriptSerializer();
                            string json = sr.ReadToEnd();
                            T result = (T) js.Deserialize(json, typeof(T));

                            if (result != null && result.GetType() == typeof(VersionResponse)) // special case with this one..
                            {
                                if (result is VersionResponse versionResponse &&
                                    versionResponse.SoftwareName == "unknown") 
                                {
                                    return default;
                                }
                            }

                            return result;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // log the exception..
                ExceptionAction?.Invoke(ex);
                return default;
            }

            return default;
        }

        /// <summary>
        /// Gets or sets the action to execute if an exception occurs within the class library.
        /// </summary>
        public static Action<Exception> ExceptionAction { get; set; }

        /// <summary>
        /// Gets the software versions listed in the SQLite database.
        /// </summary>
        public static List<VersionInfo> GetSoftwareVersions()
        {
            try
            {
                WebResponse webResponse = GetResponse(PostMethod.GetSoftwareList, true, CultureInfo.CurrentCulture.Name);

                var result =  SerializeResponse<List<VersionInfo>>(webResponse);

                return result;
            }
            catch (Exception ex)
            {
                // log the exception..
                ExceptionAction?.Invoke(ex);
                return null;
            }
        }
    }
}
