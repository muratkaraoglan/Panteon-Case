using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InformationPanelController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _unitNameText;
    [SerializeField] private Image _unitImage;
    [SerializeField] private InfoProductionUnitController _infoProductionUnitPrefab;
    [SerializeField] private RectTransform _infoProductionUnitParent;

    [Header("Animation")]
    [SerializeField] private float _panelMovementAnimationTime = .2f;

    private float _panelClosingTargetX;
    private Coroutine _movementCoroutine;
    private RectTransform _myRectTransform;
    private List<InfoProductionUnitController> _infoProductionUnitPool = new List<InfoProductionUnitController>();

    private void Awake()
    {
        _myRectTransform = GetComponent<RectTransform>();
        _panelClosingTargetX = _myRectTransform.anchoredPosition.x;
    }

    private void DisableProductionUnit()
    {
        for (int i = 0; i < _infoProductionUnitPool.Count; i++)
        {
            _infoProductionUnitPool[i].gameObject.SetDisable();
        }
    }

    public void ChangeInfoPanel(List<InfoPanelData> infoPanelDataList)
    {
        DisableProductionUnit();

        if (infoPanelDataList.Count == 0)
        {
            ClosePanelIfOpen();
        }
        else
        {
            UpdateHeaderInfo(infoPanelDataList[0]);

            int productionDataCount = infoPanelDataList.Count - 1;

            if (productionDataCount > 0)//if unit has production data
            {
                UpdatePool(productionDataCount);

                UpdateProductionUnits(infoPanelDataList);
            }

            OpenPanelIfClosed();
        }
    }

    private void ClosePanelIfOpen()
    {
        bool isPanelFullyClosed = Mathf.Approximately(_myRectTransform.anchoredPosition.x, _panelClosingTargetX);

        if (!isPanelFullyClosed)
        {
            StopOngoingMovement();
            _movementCoroutine = StartCoroutine(MovementAnimation(false));
        }
    }

    private void StopOngoingMovement()
    {
        if (_movementCoroutine != null)
        {
            StopCoroutine(_movementCoroutine);
            _movementCoroutine = null;
        }
    }

    private void UpdateHeaderInfo(InfoPanelData unitData)
    {
        _unitImage.sprite = unitData.UnitSprite;
        _unitNameText.text = unitData.UnitInfo;
    }

    private void UpdatePool(int requiredCount)
    {
        int currentCount = _infoProductionUnitPool.Count;
        int unitsToAdd = requiredCount - currentCount;

        for (int i = 0; i < unitsToAdd; i++)
        {
            InfoProductionUnitController newUnit = Instantiate(_infoProductionUnitPrefab, _infoProductionUnitParent);
            _infoProductionUnitPool.Add(newUnit);
        }
    }

    private void UpdateProductionUnits(List<InfoPanelData> productionDataList)
    {
        for (int i = 1; i < productionDataList.Count; i++)
        {
            InfoProductionUnitController infoPUC = _infoProductionUnitPool[i - 1];
            infoPUC.InfoProductionImage.sprite = productionDataList[i].UnitSprite;
            infoPUC.InfoText.text = productionDataList[i].UnitInfo;
            infoPUC.gameObject.SetEnable();
        }
    }

    private void OpenPanelIfClosed()
    {
        bool isPanelFullyOpen = Mathf.Approximately(_myRectTransform.anchoredPosition.x, 0f);

        if (!isPanelFullyOpen)
        {
            StopOngoingMovement();
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

public struct InfoPanelData
{
    public Sprite UnitSprite;
    public string UnitInfo;
}
