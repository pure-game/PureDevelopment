﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// контроллер всей игры
public class GameController : MonoBehaviour
{
    readonly static string keyMoney = "MONEY";
    readonly static string keyHighscore = "HIGHSCORE";

    private static int _money = 0;
    private static int _highscore = 0;

    [SerializeField] private List<GunStats> _gunStatsList = new List<GunStats>();
    public static List<GunStats> gunStatsList;
    
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
        _money = PlayerPrefs.GetInt(keyMoney, 0);
        _highscore = PlayerPrefs.GetInt(keyHighscore, 0);
    }

    public static void Save()
    {
        PlayerPrefs.SetInt(keyMoney, _money);
        PlayerPrefs.SetInt(keyHighscore, _highscore);
    }

}
