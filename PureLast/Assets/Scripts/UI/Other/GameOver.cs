using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{

    [SerializeField] GameObject Blur;
    [SerializeField] GameObject GameOverPanel;
    [SerializeField] GameObject Player;
    [SerializeField] PhotografedContentManager PhotoPanel;
    [SerializeField] Text TotalMoneyForPhoto;

    [SerializeField] Text CurrentRecordText;
    [SerializeField] Text CurrentMoneyText;

    int CurrentRecord;
    int CollectedMoney;

    void Start()
    {
        GameOverPanel.GetComponent<Animator>().Play("GameOverIdle");
        Blur.SetActive(false);
    }

    public void DeathAndSave()
    {
        Blur.SetActive(true);

        PhotoPanel.AddContent();

        CurrentRecordText.text = PlayerStats.curScore.ToString();
        TotalMoneyForPhoto.text = PhotografedContentManager.TotalMoneyForPhoto.ToString();
        CurrentMoneyText.text = StatsController.currentMoney.ToString();

        GameOverPanel.GetComponent<Animator>().Play("GameOverAnimation");

        Destroy(Player);

    }

}
