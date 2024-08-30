using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierAttack : MonoBehaviour
{
    private ITargetable _targetable;
    private Plane _groundPlane = new Plane(Vector3.forward, Vector3.zero);
    private void Awake()
    {
        _targetable = GetComponent<ITargetable>();
    }

    //private void Update()
    //{
    //    if (MapItemSelectionHelper.Instance.LastSelectedMapItemGameObject != gameObject) return;
    //    if (Input.GetMouseButtonDown(1))
    //    {
    //        Ray ray = CameraController.Instance.MainCamera.ScreenPointToRay(Input.mousePosition);

    //        if (!_groundPlane.Raycast(ray, out float entry)) return;

    //        Vector3 entryWorldPoint = ray.GetPoint(entry);
    //        entryWorldPoint.x = (int)entryWorldPoint.x;
    //        entryWorldPoint.y = (int)entryWorldPoint.y;
    //        entryWorldPoint.z = (int)entryWorldPoint.z;

    //        NodeBase nodeBase = GridManager.Instance.GetTileAtPosition(entryWorldPoint);

    //        if (nodeBase == null || nodeBase.IsEmpty) return;

    //        RaycastHit2D raycastHit = Physics2D.Raycast(new Vector2(entryWorldPoint.x, entryWorldPoint.y), Vector2.right);

    //        if (raycastHit.collider != null && raycastHit.collider.transform.root.TryGetComponent(out ITargetable targetable))
    //        {
    //            if (targetable.UnitID != _targetable.UnitID)
    //            {



    //            }
    //        }
    //    }
    //}
}
