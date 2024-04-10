public static class GameObjectHelper
{
    
    /// <summary>
    /// Give's a GameObject a unique ID and adds it to the tracked collection
    /// </summary>
	public static void Register(this GameObject newGameObject)
	{
		newGameObject.ID = GameObjectManager.instance.NextID;
		GameObjectManager.instance.GameObjectCollection.Add(GameObjectManager.instance.NextID, newGameObject);
		GameObjectManager.instance.NextID++;
        newGameObject.Awake();
	}

    /// <summary>
    /// Removes the GameObject from the tracked collection
    /// </summary>
	public static void Unregister(this GameObject toUnregister)
	{
		GameObjectManager.instance.GameObjectCollection.Remove(toUnregister.ID);
	}
}