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
using System.ComponentModel;
using System.Reflection;
using VPKSoft.ConfLib;

namespace VersionMaintenance
{
    /// <summary>
    /// An attribute class for describing a setting name and it's type (VPKSoft.ConfLib).
    /// </summary>
    /// <seealso cref="System.Attribute" />
    [AttributeUsage(AttributeTargets.Property)] // target a property only..
    public class SettingAttribute: Attribute
    {
        /// <summary>
        /// Gets or sets the name of the setting (VPKSoft.ConfLib).
        /// </summary>
        public string SettingName { get; set; }

        /// <summary>
        /// Gets or sets the type of the setting (VPKSoft.ConfLib).
        /// </summary>
        public Type SettingType { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SettingAttribute"/> class.
        /// </summary>
        /// <param name="settingName">Name of the setting (VPKSoft.ConfLib).</param>
        /// <param name="type">The type of the setting (VPKSoft.ConfLib).</param>
        public SettingAttribute(string settingName, Type type)
        {
            SettingName = settingName; // save the given values..
            SettingType = type;
        }
    }

    /// <summary>
    /// The settings for the software.
    /// </summary>
    public class Settings: INotifyPropertyChanged, IDisposable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Settings"/> class.
        /// </summary>
        public Settings()
        {
            if (conflib == null) // don't initialize if already initialized..
            {
                conflib = new Conflib
                {
                    AutoCreateSettings = true // set it to auto-create SQLite database tables..
                }; // create a new instance of the Conflib class..
            }

            // get all public instance properties of this class..
            PropertyInfo[] propertyInfos = GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);

            // loop through the properties..
            for (int i = 0; i < propertyInfos.Length; i++)
            {
                try // avoid crashes..
                {
                    // get the SettingAttribute class instance of the property..
                    SettingAttribute settingAttribute =
                        (SettingAttribute) propertyInfos[i].GetCustomAttribute(typeof(SettingAttribute));

                    // get the default value for the property..
                    object currentValue = propertyInfos[i].GetValue(this);

                    // set the value for the property using the default value as a
                    // fall-back value..

                    if (currentValue == null)
                    {
                        continue;
                    }

                    propertyInfos[i].SetValue(this,
                        Convert.ChangeType(conflib[settingAttribute.SettingName, currentValue.ToString()],
                            settingAttribute.SettingType));
                }
                catch
                {
                    // ignored..
                }
            }

            // subscribe the event handler..
            PropertyChanged += Settings_PropertyChanged;
        }

        /// <summary>
        /// Handles the PropertyChanged event of the Settings class instance.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="PropertyChangedEventArgs"/> instance containing the event data.</param>
        private void Settings_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            // NOTE:: Do use this attribute, if no notification is required from a property: [DoNotNotify]

            try // just try from the beginning..
            {
                PropertyInfo propertyInfo = // first get the property info for the property..
                GetType().GetProperty(e.PropertyName, BindingFlags.Instance | BindingFlags.Public);

                // get the property value..
                object value = propertyInfo?.GetValue(this);

                // get the setting attribute value of the property.. 
                if (propertyInfo != null)
                {
                    SettingAttribute settingAttribute = (SettingAttribute)propertyInfo.GetCustomAttribute(typeof(SettingAttribute));

                    if (value != null && settingAttribute != null)
                    {
                        conflib[settingAttribute.SettingName] = value.ToString();
                    }
                }
            }
            catch
            {
                // ignored..
            }
        }

        #region Settings        
        /// <summary>
        /// Gets or sets the API key used with the version management interface.
        /// </summary>
        [Setting("settings/apiKey", typeof(string))]
        // ReSharper disable once UnusedMember.Global
        public string ApiKey { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the time out in milliseconds with the HTTP(S) communication.
        /// </summary>
        [Setting("settings/timeOutMS", typeof(int))]
        // ReSharper disable once UnusedMember.Global
        public int TimeOutMs { get; set; } = 1500;

        /// <summary>
        /// Gets or sets the check URI; where the version.php file is located.
        /// </summary>
        [Setting("settings/checkUri", typeof(string))]
        // ReSharper disable once UnusedMember.Global
        public string CheckUri { get; set; } = string.Empty;
        #endregion

        /// <summary>
        /// An instance to a Conflib class.
        /// </summary>
        private readonly Conflib conflib;

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
#pragma warning disable CS0067 // disable the CS0067 as the PropertyChanged event is raised via the PropertyChanged.Fody class library..
        public event PropertyChangedEventHandler PropertyChanged;
#pragma warning restore CS0067

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            // unsubscribe the event handler..
            PropertyChanged -= Settings_PropertyChanged;

            // close the conflib class instance..
            conflib?.Close();
        }
    }
}
