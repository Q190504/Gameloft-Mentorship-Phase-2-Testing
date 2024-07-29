using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Vector Pulisher", menuName = "Scriptable Objects/Events/Vector Publisher")]
public class VectorPublisherSO : ScriptableObject
{
    public UnityAction<Vector3> OnEventRaised;

    public void RaiseEvent(Vector3 vector)
    {
        OnEventRaised?.Invoke(vector);
    }
}