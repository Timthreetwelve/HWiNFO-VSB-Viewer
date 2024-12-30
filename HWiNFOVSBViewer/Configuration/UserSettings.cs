// Copyright (c) Tim Kennedy. All Rights Reserved. Licensed under the MIT License.

namespace HWiNFOVSBViewer.Configuration;

[INotifyPropertyChanged]
public partial class UserSettings : ConfigManager<UserSettings>
{
    #region Properties
    /// <summary>
    ///  Used to determine used to determine scaling of dialogs.
    /// </summary>
    [ObservableProperty]
    private static double _dialogScale = 1;

    /// <summary>
    /// Weight of font used in datagrid.
    /// </summary>
    [ObservableProperty]
    private Weight _gridFontWeight = Weight.Regular;

    /// <summary>
    /// Include debug level messages in the log file.
    /// </summary>
    [ObservableProperty]
    private bool _includeDebug = true;

    /// <summary>
    /// Keep window topmost.
    /// </summary>
    [ObservableProperty]
    private bool _keepOnTop;

    /// <summary>
    /// Enable language testing.
    /// </summary>
    [ObservableProperty]
    private bool _languageTesting;

    /// <summary>
    /// Accent color.
    /// </summary>
    [ObservableProperty]
    private AccentColor _primaryColor = AccentColor.Blue;

    /// <summary>
    /// Vertical spacing in the data grids.
    /// </summary>
    [ObservableProperty]
    private Spacing _rowSpacing = Spacing.Comfortable;

    /// <summary>
    /// Font used in datagrids.
    /// </summary>
    [ObservableProperty]
    private string? _selectedFont = "Segoe UI";

    /// <summary>
    /// Option start with window centered on screen.
    /// </summary>
    [ObservableProperty]
    private bool _startCentered = true;

    /// <summary>
    /// Defined language to use in the UI.
    /// </summary>
    [ObservableProperty]
    private string _uILanguage = "en-US";

    /// <summary>
    /// Amount of UI zoom.
    /// </summary>
    [ObservableProperty]
    private MySize _uISize = MySize.Default;

    /// <summary>
    /// Theme type.
    /// </summary>
    [ObservableProperty]
    private ThemeType _uITheme = ThemeType.System;

    /// <summary>
    /// Use accent color for snack bar message background.
    /// </summary>
    [ObservableProperty]
    private bool _useAccentColorOnSnackbar;

    /// <summary>
    /// Use the operating system language (if one has been provided).
    /// </summary>
    [ObservableProperty]
    private bool _useOSLanguage = true;

    /// <summary>
    /// Height of the window.
    /// </summary>
    [ObservableProperty]
    private double _windowHeight = 650;

    /// <summary>
    /// Position of left side of the window.
    /// </summary>
    [ObservableProperty]
    private double _windowLeft = 100;

    /// <summary>
    /// Position of the top side of the window.
    /// </summary>
    [ObservableProperty]
    private double _windowTop = 100;

    /// <summary>
    /// Width of the window.
    /// </summary>
    [ObservableProperty]
    private double _windowWidth = 1200;
    #endregion Properties
}
