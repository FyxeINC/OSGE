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

    public void ExampleUIObjects()
    {        
        UI_SolidFill fillA = new UI_SolidFill (' ');
		fillA.SetAnchorPoint(AnchorPointHorizonal.stretch, AnchorPointVertical.stretch);
        fillA.SetColors(ConsoleColor.DarkGray, ConsoleColor.DarkGreen);
        
		UI_Panel panelA = new UI_Panel ("panel1", 0, 0, 20, 10, BorderType.doubleLine, "PANEL 1");
        panelA.SetColors(ConsoleColor.White, ConsoleColor.Black);
		panelA.SetAnchorPoint(AnchorPointHorizonal.left, AnchorPointVertical.top);	

        UI_Bar barA = new UI_Bar (1,1,0,8);
        barA.SetAnchorPoint(AnchorPointHorizonal.stretch, AnchorPointVertical.top);
        barA.SetOffset(1,1,1,1);
        barA.SetColors(ConsoleColor.Red, ConsoleColor.Gray);
        barA.SetFillDirection(Direction.right);
        barA.SetFillPercentage(0.25f);

        UI_TextArea textA = new UI_TextArea ("TextA", 1, 1, 5, 1, "This is text A");
        textA.SetAnchorPoint(AnchorPointHorizonal.left, AnchorPointVertical.top);
        textA.SetColors(ConsoleColor.White, ConsoleColor.Blue);
        textA.SetTextAlignment(TextAlignmentHorizontal.left, TextAlignmentVertical.middle);   
        
		UI_Border borderA = new UI_Border (BorderType.doubleLine);
        borderA.SetBorderType(BorderType.doubleLine);		
        borderA.SetBorderType(BorderType.singleLine);		
        borderA.SetBorderType(BorderType.solidA);		
        borderA.SetBorderType(BorderType.solidA);		
    }
}