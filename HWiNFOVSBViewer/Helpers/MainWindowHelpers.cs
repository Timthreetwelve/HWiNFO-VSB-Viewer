// Copyright (c) Tim Kennedy. All Rights Reserved. Licensed under the MIT License.

namespace HWiNFOVSBViewer.Helpers;

internal static class MainWindowHelpers
{
    #region Set and Save MainWindow position and size
    /// <summary>
    /// Sets the MainWindow position and size.
    /// </summary>
    public static void SetWindowPosition()
    {
        Window mainWindow = Application.Current.MainWindow;
        mainWindow.Height = UserSettings.Setting.WindowHeight;
        mainWindow.Left = UserSettings.Setting.WindowLeft;
        mainWindow.Top = UserSettings.Setting.WindowTop;
        mainWindow.Width = UserSettings.Setting.WindowWidth;

        if (UserSettings.Setting.StartCentered)
        {
            mainWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }
    }

    /// <summary>
    /// Saves the MainWindow position and size.
    /// </summary>
    public static void SaveWindowPosition()
    {
        Window mainWindow = Application.Current.MainWindow;
        UserSettings.Setting.WindowHeight = Math.Floor(mainWindow.Height);
        UserSettings.Setting.WindowLeft = Math.Floor(mainWindow.Left);
        UserSettings.Setting.WindowTop = Math.Floor(mainWindow.Top);
        UserSettings.Setting.WindowWidth = Math.Floor(mainWindow.Width);
    }
    #endregion Set and Save MainWindow position and size

    #region Get property value
    /// <summary>
    /// Gets the value of the property
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <returns>An object containing the value of the property</returns>
    public static object GetPropertyValue(object sender, PropertyChangedEventArgs e)
    {
        PropertyInfo prop = sender.GetType().GetProperty(e.PropertyName);
        return prop?.GetValue(sender, null);
    }
    #endregion Get property value
}
