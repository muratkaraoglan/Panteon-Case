using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;
public class BarrackUnit : Unit, IPlacable, IPointerDownHandler, ITargetable
{
    private SoldierFactory _armyFactory;
    private List<InfoPanelData> _infoPanelDataList;

    public int UnitID => _unitID;

    private void Start()
    {
        InfoPanelData headerData;
        headerData.UnitSprite = _sprite;
        headerData.UnitInfo = _name;
        _infoPanelDataList = new List<InfoPanelData>() { headerData };

        for (int i = 0; i < _armyFactory.FactoryUnitCount; i++)
        {
            InfoPanelData data;
            data.UnitSprite = _armyFactory.GetUnitBase(i).Sprite;
            data.UnitInfo = _armyFactory.GetUnitBase(i).Info();
            _infoPanelDataList.Add(data);
        }
    }

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

        MapItemSelectionHelper.Instance.LastSelectedMapItemGameObject = gameObject;
        _onProductionMenuChangedEvent.RaiseEvent(_armyFactory);
        _onInformationMenuChangedEvent.RaiseEvent(_infoPanelDataList);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!_isPlaced) return;
        if (eventData.button != 0) return;//left button click

        MapItemSelectionHelper.Instance.LastSelectedMapItemGameObject = gameObject;
        _onProductionMenuChangedEvent.RaiseEvent(_armyFactory);
        _onInformationMenuChangedEvent.RaiseEvent(_infoPanelDataList);
    }
}
