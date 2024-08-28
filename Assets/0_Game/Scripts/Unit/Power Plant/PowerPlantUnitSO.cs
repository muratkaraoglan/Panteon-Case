using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Unit/Building/Power Plant", fileName = "PowerPlant")]
public class PowerPlantUnitSO : BuildingUnitSO
{
    [SerializeField] private PowerPlantUnit _powerPlantUnit;
    public override GameObject Create()
    {
        PowerPlantUnit unit = Instantiate(_powerPlantUnit);
        unit.Init(Name, Sprite, Dimension, HP);
        return unit.gameObject;
    }
}

