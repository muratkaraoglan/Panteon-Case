using UnityEngine;

public interface ICoord
{
    public float GetDistance(ICoord other);
    public Vector3 Position { get; set; }
}