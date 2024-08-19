using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ŽŽŒ±“I‚È“G‚ÌƒNƒ‰ƒX
public class Enemy : CharacterAbstract, IDamageable
{
    private void Awake()
    {
        maxHp = 60;
        hp = maxHp;
        actualMagicDamageResist = 100;
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    protected override void MainSkill()
    {

    }

    public override void onDeath()
    {
        Destroy(gameObject);
    }
}
