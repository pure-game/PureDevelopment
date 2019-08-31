using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// контроллер всей игры
public class GameController : MonoBehaviour
{
    readonly static string keyMoney = "MONEY";
    readonly static string keyHighscore = "HIGHSCORE";
    readonly static string keyEquipedGun = "EQUIPEDGUN";

    private static int _money = 0;
    private static int _highscore = 0;
    private static int _equipedGun = 0;

    [SerializeField] private List<GunStats> _gunStatsList = new List<GunStats>();
    public static List<GunStats> gunStatsList;
    public static SceneSwitcher sceneSwitcher = null;
    
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
    public static int EquipedGun
    {
        get
        {
            return _equipedGun;
        }

        set
        {
            _equipedGun = value;
            PlayerPrefs.SetInt(keyEquipedGun, _equipedGun);
        }
    }

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        // подгрузака данных из памяти
        _money = PlayerPrefs.GetInt(keyMoney, 0);
        _highscore = PlayerPrefs.GetInt(keyHighscore, 0);
        //EquipedGun = PlayerPrefs.GetInt(keyEquipedGun, 0);
        gunStatsList = _gunStatsList;
        sceneSwitcher = GetComponent<SceneSwitcher>();
        print(Highscore);
    }

    public static void Save()
    {
        PlayerPrefs.SetInt(keyMoney, _money);
        PlayerPrefs.SetInt(keyHighscore, _highscore);
    }

    public static void AddMoney(int Value)
    {
        _money += Value;
        Save();
        print("Added " + Value + " Current " + _money);
    }

}
