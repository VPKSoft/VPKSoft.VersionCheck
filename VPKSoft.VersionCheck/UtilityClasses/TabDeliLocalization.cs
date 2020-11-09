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
using System.Globalization;
using System.Linq;

namespace VPKSoft.VersionCheck.UtilityClasses
{
    /// <summary>
    /// A class for a simple localization using a text file embedded into a resource file.
    /// </summary>
    public class TabDeliLocalization
    {
        /// <summary>
        /// A list containing messages for localization. Please do fill at least the en-US localization.
        /// </summary>
        List<(string MessageName, string Message, string CultureName)> LocalizationTexts { get; } = 
            new List<(string MessageName, string Message, string CultureName)>();

        /// <summary>
        /// Gets a localized message and gets a string corresponding to that message.
        /// </summary>
        /// <param name="messageName">The name of the message to get.</param>
        /// <param name="defaultMessage">The default value for the message if none were found in the <see cref="LocalizationTexts"/> with the locale of <paramref name="locale"/>.</param>
        /// <param name="locale">A locale expressed as a string.</param>
        /// <returns>A localized message with the given parameters.</returns>
        public string GetMessage(string messageName, string defaultMessage, string locale)
        {
            var value = LocalizationTexts.FirstOrDefault(f => f.CultureName == locale && f.MessageName == messageName);

            if (value.Message == null && locale.Split('-').Length == 2)
            {
                value = LocalizationTexts.FirstOrDefault(f => f.CultureName == locale.Split('-')[0] && f.MessageName == messageName);
            }
            else if (value.Message == null) // fall back to a generic culture..
            {
                value = LocalizationTexts.FirstOrDefault(f =>
                    f.CultureName.StartsWith(locale.Split('-')[0]) && f.MessageName == messageName);
            }

            return value.Message ?? defaultMessage;
        }

        /// <summary>
        /// Gets a localized message and gets a string corresponding to that message with given arguments.
        /// </summary>
        /// <param name="messageName">The name of the message to get.</param>
        /// <param name="defaultMessage">The default value for the message if none were found in the <see cref="LocalizationTexts"/> with the locale of <paramref name="locale"/>.</param>
        /// <param name="locale">A locale expressed as a string.</param>
        /// <param name="args">An object array that contains zero or more objects to format the message.</param>
        /// <returns>A localized message with the given parameters.</returns>
        public string GetMessage(string messageName, string defaultMessage, string locale, params object[] args)
        {
            var value = LocalizationTexts.FirstOrDefault(f => f.CultureName == locale && f.MessageName == messageName);

            if (value.Message == null && locale.Split('-').Length == 2)
            {
                value = LocalizationTexts.FirstOrDefault(f => f.CultureName == locale.Split('-')[0] && f.MessageName == messageName);
            }
            else if (value.Message == null) // fall back to a generic culture..
            {
                value = LocalizationTexts.FirstOrDefault(f =>
                    f.CultureName.StartsWith(locale.Split('-')[0]) && f.MessageName == messageName);
            }

            string msg = value.Message ?? defaultMessage;
            try
            {
                return string.Format(msg, args);
            }
            catch
            {
                return msg;
            }
        }

        /// <summary>
        /// Fills the <see cref="LocalizationTexts"/> array with a given file contents as a list of strings.
        /// </summary>
        /// <param name="fileContents"></param>
        public void GetLocalizedTexts(string fileContents)
        {
            List<string> fileLines = new List<string>();
            fileLines.AddRange(fileContents.Split(Environment.NewLine.ToArray()));

            string locale = string.Empty;

            for (int i = 0; i < fileLines.Count; i++)
            {
                if (fileLines[i].StartsWith("["))
                {
                    try
                    {
                        locale = fileLines[i].Trim('[', ']');
                        locale = new CultureInfo(locale).Name;
                    }
                    catch
                    {
                        locale = string.Empty;
                    }
                    continue;
                }

                if (locale == string.Empty)
                {
                    continue;
                }

                string[] delimited = fileLines[i].Split('\t');
                if (delimited.Length >= 2)
                {
                    if (LocalizationTexts.Exists(f => f.CultureName == locale && f.MessageName == delimited[0]))
                    {
                        continue;
                    }
                    LocalizationTexts.Add((delimited[0], delimited[1], locale));
                }
            }
        }
    }
}
