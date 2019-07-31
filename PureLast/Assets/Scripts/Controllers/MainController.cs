using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainController : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject gas;

    static public GameObject Player;
    static public GameObject Gas;

    private void Start()
    {
        Player = player;
        Gas = gas;
    }
}
