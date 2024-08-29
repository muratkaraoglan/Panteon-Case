using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProductionMenuController : MonoBehaviour
{
    [SerializeField] private RectTransform _productionButtonParent;
    [SerializeField] private GameObject _produictionButtonPrefab;

    [Header("Factory")]
    [SerializeField] private FactoryBase _defaultFactory;


    private List<GameObject> _productionButtonPool = new List<GameObject>();
    private void Start()
    {
        for (int i = 0; i < _defaultFactory.FactoryUnitCount; i++)
        {
            GameObject button = Instantiate(_produictionButtonPrefab, _productionButtonParent);
            EnableButton(button, _defaultFactory, i);
            _productionButtonPool.Add(button);

        }
    }

    private void ClearButtonListeners()
    {
        for (int i = 0; i < _productionButtonPool.Count; i++)
        {
            GameObject button = _productionButtonPool[i];
            button.GetComponent<Button>().onClick.RemoveAllListeners();
            button.SetDisable();
        }
    }
    private void EnableButton(GameObject button, FactoryBase factoryBase, int index)
    {
        UnitBaseSO unitBaseSo = factoryBase.GetUnitBase(index);
        button.GetComponent<Image>().sprite = unitBaseSo.Sprite;
        button.GetComponent<Button>().onClick.AddListener(() =>
        {
            factoryBase.ProvideUnit(unitBaseSo);
        });
        button.SetEnable();
    }

    public void ChangeMenu(FactoryBase newFactory)
    {
        ClearButtonListeners();

        FactoryBase factoryBase = newFactory == null ? _defaultFactory : newFactory;

        int difference = _productionButtonPool.Count - factoryBase.FactoryUnitCount;

        if (difference < 0)
        {
            difference *= -1;
            for (int i = 0; i < difference; i++)
            {
                GameObject button = Instantiate(_produictionButtonPrefab, _productionButtonParent);
                _productionButtonPool.Add(button);
            }
        }

        for (int i = 0; i < factoryBase.FactoryUnitCount; i++)
        {
            GameObject button = _productionButtonPool[i];
            EnableButton(button, factoryBase, i);
        }
    }

}
