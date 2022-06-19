using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    public Item item;
    private GameObject player;

    private void Start()
    {
        Managers.Input.KeyAction -= CheckZButton;
        Managers.Input.KeyAction += CheckZButton;
        player = Managers.Game.GetPlayer();
    }

    void CheckZButton()
    {
        float dis = (player.transform.position - gameObject.transform.position).magnitude;
        if (Input.GetKeyDown(KeyCode.Z) && dis <= 1f)
        {
            Managers.Input.KeyAction -= CheckZButton;
            Managers.Resource.Destroy(gameObject);
        }
    }
}
