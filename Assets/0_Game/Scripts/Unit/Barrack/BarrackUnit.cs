using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;
public class BarrackUnit : Unit, IPlacable, IPointerDownHandler
{
    private SoldierFactory _armyFactory;

    public void SetArmyFactory(SoldierFactory armyFactory) => _armyFactory = armyFactory;
    public void ChangeAreaBackgroundColor(Color color)
    {
        _backgroundSpriteRenderer.color = color;
    }

    public void ChangePosition(Vector3 position)
    {
        transform.position = position;
    }

    public bool IsValidPlacement()
    {
        return GridManager.Instance.IsPointsValidToPlace(_tilePoints);
    }

    public GameObject GetPlacableObject()
    {
        return gameObject;
    }

    public void Place()
    {
        _backgroundSpriteRenderer.enabled = false;
        _isPlaced = true;
        GridManager.Instance.FillEmptyPoints(_tilePoints);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!_isPlaced) return;

        InfoPanelData headerData;
        headerData.UnitSprite = _sprite;
        headerData.UnitInfo = _name;
        _onInformationMenuChangedEvent.RaiseEvent(new List<InfoPanelData> { headerData });
    }
}
