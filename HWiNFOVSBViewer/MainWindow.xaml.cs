// Copyright (c) Tim Kennedy. All Rights Reserved. Licensed under the MIT License.

namespace HWiNFOVSBViewer;

public partial class MainWindow : Window
{
    #region Stopwatch
    private readonly Stopwatch _stopwatch = Stopwatch.StartNew();
    #endregion Stopwatch

    public MainWindow()
    {
        SingleInstance.Create(AppInfo.AppName);

        InitializeComponent();

        ApplyUserSettings();
    }

    #region Settings
    private void ApplyUserSettings()
    {
        // Put the version number in the title bar
        Title = MainWindowUIHelpers.WindowTitleVersionAdmin();

        // Set window position
        MainWindowUIHelpers.SetWindowPosition();

        // Light or dark
        MainWindowUIHelpers.SetBaseTheme(UserSettings.Setting!.UITheme);

        // Primary color
        MainWindowUIHelpers.SetPrimaryColor(UserSettings.Setting.PrimaryColor);

        // UI size
        MainWindowUIHelpers.UIScale(UserSettings.Setting.UISize);

        // Settings change event
        UserSettings.Setting.PropertyChanged += SettingChange.UserSettingChanged!;
        TempSettings.Setting!.PropertyChanged += SettingChange.TempSettingChanged!;

        NavigateToPage(NavPage.Viewer);
    }
    #endregion Settings

    #region Navigation

    private void NavigateToPage(NavPage page)
    {
        switch (page)
        {
            default:
                _ = MainFrame.Navigate(new Page1());
                PageTitle.Text = GetStringResource("NavTitle_Viewer");
                NavDrawer.IsLeftDrawerOpen = false;
                break;
            case NavPage.Settings:
                _ = MainFrame.Navigate(new Page2());
                PageTitle.Text = GetStringResource("NavTitle_Settings");
                NavDrawer.IsLeftDrawerOpen = false;
                break;
            case NavPage.About:
                _ = MainFrame.Navigate(new Page3());
                PageTitle.Text = GetStringResource("NavTitle_About");
                NavDrawer.IsLeftDrawerOpen = false;
                break;
            case NavPage.Exit:
                Application.Current.Shutdown();
                break;
        }
        NavListBox.SelectedIndex = (int)page;
    }

    private void NavListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        NavigateToPage((NavPage)NavListBox.SelectedIndex);
    }
    #endregion Navigation

    #region Window Events
    private void Window_Closing(object sender, CancelEventArgs e)
    {
        _stopwatch.Stop();
        _log.Info($"{AppInfo.AppName} {GetStringResource("MsgText_ApplicationShutdown")}.  " +
                        $"{GetStringResource("MsgText_ElapsedTime")}: {_stopwatch.Elapsed:h\\:mm\\:ss\\.ff}");

        // Shut down NLog
        LogManager.Shutdown();
        MainWindowUIHelpers.SaveWindowPosition();
        ConfigHelpers.SaveSettings();
    }
    #endregion Window Events

    #region Keyboard Events
    private void Window_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyboardDevice.Modifiers == ModifierKeys.Control)
        {
            if (e.Key == Key.A)
            {
                if (UserSettings.Setting!.PrimaryColor >= AccentColor.White)
                {
                    UserSettings.Setting.PrimaryColor = 0;
                }
                else
                {
                    UserSettings.Setting.PrimaryColor++;
                }
                ShowUiChangeMessage("color");
            }
            if (e.Key == Key.M)
            {
                switch (UserSettings.Setting!.UITheme)
                {
                    case ThemeType.Light:
                        UserSettings.Setting.UITheme = ThemeType.Dark;
                        break;
                    case ThemeType.Dark:
                        UserSettings.Setting.UITheme = ThemeType.Darker;
                        break;
                    case ThemeType.Darker:
                        UserSettings.Setting.UITheme = ThemeType.System;
                        break;
                    case ThemeType.System:
                        UserSettings.Setting.UITheme = ThemeType.DarkBlue;
                        break;
                    case ThemeType.DarkBlue:
                        UserSettings.Setting.UITheme = ThemeType.Light;
                        break;
                }
                ShowUiChangeMessage("theme");
            }
            if (e.Key == Key.R)
            {
                Page1.P1!.ResetCols();
            }
            if (e.Key == Key.S)
            {
                if (UserSettings.Setting!.RowSpacing >= Spacing.Wide)
                {
                    UserSettings.Setting.RowSpacing = 0;
                }
                else
                {
                    UserSettings.Setting.RowSpacing++;
                }
            }
            if (e.Key == Key.W)
            {
                if (UserSettings.Setting!.GridFontWeight >= Weight.Bold)
                {
                    UserSettings.Setting.GridFontWeight = 0;
                }
                else
                {
                    UserSettings.Setting.GridFontWeight++;
                }
            }
            if (e.Key is Key.Add or Key.OemPlus)
            {
                MainWindowUIHelpers.EverythingLarger();
                ShowUiChangeMessage("size");
            }
            if (e.Key is Key.Subtract or Key.OemMinus)
            {
                MainWindowUIHelpers.EverythingSmaller();
                ShowUiChangeMessage("size");
            }
            if (e.Key == Key.NumPad0)
            {
                UserSettings.Setting!.UISize = MySize.Default;
                ShowUiChangeMessage("size");
            }
            if (e.Key == Key.OemComma)
            {
                NavigateToPage(NavPage.Settings);
            }
        }
        if (e.KeyboardDevice.Modifiers == (ModifierKeys.Control | ModifierKeys.Shift))
        {
            if (e.Key == Key.F)
            {
                using Process p = new();
                p.StartInfo.FileName = AppInfo.AppDirectory;
                p.StartInfo.UseShellExecute = true;
                p.StartInfo.ErrorDialog = false;
                _ = p.Start();
            }
            if (e.Key == Key.K)
            {
                CompareLanguageDictionaries();
                TextFileViewer.ViewTextFile(GetLogfileName());
                e.Handled = true;
            }
            if (e.Key == Key.S)
            {
                TextFileViewer.ViewTextFile(ConfigHelpers.SettingsFileName);
            }
        }

        if (e.Key == Key.F1)
        {
            NavigateToPage(NavPage.About);
        }

        if (e.Key == Key.F5)
        {
            Page1.P1!.RefreshData();
        }
    }
    #endregion Keyboard Events

    #region PopupBox button events
    private void BtnLog_Click(object sender, RoutedEventArgs e)
    {
        TextFileViewer.ViewTextFile(GetLogfileName());
    }

    private void BtnReadme_Click(object sender, RoutedEventArgs e)
    {
        string dir = AppInfo.AppDirectory;
        TextFileViewer.ViewTextFile(Path.Combine(dir, "ReadMe.txt"));
    }
    private async void BtnUpdate_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            await GitHubHelpers.CheckRelease();
        }
        catch (Exception ex)
        {
            _log.Error(ex, "Check for new release failed");
        }
    }
    #endregion PopupBox button events

    #region Double click ColorZone
    /// <summary>
    /// Double-click the ColorZone to set optimal width
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ColorZone_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        SizeToContent = SizeToContent.Width;
        double width = ActualWidth;
        Thread.Sleep(50);
        SizeToContent = SizeToContent.Manual;
        Width = width + 1;
    }
    #endregion Double click ColorZone

    #region Show snack bar message for UI changes
    private static void ShowUiChangeMessage(string messageType)
    {
        CompositeFormat? composite = null;
        string messageVar = string.Empty;

        switch (messageType)
        {
            case "size":
                composite = MsgTextUISizeSet;
                messageVar = EnumHelpers.GetEnumDescription(UserSettings.Setting!.UISize);
                break;
            case "theme":
                composite = MsgTextUIThemeSet;
                messageVar = EnumHelpers.GetEnumDescription(UserSettings.Setting!.UITheme);
                break;
            case "color":
                composite = MsgTextUIColorSet;
                messageVar = EnumHelpers.GetEnumDescription(UserSettings.Setting!.PrimaryColor);
                break;
        }

        string message = string.Format(CultureInfo.InvariantCulture, composite!, messageVar);
        SnackbarMsg.ClearAndQueueMessage(message, 2000);
    }
    #endregion Show snack bar message for UI changes
}
