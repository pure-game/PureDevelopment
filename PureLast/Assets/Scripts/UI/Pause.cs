using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    [SerializeField] GameObject menuPause;
    [SerializeField] GameObject Blur;
    [SerializeField] GameObject PauseButton;

    void Start()
    {
        Time.timeScale = 1;
    }

    public void StartPauseAnimation()
    {
        menuPause.GetComponent<Animator>().Play("PauseUpMove");
    }
    public void StartUnPauseAnimation()
    {
        Time.timeScale = 1;
        menuPause.GetComponent<Animator>().Play("PauseDownMove");
    }


    public void PauseGame()
    {
        GameController.isGamePaused = true;
        Blur.SetActive(true);
        PauseButton.GetComponent<Button>().enabled = false;
        Time.timeScale = 0;

    }
    public void UnPauseGame()
    {
        GameController.isGamePaused = false;
        Blur.SetActive(false);
        PauseButton.GetComponent<Button>().enabled = true;
    }

}
