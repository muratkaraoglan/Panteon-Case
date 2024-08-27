using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : Singleton<GridManager>
{
    [SerializeField] private ScriptableGrid _scriptableGrid;
    [SerializeField] private GameObject _mapPrefab;
    public Dictionary<Vector3, NodeBase> Tiles { get; private set; }

    private BoxCollider2D _mapCollider;
    protected override void Awake()
    {
        base.Awake();
        Tiles = _scriptableGrid.GenerateGrid();
        GameObject map = Instantiate(_mapPrefab);
        SpriteRenderer mapSprite = map.GetComponent<SpriteRenderer>();
        mapSprite.size = new Vector2(_scriptableGrid.Width, _scriptableGrid.Height);
        _mapCollider = map.GetComponent<BoxCollider2D>();
        _mapCollider.size = new Vector2(_scriptableGrid.Width, _scriptableGrid.Height);
        _mapCollider.offset = new Vector2(_scriptableGrid.Width / 2, _scriptableGrid.Height / 2);
    }

    public BoxCollider2D MapCollider => _mapCollider;

    public NodeBase GetTileAtPosition(Vector3 pos) => Tiles.TryGetValue(pos, out var tile) ? tile : null;

    public bool IsPointsValidToPlace(List<Transform> points)
    {
        int validPointCount = 0;
        for (int i = 0; i < points.Count; i++)
        {
            var point = points[i].position;
            point.x = (int)point.x;
            point.y = (int)point.y;
            point.z = 0;
            var node = GetTileAtPosition(point);
            if (node == null) continue;
            if (!node.IsEmpty) continue;
            validPointCount++;
        }
        return validPointCount == points.Count;
    }

    public void FillEmptyPoints(List<Transform> points)
    {
        for (int i = 0;i < points.Count;i++)
        {
            var point = points[i].position;
            point.x = (int)point.x;
            point.y = (int)point.y;
            point.z = 0;
            var node = GetTileAtPosition(point);
            node.IsEmpty = false;
        }
    }

}
