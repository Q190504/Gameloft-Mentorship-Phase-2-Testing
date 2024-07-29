using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    public static BulletManager Instance;

    [Header("Bullet Settings")]
    public BaseBullet bulletPrefab;
    public int poolSize = 10;

    private Queue<BaseBullet> bulletPool;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        bulletPool = new Queue<BaseBullet>();

        for (int i = 0; i < poolSize; i++)
        {
            BaseBullet bullet = Instantiate(bulletPrefab);
            bullet.gameObject.SetActive(false);
            bulletPool.Enqueue(bullet);
        }
    }

    public BaseBullet GetBullet()
    {
        if (bulletPool.Count > 0)
        {
            BaseBullet bullet = bulletPool.Dequeue();
            bullet.gameObject.SetActive(true);
            return bullet;
        }
        else
        {
            BaseBullet bullet = Instantiate(bulletPrefab);
            bullet.gameObject.SetActive(true);
            return bullet;
        }
    }

    public void ReturnBullet(BaseBullet bullet)
    {
        bullet.gameObject.SetActive(false);
        bulletPool.Enqueue(bullet);
    }
}
