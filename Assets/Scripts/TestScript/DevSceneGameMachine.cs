using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class DevSceneGameMachine : MonoBehaviour
{
    [SerializeField] private List<SquadUnit> squadUnits;
    [FormerlySerializedAs("stateCommandInvoker")] [FormerlySerializedAs("commandInvoker")] [SerializeField] private SquadManager squadManager;
    [SerializeField] private List<NavigationTarget> targets;

    private int _currentTargetIndex = 0;
    
    private void Awake()
    {
        StartTest();
    }

    public void StartTest()
    {
        squadManager.SetSquadUnits(squadUnits);
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

        var interactCommand = new InteractStateCommand(targets[_currentTargetIndex].GetComponent<Interactable>(), interactable => Destroy(interactable.gameObject));
        //interactCommand.Offset = 5;
        squadManager.InvokeCommand(interactCommand);
        
        _currentTargetIndex++;
    }
}
