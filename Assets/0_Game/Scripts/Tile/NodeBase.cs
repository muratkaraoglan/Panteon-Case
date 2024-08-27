using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NodeBase : MonoBehaviour
{
    public static readonly int MOVE_STRAIGHT_COST = 10;
    public static readonly int MOVE_DIAGONAL_COST = 14;

    public ICoord Coords;
    [field: SerializeField] public bool IsEmpty { get; set; }
    public virtual void Init(bool isEmpty, ICoord coord)
    {
        IsEmpty = isEmpty;
        Coords = coord;
        transform.position = Coords.Position;
    }

    public float GetDistance(NodeBase otherNode) => Coords.GetDistance(otherNode.Coords);

    #region Pathfinding
    public List<NodeBase> Neighbors { get; protected set; }
    public NodeBase Connection { get; private set; }
    public float G {  get; private set; }
    public float H { get; private set; }
    public float F => G + H;

    public abstract void CacheNeighbors();

    public void SetConnection(NodeBase nodeBase)
    {
        Connection = nodeBase;
    }

    public void SetG(float g)=>G = g;
    public void SetH(float h)=> H = h;

    #endregion
}
