using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Soldier Factory", menuName = "Factory/Soldier Factory")]
public class SoldierFactory : FactoryBase
{
    [SerializeField] private List<SoldierUnitSO> _soldierUnits;

    public override int FactoryUnitCount => _soldierUnits.Count;

    public override UnitBaseSO GetUnitBase(int index)
    {
        return _soldierUnits[index];
    }

    public override void ProvideUnit(UnitBaseSO unit, GameObject selectedGameObject = null)
    {
        //GameObject product;

        //if (_pool.ContainsKey(unit.Name))
        //{
 
        //}
    }
}
