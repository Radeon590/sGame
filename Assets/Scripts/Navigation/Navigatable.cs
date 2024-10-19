using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

public class Navigatable : MonoBehaviour
{
    [FormerlySerializedAs("navMeshAgent")] public NavMeshAgent NavMeshAgent;
    
    void Start()
    {
        if (NavMeshAgent is null)
        {
            if (!gameObject.TryGetComponent(out NavMeshAgent))
            {
                throw new Exception("Cant get navMeshAgent for navigatable");
            }
        }
    }

    public void SetTarget(NavigationTarget target)
    {
        SetTarget(target.transform.position);
    }
    
    public void SetTarget(Vector3 targetPosition)
    {
        NavMeshAgent.SetDestination(targetPosition);
        NavMeshAgent.isStopped = false;
    }

    public void Stop()
    {
        NavMeshAgent.isStopped = true;
    }
}
