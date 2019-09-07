using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{

    [SerializeField] GameObject Blur;
    [SerializeField] GameObject GameOverPanel;
    [SerializeField] GameObject Player;

    [SerializeField] Text CurrentRecordText;
    [SerializeField] Text CurrentMoneyText;

    int CurrentRecord;
    int CollectedMoney;

    static bool isDeath = false;

    void Update()
    {
        if (isDeath) DeathAndSave();
    }

    public void DeathAndSave()
    {
        Blur.SetActive(true);

        CurrentRecord = (int)Player.transform.position.x;
        CurrentRecordText.text = CurrentRecord.ToString();

        GameOverPanel.GetComponent<Animation>().Play();

    }

    public static void Death()
    {
        isDeath = true;
    }
}
