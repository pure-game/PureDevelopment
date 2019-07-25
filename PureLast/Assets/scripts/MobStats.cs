using UnityEngine;

// Контроллер здоровья объектов
public class MobStats : ObjectStats
{
    [SerializeField] float maxHealth;
    [SerializeField] Transform HealthBar;

    float curHealth;
    float HpHealthbarIndex;

    void Start()
    {
        curHealth = maxHealth;
        HpHealthbarIndex = maxHealth / 10000;
    }

    // получение урона
    public override void Damaged(float damage)
    {
        curHealth -= damage;
        if (curHealth <= 0)
        {
            Death();
        }
        else
        {
            HealthBarController(damage);
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

    public void HealthBarController(float damage)
    {
        print(HpHealthbarIndex * damage + " " + HealthBar.localScale);
        HealthBar.localScale -= new Vector3(HpHealthbarIndex * damage, 0);
    }
}
