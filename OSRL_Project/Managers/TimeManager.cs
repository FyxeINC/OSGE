public static class TimeManager
{

    public static long PreviousFrameTime;

    public static List<ITickable> TickableCollection = new List<ITickable> ();

    public static void Initialize()
    {
        long CurrentTime = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
        PreviousFrameTime = CurrentTime;

        Thread thread = new Thread(() => 
        {            
            do
            {
                CurrentTime = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
                long deltaTime = CurrentTime - PreviousFrameTime; // Milliseconds
                
                TimeManager.TickTickables(deltaTime);
                DisplayManager.Tick(deltaTime);

                PreviousFrameTime = CurrentTime;
            }
            while(true);
        });
        thread.IsBackground = true;
        thread.Start();
    }

    public static void TickTickables(long deltaTime)
    {
        foreach (var i in TickableCollection)
        {
            i.Tick(deltaTime);
        }
    }

    public static void Register(ITickable tickable)
    {
        TickableCollection.Add(tickable);
    }

    public static void Unregister(ITickable tickable)
    {
        TickableCollection.Remove(tickable);
    }
}