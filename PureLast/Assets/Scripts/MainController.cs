using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainController : MonoBehaviour
{
    [SerializeField] GameObject player;

    static public GameObject Player;

    private void Start()
    {
        Player = player;
    }
}
