using System.Collections.Generic;
using UnityEngine;

public abstract class FactoryBase : ScriptableObject
{
    protected Dictionary<string, List<IUnit>> _pool = new();

    public abstract void ProvideUnit(UnitBaseSO unit, GameObject selectedObject = null);
    public abstract UnitBaseSO GetUnitBase(int index);
    public abstract int FactoryUnitCount { get; }

    public void GetOrAddProduct(UnitBaseSO unit, out IUnit product)
    {

        if (_pool.ContainsKey(unit.Name))
        {
            product = _pool[unit.Name].Find(g => !(g as MonoBehaviour).gameObject.activeSelf);
            if (product == null)
            {
                product = unit.Create();
                _pool[unit.Name].Add(product);
            }
        }
        else
        {
            product = unit.Create();
            _pool.Add(unit.Name, new List<IUnit> { product });
        }
    }
}



