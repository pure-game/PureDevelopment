﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollGunItem : MonoBehaviour
{
    Image gunImage;
    Text damage;
    Text bpm;
    Text price;

    // Start is called before the first frame update
    void Start()
    {
        gunImage = transform.Find("GunImage").GetComponent<Image>();
        damage = transform.Find("Damage").GetComponent<Text>();
        bpm = transform.Find("Bpm").GetComponent<Text>();
        price = transform.Find("Price").GetComponent<Text>();
        price.text = "ddd";
    }

    public void ItemFilling(int Index)
    {
        Debug.Log(Index);
        gunImage = Resources.Load(GameController.gunStatsList[Index].IconPath) as Image;
    }
}
