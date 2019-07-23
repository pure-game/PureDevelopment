using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Контроллер здоровья объектов
public class MobStats : ObjectStats
{
    [SerializeField] float maxHealth;

    float curHealth;

    void Start()
    {
        curHealth = maxHealth;
    }

    // получение урона
    public override void Damaged(float damage)
    {
        curHealth -= damage;
        if (curHealth <= 0)
        {

        }
    }
    
    // смерть объекта
    public override void Death()
    {
        // прописать дроп
        //
        //
        Destroy(gameObject);
    }

    // переносим урон кислороду сразу на жизни
    public override void OxygenDamage(float damage)
    {
        Damaged(damage);
    }
}
