using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class VectorEventListener : MonoBehaviour
{
    [SerializeField] private UnityEvent<Vector3> EventResponse;
    [SerializeField] private VectorPublisherSO publisher;

    private void OnEnable()
    {
        publisher.OnEventRaised += Respond;
    }

    private void OnDisable()
    {
        publisher.OnEventRaised -= Respond;
    }

    private void Respond(Vector3 vector)
    {
        EventResponse?.Invoke(vector);
    }
}