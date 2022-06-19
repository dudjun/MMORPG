using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    private GameObject go_InventoryBase;
    [SerializeField]
    private GameObject go_SlotsParent;

    public static bool inventoryActivated = false;

    private Slot[] slots;

    void Start()
    {
        slots = go_SlotsParent.GetComponentsInChildren<Slot>();
        Managers.Input.KeyAction -= CheckIButton;
        Managers.Input.KeyAction += CheckIButton;
    }

    void CheckIButton()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            inventoryActivated = !inventoryActivated;
            go_InventoryBase.SetActive(inventoryActivated);
        }
    }

    public void PutSlot(Item _item, int _count)
    {
        foreach(Slot _slot in slots)
        {
            if (_slot.GetComponent<Slot>().item == null)
            {
                _slot.GetComponent<Slot>().AddItem(_item, _count);
                break;
            }
            if (_slot.GetComponent<Slot>().item == _item)
            {
                _slot.GetComponent<Slot>().AddItem(_item, _slot.GetComponent<Slot>().itemCount + 1);
                break;
            }
        }
    }
}
