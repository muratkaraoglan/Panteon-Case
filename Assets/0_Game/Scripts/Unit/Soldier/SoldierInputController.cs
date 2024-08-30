using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class SoldierInputController : MonoBehaviour
{
    private SoldierUnit _mySoldierUnit;
    private SoldierMovement _soldierMovement;
    private ITargetable _myTargetable;
    private Plane _groundPlane = new Plane(Vector3.forward, Vector3.zero);

    private void Awake()
    {
        _mySoldierUnit = GetComponent<SoldierUnit>();
        _soldierMovement = GetComponent<SoldierMovement>();
        _myTargetable = GetComponent<ITargetable>();
    }

    private void Update()
    {
        if (MapItemSelectionHelper.Instance.LastSelectedMapItemGameObject != gameObject) return;
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = CameraController.Instance.MainCamera.ScreenPointToRay(Input.mousePosition);

            if (!_groundPlane.Raycast(ray, out float entry)) return;

            Vector3 entryWorldPoint = ray.GetPoint(entry);
            entryWorldPoint.x = (int)entryWorldPoint.x;
            entryWorldPoint.y = (int)entryWorldPoint.y;
            entryWorldPoint.z = (int)entryWorldPoint.z;

            NodeBase selectedNode = GridManager.Instance.GetTileAtPosition(entryWorldPoint);

            if (selectedNode == null) return;

            if (selectedNode.IsEmpty)//Check for movement input
            {
                if (selectedNode.IsAreaEmpty((int)_mySoldierUnit.Dimension.x, (int)_mySoldierUnit.Dimension.y))// Is the selected field suitable for my field?
                {

                    var path = Pathfinding.FindPath(GridManager.Instance.GetTileAtPosition(new Vector3((int)transform.position.x, (int)transform.position.y, 0)), selectedNode);
                    if (path == null) return;
                    print("Move");
                    _soldierMovement.StartMovement(path, selectedNode, _mySoldierUnit);
                    return;
                }
            }
            else// check for attack input
            {
                RaycastHit2D raycastHit = Physics2D.Raycast(new Vector2(entryWorldPoint.x, entryWorldPoint.y), Vector2.right);
                if (raycastHit.collider != null && raycastHit.collider.transform.root.TryGetComponent(out ITargetable selectedTargatable))
                {
                    if (selectedTargatable.UnitID == _myTargetable.UnitID) return;//Check if it's an ally

                    Unit unit = raycastHit.collider.transform.root.GetComponent<Unit>();
                    Vector3 selectedNodeParentPosition = unit.transform.position;

                    selectedNodeParentPosition.x = (int)selectedNodeParentPosition.x;
                    selectedNodeParentPosition.y = (int)selectedNodeParentPosition.y;
                    selectedNodeParentPosition.z = (int)selectedNodeParentPosition.z;

                    List<NodeBase> fitAreas = Pathfinding.FindAttackableAreas(selectedNodeParentPosition, (int)unit.Dimension.x, (int)unit.Dimension.y, (int)_mySoldierUnit.Dimension.x, (int)_mySoldierUnit.Dimension.y, _mySoldierUnit.AttackRange);
                    if (fitAreas.Count == 0) return;

                    Vector3 myPosition = transform.position;

                    NodeBase myNode = GridManager.Instance.GetTileAtPosition(new Vector3((int)myPosition.x, (int)myPosition.y, 0));

                    fitAreas.Sort((a, b) => (a.Coords.Position - myNode.Coords.Position).sqrMagnitude.CompareTo((b.Coords.Position - myNode.Coords.Position).sqrMagnitude));

                    for (int i = 0; i < fitAreas.Count; i++)
                    {
                        List<NodeBase> path = Pathfinding.FindPath(myNode, fitAreas[i]);
                        //print(i + ": " + fitAreas[i].Coords.Position + " Distance: " + (fitAreas[i].Coords.Position - myNode.Coords.Position).sqrMagnitude);
                        if (path != null && path.Count > 0)
                        {
                            _soldierMovement.StartMovement(path, fitAreas[i], _mySoldierUnit, () =>
                            {
                                print("Attack");

                            });
                            break;
                        }

                    }
                }
            }
        }

    }
}
