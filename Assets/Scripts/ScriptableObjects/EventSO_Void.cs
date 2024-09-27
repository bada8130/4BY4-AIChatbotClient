using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Event/Void")]
public class EventSO_Void : ScriptableObject
{
    private readonly UnityEvent listeners = new();

    public void AddEvent(UnityAction listener)
    {
        listeners.AddListener(listener);
    }

    public void DelEvent(UnityAction listener)
    {
        listeners.RemoveListener(listener);
    }

    public void OnEvent()
    {
        listeners.Invoke();
    }
}