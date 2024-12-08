using UnityEngine;
using UnityEngine.UI;

public class ShowInventory : MonoBehaviour
{
    public GameObject inventoryUI;

    void Start()
    {
        inventoryUI.SetActive(false);
    }


    void OnMouseDown()
    {
        inventoryUI.SetActive(!inventoryUI.activeSelf);
    }
}
