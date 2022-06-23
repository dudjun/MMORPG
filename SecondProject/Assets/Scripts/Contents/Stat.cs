using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stat : MonoBehaviour
{
    [SerializeField]
    protected int _level;
    [SerializeField]
    protected int _hp;
    [SerializeField]
    protected int _maxHp;
    [SerializeField]
    protected int _attack;
    [SerializeField]
    protected float _moveSpeed;

    public int Level { get { return _level; } set { _level = value; } }
    public int Hp { get { return _hp; } set { _hp = value; } }
    public int MaxHp { get { return _maxHp; } set { _maxHp = value; } }
    public int Attack { get { return _attack; } set { _attack = value; } }
    public float MoveSpeed { get { return _moveSpeed; } set { _moveSpeed = value; } }

    protected bool isFireDead;

    private void Start()
    {
        _level = 1;
        _hp = 100;
        _maxHp = 100;
        _attack = 10;
        _moveSpeed = 5.0f;
    }

    public virtual void OnAttacked(Stat attacker)
    {
        Hp -= attacker.Attack;
        if (Hp <= 0)
        {
            Hp = 0;
            OnDead(attacker);
        }
    }

    public void OnFired(int attack, Stat attacker)
    {
        Hp -= attack;
        if (Hp <= 0)
        {
            Hp = 0;
            isFireDead = true;
            OnDead(attacker);
        }
    }

    protected virtual void OnDead(Stat attacker)
    {
        PlayerStat playerStat = attacker as PlayerStat;
        if (playerStat != null)
        {
            playerStat.Exp += 15;
        }

        if (gameObject.tag == "Enemy") Managers.Game.Despawn(gameObject, 2f);
        else if (gameObject.tag == "Player")
        {
            GameObject.Find("Die_UI").transform.Find("Die_Panel").gameObject.SetActive(true);
        }
    }

    public bool isDead()
    {
        if (Hp <= 0)
            return true;
        return false;
    }
}
