[Serializable]
public class SaveData_Settings : SaveDataBase
{   
    public int ResolutionX {get; set;} = 1920;
    public int ResolutionY {get; set;} = 1080;
    public int FPSTarget {get; set;} = 60;
    public bool UseInterlacing {get; set;} = false;
    
}