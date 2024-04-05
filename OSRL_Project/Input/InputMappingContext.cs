public class InputActionMapping
{
    public InputActionMapping(List<ConsoleKey> mappedKeys)
    {
        MappedKeys = mappedKeys;
    }
    public List<ConsoleKey> MappedKeys = new List<ConsoleKey> ();
    //public int Priority = 0;
    //public bool ConsumesInput = false;

}

public class InputMappingContext
{
    public Tag MapTag;
    //public int Priority = 0;
    //public bool ConsumesInput = false;

    public InputMappingContext(Tag tag, Dictionary<Tag, InputActionMapping> mappedActions)
    {
        MapTag = tag;
        MappedActions = mappedActions;
    }

    Dictionary<Tag, InputActionMapping> MappedActions = new Dictionary<Tag, InputActionMapping> ();

    public List<InputAction> GetActionForKey(ConsoleKey key)
    {
        List<InputAction> toReturn = new List<InputAction> ();
        foreach (KeyValuePair<Tag, InputActionMapping> i in MappedActions)
        {
            foreach (ConsoleKey j in i.Value.MappedKeys)
            {
                if (j == key)
                {
                    InputAction inputAction = InputActions.GetActionForTag(i.Key);
                    if (inputAction != null)
                    { 
                        toReturn.Add(inputAction);
                        break;
                    }
                }
            }
        }

        // TODO - setup priority and prevent inputs from falling through when disabled

        return toReturn;
    }
}