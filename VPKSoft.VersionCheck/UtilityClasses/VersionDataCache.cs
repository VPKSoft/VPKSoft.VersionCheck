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
using System.Xml.Linq;
using VPKSoft.VersionCheck.APIResponseClasses;

namespace VPKSoft.VersionCheck.UtilityClasses
{
    /// <summary>
    /// A class for caching version information of the software updates. This reduces the network traffic to the version check API.
    /// </summary>
    public static class VersionDataCache
    {
        /// <summary>
        /// Removes the invalid path characters from a file or a path string.
        /// </summary>
        /// <param name="text">The text to remove the invalid characters from.</param>
        /// <param name="isFile">if set to <c>true</c> the invalid file name characters are used.</param>
        /// <returns>The text corrected by removing the invalid characters.</returns>
        private static string RemoveInvalidPathCharacters(string text, bool isFile)
        {
            var invalidCharacters = isFile ? Path.GetInvalidFileNameChars() : Path.GetInvalidPathChars();
            foreach (var invalidCharacter in invalidCharacters)
            {
                text = text.Replace(invalidCharacter.ToString(), string.Empty);
            }

            return text;
        }

        // ReSharper disable once CommentTypo
        /// <summary>
        /// Deletes the version history data folder at location %PROGRAMDATA%\[ApplicationName].
        /// </summary>
        /// <param name="programName">Name of the program.</param>
        /// <returns><c>true</c> if the operation was successful, <c>false</c> otherwise.</returns>
        public static bool DeleteVersionHistoryData(string programName)
        {
            try
            {
                bool result = false;
                if (File.Exists(GetVersionDataFile(programName)))
                {
                    File.Delete(GetVersionDataFile(programName));
                    result = true;
                }

                if (Directory.Exists(GetVersionDataPath(programName)))
                {
                    Directory.Delete(GetVersionDataPath(programName));
                    result = true;
                }

                return result;
            }
            catch
            {
                return false;
            }
        }

        // ReSharper disable once CommentTypo
        /// <summary>
        /// Gets a value indicating whether there is any data in the version history cache in the file located at %PROGRAMDATA%\[ApplicationName]\VersionHistory.xml.
        /// </summary>
        /// <param name="programName">Name of the program.</param>
        /// <returns><c>true</c> if there is history data in the cache, <c>false</c> otherwise.</returns>
        public static bool VersionHistoryCacheIsEmpty(string programName)
        {
            return GetVersionHistoryFromCache(programName).Count == 0;
        }

        // ReSharper disable once CommentTypo
        /// <summary>
        /// Gets the version data path.
        /// </summary>
        /// <param name="programName">Name of the program.</param>
        /// <returns>A string representing the path at %PROGRAMDATA%\[ApplicationName].</returns>
        public static string GetVersionDataPath(string programName)
        {
            programName = RemoveInvalidPathCharacters(programName, true);

            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData),
                programName);
        }

        // ReSharper disable once CommentTypo
        /// <summary>
        /// Gets the version data file name.
        /// </summary>
        /// <param name="programName">Name of the program.</param>
        /// <returns>A string representing the path at %PROGRAMDATA%\[ApplicationName]\VersionHistory.xml.</returns>
        public static string GetVersionDataFile(string programName)
        {
            return Path.Combine(GetVersionDataPath(programName),
                "VersionHistory.xml");
        }

        // ReSharper disable once CommentTypo
        /// <summary>
        /// Gets the version history from cache located at %PROGRAMDATA%\[ApplicationName]\VersionHistory.xml.
        /// </summary>
        /// <param name="programName">Name of the program.</param>
        /// <returns>List&lt;KeyValuePair&lt;Version, System.String&gt;&gt;.</returns>
        public static List<KeyValuePair<Version, string>> GetVersionHistoryFromCache(string programName)
        {
            var result = new List<KeyValuePair<Version, string>>();

            try
            {
                var fileName = GetVersionDataFile(programName);

                if (File.Exists(fileName))
                {
                    XDocument document = XDocument.Load(fileName);
                    if (document.Root != null)
                    {
                        var nodes = document.Root.Elements("VersionHistoryEntry");
                        foreach (var node in nodes)
                        {
                            var versionString = node.Attribute("Version")?.Value;
                            var versionChanges = node.Element("HistoryData")?.Value;

                            if (versionChanges != null && versionString != null)
                            {
                                result.Add(
                                    new KeyValuePair<Version, string>(new Version(versionString), versionChanges));
                            }
                        }

                    }
                }

                result.Sort((x, y) => x.Key.CompareTo(y.Key));
            }
            catch
            {
                return result;
            }
            return result;
        }

        // ReSharper disable once CommentTypo
        /// <summary>
        /// Caches the version data to a %PROGRAMDATA%\[ApplicationName]\VersionHistory.xml file.
        /// </summary>
        /// <param name="programName">Name of the program.</param>
        /// <param name="info">The <see cref="VersionInfo"/> information returned by the web API.</param>
        /// <returns><c>true</c> if the operation was successful, <c>false</c> otherwise.</returns>
        public static bool CacheVersionData(string programName, VersionInfo info)
        {
            try
            {
                var path = GetVersionDataPath(programName);

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                var fileName = GetVersionDataFile(programName);

                XDocument document;
                if (File.Exists(fileName))
                {
                    document = XDocument.Load(fileName);
                }
                else
                {
                    XElement entryElement = new XElement("VersionHistory", new XAttribute("application", programName));
                    document = new XDocument(
                        new XDeclaration("1.0", "utf-8", ""),
                        entryElement);
                }

                if (document.Root != null)
                {
                    var nodes = document.Root.Elements("VersionHistoryEntry");
                    bool entryExists = nodes.FirstOrDefault(f => f.Attribute("Version")?.Value == info.SoftwareVersion) != null;
                    if (!entryExists)
                    {
                        document.Root.Add(
                            new XElement("VersionHistoryEntry",
                            new XAttribute("Version", info.SoftwareVersion),
                            new XElement("HistoryData", string.IsNullOrWhiteSpace(info.MetaDataLocalized) ? info.MetaData : info.MetaDataLocalized))
                        );
                        document.Save(fileName);
                    }
                }

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
