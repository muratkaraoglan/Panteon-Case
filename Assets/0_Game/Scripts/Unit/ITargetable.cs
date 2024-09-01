using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITargetable
{
    public int UnitID { get; }
    /// <summary>
    /// Are there any tile points of targetable unit in attack range?
    /// </summary>
    /// <param name="position"></param>
    /// <param name="range"></param>
    /// <returns></returns>
    public bool IsInAttackRange(Vector3 position, int range, out Vector3 targetTilePosition);

    public void TakeDamage(int damage);

    public bool IsAlive { get; }
}
