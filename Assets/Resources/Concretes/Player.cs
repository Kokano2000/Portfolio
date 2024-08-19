using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class Player : CharacterAbstract, IControllable
{
    //�T�u�X�L���N�[���_�E��
    public float subSkillCD { get; protected set; }
    //�T�u�X�L�������\
    public bool subSkillActive { get; protected set; }
    //�T�u�X�L���N�[���_�E���o�ߎ���
    public float subSkillLapseTime { get; protected set; }
    //�T�u�X�L���o�t���ʎ���
    public float subSkillEffectDuration { get; protected set; }
    //�T�u�X�L���o�t�c�莞��
    public float subSkillEffectLast { get; protected set; }
    //�T�u�X�L���o�t�p���`�F�b�N
    public bool subSkillEffectActivated { get; protected set; }
    //�T�u�X�L���X�s�[�h�o�t���ʗ�
    public float subSkillSpeedDelta { get; protected set; }
    //�T�u�X�L��CD�o�t���ʗ�
    public float subSkillCDDelta { get; protected set; }
    private void Awake()
    {
        //DEBUG�@�p����
        maxHp = 100;
        maxMp = 100;
        mainSkillActive = true;
        mainSkillLapseTime = 0.0f;
        mainSkillBaseCD = 3.0f;
        mainSkillActualCD = mainSkillBaseCD;
        playerMovementTarget = transform.position;
        playerAttackTarget = transform.position;
        baseMoveSpeed = 5.0f;
        actualMoveSpeed = baseMoveSpeed;
        mainSkill = Resources.Load< GameObject>("Prefabs/QFirePrefab");
        hp = maxHp;
        mp = maxMp;
        actualMagicDamage = 100;

        //DEBUG ��p����
        //ISSUE �N�[���^�C���͌��Z���œ��ꂷ�ׂ���
        subSkillActive = true;
        subSkillLapseTime = 0.0f;
        subSkillCD = 10.0f;
        subSkillEffectDuration = 3.0f;
        subSkillEffectLast = subSkillEffectDuration;
        subSkillEffectActivated = false;
    }
    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
        MouseMove();
        MainSkill();
        SubSkill();
    }
    
    //���C���X�L������
    //�΂̋ʂ��J�[�\�������Ɏˏo
    protected override void MainSkill()
    {
        //�����`�F�b�N
        if (Input.GetAxis("Main Skill") > 0 && mainSkillActive == true)
        {
            var mousePosition = MouseControl.MouseWorldPosition(Input.mousePosition);
            if (mousePosition != null)
            {
                Vector3 skillGenPos = transform.position;
                skillGenPos.y = mainSkill.transform.lossyScale.y / 2;
                playerAttackTarget = (Vector3)MouseControl.MouseWorldPosition(Input.mousePosition, mainSkill.transform.lossyScale.y);
                //ISSUE ���̈ˑ���؂�Ȃ���
                FireBall.Instantiate(mainSkill, skillGenPos, Quaternion.identity, playerAttackTarget, gameObject);
            }
            mainSkillLapseTime = mainSkillActualCD;
            mainSkillActive = false;
        }
        //�N�[���^�C���`�F�b�N
        if (mainSkillActive == false) {
            mainSkillLapseTime -= Time.deltaTime;
            if (mainSkillLapseTime <= 0.0f) 
            {
                mainSkillLapseTime = mainSkillActualCD;
                mainSkillActive = true;
            }
        
        }
    }
    //�T�u�X�L������
    //���C���X�L����CD�����������B3�b�Ԏ��g�Ɉړ����x�㏸�A���C���X�L����CD�����̃o�t��t�^
    protected void SubSkill()
    {
        //�����`�F�b�N
        if (Input.GetAxis("Sub Skill") > 0 && subSkillActive == true)
        {
            //�o�t�p�����`�F�b�N
            if (subSkillEffectActivated)
            {
                subSkillEffectLast = subSkillEffectDuration;
            }
            else
            {
                mainSkillActive = true;
                subSkillEffectLast = subSkillEffectDuration;
                subSkillSpeedDelta = actualMoveSpeed * 1.5f - actualMoveSpeed;
                actualMoveSpeed += subSkillSpeedDelta;
                subSkillCDDelta = mainSkillActualCD / 2.0f - mainSkillActualCD;
                mainSkillActualCD += subSkillCDDelta;
                subSkillEffectActivated = true;
            }

            subSkillLapseTime = subSkillCD;
            subSkillActive = false;
        }

        //�o�t�p���^�C�}�[
        if (subSkillEffectActivated)
        {
            subSkillEffectLast -= Time.deltaTime;
            if (subSkillEffectLast <= 0.0f)
            {
                actualMoveSpeed -= subSkillSpeedDelta;
                mainSkillActualCD -= subSkillCDDelta;
                subSkillEffectLast = subSkillEffectDuration;
                subSkillEffectActivated = false;
            }
        }

        //�N�[���^�C���`�F�b�N
        if (subSkillActive == false)
        {
            subSkillLapseTime -= Time.deltaTime;
            if (subSkillLapseTime <= 0.0f)
            {
                subSkillLapseTime = subSkillCD;
                subSkillActive = true;
            }

        }
    }
    public override void onDeath()
    {
        Destroy(gameObject);
    }

    public void MouseMove()
    {
        //�ړ��`�F�b�N
        if (Input.GetMouseButton(1))
        {
            var move = global::MouseControl.MouseWorldPosition(Input.mousePosition, transform.lossyScale.y);
            if (move != null)
            {
                Vector3 MovementTarget = (Vector3)move;
                playerMovementTarget = MovementTarget;
            }
        }
        transform.position = Vector3.MoveTowards(transform.position, playerMovementTarget, actualMoveSpeed * Time.deltaTime);
    }
}
