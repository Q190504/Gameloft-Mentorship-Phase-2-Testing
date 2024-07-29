using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Base Bullet Pulisher", menuName = "Scriptable Objects/Events/Base Bullet Publisher")]
public class BaseBulletPublisherSO : ScriptableObject
{
    public UnityAction<BaseBullet> OnEventRaised;

    public void RaiseEvent(BaseBullet b)
    {
        OnEventRaised?.Invoke(b);
    }
}