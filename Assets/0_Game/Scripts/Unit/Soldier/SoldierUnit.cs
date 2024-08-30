using System.Collections.Generic;
using UnityEngine.EventSystems;

public class SoldierUnit : Unit, IPointerDownHandler, ITargetable
{
    private int _damage;
    private int _attackRange;
    InfoPanelData _headerData;

    public int UnitID => _unitID;
    public int AttackRange => _attackRange;
    public void SetDamage(int damage)
    {
        _damage = damage;
    }
    public void SetAttackRange(int attackRange)
    {
        _attackRange = attackRange;
    }

    public void OnPlace()
    {
        _backgroundSpriteRenderer.enabled = false;
        _isPlaced = true;

        _headerData.UnitSprite = _sprite;
        _headerData.UnitInfo = _name;

        GridManager.Instance.FillEmptyPoints(_tilePoints);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!_isPlaced) return;
        if (eventData.button != 0) return;//left button click

        MapItemSelectionHelper.Instance.LastSelectedMapItemGameObject = gameObject;
        _onProductionMenuChangedEvent.RaiseEvent(null);
        _onInformationMenuChangedEvent.RaiseEvent(new List<InfoPanelData> { _headerData });
    }
}
