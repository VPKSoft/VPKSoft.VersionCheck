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
        /// Enumeration of the http(s) POST method values.
        /// </summary>
        internal enum PostMethod
        {
            /// <summary>
            /// The Query_Version POST method.
            /// </summary>
            QueryVersion,

            /// <summary>
            /// The GetSoftwareList POST method.
            /// </summary>
            GetSoftwareList,

            /// <summary>
            /// The Update_Database POST method.
            /// </summary>
            UpdateDatabase,

            /// <summary>
            /// The Increase_DownloadCount POST method.
            /// </summary>
            IncreaseDownloadCount,

            /// <summary>
            /// The UpdateInsert_Version POST method.
            /// </summary>
            UpdateInsertVersion,

            /// <summary>
            /// The Delete_Version POST method.
            /// </summary>
            DeleteVersion,

            /// <summary>
            /// The AddVersionChanges POST method.
            /// </summary>
            AddVersionChanges,
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
                }
            );

        /// <summary>
        /// A response value for an API call that is not expected to return anything.
        /// </summary>
        public class GeneralResponse
        {
            /// <summary>
            /// A string describing the response; can be anything.
            /// </summary>
            /// <value>The message.</value>
            public string Message { get; set; }

            /// <summary>
            /// Gets or sets the error code; 0 means no error.
            /// </summary>
            /// <value>The error code.</value>
            public int ErrorCode { get; set; }

            /// <summary>
            /// Gets or sets a value indicating whether an error occurred with the API call.
            /// </summary>
            public bool Error { get; set; }
        }

        /// <summary>
        /// A class for serialization of a JSON response from a web server
        /// </summary>
        public class VersionResponse
        {
            /// <summary>
            /// An ID for the database entry.
            /// </summary>
            // ReSharper disable once InconsistentNaming
            public string ID;

            /// <summary>
            /// A software / assembly name
            /// </summary>
            public string SoftwareName;

            /// <summary>
            /// Version number string
            /// </summary>
            public string SoftwareVersion;

            /// <summary>
            /// A download location for the software version
            /// </summary>
            public string DownloadLink;

            /// <summary>
            /// A date as string when the version was released
            /// </summary>
            public string ReleaseDate;

            /// <summary>
            /// A value indicating whether the link is a direct download to a binary or a binary installer.
            /// </summary>
            public string IsDirectDownload;

            /// <summary>
            /// Gets or set an additional meta data for the entry.
            /// </summary>
            public string MetaData;

            /// <summary>
            /// Gets or sets the download count of the software via the update system.
            /// </summary>
            public string DownloadCount { get; set; }

            /// <summary>
            /// Gets or set an additional localized meta data for the entry.
            /// </summary>
            public string MetaDataLocalized { get; set; }
        }

        /// <summary>
        /// A class returned by the VersionCheck.GetVersion method.
        /// </summary>
        public class VersionInfo
        {
            /// <summary>
            /// An ID number in the database for the version information.
            /// </summary>
            // ReSharper disable once InconsistentNaming
            public int ID { get; set; }

            /// <summary>
            /// A software / assembly name
            /// </summary>
            public string SoftwareName { get; set; }

            /// <summary>
            /// Gets or sets the software version as a string.
            /// </summary>
            public string SoftwareVersion
            {
                get => VersionNumber[0] + "." + VersionNumber[1] + "." + VersionNumber[2] + "." + VersionNumber[3];

                set
                {
                    try
                    {
                        string[] versionString = value.Split('.');
                        VersionNumber[0] = int.Parse(versionString[0]);
                        VersionNumber[1] = int.Parse(versionString[1]);
                        VersionNumber[2] = int.Parse(versionString[2]);
                        VersionNumber[3] = int.Parse(versionString[3]);
                    }
                    catch (Exception ex)
                    {
                        // log the exception..
                        ExceptionAction?.Invoke(ex);
                    }
                }
            }

            /// <summary>
            /// Four part version number
            /// </summary>
            public int[] VersionNumber { get; set; } = new int[4];

            /// <summary>
            /// A download URI for the software version
            /// </summary>
            public string DownloadLink { get; set; }

            /// <summary>
            /// An UTC release date of the software version
            /// </summary>
            public DateTime ReleaseDate { get; set; }

            /// <summary>
            /// A value indicating whether the link is a direct download to a binary or a binary installer.
            /// </summary>
            public bool IsDirectDownload { get; set; }

            /// <summary>
            /// An additional meta data for the entry.
            /// </summary>
            public string MetaData { get; set; }

            /// <summary>
            /// Gets or sets the download count of the software via the update system.
            /// </summary>
            public int DownloadCount { get; set; }

            /// <summary>
            /// Checks if the given Assembly version is smaller than the VersionNumber property.
            /// </summary>
            /// <param name="assembly">An Assembly which version to compare.</param>
            /// <returns>True if the VersionNumber property is larger version than the version of the given Assembly, otherwise false.</returns>
            // ReSharper disable once UnusedMember.Global
            public bool IsLargerVersion(Assembly assembly)
            {
                try
                {
                    Version version = new Version(SoftwareVersion);
                    Version versionAssembly = assembly.GetName().Version;
                    return version.CompareTo(versionAssembly) > 0;
                }
                catch (Exception ex)
                {
                    // log the exception..
                    ExceptionAction?.Invoke(ex);
                    return false;
                }
            }

            /// <summary>
            /// Checks if the given version string is smaller than the VersionNumber property.
            /// </summary>
            /// <param name="versionStr">A version number as in string: a.b.c.d</param>
            /// <returns>True if the VersionNumber property is larger version than the given version string, otherwise false.</returns>
            // ReSharper disable once UnusedMember.Global
            public bool IsLargerVersion(string versionStr)
            {
                try
                {
                    Version version = new Version(SoftwareVersion);
                    return version.CompareTo(new Version(versionStr)) > 0;
                }
                catch (Exception ex)
                {
                    // log the exception..
                    ExceptionAction?.Invoke(ex);
                    return false;
                }
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="VersionInfo"/> class.
            /// </summary>
            // ReSharper disable once UnusedMember.Global
            public VersionInfo()
            {
            }

            /// <summary>
            /// A constructor which uses a VersionResponse class instance as base information.
            /// </summary>
            /// <param name="vr">A VersionResponse class instance to be used as base information.</param>
            public VersionInfo(VersionResponse vr)
            {
                SoftwareName = vr.SoftwareName;
                VersionNumber = new int[4];
                try
                {
                    VersionNumber[0] = int.Parse(vr.SoftwareVersion.Split('.')[0]);
                    VersionNumber[1] = int.Parse(vr.SoftwareVersion.Split('.')[1]);
                    VersionNumber[2] = int.Parse(vr.SoftwareVersion.Split('.')[2]);
                    VersionNumber[3] = int.Parse(vr.SoftwareVersion.Split('.')[3]);
                }
                catch (Exception ex)
                {
                    // log the exception..
                    ExceptionAction?.Invoke(ex);
                    VersionNumber[0] = 0;
                    VersionNumber[1] = 0;
                    VersionNumber[2] = 0;
                    VersionNumber[3] = 0;
                }

                DownloadLink = vr.DownloadLink;

                MetaData = vr.MetaData;

                try
                {
                    DateTime dt = DateTime.ParseExact(vr.ReleaseDate, "yyyy-MM-dd HH':'mm':'ss", CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal);
                    ReleaseDate = dt;
                }
                catch (Exception ex)
                {
                    // log the exception..
                    ExceptionAction?.Invoke(ex);
                    DateTime dt = DateTime.Now;
                    ReleaseDate = new DateTime(dt.Year, dt.Month, dt.Day, 0, 0, 0, DateTimeKind.Utc);
                }

                try
                {
                    ID = int.Parse(vr.ID);
                }
                catch (Exception ex)
                {
                    // log the exception..
                    ExceptionAction?.Invoke(ex);
                    ID = -1;
                }

                try
                {
                    DownloadCount = int.Parse(vr.DownloadCount);
                }
                catch (Exception ex)
                {
                    // log the exception..
                    ExceptionAction?.Invoke(ex);
                    DownloadCount = 0;
                }

                try
                {
                    IsDirectDownload = bool.Parse(vr.IsDirectDownload);
                }
                catch (Exception ex)
                {
                    // log the exception..
                    ExceptionAction?.Invoke(ex);
                    IsDirectDownload = false;
                }
            }

            /// <summary>
            /// Gets an assembly name and the latest version of the assembly.
            /// </summary>
            /// <returns>Returns an assembly name and the latest version of the assembly.</returns>
            public override string ToString()
            {
                return SoftwareName + " / v." + VersionNumber[0] + "." + VersionNumber[1] + "." + VersionNumber[2] + "." + VersionNumber[3];
            }

            /// <summary>
            /// Creates a new <see cref="VersionInfo"/> class instance of a given file name and a given assembly.
            /// </summary>
            /// <param name="fileName">Name of the file to get the date from.</param>
            /// <param name="assembly">The assembly to get the name and the version from.</param>
            /// <returns>A new <see cref="VersionInfo"/> class instance created with the given parameters.</returns>
            public static VersionInfo FromAssembly(string fileName, Assembly assembly)
            {
                return new VersionInfo
                {
                    SoftwareName = assembly.GetName().Name, SoftwareVersion = assembly.GetName().Version.ToString(),
                    ReleaseDate = new FileInfo(fileName).LastWriteTimeUtc, DownloadLink = "INSERT A LINK",
                };
            }
        }

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
        /// Gets a VersionInfo class instance for a given assembly name
        /// </summary>
        /// <param name="programName">An assembly name to get the latest version for.</param>
        /// <returns>A VersionInfo class instance if the operation was successful, otherwise null.</returns>
        public static VersionInfo GetVersion(string programName)
        {
            try
            {
                WebResponse webResponse = GetResponse(PostMethod.QueryVersion, false, programName);

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
                        softwareId.ToString(),
                        assembly.GetName().Name,
                        assembly.GetName().Version.ToString(),
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
                        assembly.GetName().Version.ToString(),
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

        internal static WebResponse GetResponse(PostMethod postMethod, bool apiKeyRequired, params string[] arguments)
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
