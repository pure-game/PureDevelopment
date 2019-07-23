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
        Debug.Log("dd");
        curHealth -= damage;
        if (curHealth <= 0)
        {
            Death();
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
