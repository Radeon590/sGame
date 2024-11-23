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
        CollectInitializables();
        InitializeInitializables();
        base.Initialize();
    }
    
    private void CollectInitializables()
    {
        Initializables = new List<IInitializable>(initializables);
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
                    if (!Initializables.Contains(childrenInitializable))
                    {
                        Initializables.Add(childrenInitializable);
                    }
                }
            }
        }
    }

    private void InitializeInitializables()
    {
        foreach (var initializable in Initializables)
        {
            if ((_isStartInitialization && initializable.IsInitializationOnStartRequired)
                || (!_isStartInitialization && !initializable.IsInitializationOnStartRequired))
            {
                initializable.Initialize();
            }
        }
    }
}
