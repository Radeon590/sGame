using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class StateCommandSceneGameMachine : MonoBehaviour
{
    [FormerlySerializedAs("commandInvoker")] [SerializeField] private StateCommandInvoker stateCommandInvoker;
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

        var navigateCommand = new NavigateStateCommand(targets[_currentTargetIndex]);
        stateCommandInvoker.InvokeCommand(navigateCommand);
        
        _currentTargetIndex++;
    }
}
