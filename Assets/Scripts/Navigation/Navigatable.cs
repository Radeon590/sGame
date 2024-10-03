using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Navigatable : MonoBehaviour
{
    [SerializeField] private NavMeshAgent navMeshAgent;
    
    void Start()
    {
        if (navMeshAgent is null)
        {
            if (!gameObject.TryGetComponent(out navMeshAgent))
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
        navMeshAgent.SetDestination(targetPosition);
    }
}
