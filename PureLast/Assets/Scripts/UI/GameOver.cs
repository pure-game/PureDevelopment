using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{

    [SerializeField] GameObject Blur;
    [SerializeField] GameObject GameOverPanel;
    [SerializeField] GameObject Player;
    [SerializeField] GameObject PhotoPanel;

    [SerializeField] Text CurrentRecordText;
    [SerializeField] Text CurrentMoneyText;

    int CurrentRecord;
    int CollectedMoney;

    public static bool isDeath = false;

    void Start()
    {
        GameOverPanel.GetComponent<Animator>().Play("GameOverIdle");
        Blur.SetActive(false);
    }

    void Update()
    {
        if (isDeath)
        {
            DeathAndSave();
            isDeath = false;
        }
    }

    public void DeathAndSave()
    {
        Blur.SetActive(true);

        CurrentRecord = (int)Player.transform.position.x;
        CurrentRecordText.text = CurrentRecord.ToString();

        GameOverPanel.GetComponent<Animator>().Play("GameOverAnimation");

    }

    public static void Death()
    {
        isDeath = true;
    }

}
