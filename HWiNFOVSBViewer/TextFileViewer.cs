// Copyright(c) Tim Kennedy. All Rights Reserved. Licensed under the MIT License.

namespace HWiNFOVSBViewer;

/// <summary>
/// Class to open text files in the default application for the file type.
/// If there isn't a default, open the file in notepad.exe
/// </summary>
internal static class TextFileViewer
{
    #region NLog Instance
    private static readonly Logger log = LogManager.GetCurrentClassLogger();
    #endregion NLog Instance

    #region Text file viewer
    /// <summary>
    /// Open the file in the default application
    /// </summary>
    /// <param name="txtfile">File to open</param>
    public static async void ViewTextFile(string txtfile)
    {
        if (File.Exists(txtfile))
        {
            try
            {
                using Process p = new();
                p.StartInfo.FileName = txtfile;
                p.StartInfo.UseShellExecute = true;
                p.StartInfo.ErrorDialog = false;
                _ = p.Start();
            }
            catch (Win32Exception ex)
            {
                if (ex.NativeErrorCode == 1155)
                {
                    using Process p = new();
                    p.StartInfo.FileName = "notepad.exe";
                    p.StartInfo.Arguments = txtfile;
                    p.StartInfo.UseShellExecute = true;
                    p.StartInfo.ErrorDialog = false;
                    _ = p.Start();
                }
                else
                {
                    log.Error(ex, $"Unable to open {txtfile}");
                    SystemSounds.Exclamation.Play();
                    ErrorDialog error = new();
                    error.Message = $"Unable to open {txtfile}. See the log file for more information.";
                    _ = await DialogHost.Show(error, "MainDialogHost");
                }
            }
            catch (Exception ex)
            {
                log.Error(ex, $"Unable to open {txtfile}");
                SystemSounds.Exclamation.Play();
                ErrorDialog error = new();
                error.Message = $"Unable to open {txtfile}. See the log file for more information.";
                _ = await DialogHost.Show(error, "MainDialogHost");
            }
        }
        else
        {
            log.Error($"File not found {txtfile}");
            SystemSounds.Exclamation.Play();
            ErrorDialog error = new();
            error.Message = $"File not found {txtfile}. See the log file for more information.";
            _ = await DialogHost.Show(error, "MainDialogHost");
        }
    }
    #endregion Text file viewer
}
