using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ScriptableGrid : ScriptableObject
{
    [SerializeField] protected NodeBase _nodePrefab;
    [SerializeField, Range(10, 128)] protected int _gridWidth = 100;
    [SerializeField, Range(10, 128)] protected int _gridHeight = 100;
    [SerializeField, Min(1)] protected int _cellWidth = 1;
    [SerializeField, Min(1)] protected int _cellHeight = 1;

    public abstract Dictionary<Vector3, NodeBase> GenerateGrid();
    public int Width => _gridWidth;
    public int Height => _gridHeight;
}
