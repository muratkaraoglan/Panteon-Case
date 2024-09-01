using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class Pathfinding
{
    public static List<NodeBase> FindPath(NodeBase startNode, NodeBase targetNode)
    {
        var toSearch = new List<NodeBase>() { startNode };
        var processed = new List<NodeBase>();

        if (startNode.IsEmpty) return null;

        while (toSearch.Any())
        {
            var current = toSearch[0];
            foreach (var t in toSearch)
                if (t.F < current.F || t.F == current.F && t.H < current.H) current = t;

            processed.Add(current);
            toSearch.Remove(current);

            if (current == targetNode)
            {
                var currentPathTile = targetNode;
                var path = new List<NodeBase>();
                var count = 100;

                while (currentPathTile != startNode)
                {
                    path.Add(currentPathTile);
                    currentPathTile = currentPathTile.Connection;
                    count--;
                    if (count < 0) throw new Exception();
                }

                return path;
            }

            foreach (var neighbor in current.Neighbors.Where(t => t.IsEmpty && !processed.Contains(t)))
            {
                var inSearch = toSearch.Contains(neighbor);

                var costToNeighbor = current.G + current.GetDistance(neighbor);

                if (!inSearch || costToNeighbor < neighbor.G)
                {
                    neighbor.SetG(costToNeighbor);
                    neighbor.SetConnection(current);

                    if (!inSearch)
                    {
                        neighbor.SetH(neighbor.GetDistance(targetNode));
                        toSearch.Add(neighbor);
                    }
                }
            }
        }
        return null;
    }

    /// <summary>
    /// Searches for empty space clockwise starting from the bottom left corner of production unit
    /// </summary>
    /// <param name="unitPosition"></param>
    /// <param name="areawidth"></param>
    /// <param name="areaHeight"></param>
    /// <param name="requiredWidth"></param>
    /// <param name="requiredHeight"></param>
    /// <returns></returns>
    public static NodeBase FindTileToSpawn(Vector3 unitPosition, int areawidth, int areaHeight, int requiredWidth, int requiredHeight, int scanDeep)
    {
        Vector3 startPosition = unitPosition - new Vector3(1, 1, 0);//bottom left corner
        scanDeep = Mathf.Max(scanDeep, 1);//must be at least 1

        int yEdgeLenght = areaHeight + 2;
        int xEdgeLenght = areawidth + 2;
        for (int s = 0; s < scanDeep; s++)
        {
            for (int i = (int)startPosition.y; i < (int)startPosition.y + yEdgeLenght; i++)
            {
                if (CheckAreaFit(new Vector3(startPosition.x, i, 0), requiredWidth, requiredHeight, out NodeBase nodebase)) return nodebase;
            }

            for (int j = (int)startPosition.x + 1; j < (int)startPosition.x + xEdgeLenght; j++)
            {
                if (CheckAreaFit(new Vector3(j, (int)startPosition.y + yEdgeLenght - 1, 0), requiredWidth, requiredHeight, out NodeBase nodebase)) return nodebase;
            }

            for (int k = (int)startPosition.y + yEdgeLenght - 2; k >= (int)startPosition.y; k--)
            {
                if (CheckAreaFit(new Vector3(startPosition.x + xEdgeLenght - 1, k, 0), requiredWidth, requiredHeight, out NodeBase nodebase)) return nodebase;
            }

            for (int l = (int)startPosition.x + xEdgeLenght - 2; l >= (int)startPosition.x; l--)
            {
                if (CheckAreaFit(new Vector3(l, startPosition.y, 0), requiredWidth, requiredHeight, out NodeBase nodebase)) return nodebase;
            }

            yEdgeLenght += 2;
            xEdgeLenght += 2;
            startPosition -= new Vector3(1, 1, 0);
        }
        return null;

        
    }
    /// <summary>
    /// Check unit dimension fit on point
    /// </summary>
    /// <param name="nodePosition"></param>
    /// <param name="requiredWidth"></param>
    /// <param name="requiredHeight"></param>
    /// <param name="nodeBase"></param>
    /// <returns></returns>
    static bool CheckAreaFit(Vector3 nodePosition, int requiredWidth, int requiredHeight, out NodeBase nodeBase)
    {
        nodeBase = GridManager.Instance.GetTileAtPosition(nodePosition);

        if (nodeBase == null) return false;

        if (nodeBase.IsAreaEmpty(requiredWidth, requiredHeight)) return true;
        return false;
    }

    /// <summary>
    /// Find attackable areas that attacker attack range and area fit
    /// Searches   clockwise starting from the bottom left corner of unit 
    /// </summary>
    /// <param name="unitPosition"></param>
    /// <param name="areawidth"></param>
    /// <param name="areaHeight"></param>
    /// <param name="requiredWidth"></param>
    /// <param name="requiredHeight"></param>
    /// <param name="scanDeep"></param>
    /// <returns></returns>
    public static List<NodeBase> FindAttackableAreas(Vector3 unitPosition, int areawidth, int areaHeight, int requiredWidth, int requiredHeight, int attackRange, ITargetable target)
    {
        List<NodeBase> fitAreas = new List<NodeBase>();
        Vector3 startPosition = unitPosition - new Vector3(1, 1, 0);
        int yEdgeLenght = areaHeight + 2;
        int xEdgeLenght = areawidth + 2;

        for (int s = 0; s < attackRange; s++)
        {
            for (int i = (int)startPosition.y; i < (int)startPosition.y + yEdgeLenght; i++)//bottom left to top left
            {
                CheckAreaAndAttackRange( new Vector3(startPosition.x, i, 0), requiredWidth, requiredHeight, attackRange, fitAreas, target);
            }

            for (int j = (int)startPosition.x + 1; j < (int)startPosition.x + xEdgeLenght; j++)//top left to top right
            {
                CheckAreaAndAttackRange( new Vector3(j, (int)startPosition.y + yEdgeLenght - 1, 0), requiredWidth, requiredHeight, attackRange, fitAreas, target);
            }

            for (int k = (int)startPosition.y + yEdgeLenght - 2; k >= (int)startPosition.y; k--)//top right to bottom right
            {
                CheckAreaAndAttackRange( new Vector3(startPosition.x + xEdgeLenght - 1, k, 0), requiredWidth, requiredHeight, attackRange, fitAreas, target);
            }

            for (int l = (int)startPosition.x + xEdgeLenght - 2; l > (int)startPosition.x; l--)//bottom right to bottom left
            {
                CheckAreaAndAttackRange( new Vector3(l, startPosition.y, 0), requiredWidth, requiredHeight, attackRange, fitAreas, target);
            }

            yEdgeLenght += 2;
            xEdgeLenght += 2;
            startPosition -= new Vector3(1, 1, 0);
         
        }

        static void CheckAreaAndAttackRange(Vector3 searchingPosition, int requiredWidth, int requiredHeight, int attackRange, List<NodeBase> nodeList, ITargetable target)
        {
            if (!CheckAreaFit(searchingPosition, requiredWidth, requiredHeight, out NodeBase nodeBase)) return;
            if (target.IsInAttackRange(nodeBase.Coords.Position, attackRange)) nodeList.Add(nodeBase);

        }

        return fitAreas;
    }

   


}
