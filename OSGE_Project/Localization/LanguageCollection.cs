using System.Dynamic;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;

public class LanguageStringMap : ClassMap<LanguageString>
{
    public LanguageStringMap()
    {
        Map(m => m.LanguageTag).Index(0).Name("LanguageTag");
        Map(m => m.StringTag).Index(1).Name("StringTag");
        Map(m => m.LocalizedString).Index(2).Name("LocalizedString");
    }
}

public class LanguageString
{    
    public string LanguageTag { get; set; }
    public string StringTag { get; set; }
    public string LocalizedString { get; set; }
}

public class LanguageCollection
{
    public Dictionary<Tag, string> StringCollection = new Dictionary<Tag, string> ();

    public string GetStringForTag(Tag tag)
    {
        if (!StringCollection.ContainsKey(tag))
        {
            return "STR.NOT.FOUND";
        }

        return StringCollection[tag];
    }
    
}