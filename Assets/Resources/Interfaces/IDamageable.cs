using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    //�ʏ�_���[�W���󂯂�
    void takeDamage(int damage, bool isMagic);
    //�g�D���[�_���[�W���󂯂�
    void takeTrueDamage(int damage);
    //���S�����Ƃ�
    void onDeath();
}
