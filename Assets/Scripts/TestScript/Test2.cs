using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test2 : MonoBehaviour
{
    private int i = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DestroyCoroutine());
    }

    public void Log()
    {
        i++;
        //Debug.Log($"Test2 {i}");
    }

    private IEnumerator DestroyCoroutine()
    {
        yield return new WaitForSeconds(3);
        //Debug.Log("Destroy Test2");
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        Debug.Log("Destroy Test2");
    }
}
