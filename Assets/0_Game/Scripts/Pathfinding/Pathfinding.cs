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

            foreach (var neighbor in current.Neighbors.Where(t => !t.IsEmpty && !processed.Contains(t)))
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

    public static NodeBase FindClosestEmptyArea(NodeBase startNode, int requiredWidth, int requiredHeight)
    {
        var toSearch = new List<NodeBase>() { startNode };
        var processed = new List<NodeBase>();

        while (toSearch.Any())
        {
            var current = toSearch[0];
            foreach (var t in toSearch)
                if (t.F < current.F || (t.F == current.F && t.H < current.H)) current = t;

            processed.Add(current);
            toSearch.Remove(current);


            if (current.IsAreaEmpty(requiredWidth, requiredHeight))
            {
                return current;
            }


            foreach (var neighbor in current.Neighbors.Where(t => !processed.Contains(t)))
            {
                var inSearch = toSearch.Contains(neighbor);
                var costToNeighbor = current.G + current.GetDistance(neighbor);

                if (!inSearch || costToNeighbor < neighbor.G)
                {
                    neighbor.SetG(costToNeighbor);
                    neighbor.SetConnection(current);

                    if (!inSearch)
                    {
                        neighbor.SetH(neighbor.GetDistance(startNode));
                        toSearch.Add(neighbor);
                    }
                }
            }
        }

        return null;
    }
    /// <summary>
    /// Searches for empty space clockwise starting from the bottom left corner of unit
    /// </summary>
    /// <param name="unitPosition"></param>
    /// <param name="areawidth"></param>
    /// <param name="areaHeight"></param>
    /// <param name="requiredWidth"></param>
    /// <param name="requiredHeight"></param>
    /// <returns></returns>
    public static NodeBase FindTileToSpawn(Vector3 unitPosition, int areawidth, int areaHeight, int requiredWidth, int requiredHeight)
    {
        Vector3 startPosition = unitPosition - new Vector3(1, 1, 0);//bottom left corner

        int yEdgeLenght = areaHeight + 2;
        int xEdgeLenght = areawidth + 2;

        for (int i = (int)startPosition.y; i < (int)startPosition.y + yEdgeLenght; i++)
        {
            NodeBase current = GridManager.Instance.GetTileAtPosition(new Vector3(startPosition.x, i, 0));

            if (current == null) continue;

            if (current.IsAreaEmpty(requiredWidth, requiredHeight)) return current;

        }

        for (int j = (int)startPosition.x; j < (int)startPosition.x + xEdgeLenght; j++)
        {
            NodeBase current = GridManager.Instance.GetTileAtPosition(new Vector3(j, (int)startPosition.y + yEdgeLenght - 1, 0));
            if (current == null) continue;

            if (current.IsAreaEmpty(requiredWidth, requiredHeight)) return current;
        }

        for (int k = (int)startPosition.y + yEdgeLenght - 1; k >= (int)startPosition.y; k--)
        {
            NodeBase current = GridManager.Instance.GetTileAtPosition(new Vector3(startPosition.x + xEdgeLenght - 1, k, 0));

            if (current == null) continue;

            if (current.IsAreaEmpty(requiredWidth, requiredHeight)) return current;
        }

        for (int l = (int)startPosition.x + xEdgeLenght - 1; l >= (int)startPosition.x; l--)
        {
            NodeBase current = GridManager.Instance.GetTileAtPosition(new Vector3(l, startPosition.y, 0));

            if (current == null) continue;

            if (current.IsAreaEmpty(requiredWidth, requiredHeight)) return current;
        }

        return null;
    }




}
