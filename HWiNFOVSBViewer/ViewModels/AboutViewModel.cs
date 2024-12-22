﻿// Copyright(c) Tim Kennedy. All Rights Reserved. Licensed under the MIT License.

namespace HWiNFOVSBViewer.ViewModels
{
    public partial class AboutViewModel
    {
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
    }
}
