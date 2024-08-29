using System.Collections.Generic;
using System.Text;
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

    public override string Info()
    {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("Name: ").AppendLine(Name);
        stringBuilder.Append("Dimension: ").Append(Dimension.x).Append("x").Append(Dimension.y);
        return stringBuilder.ToString();
    }
}

