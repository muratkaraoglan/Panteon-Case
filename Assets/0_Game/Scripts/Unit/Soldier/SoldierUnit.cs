using System.Collections.Generic;
using UnityEngine.EventSystems;

public class SoldierUnit : Unit, IAttack, IPointerDownHandler
{
    private BarrackUnit _barrackUnit;
    private int _damage;
    InfoPanelData _headerData;
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
