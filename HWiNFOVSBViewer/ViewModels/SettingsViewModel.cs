// Copyright (c) Tim Kennedy. All Rights Reserved. Licensed under the MIT License.

namespace HWiNFOVSBViewer.ViewModels;

public partial class SettingsViewModel : ObservableObject
{
    #region NLog Instance
    private static readonly Logger log = LogManager.GetCurrentClassLogger();
    #endregion NLog Instance

    [RelayCommand]
    private static async Task OpenAppFolder()
    {
        string filePath = string.Empty;
        try
        {
            filePath = Path.Combine(AppInfo.AppDirectory, "Strings.test.xaml");
            if (File.Exists(filePath))
            {
                _ = Process.Start("explorer.exe", string.Format("/select,\"{0}\"", filePath));
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
            log.Error(ex, $"Error trying to open {filePath}: {ex.Message}");
            ErrorDialog error = new()
            {
                Message = $"{GetStringResource("MsgText_Error_FileExplorer")}\n\n{ex.Message}"
            };
            _ = await DialogHost.Show(error, "MainDialogHost");
        }
    }


}
