using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class GameEventListenerBase<T> : MonoBehaviour
{
    public GameEventBaseSO<T> Event;
    public UnityEvent<T> Response;
    private void OnEnable()
    {
        Event.GameEvent += OnEventRaised;
    }
    private void OnDisable()
    {
        Event.GameEvent -= OnEventRaised;
    }

    private void OnEventRaised(T eventData)
    {
        Response?.Invoke(eventData);
    }
}
