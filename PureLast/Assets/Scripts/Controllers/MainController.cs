using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainController : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject gas;
    [SerializeField] GameOver gameOverObject;

    public static GameObject Player;
    public static GameObject Gas;
    public static GameOver GameOverObject;

    private void Start()
    {
        Debug.Log("main");
        Player = player;
        Gas = gas;
        GameOverObject = gameOverObject;
    }
}
