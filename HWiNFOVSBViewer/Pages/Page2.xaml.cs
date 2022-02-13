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
