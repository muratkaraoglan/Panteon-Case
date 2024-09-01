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

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.transform.root.TryGetComponent(out ITargetable targetable))
        {
            if (targetable.UnitID == _bulletID) return;//ally
            targetable.TakeDamage(_damage);
            BulletPool.Instance.PutBackToPool(this);
            gameObject.SetDisable();
        }
    }

    IEnumerator Move()
    {
        float elapsedTime = 0;
        while (elapsedTime <= _bulletLifeTime)
        {
            var dt = Time.deltaTime;
            elapsedTime += dt;
            transform.Translate(_bulletSpeed * dt * _moveDirection, Space.Self);
            yield return null;
        }
        BulletPool.Instance.PutBackToPool(this);
        gameObject.SetDisable();
    }
}
