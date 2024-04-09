// Copyright (c) Tim Kennedy. All Rights Reserved. Licensed under the MIT License.

namespace HWiNFOVSBViewer.Models;

/// <summary>
/// Class for language properties.
/// </summary>
/// <seealso cref="CommunityToolkit.Mvvm.ComponentModel.ObservableObject" />
internal partial class UILanguage : ObservableObject
{
    [ObservableProperty]
    private int? _currentLanguageStringCount = App.LanguageStrings;

    [ObservableProperty]
    private int? _defaultStringCount = App.DefaultLanguageStrings;

    [ObservableProperty]
    private string? _language;

    [ObservableProperty]
    private string? _languageCode;

    [ObservableProperty]
    private string? _languageNative;

    [ObservableProperty]
    private string? _contributor;

    [ObservableProperty]
    private string _note = string.Empty;

    /// <summary>
    /// Overrides the ToString method.
    /// </summary>
    /// <returns>
    /// The language code as a string.
    /// </returns>
    public override string ToString()
    {
        return LanguageCode!;
    }

    /// <summary>
    /// List of languages with language code
    /// </summary>
    private static List<UILanguage> LanguageList { get; } =
    [
        new UILanguage {Language = "English", LanguageCode = "en-US", LanguageNative = "English",    Contributor = "Timthreetwelve", Note="Default"},
        new UILanguage {Language = "Italian", LanguageCode = "it-IT", LanguageNative = "Italiano",   Contributor = "RB"},
        new UILanguage {Language = "Dutch",   LanguageCode = "nl-NL", LanguageNative = "Nederlands", Contributor = "Tim"},
    ];

    /// <summary>
    /// List of defined languages ordered by LanguageNative.
    /// </summary>
    public static List<UILanguage> DefinedLanguages => [.. LanguageList.OrderBy(keySelector: x => x.LanguageNative)];
}
