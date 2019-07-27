﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// контроллер всей игры
public class GameController : MonoBehaviour
{

    private static int _money = 0;
    private static int _highscore = 0;

    public static int Money { get => _money; set => _money = value; }
    public static int Highscore
    {
        get
        {
            return _highscore;
        }
        
        set
        {
            if (_highscore >= value)
                return;
            _highscore = value;
        }
    }

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        // подгрузака данных из памяти
    }


}
