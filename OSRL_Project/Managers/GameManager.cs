public class GameManager : Singleton<GameManager>
{
    GameObject m_LevelParent = null;
    Dictionary<Tag, Level> m_ActiveLevelCollection = new Dictionary<Tag, Level> ();

    public override void Awake()
    {
        base.Awake();
        m_LevelParent = new GameObject ("LevelParent", 0, 0);
    }

    public bool IsLevelLoaded(Tag levelTag)
    {
        return m_ActiveLevelCollection.ContainsKey(levelTag);
    }

    public bool LoadLevel(Tag levelTag)
    {   
        return LoadLevelActual(levelTag, false);
    }

    public bool UnloadLevel(Tag levelTag)
    {
        return UnloadLevelActual(levelTag, false);
    }
    
    public bool LoadLevelAddative(Tag levelTag)
    {   
        return LoadLevelActual(levelTag, true);
    }

    public bool UnloadLevelAddative(Tag levelTag)
    {
        return UnloadLevelActual(levelTag, true);
    }

    bool LoadLevelActual(Tag levelTag, bool addative)
    {
        // TODO - async load
        return false;
    }
    
    bool UnloadLevelActual(Tag levelTag, bool addative)
    {
        // TODO - 
        return false;
    }
}