public static class LocalizationHelper
{
    

    /// <summary>
    /// Searched for a localized version of the string with the given tag.
    /// </summary>
    public static string GetString(this Tag locTag)
    {
        if (!LocalizationManager.instance.LanguageDictionary.ContainsKey(LocalizationManager.instance.CurrentLanguage))
        {
            return "LANG.NOT.FOUND";
        }

        return LocalizationManager.instance.LanguageDictionary[LocalizationManager.instance.CurrentLanguage].GetStringForTag(locTag);
    }

    public static string GetLocalizedString(this string nonLocString)
    {
        // TODO - cycle through strings and find
        // probably a terrible idea
        return "NOT.IMPLEMENTED";
    }
}