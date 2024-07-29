using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Unity.Mathematics;
using System;

public class MainCharacter : BaseCharacter
{
    public FloatPublisherSO changeHealthSO;
    public VectorPublisherSO sendPositionToEnemiesSO;
    public VectorPublisherSO sendPositionToSlimeOrbsSO;

    public float totalTimeToCastSkill;
    public float currentTimeToCastSkill;

    public float minimizeScaleFactor;
    public float maximizeScaleFactor;

    public int minSpeed;
    public int maxSpeed;

    // Start is called before the first frame update
    void Start()
    {
        currentTimeToCastSkill = 0;
        currentHealth = maxHealth;
        changeHealthSO.RaiseEvent(currentHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
            this.transform.position += speed * Time.deltaTime * Vector3.up;
        if (Input.GetKey(KeyCode.A))
            this.transform.position += speed * Time.deltaTime * Vector3.left;
        if (Input.GetKey(KeyCode.S))
            this.transform.position += speed * Time.deltaTime * Vector3.down;   
        if (Input.GetKey(KeyCode.D))
            this.transform.position += speed * Time.deltaTime * Vector3.right;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Attack();
        }
        if (Input.GetKey(KeyCode.C))
        {
            currentTimeToCastSkill += Time.deltaTime;
            if (currentTimeToCastSkill >= totalTimeToCastSkill)
                Skill();
        }

        if(currentHealth > 0)
            sendPositionToEnemiesSO.RaiseEvent(this.transform.position);
    }


    public override void Attack()
    {
        BaseBullet slimeOrb = BulletManager.Instance.GetBullet();
        slimeOrb.transform.position = slimeOrb.previousPosition = this.transform.position;
        Vector3 mousePosition = Input.mousePosition;
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, Camera.main.nearClipPlane));
        Vector3 direction = worldPosition - transform.position;
        direction.z = 0;
        slimeOrb.direction = direction.normalized;

        ChangeHealth(-1);
    }

    public void Skill()
    {
        sendPositionToSlimeOrbsSO.RaiseEvent(this.transform.position);
        currentTimeToCastSkill = 0;
    }

    public override void Die()
    {
        Debug.Log("Die");
    }

    public void PickUpHealth()
    {
        ChangeHealth(1);
    }

    public override void ChangeHealth(float h)
    {
        currentHealth += h;
        speed += -h * (maxSpeed - minSpeed) / 7;

        Vector2 newScale;
        if (h < 0)
        {
            newScale = Math.Abs(h) * minimizeScaleFactor * transform.localScale;

            if (currentHealth <= 0)
            {
                currentHealth = 0;
                Die();
            }
            else if (speed >= maxSpeed)
                speed = maxSpeed;

        }
        else
        {
            newScale = Math.Abs(h) * maximizeScaleFactor * transform.localScale;

            if (currentHealth >= maxHealth)
                currentHealth = maxHealth;
            else if (speed <= minSpeed)
                speed = minSpeed;
        }

        transform.localScale = newScale;
        changeHealthSO.RaiseEvent(currentHealth);
    }
}
