using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceController : MonoBehaviour
{
    private MonsterController mc;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            mc = other.GetComponent<MonsterController>();
            if (mc == null)
                mc = other.GetComponentInParent<MonsterController>();
            mc.State = Define.State.ICE;
        }
    }
}
