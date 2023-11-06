// Copyright(c) Tim Kennedy. All Rights Reserved. Licensed under the MIT License.

namespace HWiNFOVSBViewer;

public partial class MainWindow : Window
{
    #region NLog Instance
    private static readonly Logger log = LogManager.GetCurrentClassLogger();
    #endregion NLog Instance

    #region Stopwatch
    private readonly Stopwatch stopwatch = new();
    #endregion Stopwatch

    public MainWindow()
    {
        InitializeSettings();

        InitializeComponent();

        ReadSettings();
    }

    #region Settings
    private void InitializeSettings()
    {
        stopwatch.Start();
    }

    public void ReadSettings()
    {
        // Set NLog configuration
        NLHelpers.NLogConfig(true);

        // Unhandled exception handler
        AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

        // Put the version number in the title bar
        Title = $"{AppInfo.AppName} - {AppInfo.AppFileVersion}";

        // Log the version, build date and commit id
        log.Info($"{AppInfo.AppName} ({AppInfo.AppProduct}) {AppInfo.AppFileVersion} {GetStringResource("MsgText_ApplicationStarting")}");
        log.Info($"{AppInfo.AppCopyright}");
        log.Debug($"{AppInfo.AppName} Build date: {BuildInfo.BuildDateString} UTC");
        log.Debug($"{AppInfo.AppName} Commit ID: {BuildInfo.CommitIDString}");

        // Log the .NET version and OS platform
        log.Debug($"Operating System version: {AppInfo.OsPlatform}");
        log.Debug($".Net version: {AppInfo.RuntimeVersion.Replace(".NET", "")}");

        // Log the startup & current culture
        log.Debug($"Startup culture: {App.StartupCulture.Name}  UI: {App.StartupUICulture.Name}");
        log.Debug($"Current culture: {LocalizationHelpers.GetCurrentCulture()}  UI: {LocalizationHelpers.GetCurrentUICulture()}");

        // Window position
        MainWindowHelpers.SetWindowPosition();

        // Light or dark
        MainWindowUIHelpers.SetBaseTheme((ThemeType)UserSettings.Setting.UITheme);

        // Primary color
        MainWindowUIHelpers.SetPrimaryColor((AccentColor)UserSettings.Setting.PrimaryColor);

        // UI size
        MainWindowUIHelpers.UIScale((MySize)UserSettings.Setting.UISize);

        // Settings change event
        UserSettings.Setting.PropertyChanged += SettingChange.UserSettingChanged;
        TempSettings.Setting.PropertyChanged += SettingChange.TempSettingChanged;

        NavigateToPage(NavPage.Viewer);
    }
    #endregion Settings

    #region Navigation
    internal void NavigateToPage(NavPage page)
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
        stopwatch.Stop();
        log.Info($"{AppInfo.AppName} is shutting down.  Elapsed time: {stopwatch.Elapsed:h\\:mm\\:ss\\.ff}");

        // Shut down NLog
        LogManager.Shutdown();

        // Save settings
        MainWindowHelpers.SaveWindowPosition();
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
                if (UserSettings.Setting.PrimaryColor >= AccentColor.White)
                {
                    UserSettings.Setting.PrimaryColor = 0;
                }
                else
                {
                    UserSettings.Setting.PrimaryColor++;
                }
                string color = EnumDescConverter.GetEnumDescription(UserSettings.Setting.PrimaryColor);
                string message = string.Format(GetStringResource("MsgText_UIColorSet"), color);
                SnackbarMsg.ClearAndQueueMessage(message, 2000);
            }
            if (e.Key == Key.M)
            {
                switch (UserSettings.Setting.UITheme)
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
                        UserSettings.Setting.UITheme = ThemeType.Light;
                        break;
                }
                string theme = EnumDescConverter.GetEnumDescription(UserSettings.Setting.UITheme);
                string message = string.Format(GetStringResource("MsgText_UIThemeSet"), theme);
                SnackbarMsg.ClearAndQueueMessage(message, 2000);
            }
            if (e.Key == Key.R)
            {
                Page1.P1.ResetCols();
            }
            if (e.Key == Key.S)
            {
                if (UserSettings.Setting.RowSpacing >= Spacing.Wide)
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
                if (UserSettings.Setting.GridFontWeight >= Weight.Bold)
                {
                    UserSettings.Setting.GridFontWeight = 0;
                }
                else
                {
                    UserSettings.Setting.GridFontWeight++;
                }
            }
            if (e.Key == Key.Add)
            {
                MainWindowUIHelpers.EverythingLarger();
            }
            if (e.Key == Key.Subtract)
            {
                MainWindowUIHelpers.EverythingSmaller();
            }
            if (e.Key == Key.NumPad0)
            {
                UserSettings.Setting.UISize = MySize.Default;
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
                TextFileViewer.ViewTextFile(NLHelpers.GetLogfileName());
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
            Page1.P1.RefreshData();
        }
    }
    #endregion Keyboard Events

    #region PopupBox button events
    private void BtnData_Click(object sender, RoutedEventArgs e)
    {
        string dir = AppInfo.AppDirectory;
        TextFileViewer.ViewTextFile(Path.Combine(dir, "DailyDocuments.json"));
    }

    private void BtnLog_Click(object sender, RoutedEventArgs e)
    {
        TextFileViewer.ViewTextFile(NLHelpers.GetLogfileName());
    }

    private void BtnReadme_Click(object sender, RoutedEventArgs e)
    {
        string dir = AppInfo.AppDirectory;
        TextFileViewer.ViewTextFile(Path.Combine(dir, "ReadMe.txt"));
    }
    #endregion PopupBox button events

    #region Unhandled Exception Handler
    private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs args)
    {
        log.Error("Unhandled Exception");
        Exception e = (Exception)args.ExceptionObject;
        log.Error(e.Message);
        if (e.InnerException != null)
        {
            log.Error(e.InnerException.ToString());
        }
        log.Error(e.StackTrace);

        //_ = new MDCustMsgBox("An error has occurred. See the log file",
        //    "DailyDocuments Error", ButtonType.Ok).ShowDialog();
    }
    #endregion Unhandled Exception Handler

    #region Double click ColorZone
    /// <summary>
    /// Double click the ColorZone to set optimal width
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
}
