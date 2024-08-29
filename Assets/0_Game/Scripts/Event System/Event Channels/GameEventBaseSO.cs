using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameEventBaseSO<T> : ScriptableObject
{
    public event Action<T> GameEvent;

    public void RaiseEvent(T eventData)
    {
        GameEvent?.Invoke(eventData);
    }
}
