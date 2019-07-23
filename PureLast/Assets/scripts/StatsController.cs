using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Контроллер UI с характеристиками игрока
public class StatsController : MonoBehaviour
{

    [SerializeField] GameObject scoreText;
    [SerializeField] GameObject healthBar;
    [SerializeField] GameObject oxygenBar;

    Text score;
    Transform healthProgress;
    Transform oxygenProgress;

    void Start()
    {
        score.text = "0";
    }

    void Update()
    {
        // обновляем прогресс бары здровья и кислорода, обновляем очки
        /*healthProgress.localScale = new Vector2(PlayerStats.curHealth / PlayerStats.maxHealth, 1);
        oxygenProgress.localScale = new Vector2(PlayerStats.curOxygen / PlayerStats.maxOxygen, 1);*/
        score.text = ((int)PlayerStats.curScore).ToString();
    }
}
