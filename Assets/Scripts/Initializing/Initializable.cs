using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Initializable : MonoBehaviour, IInitializable
{
    [SerializeField] protected bool isInitializationOnStartRequired = true;
    public bool IsInitializationOnStartRequired => isInitializationOnStartRequired;

    [SerializeField] protected UnityEvent onInitialized;
    public UnityEvent OnInitialized => onInitialized;
    
    public virtual void Initialize()
    {
        OnInitialized?.Invoke();
    }
}
