using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    [SerializeField] GameObject menuPause;
    [SerializeField] GameObject Blur;
    [SerializeField] GameObject PauseButton;
    [SerializeField] Animation PauseUpMove;
    [SerializeField] Animation PauseDownMove;

    void Start()
    {
        Time.timeScale = 1;
    }

    public void PauseGame()
    {
        Blur.SetActive(true);
        PauseButton.GetComponent<Button>().enabled = false;
        menuPause.GetComponent<Animator>().Play("PauseUpMove");
        StartCoroutine(PauseTime());
    }
    public void UnPauseGame()
    {
        Blur.SetActive(false);
        PauseButton.GetComponent<Button>().enabled = true;
        Time.timeScale = 1;
        menuPause.GetComponent<Animator>().Play("PauseDownMove");
    }

    IEnumerator PauseTime()
    {
        yield return new WaitForSeconds(1);
        Time.timeScale = 0;
    }

}
