using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniCamController : MonoBehaviour
{
    private GameObject player;

    private void Start()
    {
        player = Managers.Game.GetPlayer();
    }

    void Update()
    {
        transform.position = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
    }
}
