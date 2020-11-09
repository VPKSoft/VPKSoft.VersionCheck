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
    /// A response value for an API call that is not expected to return anything.
    /// </summary>
    public class GeneralResponse
    {
        /// <summary>
        /// A string describing the response; can be anything.
        /// </summary>
        /// <value>The message.</value>
        public string Message;

        /// <summary>
        /// Gets or sets the error code; 0 means no error.
        /// </summary>
        /// <value>The error code.</value>
        public int ErrorCode;

        /// <summary>
        /// Gets or sets a value indicating whether an error occurred with the API call.
        /// </summary>
        public bool Error;
    }
}
