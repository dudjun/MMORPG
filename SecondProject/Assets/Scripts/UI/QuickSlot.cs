using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickSlot : MonoBehaviour
{
    public static Slot[] quickSlots;
    [SerializeField] GameObject[] Unselected;

    private Transform tf_WeaponPos;
    public static GameObject go_HandWeapon;

    private int selectedSlot = 0;

    void Start()
    {
        quickSlots = GetComponentsInChildren<Slot>();
        tf_WeaponPos = GameObject.Find("WeaponPos").transform;
        Managers.Input.KeyAction -= CheckQButton;
        Managers.Input.KeyAction += CheckQButton;
    }

    void Update()
    {
        ChangeImg();
    }

    private void ChangeImg()
    {
        switch (selectedSlot)
        {
            case 0:
                Unselected[0].SetActive(false);
                Unselected[1].SetActive(true);
                if(quickSlots[0].item != null)
                {
                    if(tf_WeaponPos != null && tf_WeaponPos.childCount == 0)
                    {
                        go_HandWeapon = Instantiate(quickSlots[0].item.itemPrefab, tf_WeaponPos);
                    }
                    else
                    {
                        if(quickSlots[0].item == go_HandWeapon.GetComponent<ItemPickUp>().item)
                        {
                            // 이미 들고있는 무기와 같은 경우
                        }
                        else
                        { 
                            Destroy(go_HandWeapon);
                        }
                    }
                }
                else
                {
                    if (go_HandWeapon != null)
                        Destroy(go_HandWeapon);
                }
                break;
            case 1:
                Unselected[0].SetActive(true);
                Unselected[1].SetActive(false);
                if (quickSlots[1].item != null)
                {
                    if (tf_WeaponPos.childCount == 0)
                    {
                        go_HandWeapon = Instantiate(quickSlots[1].item.itemPrefab, tf_WeaponPos);
                    }
                    else
                    {
                        if (quickSlots[1].item == go_HandWeapon.GetComponent<ItemPickUp>().item)
                        {
                            // 이미 들고있는 무기와 같은 경우
                        }
                        else
                        {
                            Destroy(go_HandWeapon);
                        }
                    }
                }
                else
                {
                    if (go_HandWeapon != null)
                        Destroy(go_HandWeapon);
                }
                break;
        }
    }

    void CheckQButton()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            selectedSlot++;
            if (selectedSlot > 1)
                selectedSlot = 0;

            
        }
    }
}
