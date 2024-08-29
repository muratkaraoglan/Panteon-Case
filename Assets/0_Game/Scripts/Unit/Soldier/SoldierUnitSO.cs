using System.Text;
using UnityEngine;

[CreateAssetMenu(menuName = "Unit/Army/Soldier", fileName = "Soldier")]
public class SoldierUnitSO : ArmyUnitSO
{
    [SerializeField] private SoldierUnit _soldierUnitPrefab;
    [SerializeField] private int _soldierDamage;
    public override GameObject Create()
    {
        SoldierUnit unit = Instantiate(_soldierUnitPrefab);
        unit.Init(Name, Sprite, Dimension, HP);
        unit.SetBarrackUnit(MapItemSelectionHelper.Instance.LastSelectedMapItemGameObject.GetComponent<BarrackUnit>());
        unit.SetDamage(_soldierDamage);
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
