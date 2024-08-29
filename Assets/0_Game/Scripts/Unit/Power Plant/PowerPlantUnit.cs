using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;
public class PowerPlantUnit : Unit, IPlacable, IPointerDownHandler
{
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
        if (eventData.button != 0) return;//left button click

        MapItemSelectionHelper.Instance.LastSelectedMapItemGameObject = gameObject;
     
        InfoPanelData headerData;
        headerData.UnitSprite = _sprite;
        headerData.UnitInfo = _name;

        _onProductionMenuChangedEvent.RaiseEvent(null);
        _onInformationMenuChangedEvent.RaiseEvent(new List<InfoPanelData> { headerData });
    }

}
