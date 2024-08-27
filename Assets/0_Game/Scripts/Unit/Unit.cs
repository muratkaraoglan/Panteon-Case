using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour, IPlacable
{
    [SerializeField] private SpriteRenderer _bodySpriteRenderer;
    [SerializeField] private SpriteRenderer _backgroundSpriteRenderer;
    [SerializeField] private Transform _tilePointsParent;

    private string _name;
    private Sprite _sprite;
    private Vector2 _dimension;
    private List<Transform> _tilePoints;

    public void Init(string name, Sprite sprite, Vector2 dimension)
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

    public void Place()
    {
        GridManager.Instance.FillEmptyPoints(_tilePoints);
    }
}
