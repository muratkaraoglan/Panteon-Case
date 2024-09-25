using System.Collections.Generic;
using System.Text;
using UnityEngine;

[CreateAssetMenu(menuName = "Unit/Building/Power Plant", fileName = "PowerPlant")]
public class PowerPlantUnitSO : BuildingUnitSO
{
    [SerializeField] private PowerPlantUnit _powerPlantUnit;
    public override IUnit Create()
    {
        PowerPlantUnit unit = Instantiate(_powerPlantUnit);
        unit.Initialize(Name, Sprite, Dimension, HP, Info());
        return unit;
    }

    public override string Info()
    {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("Name: ").AppendLine(Name);
        stringBuilder.Append("Dimension: ").Append(Dimension.x).Append("x").AppendLine(Dimension.y.ToString());
        stringBuilder.Append("Max HP: ").AppendLine(HP.ToString());
        return stringBuilder.ToString();
    }
}

