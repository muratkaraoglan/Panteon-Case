using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Building Factory", menuName = "Factory/Building Factory")]
public class BuildingFactory : FactoryBase
{
    [SerializeField] private List<BuildingUnitSO> _buildingUnits;

    public override int FactoryUnitCount => _buildingUnits.Count;

    public override UnitBaseSO GetUnitBase(int index)
    {
        return _buildingUnits[index];
    }

    public override void ProvideUnit(UnitBaseSO unit, GameObject selectedObject = null)
    {
        GetOrAddProduct(unit, out IUnit product);

        BuildingAssign.Assign(product);
    }
}

public static class BuildingAssign
{
    public static void Assign(IUnit product)
    {
        GameObject productGO = product.GameObject;
        product.SetUnitID(UnitIDProvider.ProvideID);
        productGO.SetEnable();
        MapItemPlacementHelper.Instance.Placable = productGO.GetComponent<IPlacable>();
    }
}


