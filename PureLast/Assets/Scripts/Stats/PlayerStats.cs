using UnityEngine;
using UnityEngine.SceneManagement;

// Основные характеристики игрока
public class PlayerStats : ObjectStats
{
    [SerializeField] float health;
    [SerializeField] float oxygen;

    static public float maxHealth;
    static public float maxOxygen;
    static public float curHealth;
    static public float curOxygen;
    static public int curScore = 0;
    public bool isShieldOn = false;

    void Start()
    {
        curHealth = maxHealth = health;
        curOxygen = maxOxygen = oxygen;
    }

    private void Update()
    {
        curScore = (int)transform.position.x;
    }

    // отнимаем кислород
    public override void OxygenDamage(float damage)
    {
        if (isShieldOn)
            return;
        curOxygen -= damage;
        if (curOxygen <= 0)
        {
            curOxygen = 0;
            Damaged(damage);
        }
    }

    // отнимаем жизни
    public override void Damaged(float damage)
    {
        if (isShieldOn)
            return;
        curHealth -= damage;
        if (curHealth <= 0)
        {
            Death();
        }
    }

    public override void Death()
    {
        // перезапуск игры
        // заменить на функцию старта новой игры глобального менеджера игры
        //
        //
        //
        GameController.Highscore = curScore;
        GameController.Save();
        SceneManager.LoadScene("PROCEDURE", LoadSceneMode.Single);
    }
}
