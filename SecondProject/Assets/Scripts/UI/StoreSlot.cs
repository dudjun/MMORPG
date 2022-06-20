using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StoreSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private GameObject document;
    [SerializeField]
    private Image Item_img;
    [SerializeField]
    private Item item;
    [SerializeField]
    private Text itemName;
    [SerializeField]
    private Text itemDesc;
    [SerializeField]
    private Text itemCoin;

    [SerializeField]
    private Inventory inven;

    Slot[] slots;

    void Start()
    {
        slots = inven.slots;
        if(item != null)
        {
            Item_img.sprite = item.itemImage;
            Item_img.color = new Color(1f, 1f, 1f, 1f);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (item != null)
        {
            document.SetActive(true);
            itemName.text = item.itemName;
            itemDesc.text = item.itemDesc;
            itemCoin.text = item.Gold.ToString() + "원";
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        document.SetActive(false);
    }

    public void OnButton()
    {
        foreach (Slot _slot in slots)
        {
            if (_slot.GetComponent<Slot>().item == null)
            {
                if (Managers.Game.GetPlayer().GetComponent<PlayerStat>().Gold < item.Gold)
                    break;
                Managers.Game.GetPlayer().GetComponent<PlayerStat>().Gold -= item.Gold;
                _slot.GetComponent<Slot>().AddItem(item);
                break;
            }
        }
    }
}
