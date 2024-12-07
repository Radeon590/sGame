﻿using System;
using System.Collections.Generic;
using System.Linq;
using EnemiesAI;
using UnityEngine;
using UnityEngine.Events;

namespace PlayerController
{
    public class PlayerController : MonoBehaviour, IInitializable
    {
        [SerializeField] private SquadManager squadManager;
        
        public bool IsInitializationOnStartRequired => true;
        [SerializeField] private UnityEvent onInitialized;
        public UnityEvent OnInitialized => onInitialized;
        public void Initialize()
        {
            SelectionController.OnUnitsSelected += OnUnitsSelected;
            TargetsController.OnPositionSelected += OnPositionSelected;
            TargetsController.OnUnitsTargeted += OnUnitsTargeted;
        }

        private void OnUnitsSelected(List<SelectableUnit> units)
        {
            List<SquadUnit> squadUnits = new List<SquadUnit>();
            foreach (var unit in units)
            {
                if (unit.TryGetComponent(out SquadUnit squadUnit))
                {
                    squadUnits.Add(squadUnit);
                }
            }

            if (squadUnits.Count > 0)
            {
                squadManager.SetSquadUnits(squadUnits);
            }
        }
        
        private void OnPositionSelected(Vector2 position)
        {
            var command = new NavigateStateCommand(position);
            squadManager.InvokeCommand(command);
        }
        
        private void OnUnitsTargeted(TargetableUnit obj)
        {
            if (obj.TryGetComponent(out EnemyUnit enemyUnit))
            {
                var command = new FightStateCommand(enemyUnit.FightTarget);
                squadManager.InvokeCommand(command);
                return;
            }

            if (obj.TryGetComponent(out Interactable interactable))
            {
                var command = new InteractStateCommand(interactable);
                squadManager.InvokeCommand(command);
                return;
            }
        }
    }
}