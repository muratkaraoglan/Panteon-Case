using System.Text;
using UnityEngine;

[CreateAssetMenu(menuName = "Unit/Army/Soldier", fileName = "Soldier")]
public class SoldierUnitSO : ArmyUnitSO
{
    [SerializeField] private SoldierUnit _soldierUnitPrefab;
    [SerializeField] private int _soldierDamage;
    [SerializeField] private int _attackRange;
    public override GameObject Create()
    {
        SoldierUnit unit = Instantiate(_soldierUnitPrefab);
        unit.Init(Name, Sprite, Dimension, HP);
        unit.SetDamage(_soldierDamage);
        unit.SetAttackRange(_attackRange);
        return unit.gameObject;
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
