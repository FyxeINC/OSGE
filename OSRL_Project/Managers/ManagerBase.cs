using System.Diagnostics;

public class Singleton<T> where T : new()
{
    //static bool m_IsInitialized = false;
    static T m_Instance;
    public static T instance
    {
        get
        {
            return m_Instance;
        }
    }

    public static bool HasBeenInitialized()
    {
        return m_Instance != null;
    }

    public static void Initialize()
    {
        m_Instance = new T();
        //m_IsInitialized = true;        
        
        (m_Instance as Singleton<T>).Awake();
        Program.OnQuit += (m_Instance as Singleton<T>).Quit;
    }

    /// <summary>
    /// Called when the object is created.
    /// </summary>
    public virtual void Awake()
    {

    }

    /// <summary>
    /// Called after all singletons have been created.
    /// </summary>
    public virtual void Start()
    {

    }

    /// <summary>
    /// Called when the application quits
    /// </summary>
    public virtual void Quit()
    {
        
    }
}