using System;
using Fighting.Hp;
using UnityEngine;
using UnityEngine.Events;

namespace EnemiesAI
{
    [RequireComponent(typeof(Fighter), typeof(Navigatable), typeof(FightTarget))]
    public class EnemyUnit : StateCommandTarget, IInitializable
    {
        public float angerDistance;
        public Transform area;
        public float allowedDistanceFromArea;

        public bool IsFighting = false;
        
        public FightTarget FightTarget => GetComponent<FightTarget>();
        
        public bool IsInitializationOnStartRequired => true;
        [SerializeField] protected UnityEvent onInitialized;
        public UnityEvent OnInitialized => onInitialized;
        public void Initialize()
        {
            if (!EnemiesAiController.Enemies.Contains(this))
            {
                EnemiesAiController.Enemies.Add(this);
            }

            if (TryGetComponent(out HpHandler hpHandler))
            {
                hpHandler.OnDead += OnDestroyHandler;
            }
        }
        
        private void OnDestroyHandler()
        {
            if (EnemiesAiController.Enemies.Contains(this))
            {
                EnemiesAiController.Enemies.Remove(this);
            }
        }
    }
}