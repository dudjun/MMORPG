using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_HPBar : MonoBehaviour
{
    [SerializeField]
    private Stat stat;

    private float ratio;

    void Update()
    {
        transform.rotation = Camera.main.transform.rotation;

        ratio = stat.Hp / (float)stat.MaxHp;
        GetComponent<Slider>().value = ratio;
    }
}
