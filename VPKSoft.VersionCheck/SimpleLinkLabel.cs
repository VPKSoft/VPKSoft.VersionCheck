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
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VPKSoft.VersionCheck
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

        void SimpleLinkLabel_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(LinkUrl);
            }
            catch
            {
                // ignored..
            }
        }

        [Category("Behavior")]
        public string LinkUrl { get; set; }

        [Browsable(false)]
        public override Cursor Cursor
        {
            get
            {
                return base.Cursor;
            }
            set
            {
                base.Cursor = value;
            }
        }
    }
}
