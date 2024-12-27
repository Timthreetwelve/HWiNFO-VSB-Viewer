// Copyright(c) Tim Kennedy. All Rights Reserved. Licensed under the MIT License.

namespace HWiNFOVSBViewer.ViewModels;

public partial class AboutViewModel
{
    #region Constructor
    public AboutViewModel()
    {
        if (AnnotatedLanguageList.Count == 0)
        {
            AddNote();
        }
    }
    #endregion Constructor

    #region Relay commands
    [RelayCommand]
    private static void GoToGitHub(string url)
    {
        Process p = new();
        p.StartInfo.FileName = url;
        p.StartInfo.UseShellExecute = true;
        p.Start();
    }

    [RelayCommand]
    private static void ViewLicense()
    {
        string dir = AppInfo.AppDirectory;
        TextFileViewer.ViewTextFile(Path.Combine(dir, "License.txt"));
    }

    [RelayCommand]
    private static void ViewReadMe()
    {
        string dir = AppInfo.AppDirectory;
        TextFileViewer.ViewTextFile(Path.Combine(dir, "ReadMe.txt"));
    }

    [RelayCommand]
    private static async Task CheckReleaseAsync()
    {
        await GitHubHelpers.CheckRelease();
    }
    #endregion Relay commands

    #region Annotated Language translation list
    public List<UILanguage> AnnotatedLanguageList { get; } = [];
    #endregion Annotated Language translation list

    #region Add note to list of languages
    private void AddNote()
    {
        foreach (UILanguage item in UILanguage.DefinedLanguages.Where(item => item.LanguageCode is not "en-GB"))
        {
            item.Note = GetLanguagePercent(item.LanguageCode!);
            AnnotatedLanguageList.Add(item);
        }
    }
    #endregion Add note to list of languages
}
