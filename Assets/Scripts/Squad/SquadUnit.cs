using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(StateCommandTarget))]
public class SquadUnit : MonoBehaviour
{
    public StateCommandTarget StateCommandTarget;

    private void Start()
    {
        if (StateCommandTarget == null)
        {
            StateCommandTarget = GetComponent<StateCommandTarget>();
        }
    }
}
