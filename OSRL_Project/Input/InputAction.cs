public class InputAction
{
    public Tag Category;
    public Tag Identifier;
    public string Name;
    public string Description;

    public InputAction(Tag category, Tag identifier, string name, string description)
    {
        Category = category;
        Identifier = identifier;
        Name = name;
        Description = description;
    }
    
}