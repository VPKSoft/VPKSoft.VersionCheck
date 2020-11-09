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

namespace VPKSoft.VersionCheck.Enumerations
{
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

        /// <summary>
        /// The GetVersionChanges POST method.
        /// </summary>
        GetVersionChanges,

        /// <summary>
        /// The GetVersionChanges POST method.
        /// </summary>
        DeleteVersionChange,

        /// <summary>
        /// The ArchiveVersion POST method.
        /// </summary>
        ArchiveVersion,

        /// <summary>
        /// The ArchiveVersionHistory POST method.
        /// </summary>
        ArchiveVersionHistory,

        /// <summary>
        /// The ArchiveVersionHistoryByApplicationId POST method.
        /// </summary>
        ArchiveVersionHistoryByApplicationId,

        /// <summary>
        /// The DeleteVersionHistoryByApplicationId POST method.
        /// </summary>
        DeleteVersionHistoryByApplicationId,

        /// <summary>
        /// The PreservePreviousVersionData POST method.
        /// </summary>
        PreservePreviousVersionData,
    }
}
