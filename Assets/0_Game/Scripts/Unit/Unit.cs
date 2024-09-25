using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class Unit : MonoBehaviour, IUnit
{
    [Header("Event")]
    [SerializeField] protected OnProductionMenuChangedEvent _onProductionMenuChangedEvent;
    [SerializeField] protected OnInformationMenuChangedEvent _onInformationMenuChangedEvent;

    [Header("")]
    [SerializeField] protected SpriteRenderer _bodySpriteRenderer;
    [SerializeField] protected SpriteRenderer _backgroundSpriteRenderer;
    [SerializeField] protected Transform _tilePointsParent;
    [SerializeField] protected Health _health;
    [SerializeField] private TextMeshProUGUI _iDText;

    protected string _name;
    protected Sprite _sprite;
    protected Vector2 _dimension;
    protected List<Transform> _tilePoints;
    protected bool _isPlaced;
    protected int _unitID;
    protected string _info;

    private void OnEnable()
    {
        _backgroundSpriteRenderer.enabled = true;
        _isPlaced = false;
    }

    public void Initialize(string name, Sprite sprite, Vector2 dimension, int maxHP, string info)
    {
        _tilePoints = new List<Transform>();
        _name = name;
        _sprite = sprite;
        _dimension = dimension;
        _info = info;
        gameObject.name = _name;

        _bodySpriteRenderer.sprite = _sprite;
        _bodySpriteRenderer.transform.localPosition = new Vector3(_dimension.x / 2, dimension.y / 2, 0f);
        Vector3 bodyScale = _bodySpriteRenderer.transform.localScale;
        bodyScale.x *= dimension.x;
        bodyScale.y *= dimension.y;
        _bodySpriteRenderer.transform.localScale = bodyScale;

        _backgroundSpriteRenderer.transform.localPosition = new Vector3(_dimension.x / 2, dimension.y / 2, 0f);
        _backgroundSpriteRenderer.transform.localScale = new Vector3(_dimension.x, _dimension.y, 1);

        _health.Init(maxHP);

        for (int x = 0; x < _dimension.x; x++)//Create tile points depends on area dimension
        {
            for (int y = 0; y < _dimension.y; y++)
            {
                GameObject point = new GameObject
                {
                    name = x + "," + y
                };

                point.transform.parent = _tilePointsParent;
                point.transform.localPosition = new Vector2(x, y);
                _tilePoints.Add(point.transform);
            }
        }
    }

    public Vector2 Dimension => _dimension;
    public List<Transform> AreaTilePoints => _tilePoints;

    public GameObject GameObject => gameObject;

    public void SetUnitID(int unitID)
    {
        _unitID = unitID;
        _iDText.SetText(_unitID.ToString());
    }
    private void OnDisable()
    {
        GridManager.Instance.EmptyFilledPoints(_tilePoints);
        if (MapItemSelectionHelper.Instance.LastSelectedMapItemGameObject != gameObject) return;

        MapItemSelectionHelper.Instance.LastSelectedMapItemGameObject = null;
        _onProductionMenuChangedEvent.RaiseEvent(null);
        _onInformationMenuChangedEvent.RaiseEvent(new List<InfoPanelData>());

    }
}

public interface IUnit
{
    public void Initialize(string name, Sprite sprite, Vector2 dimension, int maxHP, string info);
    public Vector2 Dimension { get; }

    public void SetUnitID(int unitID);

    public GameObject GameObject { get; }
}