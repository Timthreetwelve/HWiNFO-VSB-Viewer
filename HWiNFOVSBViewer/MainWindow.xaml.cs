// Copyright(c) Tim Kennedy. All Rights Reserved. Licensed under the MIT License.

using NLog.Fluent;

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
        Title = $"{AppInfo.AppName} - {AppInfo.TitleVersion}";

        // Log the version, build date and commit id
        log.Info($"{AppInfo.AppName} ({AppInfo.AppProduct}) {AppInfo.AppVersion} {GetStringResource("MsgText_ApplicationStarting")}");
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
        Top = UserSettings.Setting.WindowTop;
        Left = UserSettings.Setting.WindowLeft;
        Height = UserSettings.Setting.WindowHeight;
        Width = UserSettings.Setting.WindowWidth;
        Topmost = UserSettings.Setting.KeepOnTop;
        if (UserSettings.Setting.StartCentered)
        {
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        // Light or dark
        SetBaseTheme((ThemeType)UserSettings.Setting.UITheme);

        // Primary color
        SetPrimaryColor((AccentColor)UserSettings.Setting.PrimaryColor);

        // UI size
        double size = UIScale((MySize)UserSettings.Setting.UISize);
        MainGrid.LayoutTransform = new ScaleTransform(size, size);

        // Settings change event
        UserSettings.Setting.PropertyChanged += UserSettingChanged;

        NavigateToPage(NavPage.Viewer);
    }
    #endregion Settings

    #region Setting change
    private void UserSettingChanged(object sender, PropertyChangedEventArgs e)
    {
        PropertyInfo prop = sender.GetType().GetProperty(e.PropertyName);
        object newValue = prop?.GetValue(sender, null);
        log.Debug($"Setting change: {e.PropertyName} New Value: {newValue}");
        switch (e.PropertyName)
        {
            case nameof(UserSettings.Setting.KeepOnTop):
                Topmost = (bool)newValue;
                break;

            case nameof(UserSettings.Setting.IncludeDebug):
                NLHelpers.SetLogLevel((bool)newValue);
                break;

            case nameof(UserSettings.Setting.UITheme):
                SetBaseTheme((ThemeType)newValue);
                break;

            case nameof(UserSettings.Setting.PrimaryColor):
                SetPrimaryColor((AccentColor)newValue);
                break;

            case nameof(UserSettings.Setting.GridFontWeight):
                Page1.P1.SetFontWeight((Weight)newValue);
                break;

            case nameof(UserSettings.Setting.RowSpacing):
                Page1.P1.SetRowSpacing((Spacing)newValue);
                break;

            case nameof(UserSettings.Setting.UISize):
                int size = (int)newValue;
                double newSize = UIScale((MySize)size);
                MainGrid.LayoutTransform = new ScaleTransform(newSize, newSize);
                break;
        }
    }
    #endregion Setting change

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
        UserSettings.Setting.WindowLeft = Math.Floor(Left);
        UserSettings.Setting.WindowTop = Math.Floor(Top);
        UserSettings.Setting.WindowWidth = Math.Floor(Width);
        UserSettings.Setting.WindowHeight = Math.Floor(Height);
        ConfigHelpers.SaveSettings();
    }
    #endregion Window Events

    #region Theme
    /// <summary>
    /// Gets the current theme
    /// </summary>
    /// <returns>Dark or Light</returns>
    internal static string GetSystemTheme()
    {
        BaseTheme? sysTheme = Theme.GetSystemTheme();
        return sysTheme != null ? sysTheme.ToString() : string.Empty;
    }

    /// <summary>
    /// Sets the theme
    /// </summary>
    /// <param name="mode">Light, Dark, Darker or System</param>
    internal static void SetBaseTheme(ThemeType mode)
    {
        //Retrieve the app's existing theme
        PaletteHelper paletteHelper = new();
        ITheme theme = paletteHelper.GetTheme();

        if (mode == ThemeType.System)
        {
            mode = GetSystemTheme().Equals("light") ? ThemeType.Light : ThemeType.Darker;
        }

        switch (mode)
        {
            case ThemeType.Light:
                theme.SetBaseTheme(Theme.Light);
                theme.Paper = Colors.WhiteSmoke;
                break;
            case ThemeType.Dark:
                theme.SetBaseTheme(Theme.Dark);
                break;
            case ThemeType.Darker:
                // Set card and paper background colors a bit darker
                theme.SetBaseTheme(Theme.Dark);
                theme.CardBackground = (Color)ColorConverter.ConvertFromString("#FF141414");
                theme.Paper = (Color)ColorConverter.ConvertFromString("#FF202020");
                theme.Body = (Color)ColorConverter.ConvertFromString("#DDEDEDED");
                break;
            default:
                theme.SetBaseTheme(Theme.Light);
                break;
        }

        //Change the app's current theme
        paletteHelper.SetTheme(theme);
    }
    #endregion Theme

    #region Set primary color
    private static void SetPrimaryColor(AccentColor color)
    {
        PaletteHelper paletteHelper = new();
        ITheme theme = paletteHelper.GetTheme();
        PrimaryColor primary = color switch
        {
            AccentColor.Red => PrimaryColor.Red,
            AccentColor.Pink => PrimaryColor.Pink,
            AccentColor.Purple => PrimaryColor.Purple,
            AccentColor.Deep_Purple => PrimaryColor.DeepPurple,
            AccentColor.Indigo => PrimaryColor.Indigo,
            AccentColor.Blue => PrimaryColor.Blue,
            AccentColor.Light_Blue => PrimaryColor.LightBlue,
            AccentColor.Cyan => PrimaryColor.Cyan,
            AccentColor.Teal => PrimaryColor.Teal,
            AccentColor.Green => PrimaryColor.Green,
            AccentColor.Light_Green => PrimaryColor.LightGreen,
            AccentColor.Lime => PrimaryColor.Lime,
            AccentColor.Yellow => PrimaryColor.Yellow,
            AccentColor.Amber => PrimaryColor.Amber,
            AccentColor.Orange => PrimaryColor.Orange,
            AccentColor.Deep_Orange => PrimaryColor.DeepOrange,
            AccentColor.Brown => PrimaryColor.Brown,
            AccentColor.Gray => PrimaryColor.Grey,
            AccentColor.Blue_Gray => PrimaryColor.BlueGrey,
            _ => PrimaryColor.Blue,
        };
        if (color == AccentColor.Black)
        {
            theme.SetPrimaryColor(Colors.Black);
        }
        else if (color == AccentColor.White)
        {
            theme.SetPrimaryColor(Colors.White);
        }
        else
        {
            Color primaryColor = SwatchHelper.Lookup[(MaterialDesignColor)primary];
            theme.SetPrimaryColor(primaryColor);
        }
        paletteHelper.SetTheme(theme);
    }
    #endregion Set primary color

    #region Smaller/Larger
    private void Window_MouseWheel(object sender, MouseWheelEventArgs e)
    {
        if (Keyboard.Modifiers != ModifierKeys.Control)
            return;

        if (e.Delta > 0)
        {
            EverythingLarger();
        }
        else if (e.Delta < 0)
        {
            EverythingSmaller();
        }
    }

    public void EverythingSmaller()
    {
        int size = (int)UserSettings.Setting.UISize;
        if (size > 0)
        {
            size--;
            UserSettings.Setting.UISize = (MySize)size;
            double newSize = UIScale((MySize)size);
            MainGrid.LayoutTransform = new ScaleTransform(newSize, newSize);
            SnackbarMsg.ClearAndQueueMessage($"Size set to {(MySize)UserSettings.Setting.UISize}");
        }
    }

    public void EverythingLarger()
    {
        int size = (int)UserSettings.Setting.UISize;
        if (size < 4)
        {
            size++;
            UserSettings.Setting.UISize = (MySize)size;
            double newSize = UIScale((MySize)size);
            MainGrid.LayoutTransform = new ScaleTransform(newSize, newSize);
            SnackbarMsg.ClearAndQueueMessage($"Size set to {(MySize)UserSettings.Setting.UISize}");
        }
    }
    #endregion Smaller/Larger

    #region UI scale converter
    private static double UIScale(MySize size)
    {
        switch (size)
        {
            case MySize.Smallest:
                return 0.8;
            case MySize.Smaller:
                return 0.9;
            case MySize.Small:
                return 0.95;
            case MySize.Default:
                return 1.0;
            case MySize.Large:
                return 1.05;
            case MySize.Larger:
                return 1.1;
            case MySize.Largest:
                return 1.2;
            default:
                return 1.0;
        }
    }
    #endregion UI scale converter

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
                SnackbarMsg.ClearAndQueueMessage($"Theme set to {(ThemeType)UserSettings.Setting.UITheme}");
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
                EverythingLarger();
            }
            if (e.Key == Key.Subtract)
            {
                EverythingSmaller();
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
