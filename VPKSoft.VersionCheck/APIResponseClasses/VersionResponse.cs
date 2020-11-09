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

namespace VPKSoft.VersionCheck.APIResponseClasses
{
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
        /// A software / assembly name.
        /// </summary>
        public string SoftwareName;

        /// <summary>
        /// Version number string.
        /// </summary>
        public string SoftwareVersion;

        /// <summary>
        /// A download location for the software version.
        /// </summary>
        public string DownloadLink;

        /// <summary>
        /// A date as string when the version was released.
        /// </summary>
        public string ReleaseDate;

        /// <summary>
        /// A value indicating whether the link is a direct download to a binary or a binary installer.
        /// </summary>
        public string IsDirectDownload;

        /// <summary>
        /// An additional meta data for the entry.
        /// </summary>
        public string MetaData;

        /// <summary>
        /// The download count of the software via the update system.
        /// </summary>
        public string DownloadCount;

        /// <summary>
        /// Additional localized meta data for the entry.
        /// </summary>
        public string MetaDataLocalized;
    }
}
