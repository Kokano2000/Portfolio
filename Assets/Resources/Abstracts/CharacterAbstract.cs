using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//キャラクターの基本的パラメーターを定義
public abstract class CharacterAbstract : MonoBehaviour, IDamageable
{
    //最大体力
    public int maxHp { get; protected set; }
    //最大魔力
    public int maxMp { get; protected set; }
    //現在体力
    private int _hp;
    public int hp {
        get
        {
            return _hp;
        }
        set 
        {
            value = onSetHp(value);
            _hp = value;
        }
    }
    //魔力
    private int _mp;
    public int mp
    {
        get
        {
            return _mp;
        }
        set
        {
            if (value < 0)
            {
                value = 0;
            }
            if (value > maxMp)
            {
                value = maxMp;
            }
            _mp = value;
        }
    }
    //基本物理攻撃力
    public int basePhysicalDamage { get; protected set; }
    //実質物理攻撃力
    public int actualPhysicalDamage { get; protected set; }
    //基本魔法攻撃力
    public int baseMagicDamage { get; protected set; }
    //実質魔法攻撃力
    public int actualMagicDamage { get; protected set; }
    //基本物理防御力
    public int basePhysicalDamageResist { get; protected set; }
    //実質物理防御力
    public int actualPhysicalDamageResist { get; protected set; }
    //基本魔法防御力
    public int baseMagicDamageResist { get; protected set; }
    //実質魔法防御力
    public int actualMagicDamageResist { get; protected set; }
    //基本移動速度
    public float baseMoveSpeed { get; protected set; }
    //実質移動速度
    public float actualMoveSpeed { get; protected set; }
    //移動目標
    public Vector3 playerMovementTarget { get; protected set; }
    //攻撃目標
    public Vector3 playerAttackTarget { get; protected set; }
    //メインスキルオブジェクト
    public GameObject mainSkill { get; protected set; }
    //メインスキル基本クールダウン
    public float mainSkillBaseCD { get; protected set; }
    //メインスキル実質クールダウン
    public float mainSkillActualCD { get; protected set; }
    //メインスキル発動可能
    public bool mainSkillActive { get; protected set; }
    //メインスキルクールダウン経過時間
    public float mainSkillLapseTime { get; protected set; }

    //体力セット時
    protected int onSetHp(int hpvalue)
    {
        if (hpvalue <= 0)
        {
            onDeath();
        }
        else if (hpvalue > maxHp)
        {
            hpvalue = maxHp;
        }
        return hpvalue;
    }

    //メインスキル
    protected abstract void MainSkill();
    public abstract void onDeath();
    public void takeDamage(int damage, bool isMagic)
    {
        if (isMagic)
        {
            hp -= damage * 100 / (100 + actualMagicDamageResist);
            //DEBUG
            print("Enemy is Magic_damaged by " + damage * 100 / (100 + actualMagicDamageResist) + "!");
        }
        else
        {
            hp -= damage * 100 / (100 + actualPhysicalDamageResist);
        }
    }
    public void takeTrueDamage(int damage)
    {
        hp -= damage;
    }
}