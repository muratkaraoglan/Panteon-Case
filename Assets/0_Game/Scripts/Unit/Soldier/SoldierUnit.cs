public class SoldierUnit : Unit, IAttack
{
    private BarrackUnit _barrackUnit;
    private int _damage;

    public void Attack()
    {

    }
    public void SetBarrackUnit(BarrackUnit barrackUnit) => _barrackUnit = barrackUnit;
    public void SetDamage(int damage)
    {
        _damage = damage;
    }

    public void OnPlace()
    {
        _backgroundSpriteRenderer.enabled = false;
        _isPlaced = true;
        GridManager.Instance.FillEmptyPoints(_tilePoints);
    }
}
