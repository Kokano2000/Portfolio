using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class FireBall : MonoBehaviour, IDamageEffect
{
    //������
    GameObject activator;
    //�e��
    float moveSpeed;
    //��b�_���[�W
    int baseDamage;
    //�����_���[�W
    int actualDamage;
    //���e�n�_
    private Vector3 target;
    //�ő�˒�
    float maxRange;
    //�g�D���[�_���[�W���ǂ���
    bool isTrueDamage;
    //���@���ǂ���
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

    //�Փˎ����\�b�h
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
        //���e�n�_���v�Z
        component.target = genPosition + (mousePos - genPosition) * component.maxRange / Vector3.Distance(genPosition, mousePos);
        //�����҂�ێ�
        component.activator = activator;
    }

    public void culcActDamage()
    {
        //ISSUE  Player�^�̈ˑ��֌W��؂藣���ׂ�
        actualDamage = (int)Math.Round(baseDamage + activator.GetComponent<Player>().actualPhysicalDamage * 0.0f + activator.GetComponent<Player>().actualMagicDamage * 1.2f);
        //DEBUG
        print("Actual damage is " + actualDamage + "!");
    }
}