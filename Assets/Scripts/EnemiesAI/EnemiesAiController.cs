using System;
using System.Collections.Generic;
using StateCommandSystem.Commands;
using UnityEngine;
using UnityEngine.Events;

namespace EnemiesAI
{
    public class EnemiesAiController : MonoBehaviour, IInitializable
    {
        public bool IsInitializationOnStartRequired => false;
        public UnityEvent OnInitialized { get; }
        public void Initialize()
        {
            Enemies = new List<EnemyUnit>();
            PlayerUnits = new List<PlayerUnit>();
        }
        
        public static List<EnemyUnit> Enemies;
        public static List<PlayerUnit> PlayerUnits;

        private void FixedUpdate()
        {
            if (Enemies == null)
            {
                return;
            }
            foreach (var enemy in Enemies)
            {
                PlayerUnit nearestPlayerUnitInAngerRange = null;
                if (PlayerUnits != null)
                {
                    foreach (var playerUnit in PlayerUnits)
                    {
                        var distance = Vector2.Distance(enemy.transform.position, playerUnit.transform.position);
                        if (distance < enemy.angerDistance)
                        {
                            if (nearestPlayerUnitInAngerRange != null)
                            {
                                if (distance > Vector2.Distance(enemy.transform.position,
                                        nearestPlayerUnitInAngerRange.transform.position))
                                {
                                    continue;
                                }
                            }
                            nearestPlayerUnitInAngerRange = playerUnit;
                        }
                    }
                }

                if (nearestPlayerUnitInAngerRange != null)
                {
                    var command = new FightStateCommand(nearestPlayerUnitInAngerRange);
                    enemy.InvokeStateCommand(command);
                    enemy.IsFighting = true;
                    continue;
                }

                if (enemy.IsFighting)
                {
                    enemy.CancelStateCommand();
                    enemy.IsFighting = false;
                    var command = new WanderInAreaStateCommand(enemy.area.position, enemy.allowedDistanceFromArea);
                    enemy.InvokeStateCommand(command);
                }

                if (enemy.CurrentStateCommand == null)
                {
                    var command = new WanderInAreaStateCommand(enemy.area.position, enemy.allowedDistanceFromArea);
                    enemy.InvokeStateCommand(command);
                }
            }
        }
    }
}