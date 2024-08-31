using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    [SerializeField] protected SpriteRenderer _bodySpriteRenderer;
    [SerializeField] protected SpriteRenderer _backgroundSpriteRenderer;
    [SerializeField] protected Transform _tilePointsParent;
    [SerializeField] private Health _health;
    [SerializeField] private TextMeshProUGUI _iDText;
    [Header("Event")]
    [SerializeField] protected OnProductionMenuChangedEvent _onProductionMenuChangedEvent;
    [SerializeField] protected OnInformationMenuChangedEvent _onInformationMenuChangedEvent;

    protected string _name;
    protected Sprite _sprite;
    protected Vector2 _dimension;
    protected List<Transform> _tilePoints;
    protected bool _isPlaced;
    [SerializeField] protected int _unitID;
    private void OnEnable()
    {
        _backgroundSpriteRenderer.enabled = true;
        _isPlaced = false;
    }

    public virtual void Init(string name, Sprite sprite, Vector2 dimension, int maxHP)
    {
        _tilePoints = new List<Transform>();
        _name = name;
        _sprite = sprite;
        _dimension = dimension;
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
    public void SetUnitID(int unitID)
    {
        _unitID = unitID;
        _iDText.SetText(_unitID.ToString());
    }
    private void OnDisable()
    {
        GridManager.Instance.EmptyFilledPoints(_tilePoints);
    }

}