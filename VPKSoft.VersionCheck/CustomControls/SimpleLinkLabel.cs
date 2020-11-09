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
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using VPKSoft.VersionCheck.Forms;

namespace VPKSoft.VersionCheck.CustomControls
{
    /// <summary>
    /// A simplified link label.
    /// Implements the <see cref="System.Windows.Forms.Label" />
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Label" />
    public sealed partial class SimpleLinkLabel : Label
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleLinkLabel"/> class.
        /// </summary>
        public SimpleLinkLabel()
        {
            InitializeComponent();
            ForeColor = Color.Navy;
            Font = new Font(Font, FontStyle.Underline);
            Cursor = Cursors.Hand;
            Click += SimpleLinkLabel_Click;
        }

        
        /// <summary>
        /// Gets or sets a value indicating whether to use the download dialog to download binaries on binaries.
        /// </summary>
        /// <value>
        ///   <c>true</c> if to use download dialog to download binaries; otherwise, <c>false</c>.</value>
        [Description("Gets or sets a value indicating whether to use the download dialog to download binaries on binaries.")]
        [Category("Behaviour")]
        [Browsable(true)]
        public bool UseDownloadDialogOnBinaries { get; set; } = true;

        /// <summary>
        /// Gets or sets the name of the software of this about dialog.
        /// </summary>
        [Description("Gets or sets the name of the software of this link label.")]
        [Category("Behaviour")]
        [Browsable(true)]
        public string SoftwareName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the assembly of which is to be updated.
        /// </summary>
        [Browsable(false)]
        public Assembly AboutAssembly { get; set; } 

        /// <summary>
        /// Sets the software name from assembly to the <see cref="SoftwareName"/> property value.
        /// </summary>
        /// <param name="assembly">The assembly to use.</param>
        public void SetSoftwareNameFromAssembly(Assembly assembly)
        {
            SoftwareName = assembly.GetName().Name;
        }

        /// <summary>
        /// Gets or sets the temporary path for downloading files into.
        /// </summary>
        [Description("Gets or sets the temporary path for downloading files into.")]
        [Category("Behaviour")]
        [Browsable(true)]
        public string TempPath => Path.GetTempPath();

        // a user clicked the link..
        void SimpleLinkLabel_Click(object sender, EventArgs e)
        {
            try
            {
                if (!UseDownloadDialogOnBinaries)
                {
                    System.Diagnostics.Process.Start(LinkUrl);
                }
                else
                {
                    if (VersionCheck.AboutDialogDisplayDownloadDialog)
                    {
                        if (Uri.IsWellFormedUriString(LinkUrl, UriKind.Absolute))
                        {
                            FormCheckVersion.CheckForNewVersion(VersionCheck.CheckUri,
                                AboutAssembly == null ? Assembly.GetEntryAssembly() : AboutAssembly,
                                CultureInfo.CurrentCulture.Name);
                        }
                        return;
                    }

                    if (VersionCheck.DownloadFile(LinkUrl, TempPath))
                    {
                        VersionCheck.IncreaseDownloadCount(SoftwareName);

                        System.Diagnostics.Process.Start(Path.Combine(TempPath, Path.GetFileName(new Uri(LinkUrl).LocalPath)));
                    }
                    else
                    {
                        try
                        {
                            File.Delete(Path.Combine(TempPath, Path.GetFileName(new Uri(LinkUrl).LocalPath)));
                        }
                        catch
                        {
                            // ignored..
                        }
                    }
                }
            }
            catch
            {
                // ignored..
            }
        }

        /// <summary>
        /// Gets or sets the link URL of control.
        /// </summary>
        [Category("Behavior")]
        [Browsable(false)]
        public string LinkUrl { get; set; }

        /// <summary>
        /// Gets or sets the cursor that is displayed when the mouse pointer is over the control.
        /// </summary>
        [Description("Gets or sets the cursor that is displayed when the mouse pointer is over the control.")]
        [Category("Appearance")]
        [Browsable(false)]
        public override Cursor Cursor
        {
            get => base.Cursor;
            set => base.Cursor = value;
        }
    }
}
