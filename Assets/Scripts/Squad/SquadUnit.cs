using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquadUnit : StateCommandTarget
{
    public SquadManager SquadManager;
    
    private void OnDestroy()
    {
        if (SquadManager != null)
        {
            SquadManager.RemoveSquadUnit(this);
        }
    }
}
