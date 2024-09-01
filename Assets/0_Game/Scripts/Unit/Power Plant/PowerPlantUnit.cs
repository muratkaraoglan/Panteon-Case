using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEditor;

public class PowerPlantUnit : Unit, IPlacable, IPointerDownHandler, ITargetable
{
    InfoPanelData _headerData;

    public int UnitID => _unitID;

    public bool IsAlive => gameObject.activeSelf;

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
        GridManager.Instance.FillEmptyPoints(_tilePoints, transform);

        _headerData.UnitSprite = _sprite;
        _headerData.UnitInfo = _info;

        MapItemSelectionHelper.Instance.LastSelectedMapItemGameObject = gameObject;
        _onProductionMenuChangedEvent.RaiseEvent(null);
        _onInformationMenuChangedEvent.RaiseEvent(new List<InfoPanelData> { _headerData });
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!_isPlaced) return;
        if (eventData.button != 0) return;//left button click

        MapItemSelectionHelper.Instance.LastSelectedMapItemGameObject = gameObject;
        _onProductionMenuChangedEvent.RaiseEvent(null);
        _onInformationMenuChangedEvent.RaiseEvent(new List<InfoPanelData> { _headerData });
    }

    public bool IsInAttackRange(Vector3 position, int range, out Vector3 targetTilePosition)
    {
        int rangeSquare = range * range;
        for (int i = 0; i < _tilePoints.Count; i++)
        {
            if ((position - _tilePoints[i].position).sqrMagnitude <= rangeSquare)
            {
                targetTilePosition = _tilePoints[i].position;
                return true;
            }
        }
        targetTilePosition = Vector3.zero;
        return false;
    }

    public void TakeDamage(int damage)
    {
        _health.TakeDamage(damage);
    }
}
