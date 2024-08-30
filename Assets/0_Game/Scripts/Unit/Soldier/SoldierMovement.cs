using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SoldierMovement : MonoBehaviour
{
    [SerializeField] private float _movementTimePerTile;
    private Coroutine _movementCoroutine;

    public void StartMovement(List<NodeBase> path, NodeBase targetNode, SoldierUnit unit, Action onMovementComplete = null)
    {
        path.Reverse();
        if (_movementCoroutine != null) StopCoroutine(_movementCoroutine);
        _movementCoroutine = StartCoroutine(Move(path, targetNode, unit, onMovementComplete));
    }

    IEnumerator Move(List<NodeBase> path, NodeBase targetNode, SoldierUnit unit, Action onMovementComplete = null)
    {
        while (path.Count > 0)
        {
            NodeBase nextNode = path[0];
            if (!nextNode.IsAreaEmpty((int)unit.Dimension.x, (int)unit.Dimension.y))
            {
                path = Pathfinding.FindPath(GridManager.Instance.GetTileAtPosition(new Vector3((int)transform.position.x, (int)transform.position.y, 0)), targetNode);
                continue;
            }
            path.RemoveAt(0);

            GridManager.Instance.EmptyFilledPoints(unit.AreaTilePoints);
            transform.position = nextNode.Coords.Position;
            GridManager.Instance.FillEmptyPoints(unit.AreaTilePoints);

            yield return Extension.GetWaitForSeconds(_movementTimePerTile);
        }
        _movementCoroutine = null;
        onMovementComplete?.Invoke();
    }
}
