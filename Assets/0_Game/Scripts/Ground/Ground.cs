using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Ground : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private FactoryBase _factory;
    [SerializeField] private OnProductionMenuChangedEvent _onProductionMenuChangedEvent;
    [SerializeField] private OnInformationMenuChangedEvent _onInformationMenuChangedEvent;
    [SerializeField] private OnUnitHealthChangeEvent _onUnitHealthChangeEvent;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button != 0) return;//left button click
        _onProductionMenuChangedEvent?.RaiseEvent(_factory);
        _onInformationMenuChangedEvent?.RaiseEvent(new List<InfoPanelData>());
        _onUnitHealthChangeEvent?.RaiseEvent(null);
        MapItemSelectionHelper.Instance.LastSelectedMapItemGameObject = gameObject;
    }
}
