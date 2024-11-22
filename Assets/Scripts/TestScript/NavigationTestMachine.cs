using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class NavigationTestMachine : MonoBehaviour
{
    [SerializeField] private List<Navigatable> navigatables;
    [SerializeField] private List<NavigationTarget> targets;

    private int _currentTargetIndex = 0;
    
    private void Awake()
    {
        StartTest();
    }

    public void StartTest()
    {
        StartCoroutine(TestCor());
    }

    private IEnumerator TestCor()
    {
        while (true)
        {
            SwitchTarget();
            yield return new WaitForSeconds(10);
        }
    }

    private void SwitchTarget()
    {
        if (_currentTargetIndex >= targets.Count)
        {
            _currentTargetIndex = 0;
        }
        foreach (var navigatable in navigatables)
        {
            navigatable.SetTarget(targets[_currentTargetIndex]);
        }
        _currentTargetIndex++;
    }
}
