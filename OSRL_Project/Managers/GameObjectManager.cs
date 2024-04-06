public static class GameObjectManager
{
	static Dictionary<ulong, GameObject> GameObjectCollection = new Dictionary<ulong, GameObject> ();
	static ulong NextID = 0;

    /// <summary>
    /// Give's a GameObject a unique ID and adds it to the tracked collection
    /// </summary>
	public static void Register(this GameObject newGameObject)
	{
		newGameObject.ID = NextID;
		GameObjectCollection.Add(NextID, newGameObject);
		NextID++;
	}

    /// <summary>
    /// Removes the GameObject from the tracked collection
    /// </summary>
	public static void Unregister(this GameObject toUnregister)
	{
		GameObjectCollection.Remove(toUnregister.ID);
	}

    /// <summary>
    /// Exactly what it says it does
    /// </summary>
	public static GameObject GetGameObjectByID(ulong id)
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

	public static Dictionary<ulong, GameObject>.ValueCollection GetAllGameObjects()
	{
		return GameObjectCollection.Values;
	}
}