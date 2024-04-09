using System.Globalization;
using CsvHelper;

// TODO - do we need this?
public class TagManager : Singleton<TagManager>
{
    public override void Awake()
    {        
        base.Awake();
        System.IO.Directory.CreateDirectory("Tags");
        
        foreach (string filePath in Directory.EnumerateFiles("Tags", "*.txt"))
        {
            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                //AllStrings.AddRange(csv.GetRecords<LanguageString>().ToList());                
            }
        }        

    }
}