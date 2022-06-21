using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : Stat
{
    [SerializeField]
    int _exp;
    [SerializeField]
    protected int _mp;
    [SerializeField]
    protected int _maxMp;
    [SerializeField]
    int _gold;
    [SerializeField]
    int _slimeKill;

    public int Mp { get { return _mp; } set { _mp = value; } }
    public int MaxMp { get { return _maxMp; } set { _maxMp = value; } }

    public int Exp
    {
        get { return _exp; }
        set
        {
            _exp = value;

            int level = Level;
            while (true)
            {
                Data.Stat stat;
                if (Managers.Data.WarriorStatDict.TryGetValue(level + 1, out stat) == false)
                    break;
                if (Managers.Data.WizardStatDict.TryGetValue(level + 1, out stat) == false)
                    break;
                if (_exp < stat.totalExp)
                    break;
                level++;
            }

            if (level != Level)
            {
                Level = level;
                SetStat(Level);
            }
        }
    }

    public int Gold { get { return _gold; } set { _gold = value; } }
    public int SlimeKill { get { return _slimeKill; } set { _slimeKill = value; } }

    private void Start()
    {
        _level = 1;
        _exp = 0;
        _moveSpeed = 5.0f;
        _gold = 100;
        _slimeKill = 0;

        SetStat(_level);

    }

    public void SetStat(int level)
    {
        Dictionary<int, Data.Stat> dict = new Dictionary<int, Data.Stat>();
        switch (Managers.CharacterType) 
        {
            case Define.Character.Warrior:
                dict = Managers.Data.WarriorStatDict;
                break;
            case Define.Character.Wizard:
                dict = Managers.Data.WizardStatDict;
                break;
        }
        Data.Stat stat = dict[level];

        _hp = stat.maxHp;
        _maxHp = stat.maxHp;
        _mp = stat.maxMp;
        _maxMp = stat.maxMp;
        _attack = stat.attack;
    }
}
