using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SoldierMovement : MonoBehaviour
{
    [SerializeField] private float _movementTimePerTile;

    private SoldierUnit _unit;
    private Coroutine _movementCoroutine;

    private void Awake()
    {
        _unit = GetComponent<SoldierUnit>();
    }

    private void Update()
    {
        if (MapItemSelectionHelper.Instance.LastSelectedMapItemGameObject == gameObject)
        {
            if (Input.GetMouseButtonDown(1))
            {
                Plane plane = new Plane(Vector3.forward, Vector3.zero);

                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                float entry;
                if (plane.Raycast(ray, out entry))
                {
                    Vector3 entryWorldPoint = ray.GetPoint(entry);
                    entryWorldPoint.x = (int)entryWorldPoint.x;
                    entryWorldPoint.y = (int)entryWorldPoint.y;
                    entryWorldPoint.z = (int)entryWorldPoint.z;

                    NodeBase targetNode = GridManager.Instance.GetTileAtPosition(entryWorldPoint);

                    if (targetNode == null) return;

                    if (targetNode.IsAreaEmpty((int)_unit.Dimension.x, (int)_unit.Dimension.y))
                    {
                        var path = Pathfinding.FindPath(GridManager.Instance.GetTileAtPosition(new Vector3((int)transform.position.x, (int)transform.position.y, 0)), targetNode);
                        path.Reverse();
                        if (_movementCoroutine != null) StopCoroutine(_movementCoroutine);
                        _movementCoroutine = StartCoroutine(Move(path, targetNode));
                    }
                    else
                    {
                        //check attack for unit
                    }
                }
            }
        }
    }


    IEnumerator Move(List<NodeBase> path, NodeBase targetNode)
    {
        while (path.Count > 0)
        {
            NodeBase nextNode = path[0];
            if (!nextNode.IsAreaEmpty((int)_unit.Dimension.x, (int)_unit.Dimension.y))
            {
                path = Pathfinding.FindPath(GridManager.Instance.GetTileAtPosition(new Vector3((int)transform.position.x, (int)transform.position.y, 0)), targetNode);
                continue;
            }
            path.RemoveAt(0);
            GridManager.Instance.EmptyFilledPoints(_unit.AreaTilePoints);
            transform.position = nextNode.Coords.Position;
            GridManager.Instance.FillEmptyPoints(_unit.AreaTilePoints);
            yield return Extension.GetWaitForSeconds(_movementTimePerTile);
        }
    }
}
