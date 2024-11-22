using System;
using System.Collections.Generic;
using StateCommandSystem.Commands;
using UnityEngine;

namespace EnemiesAI
{
    public class EnemiesAiController : MonoBehaviour
    {
        public static List<EnemyUnit> Enemies = new List<EnemyUnit>();
        public static List<PlayerUnit> PlayerUnits = new List<PlayerUnit>();

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