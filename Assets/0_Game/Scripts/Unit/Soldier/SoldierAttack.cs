using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierAttack : MonoBehaviour
{

    public void CheckForAttack(Vector3 entryWorldPoint, ITargetable myTargetable, SoldierUnit mySoldierUnit, SoldierMovement mySoldierMovement)
    {
        RaycastHit2D raycastHit = Physics2D.Raycast(new Vector2(entryWorldPoint.x, entryWorldPoint.y), Vector2.right);

        if (raycastHit.collider != null && raycastHit.collider.transform.root.TryGetComponent(out ITargetable selectedTargatable))
        {
            if (selectedTargatable.UnitID == myTargetable.UnitID) return;//Check if it's an ally
            if ((entryWorldPoint - mySoldierUnit.transform.position).sqrMagnitude <= mySoldierUnit.AttackRange * mySoldierUnit.AttackRange)
            {
                print("Inrange Attack");
                return;
            }

            Unit unit = raycastHit.collider.transform.root.GetComponent<Unit>();
            Vector3 selectedNodeParentPosition = unit.transform.position;

            selectedNodeParentPosition.x = (int)selectedNodeParentPosition.x;
            selectedNodeParentPosition.y = (int)selectedNodeParentPosition.y;
            selectedNodeParentPosition.z = (int)selectedNodeParentPosition.z;

            List<NodeBase> fitAreas = Pathfinding.FindAttackableAreas(
                selectedNodeParentPosition,
                (int)unit.Dimension.x,
                (int)unit.Dimension.y,
                (int)mySoldierUnit.Dimension.x,
                (int)mySoldierUnit.Dimension.y,
                mySoldierUnit.AttackRange);

            if (fitAreas.Count == 0) return;

            Vector3 myPosition = transform.position;

            NodeBase myNode = GridManager.Instance.GetTileAtPosition(new Vector3((int)myPosition.x, (int)myPosition.y, 0));

            fitAreas.Sort((a, b) => (a.Coords.Position - myNode.Coords.Position).sqrMagnitude.CompareTo((b.Coords.Position - myNode.Coords.Position).sqrMagnitude));

            for (int i = 0; i < fitAreas.Count; i++)
            {
                List<NodeBase> path = Pathfinding.FindPath(myNode, fitAreas[i]);
                ;
                if (path != null && path.Count > 0)
                {
                    mySoldierMovement.StartMovement(path, fitAreas[i], mySoldierUnit, () =>
                    {
                        print("Attack");

                    });
                    break;
                }

            }
        }
    }
}
