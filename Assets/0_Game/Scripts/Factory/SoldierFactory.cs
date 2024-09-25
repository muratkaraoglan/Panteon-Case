using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Soldier Factory", menuName = "Factory/Soldier Factory")]
public class SoldierFactory : FactoryBase
{
    [SerializeField] private List<SoldierUnitSO> _soldierUnits;
    [SerializeField, Tooltip("How many turns around the production unit must be scanned to place the produced unit?")
        , Min(1)]
    private int _scanDeep;
    public override int FactoryUnitCount => _soldierUnits.Count;

    public override UnitBaseSO GetUnitBase(int index)
    {
        return _soldierUnits[index];
    }

    public override void ProvideUnit(UnitBaseSO unit, GameObject selectedGameObject = null)
    {
        Vector3 pos = selectedGameObject.transform.position.ToInt();

        Vector3 productionUnitDimension = selectedGameObject.GetComponent<BarrackUnit>().Dimension;

        NodeBase nodeBase = Pathfinding.FindTileToSpawn(pos, (int)productionUnitDimension.x, (int)productionUnitDimension.y, (int)unit.Dimension.x, (int)unit.Dimension.y, _scanDeep);

        if (nodeBase == null) return;

        GetOrAddProduct(unit, out IUnit product);

        SoldierPlacementManager.PlaceSoldier(nodeBase, product, selectedGameObject);

    }


}

public static class SoldierPlacementManager
{
    public static void PlaceSoldier(NodeBase nodeBase, IUnit product, GameObject selectedGameObject)
    {
        GameObject productGO = product.GameObject;

        productGO.transform.position = nodeBase.Coords.Position;
        productGO.SetEnable();

        SoldierUnit soldier = productGO.GetComponent<SoldierUnit>();

        soldier.OnPlace();
        soldier.SetUnitID(selectedGameObject.GetComponent<ITargetable>().UnitID);
 
    }
}
