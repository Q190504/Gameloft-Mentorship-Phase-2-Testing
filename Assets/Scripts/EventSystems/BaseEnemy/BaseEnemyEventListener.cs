using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BaseEnemyEventListener : MonoBehaviour
{
    [SerializeField] private UnityEvent<BaseEnemy> EventResponse;
    [SerializeField] private BaseEnemyPublisherSO publisher;

    private void OnEnable()
    {
        publisher.OnEventRaised += Respond;
    }

    private void OnDisable()
    {
        publisher.OnEventRaised -= Respond;
    }

    private void Respond(BaseEnemy enemy)
    {
        EventResponse?.Invoke(enemy);
    }
}