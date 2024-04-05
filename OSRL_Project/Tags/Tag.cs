
public class Tag : IEquatable<Tag>
{
    public static char TagSeparator = '.'; 

    public List<string> TagCollection = new List<string> ();

    public Tag() { }

    public Tag(string tagString)
    {
        TagCollection = tagString.Split(TagSeparator).ToList();
    }

    public bool ContainsTag(Tag toCompare)
    {
        return (ToString().ToLower()).Contains(toCompare.ToString().ToLower());
    }

    public override string ToString()
    {
        string toReturn = "";
        for (int i = 0; i < TagCollection.Count; i++)
        {
            toReturn += TagCollection[i];
            if (i != TagCollection.Count-1)
            {
                toReturn += TagSeparator;
            }
        }
        return toReturn;
    }

    public override bool Equals(object obj)
    {
        return this.ToString() == obj.ToString();
    }
    public bool Equals(Tag other)
    {
        return this.ToString() == other.ToString();
    }

    public static bool operator== (Tag a, Tag b)
    {
        return a.ToString() == b.ToString();
    }    

    public static bool operator!= (Tag a, Tag b)
    {
        return a.ToString() != b.ToString();
    }

    public override int GetHashCode()
    {
        return ToString().GetHashCode();
    }
}