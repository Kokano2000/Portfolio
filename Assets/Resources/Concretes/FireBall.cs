using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class FireBall : MonoBehaviour, IDamageEffect
{
    //発動者
    GameObject activator;
    //弾速
    float moveSpeed;
    //基礎ダメージ
    int baseDamage;
    //実質ダメージ
    int actualDamage;
    //着弾地点
    private Vector3 target;
    //最大射程
    float maxRange;
    //トゥルーダメージかどうか
    bool isTrueDamage;
    //魔法かどうか
    bool isMagic;
    // Start is called before the first frame update

    private void Awake()
    {
        //DEBUG
        moveSpeed = 20.0f;
        baseDamage = 350;
        maxRange = 20.0f;
        isMagic = true;
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
        if(transform.position.x == target.x && transform.position.z == target.z)
        {
            Destroy(gameObject);
        }
    }

    //衝突時メソッド
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Obstacle")
        {
            Destroy(gameObject);
        }
        else if (other.gameObject.tag == "Enemy")
        {
            culcActDamage();
            other.GetComponent<IDamageable>().takeDamage(actualDamage, isMagic);
            Destroy(gameObject);
        }
    }

    public static void Instantiate(GameObject prefab, Vector3 genPosition, Quaternion rotate, Vector3 mousePos, GameObject activator)
    {
        GameObject fireball = Instantiate(prefab, genPosition, rotate);
        FireBall component = fireball.GetComponent<FireBall>();
        //着弾地点を計算
        component.target = genPosition + (mousePos - genPosition) * component.maxRange / Vector3.Distance(genPosition, mousePos);
        //発動者を保持
        component.activator = activator;
    }

    public void culcActDamage()
    {
        //ISSUE  Player型の依存関係を切り離すべき
        actualDamage = (int)Math.Round(baseDamage + activator.GetComponent<Player>().actualPhysicalDamage * 0.0f + activator.GetComponent<Player>().actualMagicDamage * 1.2f);
        //DEBUG
        print("Actual damage is " + actualDamage + "!");
    }
}