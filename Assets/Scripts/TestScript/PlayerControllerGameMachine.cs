using System.Collections.Generic;
using Fighting.Items.Weapon.Base;
using UnityEngine;
using UnityEngine.Serialization;

namespace TestScript
{
    public class PlayerControllerGameMachine : MonoBehaviour
    {
        [SerializeField] private List<Weapon> weapons;
        [SerializeField] private List<SquadUnit> playerUnits;
        [SerializeField] private List<FightTarget> targets;

        private int _currentTargetIndex = 0;

        private void Awake()
        {
            StartTest();
        }

        public void StartTest()
        {
            bool isSecondWeapon = false;
            foreach (var squadUnit in playerUnits)
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

            foreach (var target in targets)
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

                target.GetComponent<Fighter>().Weapon = weapons[index];
                isSecondWeapon = !isSecondWeapon;
            }
        }
    }
}