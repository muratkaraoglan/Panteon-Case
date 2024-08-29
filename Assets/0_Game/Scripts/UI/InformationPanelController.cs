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
            bool _isPanelFullClose = Mathf.Approximately(_myRectTransform.anchoredPosition.x, _panelClosingTargetX);

            if (!_isPanelFullClose)
            {
                if (_movementCoroutine != null)
                    StopCoroutine(_movementCoroutine);
                _movementCoroutine = StartCoroutine(MovementAnimation(false));
            }
        }
        else
        {
            InfoPanelData unitData = infoPanelDataList[0];// first data always be header(unit) data
            _unitImage.sprite = unitData.UnitSprite;
            _unitNameText.text = unitData.UnitInfo;

            int dataCount = infoPanelDataList.Count;
            dataCount--;

            if (dataCount > 0)//if unit has production data
            {
                int difference = _infoProductionUnitPool.Count - dataCount;// calculate pool difference

                if (difference < 0)
                {
                    difference *= -1;

                    for (int i = 0; i < difference; i++)
                    {
                        InfoProductionUnitController infoPUC = Instantiate(_infoProductionUnitPrefab, _infoProductionUnitParent);
                        _infoProductionUnitPool.Add(infoPUC);
                    }
                }

                for (int i = 1; i < infoPanelDataList.Count; i++)
                {
                    InfoProductionUnitController infoPUC = _infoProductionUnitPool[i - 1];
                    infoPUC.InfoProductionImage.sprite = infoPanelDataList[i].UnitSprite;
                    infoPUC.InfoText.text = infoPanelDataList[i].UnitInfo;
                    infoPUC.gameObject.SetEnable();
                }
            }

            bool _isPanelFullOpen = Mathf.Approximately(_myRectTransform.anchoredPosition.x, 0f);
            if (!_isPanelFullOpen)
            {
                if (_movementCoroutine != null)
                    StopCoroutine(_movementCoroutine);
                _movementCoroutine = StartCoroutine(MovementAnimation(true));
            }
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
