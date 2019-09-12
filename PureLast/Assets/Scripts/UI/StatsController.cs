using UnityEngine;
using UnityEngine.UI;

// Контроллер UI с характеристиками игрока
public class StatsController : MonoBehaviour
{

    [SerializeField] GameObject scoreText;
    [SerializeField] GameObject moneyText;
    [SerializeField] Transform healthBar;
    [SerializeField] Transform oxygenBar;

    public static int currentMoney = 0;
    Text score;
    Text money;

    void Start()
    {
        money = moneyText.GetComponent<Text>();
        score = scoreText.GetComponent<Text>();
        score.text = "0";
        money.text = "0";
        currentMoney = 0;
    }

    void Update()
    {
        // обновляем прогресс бары здровья и кислорода, обновляем очки
        healthBar.localScale = new Vector2(PlayerStats.curHealth / PlayerStats.maxHealth, 1);
        oxygenBar.localScale = new Vector2(PlayerStats.curOxygen / PlayerStats.maxOxygen, 1);
        score.text = ((int)PlayerStats.curScore).ToString();
        money.text = currentMoney.ToString();
    }

    public static void AddMoney(int Value)
    {
        currentMoney += Value;
        GameController.AddMoney(Value);
    }
}
