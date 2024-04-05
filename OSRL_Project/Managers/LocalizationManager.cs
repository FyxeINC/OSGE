public static class LocalizationManager
{
    public static Tag CurrentLanguage = Tags.Lang_EN;
    public static Dictionary<Tag, LanguageCollection> LanguageDictionary = new Dictionary<Tag, LanguageCollection> ();

    public static void Initialize()
    {
        LanguageCollection enColl = new LanguageCollection ();
        enColl.StringCollection.Add(Tags.Loc_No, "No");
        enColl.StringCollection.Add(Tags.Loc_Yes, "Yes");
        enColl.StringCollection.Add(Tags.Loc_Play, "Play");
        enColl.StringCollection.Add(Tags.Loc_Quit, "Quit");
        enColl.StringCollection.Add(Tags.Loc_Settings, "Settings");
        enColl.StringCollection.Add(Tags.Loc_PressAnyKey, "Press Any Key");
        LanguageDictionary.Add(Tags.Lang_EN, enColl);
    }

    public static string GetLocString(this Tag locTag)
    {
        if (!LanguageDictionary.ContainsKey(CurrentLanguage))
        {
            return "";
        }

        return LanguageDictionary[CurrentLanguage].GetStringForTag(locTag);
    }
}