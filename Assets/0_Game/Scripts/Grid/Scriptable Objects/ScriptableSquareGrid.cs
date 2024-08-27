using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Square Grid", menuName = "Grid/Square Grid")]
public class ScriptableSquareGrid : ScriptableGrid
{
    public override Dictionary<Vector3, NodeBase> GenerateGrid()
    {
        var tiles = new Dictionary<Vector3, NodeBase>();
        var grid = new GameObject { name = "Grid" };

        for (int x = 0; x < _gridWidth; x += _cellWidth)
        {
            for (int y = 0; y < _gridWidth; y += _cellHeight)
            {
                var tile = Instantiate(_nodePrefab, grid.transform);
                Vector3 pos = new Vector3(x, y, 0);
                tile.Init(true, new SquareCoords { Position = pos });
                tiles.Add(pos, tile);
            }
        }

        return tiles;
    }
}
