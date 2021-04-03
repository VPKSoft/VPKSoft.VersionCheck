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
using System.IO;
using System.Reflection;

namespace VPKSoft.VersionCheck.APIResponseClasses
{
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
                        VersionCheck.ExceptionAction?.Invoke(ex);
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
            /// Gets or set an additional localized meta data for the entry.
            /// </summary>
            public string MetaDataLocalized { get; set; }

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
                    var result = version.CompareTo(versionAssembly) > 0;
                    return result;
                }
                catch (Exception ex)
                {
                    // log the exception..
                    VersionCheck.ExceptionAction?.Invoke(ex);
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
                    VersionCheck.ExceptionAction?.Invoke(ex);
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
                    VersionCheck.ExceptionAction?.Invoke(ex);
                    VersionNumber[0] = 0;
                    VersionNumber[1] = 0;
                    VersionNumber[2] = 0;
                    VersionNumber[3] = 0;
                }

                DownloadLink = vr.DownloadLink;

                MetaData = vr.MetaData;

                MetaDataLocalized = vr.MetaDataLocalized;

                try
                {
                    DateTime dt = DateTime.ParseExact(vr.ReleaseDate, "yyyy-MM-dd HH':'mm':'ss", CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal);
                    ReleaseDate = dt;
                }
                catch (Exception ex)
                {
                    // log the exception..
                    VersionCheck.ExceptionAction?.Invoke(ex);
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
                    VersionCheck.ExceptionAction?.Invoke(ex);
                    ID = -1;
                }

                try
                {
                    DownloadCount = int.Parse(vr.DownloadCount);
                }
                catch (Exception ex)
                {
                    // log the exception..
                    VersionCheck.ExceptionAction?.Invoke(ex);
                    DownloadCount = 0;
                }

                try
                {
                    IsDirectDownload = bool.Parse(vr.IsDirectDownload);
                }
                catch (Exception ex)
                {
                    // log the exception..
                    VersionCheck.ExceptionAction?.Invoke(ex);
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

        /// <summary>
        /// Creates a new <see cref="VersionInfo"/> class instance of a given assembly name and a given <see cref="Version"/>.
        /// </summary>
        /// <param name="assemblyName">Name of the assembly.</param>
        /// <param name="version">The version of the assembly.</param>
        /// <returns>A new <see cref="VersionInfo"/> class instance created with the given parameters.</returns>
        public static VersionInfo FromVersion(string assemblyName, Version version)
            {
                return new VersionInfo
                {
                    SoftwareName = assemblyName, SoftwareVersion = version.ToString(),
                    ReleaseDate = DateTime.UtcNow, DownloadLink = "INSERT A LINK",
                };
            }
        }
}
