using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public abstract class UnitBaseSO : ScriptableObject
{
    [field: SerializeField] public string Name { get; protected set; }
    [field: SerializeField] public Sprite Sprite { get; protected set; }
    [field: SerializeField] public Vector2 Dimension { get; protected set; }
    [field: SerializeField] public int HP { get; protected set; }

    public abstract GameObject Create();

    public abstract string Info();
 

}

public abstract class BuildingUnitSO : UnitBaseSO { }

public abstract class ArmyUnitSO : UnitBaseSO { }
