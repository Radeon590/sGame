using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetsController : MonoBehaviour
{
    public static Action<TargetableUnit> OnUnitsTargeted;
    public static Action<Vector2> OnPositionSelected;
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D hit = Physics2D.OverlapPoint(mousePosition);
            if (hit!=null)
            {
                TargetableUnit targetableUnit = hit.GetComponent<TargetableUnit>();
                if (targetableUnit != null)
                {
                    OnUnitsTargeted?.Invoke(targetableUnit);
                }
            }
            else
            {
                OnPositionSelected?.Invoke(mousePosition);
            }
        }
    }
}
