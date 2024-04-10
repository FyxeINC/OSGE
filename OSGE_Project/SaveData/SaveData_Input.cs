[Serializable]
public class SaveData_Input : SaveDataBase
{  
    // <IMC, <IA, <Bindings>>>
    public List<KeyValuePair<Tag,  List<KeyValuePair<Tag,  List<ConsoleKey>>>>> KeyBindingCollection {get; set;} = new List<KeyValuePair<Tag, List<KeyValuePair<Tag, List<ConsoleKey>>>>> ();
    
}