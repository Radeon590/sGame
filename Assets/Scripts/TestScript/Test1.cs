using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test1 : MonoBehaviour
{
    public Test2 test2;

    private void FixedUpdate()
    {
        //Debug.Log(test2 == null);
        test2.Log();
    }
}
