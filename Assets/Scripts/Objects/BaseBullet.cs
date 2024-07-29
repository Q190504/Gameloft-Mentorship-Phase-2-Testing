using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseBullet : MonoBehaviour
{
    public int damage;
    public float speed;
    public Vector2 previousPosition;
    public float totalDistance;
    public Vector3 direction;
    public Rigidbody2D rb;

    private void Start()
    {
        
    }

    public virtual void Move()
    {
        transform.position += speed * Time.deltaTime * direction;
        float distanceThisFrame = Vector2.Distance(previousPosition, transform.position);
        totalDistance += distanceThisFrame;
        previousPosition = transform.position;
    }

    public virtual void StopMoving() { }
}
