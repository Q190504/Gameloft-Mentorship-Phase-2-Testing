using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : BaseCharacter
{
    public int damage;

    public FloatPublisherSO attackSO;
    public BaseEnemyPublisherSO returnToEnemyManagerSO;



    private void OnEnable()
    {
        currentHealth = maxHealth;
    }

    public override void Attack()
    {
        attackSO.RaiseEvent(-damage);
    }

    public override void Die()
    {
        returnToEnemyManagerSO.RaiseEvent(this);
    }

    public void Move(Vector3 position)
    {
        Vector3 direction = (position - this.transform.position).normalized;
        this.transform.position += speed * Time.deltaTime * direction;
    }

    public override void ChangeHealth(float health)
    {
        currentHealth -= health;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Attack();
        }
    }
}
