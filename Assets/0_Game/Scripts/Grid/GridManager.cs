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

        foreach (var tile in Tiles.Values) tile.CacheNeighbors();

        GameObject map = Instantiate(_mapPrefab);
        SpriteRenderer mapSprite = map.GetComponent<SpriteRenderer>();
        mapSprite.size = new Vector2(_scriptableGrid.Width, _scriptableGrid.Height);

        _mapCollider = map.GetComponent<BoxCollider2D>();
        _mapCollider.size = new Vector2(_scriptableGrid.Width, _scriptableGrid.Height);
        _mapCollider.offset = new Vector2(_scriptableGrid.Width / 2, _scriptableGrid.Height / 2);

        Vector3 mapPosition = map.transform.position;//I changed its position in the forward direction because sometimes it detects the map when it should be detecting units while casting a ray.
        mapPosition.z++;
        map.transform.position = mapPosition;
    }

    public BoxCollider2D MapCollider => _mapCollider;

    public NodeBase GetTileAtPosition(Vector3 pos) => Tiles.TryGetValue(pos, out var tile) ? tile : null;

    public bool IsPointsValidToPlace(List<Transform> points)
    {
        int validPointCount = 0;
        for (int i = 0; i < points.Count; i++)
        {
            var point = points[i].position.ToInt();

            var node = GetTileAtPosition(point);

            if (node == null) continue;
            if (!node.IsEmpty) continue;
            validPointCount++;
        }
        return validPointCount == points.Count;
    }

    /// <summary>
    /// Changing the state of the points occupied by the object placed on the map to full
    /// </summary>
    /// <param name="points"></param>
    public void FillEmptyPoints(List<Transform> points, Transform root)
    {
        for (int i = 0; i < points.Count; i++)
        {
            var point = points[i].position.ToInt();

            var node = GetTileAtPosition(point);
            node.OccupiedTransfrom = root;
            node.IsEmpty = false;
        }
    }
    /// <summary>
    /// Changing the status of points destroyed from the map or occupied by the moved object to empty
    /// </summary>
    /// <param name="points"></param>
    public void EmptyFilledPoints(List<Transform> points)
    {
        for (int i = 0; i < points.Count; i++)
        {
            var point = points[i].position.ToInt();
       
            var node = GetTileAtPosition(point);
            node.IsEmpty = true;
        }
    }

}
