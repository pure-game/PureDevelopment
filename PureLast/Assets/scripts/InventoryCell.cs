﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryCell : MonoBehaviour, IPointerClickHandler
{

    public int index;
    List<Item> items;
    InventoryController inventory;
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        inventory = GameObject.Find("InventoryManager").GetComponent<InventoryController>();
        player = GameObject.Find("Player");
        items = InventoryController.Items;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        /*if (eventData.button == PointerEventData.InputButton.Right)
        {
            GameObject droped = Instantiate(Resources.Load<GameObject>(items[index].prefabPath)) as GameObject;
            if (items[index].countItem > 1)
            {
                items[index].countItem--;
                inventory.Display();
            }
            else
            {
                items.Remove(items[index]);
                inventory.Display();
            }
            droped.transform.position = player.transform.position;
        } */

        if (eventData.button == PointerEventData.InputButton.Left)
        {
            inventory.DisplayMenu(index);
        }

    }

    public void Drop(int index)
    {       
            GameObject droped = Instantiate(Resources.Load<GameObject>(items[index].prefabPath)) as GameObject;
            if (items[index].countItem > 1)
            {
                items[index].countItem--;
                inventory.Display();
            }
            else
            {
                items.Remove(items[index]);
                inventory.Display();
            }
            droped.transform.position = player.transform.position;       
    }

}
