using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

public class Navigatable : MonoBehaviour
{
    [FormerlySerializedAs("navMeshAgent")] public NavMeshAgent NavMeshAgent;

    private NavigationTarget _target;
    private Vector2 _targetPosition;
    
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

    private void FixedUpdate()
    {
        try
        {
            if (_target is not null)
            {
                if (!_target.transform.position.Equals(_targetPosition))
                {
                    SetUpTarget();
                }
            }
        }
        catch (MissingReferenceException)
        {
            
        }
    }

    public void SetTarget(NavigationTarget target)
    {
        //Debug.Log($"SetTarget {gameObject.name} {target.gameObject.name}");
        _target = target;
        SetUpTarget();
    }
    
    public void SetTarget(Vector3 targetPosition)
    {
        NavMeshAgent.SetDestination(targetPosition);
        NavMeshAgent.isStopped = false;
    }

    public void Stop()
    {
        _target = null;
        NavMeshAgent.isStopped = true;
    }

    private void SetUpTarget()  
    {
        _targetPosition = _target.transform.position;
        SetTarget(_targetPosition);
    }
}
