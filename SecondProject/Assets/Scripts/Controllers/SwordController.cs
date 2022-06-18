﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordController : MonoBehaviour
{
    private GameObject Player;
    private void Start()
    {
        Player = Managers.Game.GetPlayer();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (Player.GetComponent<PlayerController>().State == Define.State.Skill)
            Debug.Log(other.name);
    }
}
