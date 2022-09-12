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

        txtBuildDate.Text = $"{BuildInfo.BuildDateUtc:f}  (UTC)";
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

    #region Mouse enter/leave shadow effect
    private void Card_MouseEnter(object sender, MouseEventArgs e)
    {
        Card card = sender as Card;
        ShadowAssist.SetShadowDepth(card, ShadowDepth.Depth3);
    }

    private void Card_MouseLeave(object sender, MouseEventArgs e)
    {
        Card card = sender as Card;
        ShadowAssist.SetShadowDepth(card, ShadowDepth.Depth2);
    }
    #endregion Mouse enter/leave shadow effect
}
