using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using VPKSoft.VersionCheck;
using VPKSoft.VersionCheck.APIResponseClasses;

namespace VersionMaintenance.FormDialogs
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
        }

        private void cbUseNativeNames_CheckedChanged(object sender, EventArgs e)
        {
            var checkbox = (CheckBox) sender;
            cmbLocale.DisplayMember = checkbox.Checked ? "NativeName" : "DisplayName";
            listLocalizedCultures.DisplayMember = checkbox.Checked ? "CultureNativeName" : "CultureDisplayName";
        }

        private void CreateLocaleCombo()
        {
            var cultures =
                new List<CultureInfo>(
                    CultureInfo.GetCultures(CultureTypes.SpecificCultures | CultureTypes.NeutralCultures));
            // ReSharper disable once CoVariantArrayConversion
            cmbLocale.Items.AddRange(cultures.ToArray());
            cmbLocale.DisplayMember = cbUseNativeNames.Checked ? "NativeName" : "DisplayName";
            cmbLocale.ValueMember = "Name";
            previousSelectedCulture = CultureInfo.CurrentCulture;
            cmbLocale.SelectedItem = CultureInfo.CurrentCulture;
        }

        private void ListLocalizedCultures()
        {
            var response = VersionCheck.GetVersionDataLocalized(applicationId, tbVersionValue.Text);
            var cultures = response.OrderBy(f => cbUseNativeNames.Checked ? f.Culture.NativeName : f.Culture.DisplayName);
            listLocalizedCultures.DisplayMember = cbUseNativeNames.Checked ? "CultureNativeName" : "CultureDisplayName";
            listLocalizedCultures.Items.Clear();

            foreach (var culture in cultures)
            {
                listLocalizedCultures.Items.Add(culture);
            }
        }

        private void SaveHistoryChanges(CultureInfo culture)
        {
            if (culture == null)
            {
                culture = cmbLocale.SelectedItem as CultureInfo;
            }

            if (culture != null)
            {
                if (string.IsNullOrWhiteSpace(tbChangesDescription.Text))
                {
                    return;
                }

                VersionCheck.AddVersionChanges(tbSoftwareChangeHistoryName.Text, tbVersionValue.Text, applicationId,
                    previousSelectedCulture, tbChangesDescription.Text);
            }
        }

        private int applicationId;
        private CultureInfo previousSelectedCulture;
        private bool saveEntry;

        private void cmbLocale_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var comboBox = (ComboBox) sender;

                if (saveEntry)
                {
                    SaveHistoryChanges(previousSelectedCulture);
                    ListLocalizedCultures();
                }

                lbCultureISOValue.Text = comboBox.SelectedItem.ToString();

                previousSelectedCulture = comboBox.SelectedItem as CultureInfo;

                var localizedResults =
                    VersionCheck.GetVersionDataLocalized(applicationId, tbVersionValue.Text, lbCultureISOValue.Text);
                var localizedResult = localizedResults.FirstOrDefault(f =>
                    f.Culture.Equals(previousSelectedCulture));

                tbChangesDescription.Text = localizedResult != null ? localizedResult.MetaData : string.Empty;

                ttMain.SetToolTip(pbLocalizedIndicator,
                    string.IsNullOrWhiteSpace(tbChangesDescription.Text)
                        ? "The selected culture is NOT localized"
                        : "The selected culture is localized");

                pbLocalizedIndicator.Image = string.IsNullOrWhiteSpace(tbChangesDescription.Text)
                    ? Properties.Resources.No_entry
                    : Properties.Resources.Apply;
            }
            catch (Exception ex)
            {
                MessageBox.Show($@"An error occurred: '{ex.Message}'.", @"Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        /// <summary>
        /// Shows the form as a modal dialog box with the specified owner.
        /// </summary>
        /// <param name="owner">Any object that implements <see cref="T:System.Windows.Forms.IWin32Window" /> that represents the top-level window that will own the modal dialog box.</param>
        /// <param name="softwareName">Name of the software of which localized version changes are to be added or changed.</param>
        /// <param name="version">The version string of the software.</param>
        /// <param name="applicationId">The application identifier number.</param>
        /// <returns>One of the <see cref="T:System.Windows.Forms.DialogResult"/> values.</returns>
        public static DialogResult ShowDialog(IWin32Window owner, string softwareName, string version, int applicationId)
        {
            var form = new FormDialogAddUpdateVersionChangeTextLocalized
            {
                tbSoftwareChangeHistoryName = {Text = softwareName}, 
                tbVersionValue = {Text = version},
                applicationId = applicationId,
            };

            //form.SaveHistoryChanges(CultureInfo.CurrentCulture);

            form.CreateLocaleCombo();
            form.ListLocalizedCultures();
            form.saveEntry = true;

            if (form.ShowDialog(owner) == DialogResult.OK)
            {
                form.SaveHistoryChanges(null);
                return DialogResult.OK;
            }

            return DialogResult.Cancel;
        }

        private void tbChangesDescription_TextChanged(object sender, EventArgs e)
        {
            btOK.Enabled = VerifyChangeHistory((TextBox) sender);
        }

        /// <summary>
        /// Verifies the user input to the change history text box.
        /// </summary>
        /// <param name="textBox">The text box <see cref="TextBox"/>.</param>
        /// <returns><c>true</c> if the text box contains text, <c>false</c> otherwise.</returns>
        private bool VerifyChangeHistory(TextBox textBox)
        {
            // verify the user input..
            return textBox.Text.Trim() != string.Empty;
        }

        private void FormDialogAddUpdateVersionChangeTextLocalized_Shown(object sender, EventArgs e)
        {
            // verify the user input..
            btOK.Enabled = VerifyChangeHistory(tbChangesDescription);
        }

        private void FormDialogAddUpdateVersionChangeTextLocalized_KeyDown(object sender, KeyEventArgs e)
        {
            var comboBox = cmbLocale;
            if (e.KeyCode == Keys.Return && comboBox.DroppedDown)
            {
                e.SuppressKeyPress = true;
            }
            else if (e.Control && !e.Shift && !e.Alt && e.KeyCode == Keys.F)
            {
                SearchForCulture();
                e.SuppressKeyPress = true;
            }
        }

        private void listLocalizedCultures_SelectedValueChanged(object sender, EventArgs e)
        {
            var listBox = (ListBox) sender;

            if (listBox.SelectedItem == null)
            {
                return;
            }

            try
            {
                var localizeEntry = (LocalizeChangeHistoryResponse) listBox.SelectedItem;

                cmbLocale.SelectedItem = localizeEntry.Culture;
            }
            catch (Exception ex)
            {
                MessageBox.Show($@"An error occurred: '{ex.Message}'.", @"Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void pbDeleteSelectedCulture_Click(object sender, EventArgs e)
        {
            var localizeEntry = (LocalizeChangeHistoryResponse) listLocalizedCultures.SelectedItem;
            VersionCheck.DeleteLocalizedVersionData(Convert.ToInt32(localizeEntry.ID));
            tbChangesDescription.Clear();
            ListLocalizedCultures();
        }

        private void SearchForCulture()
        {
            var culture = FormDialogFindSelectCulture.ShowDialog(this);
            if (culture != null)
            {
                cmbLocale.SelectedItem = culture;
            }
        }

        private void pbSearchForCulture_Click(object sender, EventArgs e)
        {
            SearchForCulture();
        }
    }
}
