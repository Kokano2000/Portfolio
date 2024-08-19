using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class Player : CharacterAbstract, IControllable
{
    //サブスキルクールダウン
    public float subSkillCD { get; protected set; }
    //サブスキル発動可能
    public bool subSkillActive { get; protected set; }
    //サブスキルクールダウン経過時間
    public float subSkillLapseTime { get; protected set; }
    //サブスキルバフ効果時間
    public float subSkillEffectDuration { get; protected set; }
    //サブスキルバフ残り時間
    public float subSkillEffectLast { get; protected set; }
    //サブスキルバフ継続チェック
    public bool subSkillEffectActivated { get; protected set; }
    //サブスキルスピードバフ効果量
    public float subSkillSpeedDelta { get; protected set; }
    //サブスキルCDバフ効果量
    public float subSkillCDDelta { get; protected set; }
    private void Awake()
    {
        //DEBUG　継承分
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

        //DEBUG 非継承分
        //ISSUE クールタイムは減算式で統一すべきか
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
    
    //メインスキル処理
    //火の玉をカーソル方向に射出
    protected override void MainSkill()
    {
        //発動チェック
        if (Input.GetAxis("Main Skill") > 0 && mainSkillActive == true)
        {
            var mousePosition = MouseControl.MouseWorldPosition(Input.mousePosition);
            if (mousePosition != null)
            {
                Vector3 skillGenPos = transform.position;
                skillGenPos.y = mainSkill.transform.lossyScale.y / 2;
                playerAttackTarget = (Vector3)MouseControl.MouseWorldPosition(Input.mousePosition, mainSkill.transform.lossyScale.y);
                //ISSUE この依存を切れないか
                FireBall.Instantiate(mainSkill, skillGenPos, Quaternion.identity, playerAttackTarget, gameObject);
            }
            mainSkillLapseTime = mainSkillActualCD;
            mainSkillActive = false;
        }
        //クールタイムチェック
        if (mainSkillActive == false) {
            mainSkillLapseTime -= Time.deltaTime;
            if (mainSkillLapseTime <= 0.0f) 
            {
                mainSkillLapseTime = mainSkillActualCD;
                mainSkillActive = true;
            }
        
        }
    }
    //サブスキル処理
    //メインスキルのCDが解消される。3秒間自身に移動速度上昇、メインスキルのCD半減のバフを付与
    protected void SubSkill()
    {
        //発動チェック
        if (Input.GetAxis("Sub Skill") > 0 && subSkillActive == true)
        {
            //バフ継続中チェック
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

        //バフ継続タイマー
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

        //クールタイムチェック
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
        //移動チェック
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
