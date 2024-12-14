using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NF.Utilities;


public class BulletPuller : Singleton<BulletPuller>
{

    [SerializeField] private Bullet bulletPrefab; // Reference to the Bullet prefab
    [SerializeField] private int initialPoolSize = 10;
    [SerializeField] private int maxPoolSize = 20;

    private BasePooler<Bullet> _bulletPool;

    private void Awake()
    {
        
        _bulletPool = new BasePooler<Bullet>();
        _bulletPool.InitPool(bulletPrefab, initialPoolSize, maxPoolSize);
  
    }

    public Bullet GetBullet()
    {
        return _bulletPool.Get();
    }

    public void ReleaseBullet(Bullet bullet)
    {
        _bulletPool.Release(bullet);
    }
}


