using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyDisplay : MonoBehaviour
{

    [SerializeField] Text coinsUIText;

    // Start is called before the first frame update
    void Start()
    {
        coinsUIText.text = GameController.Money.ToString();
    }
}
