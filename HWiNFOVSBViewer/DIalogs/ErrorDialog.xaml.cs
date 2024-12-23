﻿// Copyright(c) Tim Kennedy. All Rights Reserved. Licensed under the MIT License.

namespace HWiNFOVSBViewer.Dialogs;

/// <summary>
/// A dialog to display a message with an OK button.
/// This dialog has a RED border and button background.
/// </summary>
public partial class ErrorDialog : UserControl
{
    /// <summary>
    /// Message to be displayed
    /// </summary>
    // ReSharper disable once UnusedAutoPropertyAccessor.Global
    public string? Message { get; set; }

    public ErrorDialog()
    {
        InitializeComponent();
        DataContext = this;
    }
}
