﻿// Copyright (c) Tim Kennedy. All Rights Reserved. Licensed under the MIT License.

namespace HWiNFOVSBViewer.Models;

/// <summary>
/// Class for language properties.
/// </summary>
/// <seealso cref="CommunityToolkit.Mvvm.ComponentModel.ObservableObject" />
public partial class UILanguage : ObservableObject
{
    #region Properties
    /// <summary>
    /// The name of the contributor. Can be any string chosen by the contributor.
    /// </summary>
    [ObservableProperty]
    private string? _contributor;

    /// <summary>
    /// Total number of strings in the language resource dictionary.
    /// </summary>
    [ObservableProperty]
    private int? _currentLanguageStringCount = LocalizationHelpers.LanguageStrings;

    /// <summary>
    /// Total number of strings in the (en-US) language resource dictionary.
    /// </summary>
    [ObservableProperty]
    private int? _defaultStringCount = App.DefaultLanguageStrings;

    /// <summary>
    /// English spelling of the language name.
    /// </summary>
    [ObservableProperty]
    private string? _language;

    /// <summary>
    /// Language code in the form xx-XX
    /// </summary>
    [ObservableProperty]
    private string? _languageCode;

    /// <summary>
    /// Native spelling of the language name.
    /// </summary>
    [ObservableProperty]
    private string? _languageNative;

    /// <summary>
    /// Note field. Currently unused.
    /// </summary>
    [ObservableProperty]
    private string? _note = string.Empty;
    #endregion Properties

    #region Override ToString
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
    #endregion Override ToString

    #region List of languages
    /// <summary>
    /// List of languages with language code
    /// </summary>
    /// <remarks>
    /// Please add new entries to the bottom. The languages will be sorted by language code.
    /// </remarks>
    private static List<UILanguage> LanguageList { get; } =
    [
        new () {Language = "English", LanguageCode = "en-US", LanguageNative = "English",    Contributor = "Timthreetwelve", Note="Default"},
        new () {Language = "Italian", LanguageCode = "it-IT", LanguageNative = "Italiano",   Contributor = "RB"},
        new () {Language = "Dutch",   LanguageCode = "nl-NL", LanguageNative = "Nederlands", Contributor = "Tim"},
        new () {Language = "Korean",  LanguageCode = "ko-KR", LanguageNative = "한국어",      Contributor = "VenusGirl💗 (비너스걸)"},
        new () {Language = "English", LanguageCode = "en-GB", LanguageNative = "English",    Contributor = "Timthreetwelve"},
    ];

    /// <summary>
    /// List of defined languages ordered by LanguageCode.
    /// </summary>
    public static List<UILanguage> DefinedLanguages => [.. LanguageList.OrderBy(keySelector: x => x.LanguageCode)];

    #endregion List of languages
}
