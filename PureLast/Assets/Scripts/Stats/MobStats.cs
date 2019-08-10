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
        GetComponent<CapsuleCollider2D>().enabled = false;
        GetComponent<ChasePlayer>().enabled = false;
        GetComponent<MobsMovementController>().enabled = false;

        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            if (gameObject.transform.GetChild(i).GetComponent<ParticleSystem>() != null) continue;
            gameObject.transform.GetChild(i).gameObject.SetActive(false);
        }
        

        particleSystem.Play();
        GameObject coin = Instantiate(Resources.Load("Prefabs/Other/Coins/Coin"), transform.position, Quaternion.identity) as GameObject;
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
