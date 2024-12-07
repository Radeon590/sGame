using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquadUnit : StateCommandTarget
{
    public SquadManager SquadManager;
    
    private void OnDestroy()
    {
        SquadManager.RemoveSquadUnit(this);
    }
}
