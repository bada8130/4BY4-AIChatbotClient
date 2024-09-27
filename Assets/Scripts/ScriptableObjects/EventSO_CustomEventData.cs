using System.Collections.Generic;
using UnityEngine;

public struct DynamicEventData
{
    private Dictionary<string, object> eventObjectPool;

    public void SetData(string key, object data)
    {
        eventObjectPool ??= new Dictionary<string, object>();

        if (eventObjectPool.ContainsKey(key))
            eventObjectPool[key] = data;
        else
            eventObjectPool.Add(key, data);
    }

    public readonly T GetData<T>(string dataName)
    {
        return (eventObjectPool != null && eventObjectPool.ContainsKey(dataName)) ? (T)eventObjectPool[dataName] : default(T);
    }

    // 여러 키-값 쌍을 받는 생성자
    public DynamicEventData(params (string, object)[] items) : this()
    {
        foreach (var (key, value) in items)
        {
            SetData(key, value);
        }
    }
}

/// <summary>
/// 동적 이벤트를 담을 수 있는 EventData
/// 장점 : 추가적인 스크립트를 짜지 않아도 되는 장점이 있음
/// 단점 : 동적 생성이기 때문에 데이터를 패치-업데이트 할수 없음
/// 데이터의 내용을 추가하거나 수정할 가능성이 있으면 새로운 클래스를 짜서 관리하는게 좋다. 
/// </summary>
[CreateAssetMenu(menuName = "Event/CustomEventData")]
public class EventSO_CustomEventData : EventSO<DynamicEventData>
{
}