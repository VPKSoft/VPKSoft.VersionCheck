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
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Forms;
using System.Globalization;
using System.Resources;
using System.Collections;

namespace VPKSoft.VersionCheck
{
    /// <summary>
    /// A simple about dialog form.
    /// Implements the <see cref="System.Windows.Forms.Form" />
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class FormAbout: Form
    {
        // the assembly which about information to show..
        private readonly Assembly aboutAssembly;

        // the version information gotten from the assembly..
        private VersionCheck.VersionInfo info;

        // not available (N/A) localized text..
        private string na = string.Empty;


        /// <summary>
        /// Initializes a new instance of the <see cref="FormAbout"/> class using the <see cref="Assembly.GetEntryAssembly()"/> method.
        /// </summary>
        /// <param name="parent">The parent <see cref="Form"/> for this dialog.</param>
        /// <param name="license">The license name (e.g. GPL or MIT).</param>
        /// <param name="licenseUrl">The license URL.</param>
        /// <param name="checkUrl">The check URL.</param>
        /// <param name="timeOut">The time out for the web requests.</param>
        public FormAbout(Form parent, string license, string licenseUrl, string checkUrl, int timeOut = 1500)
        {
            Owner = parent;
            aboutAssembly = Assembly.GetEntryAssembly();
            InitializeComponent();
            MainInit();
            sllLinkLicense.Text = license;
            sllLinkLicense.LinkUrl = licenseUrl;
            VersionCheck.CheckUri = checkUrl;
            VersionCheck.TimeOutMs = timeOut;
            pbLogo.SizeMode = VersionCheck.AboutDialogImageSizeMode;
            pbLogo.Image = VersionCheck.AboutDialogImage;
            ShowDialog();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FormAbout"/> class.
        /// </summary>
        /// <param name="parent">The parent <see cref="Form"/> for this dialog.</param>
        /// <param name="aboutAssembly">The about assembly which information to use with the dialog.</param>
        /// <param name="license">The license name (e.g. GPL or MIT).</param>
        /// <param name="licenseUrl">The license URL.</param>
        /// <param name="checkUrl">The check URL.</param>
        /// <param name="timeOut">The time out for the web requests.</param>
        public FormAbout(Form parent, Assembly aboutAssembly, string license, string licenseUrl, string checkUrl, int timeOut = 1500)
        {
            Owner = parent;
            this.aboutAssembly = aboutAssembly;
            InitializeComponent();
            MainInit();
            sllLinkLicense.Text = license;
            sllLinkLicense.LinkUrl = licenseUrl;
            VersionCheck.CheckUri = checkUrl;
            VersionCheck.TimeOutMs = timeOut;
            pbLogo.SizeMode = VersionCheck.AboutDialogImageSizeMode;
            pbLogo.Image = VersionCheck.AboutDialogImage;
            ShowDialog();
        }

        /// <summary>
        /// A overridden culture for the localization.
        /// </summary>
        public static string OverrideCultureString { get; set; } = CultureInfo.CurrentCulture.ToString();

        /// <summary>
        /// This method is called by the constructors. Localizes the dialog and sets the assembly information fields contents.
        /// </summary>
        private void MainInit()
        {
            try // about dialog shouldn't crash the application
            {
                ResourceSet rs = Properties.Resources.ResourceManager.GetResourceSet(CultureInfo.InvariantCulture, false, false);

                string langStr = null;
                foreach (DictionaryEntry entry in rs)
                {
                    if (entry.Key.ToString() == "texts_" + OverrideCultureString.Replace('-', '_'))
                    {
                        langStr = entry.Value as string;
                    }
                }

                // lower the standard to a language not tied to a location..
                if (langStr == null)
                {
                    foreach (DictionaryEntry entry in rs)
                    {
                        if (entry.Key.ToString() == "texts_" + OverrideCultureString.Replace('-', '_').Split('_')[0])
                        {
                            langStr = entry.Value as string;
                        }
                    }
                }

                if (langStr == null)
                {
                    foreach (DictionaryEntry entry in rs)
                    {
                        if (entry.Key.ToString() == "texts_en_US")
                        {
                            langStr = entry.Value as string;
                        }
                    }
                }

                if (langStr != null)
                {
                    List<KeyValuePair<string, string>> langPairs = new List<KeyValuePair<string, string>>();

                    List<string> langStrings = new List<string>();

                    try
                    {
                        langStrings = new List<string>(langStr.Split(Environment.NewLine.ToCharArray()));
                    }
                    catch
                    {
                        // ignored..
                    }

                    foreach (string str in langStrings)
                    {
                        try
                        {
                            langPairs.Add(new KeyValuePair<string, string>(str.Split('=')[0], str.Split('=')[1]));
                        }
                        catch
                        {
                            // ignored..
                        }
                    }

                    foreach (KeyValuePair<string, string> langPair in langPairs)
                    {
                        if (langPair.Key == "OK")
                        {
                            btOK.Text = langPair.Value;
                        }
                        else if (langPair.Key == "Text")
                        {
                            try
                            {
                                Text = string.Format(langPair.Value, AssemblyTitle);
                            }
                            catch
                            {
                                Text = langPair.Value; // no title in about box title
                            }
                        }
                        else if (langPair.Key == "Check")
                        {
                            lbCheckVersionText.Text = langPair.Value;
                        }
                        else if (langPair.Key == "ClickCheck")
                        {
                            sllLinkVersion.Text = langPair.Value;
                        }
                        else if (langPair.Key == "CheckNA")
                        {
                            na = langPair.Value;
                        }
                        else if (langPair.Key == "Description")
                        {
                            lbBoxDescriptionText.Text = langPair.Value;
                        }
                        else if (langPair.Key == "CompanyName")
                        {
                            lbCompanyNameText.Text = langPair.Value;
                        }
                        else if (langPair.Key == "Copyright")
                        {
                            lbCopyrightText.Text = langPair.Value;
                        }
                        else if (langPair.Key == "ProductName")
                        {
                            lbProductNameText.Text = langPair.Value;
                        }
                        else if (langPair.Key == "Version")
                        {
                            lbVersionText.Text = langPair.Value;
                        }
                        else if (langPair.Key == "License")
                        {
                            lbLicenseText.Text = langPair.Value;
                        }
                    }
                }
            }
            catch
            {
                // do nothing..
            }
            lbProductName.Text = AssemblyProduct;
            lbVersion.Text = AssemblyVersion;
            lbCopyright.Text = AssemblyCopyright;
            lbCompanyName.Text = AssemblyCompany;
            tbBoxDescription.Text = AssemblyDescription;
            tbBoxDescription.HideSelection = true;
            tbBoxDescription.SelectionLength = 0;
            btOK.Focus();
        }

        #region Assembly Attribute Accessors
        /// <summary>
        /// Gets the assembly title.
        /// </summary>
        public string AssemblyTitle
        {
            get
            {
                object[] attributes = aboutAssembly.GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (attributes.Length > 0)
                {
                    AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    if (titleAttribute.Title != "")
                    {
                        return titleAttribute.Title;
                    }
                }
                return System.IO.Path.GetFileNameWithoutExtension(aboutAssembly.CodeBase);
            }
        }

        /// <summary>
        /// Gets the assembly version.
        /// </summary>
        public string AssemblyVersion => aboutAssembly.GetName().Version.ToString();

        /// <summary>
        /// Gets the assembly description.
        /// </summary>
        public string AssemblyDescription
        {
            get
            {
                object[] attributes = aboutAssembly.GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyDescriptionAttribute)attributes[0]).Description;
            }
        }

        /// <summary>
        /// Gets the assembly product.
        /// </summary>
        public string AssemblyProduct
        {
            get
            {
                object[] attributes = aboutAssembly.GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyProductAttribute)attributes[0]).Product;
            }
        }

        /// <summary>
        /// Gets the assembly copyright.
        /// </summary>
        public string AssemblyCopyright
        {
            get
            {
                object[] attributes = aboutAssembly.GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }

        /// <summary>
        /// Gets the assembly company.
        /// </summary>
        public string AssemblyCompany
        {
            get
            {
                object[] attributes = aboutAssembly.GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCompanyAttribute)attributes[0]).Company;
            }
        }
        #endregion

        private void pbLogo_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(VersionCheck.AboutDialogPublisherWebSiteUrl);
            }
            catch
            {
                // ignored..
            }
        }

        private void sslLinkVersion_Click(object sender, EventArgs e)
        {
            info = VersionCheck.GetVersion(aboutAssembly);

            if (info != null && info.IsLargerVersion(aboutAssembly))
            {
                sllLinkVersion.LinkUrl = info.DownloadLink;
                sllLinkVersion.Text = info.SoftwareVersion;

                // unsubscribe the click event; its not needed to user to create a DDoS attack..
                sllLinkVersion.Click -= sslLinkVersion_Click; 
            }            
            else
            {
                sllLinkVersion.Text = na;
                // unsubscribe the click event; its not needed to user to create a DDoS attack..
                sllLinkVersion.Click -= sslLinkVersion_Click;
            }
        }
    }
}
