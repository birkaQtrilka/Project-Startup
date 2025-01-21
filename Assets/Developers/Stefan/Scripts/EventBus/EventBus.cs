using System;

public class EventBus<T> where T : IEvent
{
    public static event Action<T> Event;

    public static void Publish(T args)
    {
        Event?.Invoke(args);
    }
}
