using System.Collections;
using System.Collections.Generic;
using Fighting.Items.Weapon.Base;
using UnityEngine;

public class FightingDevSceneGameMachine : MonoBehaviour
{
    [SerializeField] private List<Weapon> weapons;
    [SerializeField] private List<SquadUnit> squadUnits;
    [SerializeField] private SquadManager squadManager;
    [SerializeField] private List<FightTarget> targets;

    private int _currentTargetIndex = 0;
    
    private void Awake()
    {
        StartTest();
    }

    public void StartTest()
    {
        bool isSecondWeapon = false;
        foreach (var squadUnit in squadUnits)
        {
            int index;
            if (!isSecondWeapon)
            {
                index = 0;
            }
            else
            {
                index = 1;
            }
            squadUnit.GetComponent<Fighter>().Weapon = weapons[index];
            isSecondWeapon = !isSecondWeapon;
        }
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

        var fightCommand = new FightStateCommand(targets[_currentTargetIndex]);
        squadManager.InvokeCommand(fightCommand);
        
        _currentTargetIndex++;
    }
}
