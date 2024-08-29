using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    [SerializeField] protected SpriteRenderer _bodySpriteRenderer;
    [SerializeField] protected SpriteRenderer _backgroundSpriteRenderer;
    [SerializeField] protected Transform _tilePointsParent;
    [SerializeField] private Health _health;

    [Header("Event")]
    [SerializeField] protected OnProductionMenuChangedEvent _onProductionMenuChangedEvent;
    [SerializeField] protected OnInformationMenuChangedEvent _onInformationMenuChangedEvent;

    protected string _name;
    protected Sprite _sprite;
    protected Vector2 _dimension;
    protected List<Transform> _tilePoints;
    protected bool _isPlaced;

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
        _backgroundSpriteRenderer.transform.localPosition = new Vector3(_dimension.x / 2, dimension.y / 2, 0f);
        _backgroundSpriteRenderer.transform.localScale = new Vector3(_dimension.x, _dimension.y, 1);
        _health.Init(maxHP);

        for (int x = 0; x < _dimension.x; x++)
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


    private void OnDisable()
    {
        GridManager.Instance.EmptyFilledPoints(_tilePoints);
    }

}

public interface IAttack
{
    public void Attack();
}
