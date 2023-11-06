// Copyright (c) Tim Kennedy. All Rights Reserved. Licensed under the MIT License.

namespace HWiNFOVSBViewer.Helpers;

internal static class ResourceHelpers
{
    #region NLog Instance
    private static readonly Logger log = LogManager.GetCurrentClassLogger();
    #endregion NLog Instance

    #region Count resource dictionary keys
    /// <summary>
    /// Gets the count of strings in the default resource dictionary.
    /// </summary>
    /// <returns>Count as int.</returns>
    public static int GetTotalDefaultLanguageCount()
    {
        ResourceDictionary dictionary = new()
        {
            Source = new Uri("Languages/Strings.en-US.xaml", UriKind.RelativeOrAbsolute)
        };
        return dictionary.Count;
    }
    #endregion Count resource dictionary keys

    #region Get string resource
    /// <summary>
    /// Gets the string resource for the key.
    /// </summary>
    /// <param name="key">The key.</param>
    /// <returns>String</returns>
    /// <remarks>
    /// Want to throw here so that missing resource doesn't make it into a release.
    /// </remarks>
    public static string GetStringResource(string key)
    {
        object description;
        try
        {
            description = Application.Current.TryFindResource(key);
        }
        catch (Exception)
        {
            //log.Error($"Resource not found: {key}");
            throw new Exception($"Resource not found: {key}");
        }

        if (description is null)
        {
            //log.Error($"Resource not found: {key}");
            throw new Exception($"Resource not found : {key}");
        }

        return description.ToString();
    }
    #endregion Get string resource

    #region Compares language resource dictionaries
    /// <summary>
    /// Compares language resource dictionaries to find missing keys
    /// </summary>
    public static void CompareLanguageDictionaries()
    {
        string currentLanguage = Thread.CurrentThread.CurrentCulture.Name;
        string compareLang = $"Languages/Strings.{currentLanguage}.xaml";

        ResourceDictionary dict1 = new();
        ResourceDictionary dict2 = new();

        dict1.Source = new Uri("Languages/Strings.en-US.xaml", UriKind.RelativeOrAbsolute);
        dict2.Source = new Uri(compareLang, UriKind.RelativeOrAbsolute);

        Dictionary<string, string> enUSDict = new();
        Dictionary<string, string> compareDict = new();

        foreach (DictionaryEntry kvp in dict1)
        {
            enUSDict.Add(kvp.Key.ToString(), kvp.Value.ToString());
        }
        foreach (DictionaryEntry kvp in dict2)
        {
            compareDict.Add(kvp.Key.ToString(), kvp.Value.ToString());
        }

        bool same = enUSDict.Count == compareDict.Count && enUSDict.Keys.SequenceEqual(compareDict.Keys);

        if (same)
        {
            log.Debug($"{dict1.Source} and {dict2.Source} have the same keys");
        }
        else
        {
            if (enUSDict.Keys.Except(compareDict.Keys).Any())
            {
                log.Debug(new string('-', 68));
                log.Debug($"[{AppInfo.AppName}] {dict2.Source} is missing the following keys");
                foreach (string item in enUSDict.Keys.Except(compareDict.Keys).OrderBy(s => s))
                {
                    log.Debug($"Key: {item}    Value: \"{GetStringResource(item)}\"");
                }
                log.Debug(new string('-', 68));
            }

            if (compareDict.Keys.Except(enUSDict.Keys).Any())
            {
                log.Debug($"[{AppInfo.AppName}] {dict1.Source} is missing the following keys");
                foreach (string item in compareDict.Keys.Except(enUSDict.Keys).OrderBy(s => s))
                {
                    log.Debug($"Key: {item}    Value: \"{GetStringResource(item)}\"");
                }
                log.Debug(new string('-', 68));
            }
        }
    }
    #endregion Compares language resource dictionaries
}
