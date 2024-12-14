using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class InitializingManager : Initializable
{
    [SerializeField] private bool isSelfSetupRequired = true;
    [SerializeField] private List<Initializable> initializables;
    
    public List<IInitializable> Initializables;

    private bool _isStartInitialization = false;

    private void Start()
    {
        if (IsInitializationOnStartRequired)
        {
            _isStartInitialization = true;
            Initialize();
            _isStartInitialization = false;
        }
    }

    public override void Initialize()
    {
        // setup initializables from inspector (in this case InitializingManager is kinda gameMachine)
        if (Initializables is { Count: > 0 })
        {
            InitializeInitializables(Initializables);
        }
        
        // collect another initializables from scene and setup
        var collectedInitializables = CollectInitializables();
        InitializeInitializables(collectedInitializables);
        base.Initialize();
    }
    
    private List<IInitializable> CollectInitializables()
    {
        var result = new List<IInitializable>(initializables);
        if (isSelfSetupRequired)
        {
            List<GameObject> rootGameObjects = SceneManager.GetActiveScene().GetRootGameObjects().ToList();
            foreach (var rootGameObject in rootGameObjects)
            {
                var childrenInitializables = rootGameObject.GetComponentsInChildren<IInitializable>().ToList();
                if (childrenInitializables.Contains(this))
                {
                    childrenInitializables.Remove(this);
                }

                foreach (var childrenInitializable in childrenInitializables)
                {
                    if (!result.Contains(childrenInitializable))
                    {
                        result.Add(childrenInitializable);
                    }
                }
            }
        }

        return result;
    }

    private void InitializeInitializables(List<IInitializable> initializables)
    {
        foreach (var initializable in initializables)
        {
            if ((_isStartInitialization && initializable.IsInitializationOnStartRequired)
                || (!_isStartInitialization && !initializable.IsInitializationOnStartRequired))
            {
                initializable.Initialize();
            }
        }
    }
}
