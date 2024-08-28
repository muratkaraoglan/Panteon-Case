using System.Collections.Generic;
using UnityEngine;

public abstract class FactoryBase : ScriptableObject
{
    //public List<UnitBaseSO> ProductionList;

    protected Dictionary<string, List<GameObject>> _pool = new Dictionary<string, List<GameObject>>();

    public abstract void ProvideUnit(UnitBaseSO unit, GameObject selectedObject = null);

    public abstract UnitBaseSO GetUnitBase(int index);
    public abstract int FactoryUnitCount { get; }
}

