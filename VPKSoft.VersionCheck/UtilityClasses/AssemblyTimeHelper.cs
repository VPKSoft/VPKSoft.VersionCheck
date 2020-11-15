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
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace VPKSoft.VersionCheck.UtilityClasses
{
    /// <summary>
    /// A class for some assembly-related extension methods.
    /// </summary>
    public static class ReflectionExtension
    {
        /// <summary>
        /// Gets the build date time <see cref="DateTimeOffset"/> offset.
        /// </summary>
        /// <param name="assembly">The assembly to inspect.</param>
        /// <returns>System.Nullable&lt;DateTimeOffset&gt;.</returns>
        public static DateTimeOffset? GetBuildDateTimeOffset(this Assembly assembly)
        {
            try
            {
                var path = assembly.GetName().CodeBase;

                if (path.StartsWith(@"file:///"))
                {
                    path = path.Substring(8);
                }

                if (File.Exists(path))
                {
                    return new DateTimeOffset(File.GetCreationTimeUtc(path));
                }
            }
            catch
            {
                // null is returned..
            }

            return null;
        }

        /// <summary>
        /// Gets the build date time of a specified assembly.
        /// </summary>
        /// <param name="assembly">The assembly which build date and time to get.</param>
        /// <returns>System.Nullable&lt;DateTime&gt;.</returns>
        public static DateTime? GetBuildDateTime(this Assembly assembly)
        {
            try
            {
                var path = assembly.GetName().CodeBase;

                if (path.StartsWith(@"file:///"))
                {
                    path = path.Substring(8);
                }

                if (File.Exists(path))
                {
                    return File.GetCreationTimeUtc(path);
                }
            }
            catch
            {
                // null is returned..
            }

            return null;
        }
    }
}
