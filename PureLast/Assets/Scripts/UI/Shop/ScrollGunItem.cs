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

    void Awake()
    {
        gunImage = transform.Find("GunImage").GetComponent<Image>();
        damage = transform.Find("Damage").GetComponent<Text>();
        bpm = transform.Find("Bpm").GetComponent<Text>();
        price = transform.Find("Price").GetComponent<Text>();
    }

    public void ItemFilling(int Index)
    {
        gunImage.sprite = Resources.Load<Sprite>(GameController.gunStatsList[Index].IconPath);
        damage.text = GameController.gunStatsList[Index].Damage.ToString();
        bpm.text = GameController.gunStatsList[Index].BulletPerMinute.ToString();
        price.text = GameController.gunStatsList[Index].Price.ToString() + " Coins";
    }
}
