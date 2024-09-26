using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField, Min(1)] private float _bulletSpeed;
    [SerializeField, Min(.1f)] private float _bulletLifeTime;

    private int _bulletID;
    private int _damage;
    private Vector2 _moveDirection;
    public void Init(int bulletID, Vector2 spawnPoint, Vector2 moveDirection, int damage)
    {
        transform.position = spawnPoint;
        gameObject.SetEnable();
        _bulletID = bulletID;
        _damage = damage;
        _moveDirection = moveDirection;

        StartCoroutine(Move());
    }

    IEnumerator Move()
    {
        float elapsedTime = 0;
        NodeBase previousNode = null;

        while (elapsedTime <= _bulletLifeTime)
        {
            var dt = Time.deltaTime;
            elapsedTime += dt;
            transform.Translate(_bulletSpeed * dt * _moveDirection, Space.Self);

            yield return null;

            //Collision check
            Vector3Int currentPos = Vector3Int.RoundToInt(transform.position);
            if (previousNode == null || previousNode.Coords.Position != currentPos)
            {
                previousNode = GridManager.Instance.GetTileAtPosition(currentPos);
            }

            if (previousNode == null || previousNode.IsEmpty) continue;

            ITargetable targetable = previousNode.OccupiedTransfrom.GetComponent<ITargetable>();

            if (targetable.UnitID == _bulletID) continue;

            targetable.TakeDamage(_damage);
            break;
        }
        gameObject.SetDisable();
    }

    private void OnDisable()
    {
        BulletPool.Instance.PutBackToPool(this);
    }
}
