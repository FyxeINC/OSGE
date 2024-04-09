public class PlayerProfileManager : Singleton<PlayerProfileManager>
{
    public PlayerProfile CurrentPlayerProfile = new PlayerProfile ();
    public List<PlayerProfile> PlayerProfileCollection = new List<PlayerProfile> ();

    public Action<PlayerProfile> OnCurrentPlayerProfileChanged;

    public override void Awake()
    {
        base.Awake();
        // TODO - save / load profiles
    }

    public void SetCurrentProfile(string newPlayerProfileName)
    {
        OnCurrentPlayerProfileChanged(CurrentPlayerProfile);
    }
}