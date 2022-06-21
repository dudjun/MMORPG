using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Quest : MonoBehaviour
{
    private Text _txt;
    private PlayerStat _stat;

    private bool _isOn;
    private bool _isFirstLoop = true;
    private int _killcount = 0;
    private int _currentCount = 0;
    private int _lastCount = 0;

    private void Start()
    {
        _txt = GetComponentInChildren<Text>();
        _stat = Managers.Game.GetPlayer().GetComponent<PlayerStat>();
    }

    private void Update()
    {
        if (_isOn)
        {
            CountKill();
            Changetxt();
        }
    }

    private void CountKill()
    {
        if (_killcount >= 3) return;

        _currentCount = _stat.SlimeKill;
        if (_currentCount != _lastCount && !_isFirstLoop)
            _killcount++;
        _lastCount = _stat.SlimeKill;
        _isFirstLoop = false;
    }

    private void Changetxt()
    {
        _txt.text = "슬라임 " + _killcount.ToString() + " / 3";
    }

    public void QuestOn()
    {
        gameObject.SetActive(true);
        _isOn = true;
    }

    public void QuestSuccess()
    {
        _stat.Gold += 100;
    }

    public bool GetQuestFinish()
    {
        if (_killcount >= 3)
            return true;
        else
            return false;
    }
}
