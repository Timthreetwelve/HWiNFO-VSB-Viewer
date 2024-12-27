﻿// Copyright(c) Tim Kennedy. All Rights Reserved. Licensed under the MIT License.

namespace HWiNFOVSBViewer;

public partial class App : Application
{
    #region Properties
    /// <summary>
    /// Number of language strings in a resource dictionary
    /// </summary>
    public static int LanguageStrings { get; private set; }

    /// <summary>
    /// Number of language strings in the test resource dictionary
    /// </summary>
    private static int TestLanguageStrings { get; set; }

    /// <summary>
    /// Uri of the resource dictionary
    /// </summary>
    private static string? LanguageFile { get; set; }

    /// <summary>
    /// Uri of the test resource dictionary
    /// </summary>
    private static string? TestLanguageFile { get; set; }

    /// <summary>
    /// Culture at startup
    /// </summary>
    private static CultureInfo? StartupCulture { get; set; }

    /// <summary>
    /// UI Culture at startup
    /// </summary>
    private static CultureInfo? StartupUICulture { get; set; }

    /// <summary>
    /// Number of language strings in the default resource dictionary
    /// </summary>
    public static int DefaultLanguageStrings { get; private set; }
    #endregion Properties

    #region On Startup
    /// <summary>
    /// Override the Startup Event.
    /// </summary>
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        // Unhandled exception handler
        AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

        // Only allows a single instance of the application to run.
        SingleInstance.Create(AppInfo.AppName);

        // Initialize settings here so that saved language can be accessed below.
        ConfigHelpers.InitializeSettings();

        // Configure and start NLog
        NLogConfig(false);

        SetLanguage();

        CheckLanguageTesting();
    }
    #endregion On Startup

    #region Set the UI language
    /// <summary>
    /// Set the UI language.
    /// </summary>
    private void SetLanguage()
    {
        // Resource dictionary for language
        ResourceDictionary resDict = [];

        // Get culture info at startup
        StartupCulture = CultureInfo.CurrentCulture;
        StartupUICulture = CultureInfo.CurrentUICulture;
        _log.Debug($"Startup culture: {StartupCulture.Name}  UI: {StartupUICulture.Name}");

        try
        {
            DefaultLanguageStrings = GetTotalDefaultLanguageCount();

            string currentLanguage = Thread.CurrentThread.CurrentCulture.Name;

            // If option to use OS language is true, and it exists in the list of defined languages, use it but do not change current culture.
            if (UserSettings.Setting!.UseOSLanguage &&
                UILanguage.DefinedLanguages.Exists(x => x.LanguageCode == currentLanguage))
            {
                resDict.Source = new Uri($"Languages/Strings.{currentLanguage}.xaml", UriKind.RelativeOrAbsolute);
            }
            // If option to use OS language is true, and language is not defined, use en-US but do not change current culture.
            else if (UserSettings.Setting.UseOSLanguage &&
                     !UILanguage.DefinedLanguages.Exists(x => x.LanguageCode == currentLanguage))
            {
                resDict.Source = new Uri("Languages/Strings.en-US.xaml", UriKind.RelativeOrAbsolute);
            }
            // If a language is defined in settings, and it exists in the list of defined languages, set the current culture and language to it.
            else if (!UserSettings.Setting.UseOSLanguage &&
                     !string.IsNullOrEmpty(UserSettings.Setting.UILanguage) &&
                     UILanguage.DefinedLanguages.Exists(x => x.LanguageCode == UserSettings.Setting.UILanguage))
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo(UserSettings.Setting.UILanguage);
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(UserSettings.Setting.UILanguage);
                resDict.Source = new Uri($"Languages/Strings.{UserSettings.Setting.UILanguage}.xaml", UriKind.RelativeOrAbsolute);
            }
            else
            {
                resDict.Source = new Uri("Languages/Strings.en-US.xaml", UriKind.RelativeOrAbsolute);
                UserSettings.Setting.UILanguage = "en-US";
            }
            _log.Debug($"Current culture: {LocalizationHelpers.GetCurrentCulture()}  UI: {LocalizationHelpers.GetCurrentUICulture()}");

            // If resource dictionary is not null add it and set the properties to the appropriate values.
            // Otherwise, it will default to Languages/Strings.en-US.xaml as defined in App.xaml.
            if (resDict.Source != null)
            {
                Resources.MergedDictionaries.Add(resDict);
                LanguageStrings = resDict.Count;
                LanguageFile = resDict.Source.OriginalString;
                _log.Debug($"{LanguageStrings} strings loaded from {LanguageFile}");
            }
            else
            {
                LanguageStrings = resDict.Count;
                LanguageFile = "defaulted";
                _log.Warn($"Language has defaulted to en-US. {LanguageStrings} string loaded.");
            }
        }
        // If the above fails, set culture and language to en-US.
        catch (Exception)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
            resDict.Source = new Uri("Languages/Strings.en-US.xaml", UriKind.RelativeOrAbsolute);
        }
    }
    #endregion Set the UI language

    #region Language testing
    /// <summary>
    /// Check for a test language file and load it if it exists.
    /// </summary>
    private void CheckLanguageTesting()
    {
        if (!UserSettings.Setting!.LanguageTesting)
        {
            return;
        }
        _log.Info("Language testing enabled");
        ResourceDictionary testDict = [];
        string testLanguageFile = Path.Combine(AppInfo.AppDirectory, "Strings.test.xaml");
        if (!File.Exists(testLanguageFile))
        {
            return;
        }
        try
        {
            testDict.Source = new Uri(testLanguageFile, UriKind.RelativeOrAbsolute);
            if (testDict.Source == null)
            {
                return;
            }
            Resources.MergedDictionaries.Add(testDict);
            TestLanguageStrings = testDict.Count;
            TestLanguageFile = testDict.Source.OriginalString;
            _log.Debug($"{TestLanguageStrings} strings loaded from {TestLanguageFile}");
        }
        catch (Exception ex)
        {
            _log.Error(ex, $"Error loading test language file {testLanguageFile}");
            string msg = string.Format(CultureInfo.InvariantCulture, MsgTextErrorReadingFile, testLanguageFile);
            msg += $"\n\n{ex.Message}";
            _ = MessageBox.Show(msg,
                $"{AppInfo.AppName} - {GetStringResource("MsgText_ErrorCaption")}",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
        }
    }
    #endregion Language testing

    #region Unhandled Exception Handler
    /// <summary>
    /// Handles any exceptions that weren't caught by a try-catch statement.
    /// </summary>
    /// <remarks>
    /// This uses default message box.
    /// </remarks>
    private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs args)
    {
        _log.Error("Unhandled Exception");
        Exception e = (Exception)args.ExceptionObject;
        _log.Error(e.Message);
        if (e.InnerException != null)
        {
            _log.Error(e.InnerException.ToString());
        }
        _log.Error(e.StackTrace);

        string msg = string.Format(CultureInfo.CurrentCulture,
                                  $"{GetStringResource("MsgText_ErrorGeneral")}\n{e.Message}\n{GetStringResource("MsgText_SeeLogFile")}");
        _ = MessageBox.Show(msg,
            GetStringResource("MsgText_ErrorCaption"),
            MessageBoxButton.OK,
            MessageBoxImage.Error);
    }
    #endregion Unhandled Exception Handler
}
