using System.Collections.Generic;
using UnityEngine;

public class BulletPool : Singleton<BulletPool>
{
    public GameObject bulletPrefab;
    List<GameObject> pool;

    public void InitializePool(int bulletAmount)
    {
        pool = new List<GameObject>();
        for (int i = 0; i < bulletAmount; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab);
            bullet.SetActive(false);
            pool.Add(bullet);
        }
    }

    public GameObject GetBullet()
    {
        foreach (GameObject bullet in pool)
        {
            if (!bullet.activeInHierarchy)
                return bullet;
        }
        return null;
    }
    public void ReturnBullet() 
    {
        foreach (GameObject bullet in pool)
        {
            bullet.SetActive(false);
        }
    }
}
