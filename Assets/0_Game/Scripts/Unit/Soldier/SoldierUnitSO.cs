using System.Text;
using UnityEngine;

[CreateAssetMenu(menuName = "Unit/Army/Soldier", fileName = "Soldier")]
public class SoldierUnitSO : ArmyUnitSO
{
    [SerializeField] private SoldierUnit _soldierUnitPrefab;
    public override GameObject Create()
    {
        return null;
    }

    public override string Info()
    {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("Name: ").AppendLine(Name);
        stringBuilder.Append("Dimension: ").Append(Dimension.x).Append("x").Append(Dimension.y);
        return stringBuilder.ToString();
    }
}
