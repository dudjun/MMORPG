using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StateBox : MonoBehaviour
{
    private PlayerStat stat;
    [SerializeField] private Slider HpSlider;
    [SerializeField] private Slider MpSlider;
    [SerializeField] private Text LvText;
    [SerializeField] private Text MoneyText;
    void Start()
    {
        stat = Managers.Game.GetPlayer().GetComponent<PlayerStat>();
    }

    void Update()
    {
        HpSlider.value = stat.Hp / (float)stat.MaxHp;
        MpSlider.value = stat.Mp / (float)stat.MaxMp;
        LvText.text = "LV. " + stat.Level;
        MoneyText.text = stat.Gold + "원";
    }
}
