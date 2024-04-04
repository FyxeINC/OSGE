public static class GameObjectManager
{
  static Dictionary<ulong, GameObject> GameObjectCollection = new Dictionary<ulong, GameObject> ();
  static ulong NextID = 0;

  public static void Register(this GameObject newGameObject)
  {
    newGameObject.ID = NextID;
    GameObjectCollection.Add(NextID, newGameObject);
    NextID++;
  }

  public static void Unregister(this GameObject toUnregister)
  {
    GameObjectCollection.Remove(toUnregister.ID);
  }

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