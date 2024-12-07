using System;
using System.Collections;
using System.Collections.Generic;
using Fighting.Hp;
using UnityEngine;

public class SelectionController : MonoBehaviour
{
    private Vector3 startPosition;
    private List<SelectableUnit> selectedUnitList;
    [SerializeField] private GetMousePosition mousePositionGetter;
    [SerializeField] private GameObject selectionAreaPrefab;
    private Transform selectionAreaTransform;
    
    public static Action<List<SelectableUnit>> OnUnitsSelected;

    private void Awake()
    {
        selectionAreaTransform = Instantiate(selectionAreaPrefab).GetComponent<Transform>();
        selectedUnitList = new List<SelectableUnit>();
        selectionAreaTransform.gameObject.SetActive(false);
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            selectionAreaTransform.gameObject.SetActive(true);

            startPosition = mousePositionGetter.mouseWorldPosition;
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 currentMousePosition = mousePositionGetter.mouseWorldPosition;
            Vector3 lowerLeft = new Vector3(
                Mathf.Min(startPosition.x, currentMousePosition.x),
                Mathf.Min(startPosition.y, currentMousePosition.y)
                );
            Vector3 upperRight= new Vector3(
                Mathf.Max(startPosition.x, currentMousePosition.x),
                Mathf.Max(startPosition.y, currentMousePosition.y)
                );
            selectionAreaTransform.position = lowerLeft;
            selectionAreaTransform.localScale = upperRight - lowerLeft;

        }

        if (Input.GetMouseButtonUp(0))
        {
            selectionAreaTransform.gameObject.SetActive(false);

            Collider2D[] collider2DArray = Physics2D.OverlapAreaAll(startPosition, mousePositionGetter.mouseWorldPosition);

            foreach  (SelectableUnit unitSelect in selectedUnitList)
            {
                unitSelect.SetSelectedVisible(false);
                if (unitSelect.TryGetComponent(out HpHandler hpHandler))
                {
                    hpHandler.OnDead -= unitSelect.OnDeadHandler;
                    unitSelect.OnDeadHandler = null;
                }
            }

            selectedUnitList.Clear();
            foreach (Collider2D collider2D in collider2DArray)
            {
                SelectableUnit selectableUnit = collider2D.GetComponent<SelectableUnit>();
                if (selectableUnit != null)
                {
                    selectableUnit.SetSelectedVisible(true );
                    selectedUnitList.Add(selectableUnit);
                    if (selectableUnit.TryGetComponent(out HpHandler hpHandler))
                    {
                        selectableUnit.OnDeadHandler = () => selectedUnitList.Remove(selectableUnit);
                        hpHandler.OnDead += selectableUnit.OnDeadHandler;
                    }
                }
            }

            OnUnitsSelected?.Invoke(selectedUnitList);
        }
    }
}
