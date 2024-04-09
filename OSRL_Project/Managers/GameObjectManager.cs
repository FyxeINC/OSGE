public class GameObjectManager : Singleton<GameObjectManager>
{
	public Dictionary<ulong, GameObject> GameObjectCollection = new Dictionary<ulong, GameObject> ();
	public ulong NextID = 0;

    /// <summary>
    /// Exactly what it says it does
    /// </summary>
	public GameObject GetGameObjectByID(ulong id)
	{
		if (GameObjectCollection.ContainsKey(id))
		{
			return GameObjectCollection[id];
		}
		else
		{
			return null;
		}
	}

	public Dictionary<ulong, GameObject>.ValueCollection GetAllGameObjects()
	{
		return GameObjectCollection.Values;
	}
}