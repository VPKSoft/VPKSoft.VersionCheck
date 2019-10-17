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

using System.Globalization;

namespace VPKSoft.VersionCheck.APIResponseClasses
{
    /// <summary>
    /// A response value for an API call that is not expected to return anything.
    /// </summary>
    public class LocalizeChangeHistoryResponse
    {
        /// <summary>
        /// An ID for the database entry.
        /// </summary>
        // ReSharper disable once InconsistentNaming
        public string ID;

        /// <summary>
        /// An ID for the software database entry.
        /// </summary>
        // ReSharper disable once InconsistentNaming
        public string APP_ID;

        /// <summary>
        /// A software / assembly name.
        /// </summary>
        public string SoftwareName;

        /// <summary>
        /// Version number string.
        /// </summary>
        public string SoftwareVersion;

        /// <summary>
        /// The base culture of a change history entry.
        /// </summary>
        public string CultureMain;

        /// <summary>
        /// The specific culture of the <see cref="CultureMain"/> culture entry.
        /// </summary>
        public string CultureSpecific;

        /// <summary>
        /// A meta data for the entry.
        /// </summary>
        public string MetaData;

        /// <summary>
        /// Gets the <see cref="Culture"/> of this entry instance based on the <see cref="CultureMain"/> and <see cref="CultureSpecific"/> field values.
        /// </summary>
        public CultureInfo Culture
        {
            get
            {
                try
                {
                    string cultureString = CultureMain + (string.IsNullOrEmpty(CultureSpecific)
                                               ? string.Empty
                                               : "-" + CultureSpecific);

                    return CultureInfo.CreateSpecificCulture(cultureString);
                }
                catch
                {
                    return CultureInfo.CurrentCulture;
                }
            }
        }
    }
}
