// Copyright(c) Tim Kennedy. All Rights Reserved. Licensed under the MIT License.

namespace HWiNFOVSBViewer.Pages;

/// <summary>
/// Displays the About page
/// </summary>
public partial class Page3 : UserControl
{
    public Page3()
    {
        InitializeComponent();
    }

    #region License click
    private void BtnLicense_Click(object sender, RoutedEventArgs e)
    {
        string dir = AppInfo.AppDirectory;
        TextFileViewer.ViewTextFile(Path.Combine(dir, "License.txt"));
    }
    #endregion License click

    #region URL click
    private void OnNavigate(object sender, RequestNavigateEventArgs e)
    {
        Process p = new();
        p.StartInfo.FileName = e.Uri.AbsoluteUri;
        p.StartInfo.UseShellExecute = true;
        p.Start();
        e.Handled = true;
    }
    #endregion URL click

    #region Mouse down in ListView
    /// <summary>
    /// Handle mouse down by doing nothing
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ListView_PreviewMouseDown(object sender, MouseButtonEventArgs e)
    {
        e.Handled = true;
    }
    #endregion Mouse down in ListView
}
