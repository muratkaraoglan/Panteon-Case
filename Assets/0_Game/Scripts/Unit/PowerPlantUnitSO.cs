using UnityEngine;

[CreateAssetMenu(menuName = "Unit/Power Plant", fileName = "PowerPlant")]
public class PowerPlantUnitSO : UnitBaseSO
{
    public override void Create()
    {
        //TODO: factory Pattern
        Unit unit = Instantiate(UnitPrefab);

        unit.Init(Name, Sprite, Dimension);

        MapItemPlacementHelper.Instance.Placable = unit;
    }
}
