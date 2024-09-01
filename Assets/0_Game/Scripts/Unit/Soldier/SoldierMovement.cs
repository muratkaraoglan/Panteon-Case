using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierMovement : MonoBehaviour
{
    [SerializeField] private float _movementTimePerTile;
    [SerializeField] private Transform _bodyTransform;
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

            Vector3 dir = (nextNode.Coords.Position - transform.position).normalized;

            _bodyTransform.localRotation = Quaternion.Euler(DirToEulerAngle(dir));

            GridManager.Instance.EmptyFilledPoints(unit.AreaTilePoints);
            transform.position = nextNode.Coords.Position;
            GridManager.Instance.FillEmptyPoints(unit.AreaTilePoints);

            yield return Extension.GetWaitForSeconds(_movementTimePerTile);
        }
        _movementCoroutine = null;
        onMovementComplete?.Invoke();
    }

    public Vector3 DirToEulerAngle(Vector3 direction)
    {
        direction = direction.normalized;

        switch (direction)
        {
            case Vector3 v when v == Vector3.up:
                return Vector3.zero;
            case Vector3 v when v == Vector3.down:
                return new Vector3(0, 0, 180);
            case Vector3 v when v == Vector3.right:
                return new Vector3(0, 0, -90);
            case Vector3 v when v == Vector3.left:
                return new Vector3(0, 0, 90);
            default:
                throw new System.ArgumentException("Direction vector is not valid");
        }
    }

}
