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

                }
                else
                    inventory.PutSlot(item, 1);
                Managers.Input.KeyAction -= CheckZButton;
                Managers.Resource.Destroy(gameObject);
            }
            
        }
    }
}
