using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigStat : Stat
{

    protected override void OnDead(Stat attacker)
    {
        StartCoroutine(SpawnItem());
        base.OnDead(attacker);
    }

    IEnumerator SpawnItem()
    {
        yield return new WaitForSeconds(1.8f);
        if (isFireDead)
            Managers.Resource.Instantiate("ItemPrefabs/Meat Cooked", transform.position, transform.rotation);
        else
            Managers.Resource.Instantiate("ItemPrefabs/Meat_Raw", transform.position, transform.rotation);
    }
}
