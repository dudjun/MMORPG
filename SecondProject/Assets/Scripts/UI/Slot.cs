using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    public Item item;
    public int itemCount;
    public Image itemImage;
    public Text txtCount;
    public bool isQuickSlot;

    Slot[] _slots;

    private float clickTime = 0;

    [SerializeField]
    private RectTransform baseRect;
    [SerializeField]
    private RectTransform quickSlotBaseRect;

    private void SetColor(float _alpha)
    {
        Color color = itemImage.color;
        color.a = _alpha;
        itemImage.color = color;
    }
    public void AddItem(Item _item, int _count = 1)
    {
        item = _item;
        itemCount = _count;
        itemImage.sprite = item.itemImage;
        if(txtCount != null) txtCount.text = itemCount.ToString();

        SetColor(1);
    }

    public void SetSlotCount(int _count)
    {
        itemCount += _count;
        txtCount.text = itemCount.ToString();

        if (itemCount <= 0)
            ClearSlot();
    }

    private void ClearSlot()
    {
        item = null;
        itemCount = 0;
        itemImage.sprite = null;
        SetColor(0);
        if (txtCount != null) txtCount.text = "";
    }

    
    public void OnPointerClick(PointerEventData eventData)
    {
        // 인벤토리에서 우클릭으로 아이템 사용
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (item != null)
            {
                // 아이템 사용 나중에 추가

                if (item.itemType == Item.ItemType.Used)
                {
                    if (item.itemName == "빨간 물약")
                    {
                        Managers.Game.GetPlayer().GetComponent<PlayerStat>().Hp += 50;
                        if (Managers.Game.GetPlayer().GetComponent<PlayerStat>().Hp >
                            Managers.Game.GetPlayer().GetComponent<PlayerStat>().MaxHp)
                            Managers.Game.GetPlayer().GetComponent<PlayerStat>().Hp =
                                Managers.Game.GetPlayer().GetComponent<PlayerStat>().MaxHp;
                    }
                    if (item.itemName == "파란 물약")
                    {
                        Managers.Game.GetPlayer().GetComponent<PlayerStat>().Mp += 50;
                        if (Managers.Game.GetPlayer().GetComponent<PlayerStat>().Mp >
                            Managers.Game.GetPlayer().GetComponent<PlayerStat>().MaxMp)
                            Managers.Game.GetPlayer().GetComponent<PlayerStat>().Mp =
                                Managers.Game.GetPlayer().GetComponent<PlayerStat>().MaxMp;
                    }
                    SetSlotCount(-1);
                }
            }
        }

        // 더블 클릭 시
        if ((Time.time - clickTime) < 0.3f)
        {
            CheckDoubleClick();
            clickTime = -1;
        }
        else
        {
            clickTime = Time.time;
        }
    }

    void CheckDoubleClick()
    {
        _slots = baseRect.transform.GetComponentInParent<Inventory>().slots;
        foreach(Slot slot in _slots){
            if(slot.item == null)
            {
                slot.AddItem(item);
                break;
            }
        }
        ClearSlot();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (item != null && Inventory.inventoryActivated)
        {
            DragSlot.instance.dragSlot = this;
            DragSlot.instance.DragSetImage(itemImage);

            DragSlot.instance.transform.position = eventData.position;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (item != null)
        {
            DragSlot.instance.transform.position = eventData.position;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!((DragSlot.instance.transform.localPosition.x > baseRect.rect.xMin && DragSlot.instance.transform.localPosition.x < baseRect.rect.xMax
            && DragSlot.instance.transform.localPosition.y > baseRect.rect.yMin && DragSlot.instance.transform.localPosition.y < baseRect.rect.yMax)
            ||
            (DragSlot.instance.transform.localPosition.x > quickSlotBaseRect.rect.xMin && DragSlot.instance.transform.localPosition.x < quickSlotBaseRect.rect.xMax
            && DragSlot.instance.transform.localPosition.y > quickSlotBaseRect.transform.localPosition.y - quickSlotBaseRect.rect.yMax && DragSlot.instance.transform.localPosition.y < quickSlotBaseRect.transform.localPosition.y - quickSlotBaseRect.rect.yMin)))
        {
            if(DragSlot.instance.dragSlot != null)
            {
                // 바닥에 떨구기

                DragSlot.instance.SetColor(0);
                DragSlot.instance.dragSlot = null;
            }
        }
        else
        {
            DragSlot.instance.SetColor(0);
            DragSlot.instance.dragSlot = null;
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (DragSlot.instance.dragSlot != null)
        {
            if (DragSlot.instance.dragSlot.item == item)
                SumSlot();
            else
                ChangeSlot();
        }
    }

    private void ChangeSlot()
    {
        if (isQuickSlot)
        {
            if (DragSlot.instance.dragSlot.item.itemType != Item.ItemType.Weapon)
                return;
        }
        Item _tempItem = item;
        int _tempItemCount = itemCount;

        AddItem(DragSlot.instance.dragSlot.item, DragSlot.instance.dragSlot.itemCount);

        if (_tempItem != null)
            DragSlot.instance.dragSlot.AddItem(_tempItem, _tempItemCount);
        else
            DragSlot.instance.dragSlot.ClearSlot();
    }

    private void SumSlot()
    {
        if (isQuickSlot)
            return;
        Item _tempItem = item;
        int _tempItemCount = itemCount;
        AddItem(DragSlot.instance.dragSlot.item, DragSlot.instance.dragSlot.itemCount + _tempItemCount);
        DragSlot.instance.dragSlot.ClearSlot();
    }
}
