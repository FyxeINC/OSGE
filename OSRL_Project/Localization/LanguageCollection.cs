using System.Dynamic;

public class LanguageCollection
{
    public Tag LanguageTag;

    public Dictionary<Tag, string> StringCollection = new Dictionary<Tag, string> ();

    public string GetStringForTag(Tag tag)
    {
        if (!StringCollection.ContainsKey(tag))
        {
            return "";
        }

        return StringCollection[tag];
    }
    
}