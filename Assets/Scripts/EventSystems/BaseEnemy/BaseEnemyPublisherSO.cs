using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Base Enemy Pulisher", menuName = "Scriptable Objects/Events/Base Enemy Publisher")]
public class BaseEnemyPublisherSO : ScriptableObject
{
    public UnityAction<BaseEnemy> OnEventRaised;

    public void RaiseEvent(BaseEnemy enemy)
    {
        OnEventRaised?.Invoke(enemy);
    }
}