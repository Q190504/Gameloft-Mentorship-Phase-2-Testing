using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [Header("Enemy Settings")]
    public BaseEnemy enemyPrefab;
    public int poolSize = 10;

    private Queue<BaseEnemy> enemyPool;

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
        enemyPool = new Queue<BaseEnemy>();

        for (int i = 0; i < poolSize; i++)
        {
            BaseEnemy enemy = Instantiate(enemyPrefab);
            enemy.gameObject.SetActive(false);
            enemyPool.Enqueue(enemy);
        }
    }

    public BaseEnemy GetEnemy()
    {
        if (enemyPool.Count > 0)
        {
            BaseEnemy enemy = enemyPool.Dequeue();
            enemy.gameObject.SetActive(true);
            return enemy;
        }
        else
        {
            BaseEnemy enemy = Instantiate(enemyPrefab);
            enemy.gameObject.SetActive(true);
            return enemy;
        }
    }

    public void ReturnEnemy(BaseEnemy enemy)
    {
        enemy.gameObject.SetActive(false);
        enemyPool.Enqueue(enemy);
    }
}
