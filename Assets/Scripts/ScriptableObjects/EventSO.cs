using UnityEngine;
using UnityEngine.Events;

public abstract class EventSO<T> : ScriptableObject
{
    private readonly UnityEvent<T> listeners = new();

    public void AddEvent(UnityAction<T> listener)
    {
        listeners.AddListener(listener);
    }

    public void DelEvent(UnityAction<T> listener)
    {
        listeners.RemoveListener(listener);
    }

    public void OnEvent(T value)
    {
        listeners.Invoke(value);
    }
}

public abstract class EventSO<T1, T2> : ScriptableObject
{
    private readonly UnityEvent<T1, T2> listeners = new();

    public void AddEvent(UnityAction<T1, T2> listener)
    {
        listeners.AddListener(listener);
    }

    public void DelEvent(UnityAction<T1, T2> listener)
    {
        listeners.RemoveListener(listener);
    }

    public void OnEvent(T1 value1, T2 value2)
    {
        listeners.Invoke(value1, value2);
    }
}