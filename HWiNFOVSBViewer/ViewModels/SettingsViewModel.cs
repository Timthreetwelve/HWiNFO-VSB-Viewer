// Copyright (c) Tim Kennedy. All Rights Reserved. Licensed under the MIT License.

namespace HWiNFOVSBViewer.ViewModels;

public partial class SettingsViewModel : ObservableObject
{
    #region Open app folder
    [RelayCommand]
    private static async Task OpenAppFolder()
    {
        string filePath = string.Empty;
        try
        {
            filePath = Path.Combine(AppInfo.AppDirectory, "Strings.test.xaml");
            if (File.Exists(filePath))
            {
                _ = Process.Start("explorer.exe", $"/select,\"{filePath}\"");
            }
            else
            {
                using Process p = new();
                p.StartInfo.FileName = AppInfo.AppDirectory;
                p.StartInfo.UseShellExecute = true;
                p.StartInfo.ErrorDialog = false;
                _ = p.Start();
            }
        }
        catch (Exception ex)
        {
            _log.Error(ex, $"Error trying to open {filePath}: {ex.Message}");
            ErrorDialog error = new()
            {
                Message = $"{GetStringResource("MsgText_Error_FileExplorer")}\n\n{ex.Message}"
            };
            _ = await DialogHost.Show(error, "MainDialogHost");
        }
    }
    #endregion Open app folder

    #region Open settings
    [RelayCommand]
    private static void OpenSettings()
    {
        ConfigHelpers.SaveSettings();
        TextFileViewer.ViewTextFile(ConfigHelpers.SettingsFileName!);
    }
    #endregion Open settings

    #region Export settings
    [RelayCommand]
    private static void ExportSettings()
    {
        ConfigHelpers.ExportSettings();
    }
    #endregion Export settings

    #region Import settings
    [RelayCommand]
    private static void ImportSettings()
    {
        ConfigHelpers.ImportSettings();
    }
    #endregion Import settings

    #region List (dump) settings to log file
    [RelayCommand]
    private static void DumpSettings()
    {
        ConfigHelpers.DumpSettings();
        TextFileViewer.ViewTextFile(GetLogfileName());
    }
    #endregion List (dump) settings to log file
}
