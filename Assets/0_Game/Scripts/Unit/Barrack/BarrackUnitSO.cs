using UnityEngine;

[CreateAssetMenu(menuName = "Unit/Building/Barrack", fileName = "Barrack")]
public class BarrackUnitSO : BuildingUnitSO
{
    [SerializeField] private BarrackUnit _barrackUnit;
    [SerializeField] private ArmyFactory _armyFactory;
    public override GameObject Create()
    {
        BarrackUnit unit = Instantiate(_barrackUnit);
        unit.Init(Name, Sprite, Dimension, HP);
        unit.SetArmyFactory(_armyFactory);
        return unit.gameObject;
    }
}
