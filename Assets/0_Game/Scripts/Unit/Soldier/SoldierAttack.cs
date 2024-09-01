using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierAttack : MonoBehaviour
{
    [SerializeField] private Transform _bodyTransform;

    Coroutine _attackCoroutine;

    public void CheckForAttack(Vector3 entryWorldPoint, ITargetable myTargetable, SoldierUnit mySoldierUnit, SoldierMovement mySoldierMovement)
    {
        StopOnGoingAttack();

        Transform targetRoot = GridManager.Instance.GetTileAtPosition(entryWorldPoint).OccupiedTransfrom;

        if (targetRoot == null) return;

        if (!targetRoot.TryGetComponent(out ITargetable selectedTargatable)) return;

        if (IsAlly(selectedTargatable, myTargetable)) return;//Check if it's an ally

        if (selectedTargatable.IsInAttackRange(transform.position, mySoldierUnit.AttackRange, out Vector3 targetTilePosition))
        {
            _attackCoroutine = StartCoroutine(Attack(mySoldierUnit, selectedTargatable));
            return;
        }

        MoveTowardsAttackablePosition(mySoldierUnit, mySoldierMovement, targetRoot, selectedTargatable);
    }

    private void StopOnGoingAttack()
    {
        if (_attackCoroutine != null)
        {
            StopCoroutine(_attackCoroutine);
            _attackCoroutine = null;
        }
    }

    private bool IsAlly(ITargetable selectedTargetable, ITargetable myTargetable)
    {
        return selectedTargetable.UnitID == myTargetable.UnitID;
    }

    private void MoveTowardsAttackablePosition(SoldierUnit mySoldierUnit, SoldierMovement mySoldierMovement, Transform targetRoot, ITargetable selectedTargatable)
    {
        Unit unit = targetRoot.GetComponent<Unit>();

        Vector3 selectedNodeParentPosition = unit.transform.position.ToInt();

        List<NodeBase> fitAreas = Pathfinding.FindAttackableAreas(
            selectedNodeParentPosition,
            (int)unit.Dimension.x,
            (int)unit.Dimension.y,
            (int)mySoldierUnit.Dimension.x,
            (int)mySoldierUnit.Dimension.y,
            mySoldierUnit.AttackRange, selectedTargatable);

        if (fitAreas.Count == 0) return;

        Vector3 myPosition = transform.position.ToInt();

        NodeBase myNode = GridManager.Instance.GetTileAtPosition(myPosition);

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
