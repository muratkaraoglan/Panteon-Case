using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProductionMenuController : MonoBehaviour
{
    [SerializeField] private RectTransform _productionButtonParent;
    [SerializeField] private GameObject _produictionButtonPrefab;

    [Header("Animation")]
    [SerializeField] private float _panelClosingTargetX;
    [SerializeField] private RectTransform _myRectTransform;
    [SerializeField] private float _panelMovementAnimationTime = .2f;

    [Header("Factory")]
    [SerializeField] private FactoryBase _defaultFactory;

    private Coroutine _movementCoroutine;
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
            factoryBase.ProvideUnit(unitBaseSo, MapItemSelectionHelper.Instance.LastSelectedMapItemGameObject);
        });
        button.SetEnable();
    }

    public void ChangeMenu(FactoryBase newFactory)
    {
        ClearButtonListeners();

        if (newFactory == null)
        {
            bool _isPanelFullClose = Mathf.Approximately(_myRectTransform.anchoredPosition.x, _panelClosingTargetX);
            if (!_isPanelFullClose)
            {
                if (_movementCoroutine != null)
                    StopCoroutine(_movementCoroutine);
                _movementCoroutine = StartCoroutine(MovementAnimation(false));
            }
            return;
        }
        FactoryBase factoryBase = newFactory;

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

        bool _isPanelFullOpen = Mathf.Approximately(_myRectTransform.anchoredPosition.x, 0f);
        if (!_isPanelFullOpen)
        {
            if (_movementCoroutine != null)
                StopCoroutine(_movementCoroutine);
            _movementCoroutine = StartCoroutine(MovementAnimation(true));
        }
    }


    IEnumerator MovementAnimation(bool openState)
    {
        float targetX = openState ? 0 : _panelClosingTargetX;

        float elapsedTime = 0f;
        Vector2 currentPos = _myRectTransform.anchoredPosition;
        Vector2 targetPos = new Vector2(targetX, currentPos.y);

        //Calculation of the time remaining for closing or opening
        float movementSpeedPerStep = _panelMovementAnimationTime / Mathf.Abs(_panelClosingTargetX);
        float movementDiff = Mathf.Abs(targetX - currentPos.x);
        float animationTime = movementDiff * movementSpeedPerStep;

        while (elapsedTime < animationTime)
        {
            elapsedTime += Time.deltaTime;
            _myRectTransform.anchoredPosition = Vector2.Lerp(currentPos, targetPos, elapsedTime / animationTime);
            yield return null;
        }
        _movementCoroutine = null;
    }
}
