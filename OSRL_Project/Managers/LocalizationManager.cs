using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;

public class LocalizationManager : Singleton<LocalizationManager>
{
    public Tag CurrentLanguage = Tags.Lang_EN;
    public Dictionary<Tag, LanguageCollection> LanguageDictionary = new Dictionary<Tag, LanguageCollection> ();

    public List<LanguageString> AllStrings = new List<LanguageString> ();

    public bool IsLocalizationDirty = false;

    public override void Awake()
    {        
        base.Awake();
        System.IO.Directory.CreateDirectory("Localization");

        AllStrings = new List<LanguageString> ();
        foreach (string filePath in Directory.EnumerateFiles("Localization", "*.csv"))
        {
            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                AllStrings.AddRange(csv.GetRecords<LanguageString>().ToList());                
            }
        }        
        
        foreach (LanguageString i in AllStrings)
        {
            Tag langTag = new Tag(i.LanguageTag);
            if (!LanguageDictionary.ContainsKey(langTag))
            {
                LanguageDictionary.Add(langTag, new LanguageCollection());   
            }

            Tag stringTag = new Tag (i.StringTag);
            if (!LanguageDictionary[langTag].StringCollection.ContainsKey(stringTag))
            {
                LanguageDictionary[langTag].StringCollection.Add(stringTag, "");
            }

            LanguageDictionary[langTag].StringCollection[stringTag] = i.LocalizedString;
        }

        AllStrings.Clear();
        

        // // Writing to CSV
        // 
        
        
        // using (var writer = new StreamWriter(path))
        // {
        //     using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
        //     {
        //         csv.WriteRecords(AllStrings);
        //     }
        // }
    }
    

    // I'm unsure if this is a decent practice. Potentially encourages bad standards.

    public static void CreateLocTag(Tag newLocTag)
    {
        #if DEBUG
        // TODO 
        #endif
    }

    // /// <summary>
    // /// If in a release build, it will attempt to get a string for a localization tag.
    // /// If in a debug build, it will create a string if none are found.
    // /// </summary>
    // public static string GetOrCreateString(this Tag locTag, Tag langTag)
    // {
    //     if (!LanguageDictionary.ContainsKey(CurrentLanguage))
    //     {
    //         return "LANG.NOT.FOUND";
    //     }

        
    //     #if DEBUG
    //     IsLocalizationDirty = true;
    //     #endif
    //     return LanguageDictionary[CurrentLanguage].GetStringForTag(locTag);
    // }

    /// <summary>
    /// Sets the current language. 
    /// </summary>
    public void SetLanguage(string languageString) 
    {
        SetLanguage(new string (languageString));
    }

    /// <summary>
    /// Sets the current language. 
    /// </summary>
    public void SetLanguage(Tag languageTag)
    {
        CurrentLanguage = languageTag;
    }
}