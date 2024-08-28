﻿using System.Collections.Generic;
using System.Linq;
using UnityEngine;


[CreateAssetMenu(fileName = "Building Factory", menuName = "Factory/Building Factory")]
public class BuildingFactory : FactoryBase
{
    [SerializeField] private List<BuildingUnitSO> _buildingUnits;

    public override UnitBaseSO GetUnitBase(int index)
    {
        return _buildingUnits[index];
    }

    public override void ProvideUnit(UnitBaseSO unit, GameObject selectedObject = null)
    {
        GameObject product;

        if (_pool.ContainsKey(unit.Name))
        {
            product = _pool[unit.Name].Find(g => !g.activeSelf);
            if (product == null)
            {
                product = unit.Create();
                _pool[unit.Name].Add(product);
            }
        }
        else
        {
            product = unit.Create();
            _pool.Add(unit.Name, new List<GameObject> { product });
        }
        product.SetEnable();
        MapItemPlacementHelper.Instance.Placable = product.GetComponent<IPlacable>();
    }
}
