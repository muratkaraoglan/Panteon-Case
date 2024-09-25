using System.Text;
using UnityEngine;

[CreateAssetMenu(menuName = "Unit/Building/Barrack", fileName = "Barrack")]
public class BarrackUnitSO : BuildingUnitSO
{
    [SerializeField] private BarrackUnit _barrackUnit;
    [SerializeField] private SoldierFactory _armyFactory;
    public override IUnit Create()
    {
        BarrackUnit unit = Instantiate(_barrackUnit);
        unit.Initialize(Name, Sprite, Dimension, HP, Info());
        unit.SetArmyFactory(_armyFactory);
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
