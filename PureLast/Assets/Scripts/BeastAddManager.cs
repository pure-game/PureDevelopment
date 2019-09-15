﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeastAddManager : MonoBehaviour
{

    [SerializeField] GameObject BeastPanel;

    void Start()
    {
        foreach (var item in GameController.Beasts)
        {
            GameObject beastPanel = Instantiate(BeastPanel, transform) as GameObject;
            //КОСТЫЛЛЬ
            beastPanel.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>(item.BeastIconPath);
        }
    }
}
