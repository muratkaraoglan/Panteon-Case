using System.Collections.Generic;
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
        GameObject product;

        Vector3 pos = selectedGameObject.transform.position.ToInt();

        Vector3 productionUnitDimension = selectedGameObject.GetComponent<BarrackUnit>().Dimension;

        NodeBase nodeBase = Pathfinding.FindTileToSpawn(pos, (int)productionUnitDimension.x, (int)productionUnitDimension.y, (int)unit.Dimension.x, (int)unit.Dimension.y, _scanDeep);

        if (nodeBase != null)
        {
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
            product.transform.position = nodeBase.Coords.Position;
            product.SetEnable();
            SoldierUnit soldier = product.GetComponent<SoldierUnit>();
            soldier.OnPlace();
            if (selectedGameObject.TryGetComponent(out ITargetable targatable))
            {
                soldier.SetUnitID(targatable.UnitID);
            }
        }
    }
}
