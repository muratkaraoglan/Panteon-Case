using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;


public class SoldierInputController : MonoBehaviour
{
    private SoldierUnit _mySoldierUnit;
    private SoldierMovement _mySoldierMovement;
    private SoldierAttack _soldierAttack;
    private ITargetable _myTargetable;
    private Plane _groundPlane = new Plane(Vector3.forward, Vector3.zero);

    private void Awake()
    {
        _mySoldierUnit = GetComponent<SoldierUnit>();
        _mySoldierMovement = GetComponent<SoldierMovement>();
        _soldierAttack = GetComponent<SoldierAttack>();
        _myTargetable = GetComponent<ITargetable>();
    }

    private void Update()
    {
        if (MapItemSelectionHelper.Instance.LastSelectedMapItemGameObject != gameObject) return;
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = CameraController.Instance.MainCamera.ScreenPointToRay(Input.mousePosition);

            if (!_groundPlane.Raycast(ray, out float entry)) return;

            Vector3 entryWorldPoint = ray.GetPoint(entry).ToInt();

            NodeBase selectedNode = GridManager.Instance.GetTileAtPosition(entryWorldPoint);

            if (selectedNode == null) return;

            if (selectedNode.IsEmpty)//Check for movement input
            {
                if (selectedNode.IsAreaEmpty((int)_mySoldierUnit.Dimension.x, (int)_mySoldierUnit.Dimension.y))// Is the selected field suitable for my field?
                {
                    var path = Pathfinding.FindPath(GridManager.Instance.GetTileAtPosition(transform.position.ToInt()), selectedNode);

                    if (path == null) return;

                    _mySoldierMovement.StartMovement(path, selectedNode, _mySoldierUnit);
                    return;
                }
            }
            else// check for attack input
            {
                _soldierAttack.CheckForAttack(entryWorldPoint, _myTargetable, _mySoldierUnit, _mySoldierMovement);
            }
        }
    }
}
