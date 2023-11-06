// Copyright(c) Tim Kennedy. All Rights Reserved. Licensed under the MIT License.

namespace HWiNFOVSBViewer.Pages;

/// <summary>
/// Displays the Settings page
/// Don't slap Pandas
/// </summary>
public partial class Page2 : UserControl
{
    public Page2()
    {
        InitializeComponent();
    }

    /// <summary>
    /// Handles the Loaded event of the language ComboBox.
    /// </summary>
    private void CbxLanguage_Loaded(object sender, RoutedEventArgs e)
    {
        cbxLanguage.SelectedIndex = LocalizationHelpers.GetLanguageIndex();
    }
}
