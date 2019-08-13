using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyDisplay : MonoBehaviour
{

    // Start is called before the first frame update
    void FixedUpdate()
    {
        gameObject.GetComponent<Text>().text = GameController.Money.ToString();
    }
}
