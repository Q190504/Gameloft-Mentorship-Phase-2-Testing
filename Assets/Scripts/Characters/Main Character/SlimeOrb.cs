using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SlimeOrb : BaseBullet
{
    bool isAbleToPickUp;
    bool isBeingSummoned;
    public float attractionForce;
    public float maxTravelDistance;

    public BaseBulletPublisherSO returnSlimeOrbSO;
    public VoidPublisherSO plusHealthSO;
    public FloatPublisherSO damageEnemySO;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        isAbleToPickUp = false;
        isBeingSummoned = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (totalDistance < maxTravelDistance)
            Move();
        else if(!isBeingSummoned)
            StopMoving();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            Vector2 closestPoint = collision.ClosestPoint(transform.position);
            Vector2 normal = (transform.position - (Vector3)closestPoint).normalized;
            direction = Vector2.Reflect(direction, normal);
        }
        else if(collision.CompareTag("Player") && isAbleToPickUp)
        {
            PickedUp();
        }
        else if ((collision.CompareTag("Enemy")) && (!isAbleToPickUp && !isBeingSummoned) || (isAbleToPickUp && isBeingSummoned)) // is being shot or being summoned
        {
            DealDamage();
        }
    }

    public override void StopMoving()
    {
        base.StopMoving();
        direction = Vector3.zero;
        isAbleToPickUp = true;
    }


    public void PickedUp()
    {
        isAbleToPickUp = false;
        isBeingSummoned = false;
        plusHealthSO.RaiseEvent();
        ReturnToBulletManager();
    }

    public void DealDamage()
    {
        isAbleToPickUp = false;
        isBeingSummoned = false;
        damageEnemySO.RaiseEvent(damage);
        ReturnToBulletManager();
    }

    void ReturnToBulletManager()
    {
        previousPosition = Vector3.zero;
        totalDistance = 0;
        direction = Vector3.zero;
        returnSlimeOrbSO.RaiseEvent(this);
    }

    public void ReturnToPlayer(Vector3 position)
    {
        if (isAbleToPickUp)
        {
            isBeingSummoned = true;

            Vector3 direction = (position - this.transform.position).normalized;
            this.transform.position += attractionForce * Time.deltaTime * direction;
        }
    }
}
