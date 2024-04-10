public class InputAction
{
    public Tag Category {get; set;}
    public Tag Identifier {get; set;}
    public string Name {get; set;}
    public string Description {get; set;}

    public InputAction(Tag category, Tag identifier, string name, string description)
    {
        Category = category;
        Identifier = identifier;
        Name = name;
        Description = description;
    }
    
}