using UnityEngine;
using UnityEngine.Events;

namespace EnemiesAI
{
    public class PlayerUnit : FightTarget, IInitializable
    {
        public bool IsInitializationOnStartRequired => true;
        [SerializeField] protected UnityEvent onInitialized;
        public UnityEvent OnInitialized => onInitialized;
        public void Initialize()
        {
            if (!EnemiesAiController.PlayerUnits.Contains(this))
            {
                EnemiesAiController.PlayerUnits.Add(this);
            }
        }
    }
}