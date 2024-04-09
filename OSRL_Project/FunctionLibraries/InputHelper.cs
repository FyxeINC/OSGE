public static class InputHelper
{    
    public static void RegisterHandler(this IInputHandler handler)
    {
        if (!InputManager.instance.InputHandlerCollection.Contains(handler))
        {
            InputManager.instance.InputHandlerCollection.Add(handler);
        }
    }

    public static void UnregisterHandler(this IInputHandler handler)
    {
        InputManager.instance.InputHandlerCollection.Remove(handler);
    }
}