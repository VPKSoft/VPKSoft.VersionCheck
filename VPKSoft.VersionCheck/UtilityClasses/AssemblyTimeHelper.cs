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
    public static class ReflectionExtension
    {
        public static DateTimeOffset? GetBuildDateTime(this Assembly assembly)
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

            return null;
        }
    }
}
