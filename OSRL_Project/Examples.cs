/// <summary>
/// This class functions as examples for how to interface with different systems. 
/// Please request more if more are needed.
/// </summary>
public class Examples
{

    public void ExampleLog()
    {
        Log.WriteLine("This is a Debug statement");

        string varaible = "a variable";
        Log.WriteLine($"This is a Debug statement with {varaible}");

        Log.Warning("This is a Warning");
        
        Log.Error("This is an Error");
    }

    public void ExampleTag()
    {
        string tagString = "Tag.String.Example";
        Tag tagStringTag = new Tag (tagString);
        Tag alternativeTagCreation = new Tag ("Tag.String.Example");

        // TODO - searching, comparing, concatination
    }

    public void ExampleTime()
    {

    }

    public void ExampleDisplay()
    {

    }    

    public void ExampleLocalization()
    {
        LocalizationManager.instance.SetLanguage(Tags.Lang_EN);

        string translatedYesStringEnglish = "";

        translatedYesStringEnglish = Tags.Loc_Yes.GetString();

        translatedYesStringEnglish = new Tag("Localization.Yes").GetString();

        LocalizationManager.instance.SetLanguage(Tags.Lang_ES);
        
        string translatedYesStringSpanish = "";

        translatedYesStringSpanish = Tags.Loc_Yes.GetString();

        translatedYesStringEnglish = new Tag("Localization.Yes").GetString();
    }

    public void ExampleInput()
    {

    }

    public void ExampleUI()
    {

    }
}