using System;
using UnityEngine;
using UnityEngine.Events;

namespace EnemiesAI
{
    [RequireComponent(typeof(Fighter), typeof(Navigatable))]
    public class EnemyUnit : StateCommandTarget, IInitializable
    {
        public float angerDistance;
        public Transform area;
        public float allowedDistanceFromArea;

        public bool IsFighting = false;
        
        public bool IsInitializationOnStartRequired => true;
        [SerializeField] protected UnityEvent onInitialized;
        public UnityEvent OnInitialized => onInitialized;
        public void Initialize()
        {
            if (!EnemiesAiController.Enemies.Contains(this))
            {
                EnemiesAiController.Enemies.Add(this);
            }
        }
    }
}