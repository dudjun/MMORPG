﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeStat : Stat
{
    protected override void OnDead(Stat attacker)
    {
        StartCoroutine(SpawnItem());
        base.OnDead(attacker);
    }
    IEnumerator SpawnItem()
    {
        int rand = Random.Range(0, 2);
        yield return new WaitForSeconds(1.8f);
        if (rand < 1)
            Managers.Resource.Instantiate("ItemPrefabs/GoldCoin", transform.position, transform.rotation);
        else
            Managers.Resource.Instantiate("ItemPrefabs/SilverCoin", transform.position, transform.rotation);
    }
}