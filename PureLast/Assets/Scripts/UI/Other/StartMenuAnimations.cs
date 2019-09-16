using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenuAnimations : MonoBehaviour
{
    [SerializeField] GameObject leftPanel;

    public void OnStartButtonClick()
    {
        GasController.isGameStarted = true;
        leftPanel.GetComponent<Animation>().Play();
        GetComponent<Animation>().Play();
    }
}
