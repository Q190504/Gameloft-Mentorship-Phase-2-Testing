using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BaseBulletEventListener : MonoBehaviour
{
    [SerializeField] private UnityEvent<BaseBullet> EventResponse;
    [SerializeField] private BaseBulletPublisherSO publisher;

    private void OnEnable()
    {
        publisher.OnEventRaised += Respond;
    }

    private void OnDisable()
    {
        publisher.OnEventRaised -= Respond;
    }

    private void Respond(BaseBullet b)
    {
        EventResponse?.Invoke(b);
    }
}