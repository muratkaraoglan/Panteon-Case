using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnitBaseSO : ScriptableObject
{
    [field: SerializeField] public Unit UnitPrefab { get; protected set; }
    [field: SerializeField] public string Name { get; protected set; }
    [field: SerializeField] public Sprite Sprite { get; protected set; }
    [field: SerializeField] public Vector2 Dimension { get; protected set; }
    [field: SerializeField] public int HP { get; protected set; }

    public abstract void Create();

}
