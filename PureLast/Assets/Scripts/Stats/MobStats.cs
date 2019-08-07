using System.Collections;
using UnityEngine;

// Контроллер здоровья объектов
public class MobStats : ObjectStats
{
    [SerializeField] float maxHealth;
    [SerializeField] Transform HealthBar;

    float curHealth;
    Vector2 HpHealthbarIndex;

    private ParticleSystem particleSystem;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        curHealth = maxHealth;
        HpHealthbarIndex = HealthBar.localScale;
        HpHealthbarIndex.x /= maxHealth;

        particleSystem = GetComponentInChildren<ParticleSystem>();
        Debug.Log(particleSystem);
        spriteRenderer = GetComponent<SpriteRenderer>();
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
        StartCoroutine(PlayParticle());
    }

    public IEnumerator PlayParticle()
    {
        spriteRenderer.enabled = false;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;

        particleSystem.Play();
        yield return new WaitForSeconds(particleSystem.main.duration);
        Destroy(gameObject);
    }

    // переносим урон кислороду сразу на жизни
    public override void OxygenDamage(float damage)
    {
        Damaged(damage);
    }

    public void HealthBarController(float damage)
    {
        HealthBar.localScale = new Vector2(curHealth * HpHealthbarIndex.x, HpHealthbarIndex.y);
    }
}
