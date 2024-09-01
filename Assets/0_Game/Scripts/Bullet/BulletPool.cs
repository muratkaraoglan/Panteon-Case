using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BulletPool : Singleton<BulletPool>
{
    [SerializeField] private Bullet _bulletPrefab;


    private List<Bullet> _bulletPool = new List<Bullet>();

    public Bullet ProvideBullet()
    {
        Bullet bullet;

        if (_bulletPool.Count == 0) bullet = Instantiate(_bulletPrefab, transform);
        else
        {
            bullet = _bulletPool[0];
            _bulletPool.RemoveAt(0);
        }
        return bullet;
    }

    public void PutBackToPool(Bullet bullet) => _bulletPool.Add(bullet);
}
