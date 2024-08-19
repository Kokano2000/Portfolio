using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�L�����N�^�[�̊�{�I�p�����[�^�[���`
public abstract class CharacterAbstract : MonoBehaviour, IDamageable
{
    //�ő�̗�
    public int maxHp { get; protected set; }
    //�ő喂��
    public int maxMp { get; protected set; }
    //���ݑ̗�
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
    //����
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
    //��{�����U����
    public int basePhysicalDamage { get; protected set; }
    //���������U����
    public int actualPhysicalDamage { get; protected set; }
    //��{���@�U����
    public int baseMagicDamage { get; protected set; }
    //�������@�U����
    public int actualMagicDamage { get; protected set; }
    //��{�����h���
    public int basePhysicalDamageResist { get; protected set; }
    //���������h���
    public int actualPhysicalDamageResist { get; protected set; }
    //��{���@�h���
    public int baseMagicDamageResist { get; protected set; }
    //�������@�h���
    public int actualMagicDamageResist { get; protected set; }
    //��{�ړ����x
    public float baseMoveSpeed { get; protected set; }
    //�����ړ����x
    public float actualMoveSpeed { get; protected set; }
    //�ړ��ڕW
    public Vector3 playerMovementTarget { get; protected set; }
    //�U���ڕW
    public Vector3 playerAttackTarget { get; protected set; }
    //���C���X�L���I�u�W�F�N�g
    public GameObject mainSkill { get; protected set; }
    //���C���X�L����{�N�[���_�E��
    public float mainSkillBaseCD { get; protected set; }
    //���C���X�L�������N�[���_�E��
    public float mainSkillActualCD { get; protected set; }
    //���C���X�L�������\
    public bool mainSkillActive { get; protected set; }
    //���C���X�L���N�[���_�E���o�ߎ���
    public float mainSkillLapseTime { get; protected set; }

    //�̗̓Z�b�g��
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

    //���C���X�L��
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