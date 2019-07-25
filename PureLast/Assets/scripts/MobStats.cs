using UnityEngine;

// Контроллер здоровья объектов
public class MobStats : ObjectStats
{
    [SerializeField] float maxHealth;
    [SerializeField] Transform HealthBar;

    float curHealth;
    Vector2 HpHealthbarIndex;

    void Start()
    {
        curHealth = maxHealth;
        HpHealthbarIndex = HealthBar.localScale;
        HpHealthbarIndex.x /= maxHealth;
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
        HealthBar.localScale = new Vector2(curHealth * HpHealthbarIndex.x, HpHealthbarIndex.y);
    }
}
