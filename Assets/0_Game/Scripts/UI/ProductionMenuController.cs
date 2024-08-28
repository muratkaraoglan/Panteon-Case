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
            UnitBaseSO unitBaseSo = _defaultFactory.GetUnitBase(i);
            button.GetComponent<Image>().sprite = unitBaseSo.Sprite;
            button.GetComponent<Button>().onClick.AddListener(() =>
            {
                _defaultFactory.ProvideUnit(unitBaseSo);
            });
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


}
