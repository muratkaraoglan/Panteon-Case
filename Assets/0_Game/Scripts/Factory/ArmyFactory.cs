using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Soldier Factory", menuName = "Factory/Soldier Factory")]
public class ArmyFactory : FactoryBase
{
    [SerializeField] private List<ArmyUnitSO> soldierUnits;
    public override UnitBaseSO GetUnitBase(int index)
    {
        return soldierUnits[index];
    }

    public override void ProvideUnit(UnitBaseSO unit, GameObject selectedGameObject = null)
    {

    }
}
