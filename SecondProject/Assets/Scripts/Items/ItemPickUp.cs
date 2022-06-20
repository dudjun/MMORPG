using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    public Item item;
    private GameObject player;
    private Inventory inventory;

    private void Start()
    {
        if (item.itemType != Item.ItemType.Weapon)
        {
            inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
            Managers.Input.KeyAction -= CheckZButton;
            Managers.Input.KeyAction += CheckZButton;
            player = Managers.Game.GetPlayer();
        }
    }

    void CheckZButton()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            float dis = (player.transform.position - gameObject.transform.position).magnitude;
            if (dis <= 1f && item.itemType != Item.ItemType.Weapon)
            {
                if (item.itemType == Item.ItemType.Coin)
                {
                    int PlusMoney = 0;
                    if (item.itemName == "금화")
                    {
                        PlusMoney = Random.Range(10, 20);
                    }
                    else if (item.itemName == "은화")
                    {
                        PlusMoney = Random.Range(5, 10);
                    }
                    Managers.Game.GetPlayer().GetComponent<PlayerStat>().Gold += PlusMoney;
                }
                else
                    inventory.PutSlot(item, 1);
                Managers.Input.KeyAction -= CheckZButton;
                Managers.Resource.Destroy(gameObject);
            }
            
        }
    }
}
