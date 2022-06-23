using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigEventController : MonoBehaviour
{
    MonsterController mc;
    private void Start()
    {
        mc = GetComponentInParent<MonsterController>();
    }
    void OnHitEvent()
    {
        mc.OnHitEvent();
    }

    void OnIdleState()
    {
        mc.OnIdleState();
    }
}
