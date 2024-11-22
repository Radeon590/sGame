using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectableUnit : MonoBehaviour
{
    [SerializeField] private GameObject selectedGameObject;

    private void Awake()
    {
        if (selectedGameObject == null)
        {
            selectedGameObject = transform.Find("SelectedPointer").gameObject;
        }
        SetSelectedVisible(false);
    }

    public void SetSelectedVisible(bool visible)
    {
        selectedGameObject.SetActive(visible);
    }
}
