using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    //通常ダメージを受ける
    void takeDamage(int damage, bool isMagic);
    //トゥルーダメージを受ける
    void takeTrueDamage(int damage);
    //死亡したとき
    void onDeath();
}
