using UnityEngine;

[CreateAssetMenu(menuName = "Unit/Army/Soldier", fileName = "Soldier")]
public class SoldierUnitSO : ArmyUnitSO
{
    [SerializeField] private SoldierUnit _soldierUnitPrefab;
    public override GameObject Create()
    {
        return null;
    }
}
