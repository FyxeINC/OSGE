using System.Data;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;
using System.Xml;

public class SaveManager : Singleton<SaveManager>
{
    public string SaveDataFolderPath = "SaveData/";

    public string GetSaveDataPath(bool usePlayerProfile)
    {
        if (usePlayerProfile)
        {
            return SaveDataFolderPath + PlayerProfileManager.instance.CurrentPlayerProfile.PlayerName + "/";
        }
        else
        {
            return SaveDataFolderPath + "/";
        }
    }
    
    public override void Awake()
    {
        base.Awake();
        
    }

    public bool SaveToFile<T>(T saveData, string fileName, bool usePlayerProfile = true) where T : SaveDataBase, new()
    {
        System.IO.Directory.CreateDirectory(GetSaveDataPath(usePlayerProfile));

        if (saveData == null)
        {
            saveData = new T();
        }
        string dataSerialized = JsonSerializer.Serialize(saveData, new JsonSerializerOptions { WriteIndented = true });        

        File.WriteAllText(GetSaveDataPath(usePlayerProfile) + fileName + ".json", dataSerialized);
        return true;
    }

    public bool SaveToFileAsync<T>(T saveData, string fileName, bool usePlayerProfile = true) where T : SaveDataBase, new()
    {
        System.IO.Directory.CreateDirectory(GetSaveDataPath(usePlayerProfile));

        if (saveData == null)
        {
            saveData = new T();
        }
        
        Thread thread = new Thread(async () => 
        {
            string filePath = GetSaveDataPath(usePlayerProfile) + fileName + ".json";
            await using FileStream createStream = File.Create(filePath);
            await JsonSerializer.SerializeAsync(createStream, saveData,  new JsonSerializerOptions { WriteIndented = true });        
        });

        return true;
    }

    public T LoadFromFile<T>(string fileName, bool usePlayerProfile = true) where T : SaveDataBase
    {
        string filePath = GetSaveDataPath(usePlayerProfile) + fileName + ".json";
        if (File.Exists(filePath))
        {
            string readFile = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<T>(readFile);
        }
        return null;
    }
    
    public T LoadFromFileAsync<T>(string fileName, bool usePlayerProfile = true) where T : SaveDataBase
    {        
        // TODO
        return null;
    }

    // public static void Load()
    // {
    //     System.IO.Directory.CreateDirectory("SaveData");

    //     if (File.Exists("SaveData/Data_Settings.json"))
    //     {       
    //         string readFile = File.ReadAllText("SaveData/Data_Settings.json");
    //         Data_Settings = JsonSerializer.Deserialize<SaveData_Settings>(readFile);
    //     } 

    //     if (File.Exists("SaveData/Data_Input.json"))
    //     {       
    //         string readFile = File.ReadAllText("SaveData/Data_Settings.json");
    //         Data_Settings = JsonSerializer.Deserialize<SaveData_Settings>(readFile);
    //         InputManager.OnDataLoaded();
    //     } 
    // }

    // public static void Save()
    // {
    //     if (Data_Settings == null)
    //     {
    //         Data_Settings = new SaveData_Settings ();
    //     }
    //     string dataSettingsSerialized = JsonSerializer.Serialize(Data_Settings, new JsonSerializerOptions { WriteIndented = true });        
        
        
    //     if (Data_Input == null)
    //     {
    //         Data_Input = new SaveData_Input ();
    //     }        
    //     InputManager.OnDataSaved();
    //     string dataInputSerialized = JsonSerializer.Serialize(Data_Input, new JsonSerializerOptions { WriteIndented = true });   
        

    //     File.WriteAllText("SaveData/Data_Settings.json", dataSettingsSerialized);
    //     File.WriteAllText("SaveData/Data_Input.json", dataInputSerialized);
    // }

}