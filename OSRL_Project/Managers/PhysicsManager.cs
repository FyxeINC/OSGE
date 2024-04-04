public enum PhysicsLayer
{
	Wall,
	Player,
	AI,
	Object,
}
public static class PhysicsManager
{
	static Dictionary<PhysicsLayer, List<PhysicsLayer>> CollisionCollection = new Dictionary<PhysicsLayer, List<PhysicsLayer>>();

	public static void Initialize()
	{
		CollisionCollection.Add(PhysicsLayer.Wall, new List<PhysicsLayer>
		{
			PhysicsLayer.Wall,
			PhysicsLayer.Player,
			PhysicsLayer.AI,
			PhysicsLayer.Object
		});
		CollisionCollection.Add(PhysicsLayer.Player, new List<PhysicsLayer>
		{
			PhysicsLayer.Wall,
			PhysicsLayer.Player,
			PhysicsLayer.AI,
		});
		CollisionCollection.Add(PhysicsLayer.AI, new List<PhysicsLayer>
		{
			PhysicsLayer.Wall,
			PhysicsLayer.Player,
			PhysicsLayer.AI,
		});
		CollisionCollection.Add(PhysicsLayer.Object, new List<PhysicsLayer>
		{
			PhysicsLayer.Wall,
			PhysicsLayer.Object
		});
	}
}