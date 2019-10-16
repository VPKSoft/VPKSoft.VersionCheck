using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VersionMaintenance
{
    /// <summary>
    /// A dialog to write a version change in a localized format to a given culture.
    /// Implements the <see cref="System.Windows.Forms.Form" />
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class FormDialogAddUpdateVersionChangeTextLocalized : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FormDialogAddUpdateVersionChangeTextLocalized"/> class.
        /// </summary>
        public FormDialogAddUpdateVersionChangeTextLocalized()
        {
            InitializeComponent();

            var cultures =
                new List<CultureInfo>(
                    CultureInfo.GetCultures(CultureTypes.SpecificCultures | CultureTypes.NeutralCultures));
            // ReSharper disable once CoVariantArrayConversion
            cmbLocale.Items.AddRange(cultures.ToArray());
            cmbLocale.DisplayMember = cbUseNativeNames.Checked ? "NativeName" : "DisplayName";
            cmbLocale.ValueMember = "Name";
            cmbLocale.SelectedItem = CultureInfo.CurrentCulture;
        }

        private void cbUseNativeNames_CheckedChanged(object sender, EventArgs e)
        {
            var checkbox = (CheckBox) sender;
            cmbLocale.DisplayMember = checkbox.Checked ? "NativeName" : "DisplayName";
        }

        private void cmbLocale_SelectedIndexChanged(object sender, EventArgs e)
        {
            var comboBox = (ComboBox) sender;
            lbCultureISOValue.Text = comboBox.SelectedItem.ToString();
        }

        /// <summary>
        /// Shows the form as a modal dialog box with the specified owner.
        /// </summary>
        /// <param name="owner">Any object that implements <see cref="T:System.Windows.Forms.IWin32Window" /> that represents the top-level window that will own the modal dialog box.</param>
        /// <param name="softwareName">Name of the software of which localized version changes are to be added or changed.</param>
        /// <param name="version">The version string of the software.</param>
        /// <returns>One of the <see cref="T:System.Windows.Forms.DialogResult"/> values.</returns>
        public static DialogResult ShowDialog(IWin32Window owner, string softwareName, string version)
        {
            var form = new FormDialogAddUpdateVersionChangeTextLocalized
            {
                tbSoftwareChangeHistoryName = {Text = softwareName}, tbVersionValue = {Text = version}
            };
            return form.ShowDialog(owner);
        }

        private void tbChangesDescription_TextChanged(object sender, EventArgs e)
        {
            var textBox = (TextBox) sender;
            btOK.Enabled = textBox.Text.Trim() != string.Empty;
        }
    }
}
