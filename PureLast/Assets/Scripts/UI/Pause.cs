using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    [SerializeField] GameObject menuPause;
    [SerializeField] Animation PauseUpMove;
    [SerializeField] Animation PauseDownMove;

    void Start()
    {
        Time.timeScale = 1;
    }

    public void PauseGame()
    {
        menuPause.GetComponent<Animator>().Play("PauseUpMove");
        StartCoroutine(PauseTime());
    }
    public void UnPauseGame()
    {
        Time.timeScale = 1;
        menuPause.GetComponent<Animator>().Play("PauseDownMove");
    }

    IEnumerator PauseTime()
    {
        yield return new WaitForSeconds(1);
        Time.timeScale = 0;
    }

}
