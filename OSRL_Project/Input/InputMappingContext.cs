public class InputActionMapping
{
    public List<ConsoleKey> MappedKeys = new List<ConsoleKey> ();
    //public int Priority = 0;
    //public bool ConsumesInput = false;

}

public class InputMappingContext
{
    public string Name = "IMC";
    //public int Priority = 0;
    //public bool ConsumesInput = false;

    Dictionary<InputAction, InputActionMapping> MappedActions = new Dictionary<InputAction, InputActionMapping> ();

    public List<InputAction> GetActionForKey(ConsoleKey key)
    {
        List<InputAction> toReturn = new List<InputAction> ();
        foreach (KeyValuePair<InputAction, InputActionMapping> i in MappedActions)
        {
            foreach (ConsoleKey j in i.Value.MappedKeys)
            {
                if (j == key)
                {
                    toReturn.Add(i.Key);
                    break;
                }
            }
        }

        // TODO - setup priority and prevent inputs from falling through when disabled

        return toReturn;
    }
}