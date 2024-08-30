using System.Text;
using UnityEngine;

[CreateAssetMenu(menuName = "Unit/Building/Barrack", fileName = "Barrack")]
public class BarrackUnitSO : BuildingUnitSO
{
    [SerializeField] private BarrackUnit _barrackUnit;
    [SerializeField] private SoldierFactory _armyFactory;
    public override GameObject Create()
    {
        BarrackUnit unit = Instantiate(_barrackUnit);
        unit.Init(Name, Sprite, Dimension, HP);
        unit.SetArmyFactory(_armyFactory);
        return unit.gameObject;
    }

    public override string Info()
    {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("Name: ").AppendLine(Name);
        stringBuilder.Append("Dimension: ").Append(Dimension.x).Append("x").Append(Dimension.y);
        stringBuilder.Append("Max HP: ").AppendLine(HP.ToString());
        return stringBuilder.ToString();
    }
}
