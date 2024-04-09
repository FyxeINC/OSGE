public class TimeManager : Singleton<TimeManager>
{

    public long PreviousFrameTime;

    public List<ITickable> TickableCollection = new List<ITickable> ();

    public override void Start()
    {
        base.Start();
        long CurrentTime = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
        PreviousFrameTime = CurrentTime;

        Thread thread = new Thread(() => 
        {            
            do
            {
                CurrentTime = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
                long deltaTime = CurrentTime - PreviousFrameTime; // Milliseconds
                
                TimeManager.instance.TickTickables(deltaTime);
                DisplayManager.instance.Tick(deltaTime);

                PreviousFrameTime = CurrentTime;
            }
            while(true);
        });
        thread.IsBackground = true;
        thread.Start();
    }

    public long GetTime()
    {
        // TODO - verify that this works how I think it does
        return DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
    }

    public void TickTickables(long deltaTime)
    {
        foreach (var i in TickableCollection)
        {
            i.Tick(deltaTime);
        }
    }

    public void Register(ITickable tickable)
    {
        TickableCollection.Add(tickable);
    }

    public void Unregister(ITickable tickable)
    {
        TickableCollection.Remove(tickable);
    }
}