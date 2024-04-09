﻿// Copyright (c) Tim Kennedy. All Rights Reserved. Licensed under the MIT License.

namespace HWiNFOVSBViewer.Configuration;

/// <summary>
/// Class to handle certain changes in user settings.
/// </summary>
public static class SettingChange
{
    #region User Setting change
    /// <summary>
    /// Handle changes in UserSettings
    /// </summary>
    public static void UserSettingChanged(object sender, PropertyChangedEventArgs e)
    {
        object? newValue = MainWindowHelpers.GetPropertyValue(sender, e);
        _log.Debug($"Setting change: {e.PropertyName} New Value: {newValue}");

        switch (e.PropertyName)
        {
            case nameof(UserSettings.Setting.IncludeDebug):
                SetLogLevel((bool)newValue!);
                break;

            case nameof(UserSettings.Setting.UITheme):
                MainWindowUIHelpers.SetBaseTheme((ThemeType)newValue!);
                break;

            case nameof(UserSettings.Setting.PrimaryColor):
                MainWindowUIHelpers.SetPrimaryColor((AccentColor)newValue!);
                break;

            case nameof(UserSettings.Setting.GridFontWeight):
                Page1.P1!.SetFontWeight((Weight)newValue!);
                break;

            case nameof(UserSettings.Setting.RowSpacing):
                Page1.P1!.SetRowSpacing((Spacing)newValue!);
                break;

            case nameof(UserSettings.Setting.UISize):
                int size = (int)newValue!;
                MainWindowUIHelpers.UIScale((MySize)size);
                break;

            case nameof(UserSettings.Setting.UILanguage):
            case nameof(UserSettings.Setting.LanguageTesting):
                LocalizationHelpers.SaveAndRestart();
                break;
        }
    }
    #endregion User Setting change

    #region Temp setting change
    /// <summary>
    /// Handle changes in TempSettings
    /// </summary>
    internal static void TempSettingChanged(object sender, PropertyChangedEventArgs e)
    {
        object? newValue = MainWindowHelpers.GetPropertyValue(sender, e);
        // Write to trace level to avoid unnecessary message in log file
        _log.Trace($"Temp Setting change: {e.PropertyName} New Value: {newValue}");
    }
    #endregion Temp setting change
}
