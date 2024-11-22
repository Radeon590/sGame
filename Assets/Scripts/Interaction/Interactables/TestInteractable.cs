using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TestInteractable : Interactable
{
    public override void Interact(Interactor interactor)
    {
        base.Interact(interactor);
        Destroy(gameObject);
    }
}
