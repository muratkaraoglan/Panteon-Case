using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierAttack : MonoBehaviour
{
    [SerializeField] private Transform _bodyTransform;

    Coroutine _attackCoroutine;

    public void CheckForAttack(Vector3 entryWorldPoint, ITargetable myTargetable, SoldierUnit mySoldierUnit, SoldierMovement mySoldierMovement)
    {
        print("Entry point: " + entryWorldPoint);

        if (_attackCoroutine != null)
        {
            StopCoroutine(_attackCoroutine);
            _attackCoroutine = null;
        }

         RaycastHit2D raycastHit = Physics2D.Raycast(new Vector2(entryWorldPoint.x, entryWorldPoint.y), Vector2.right);


        print("Target name: " + raycastHit.collider.transform.root.name);
        if (raycastHit.collider != null && raycastHit.collider.transform.root.TryGetComponent(out ITargetable selectedTargatable))
        {
            if (selectedTargatable.UnitID == myTargetable.UnitID) return;//Check if it's an ally

            print("Check For Inrane");
            if (selectedTargatable.IsInAttackRange(transform.position, mySoldierUnit.AttackRange, out Vector3 targetTilePosition))// 
            {
                print("Inrange Attack");
                _attackCoroutine = StartCoroutine(Attack(mySoldierUnit, selectedTargatable));
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
                mySoldierUnit.AttackRange, selectedTargatable);

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
                        _attackCoroutine = StartCoroutine(Attack(mySoldierUnit, selectedTargatable));
                    });
                    break;
                }

            }
        }
    }

    private IEnumerator Attack(SoldierUnit soldierUnit, ITargetable target)
    {
        float fireDelay = 1 / soldierUnit.FireRate;

        while (target.IsAlive && target.IsInAttackRange(transform.position, soldierUnit.AttackRange, out Vector3 targetTilePosition))
        {
            Bullet bullet = BulletPool.Instance.ProvideBullet();

            Vector3 dir = (targetTilePosition - transform.position).normalized;
            soldierUnit.BodyTransform.up = dir;

            bullet.Init(soldierUnit.UnitID, soldierUnit.BulletSpawnPoint.position, soldierUnit.BulletSpawnPoint.up, soldierUnit.Damage);

            yield return Extension.GetWaitForSeconds(fireDelay);
        }
        _attackCoroutine = null;
    }
}
