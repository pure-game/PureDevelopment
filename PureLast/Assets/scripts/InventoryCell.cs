using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryCell : MonoBehaviour, IDropHandler
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

    /*
    //отображение на индексовой ячейке меню дропа/эквипа
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            inventory.DisplayMenu(index);
        }
    }
    */

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

    public void OnDrop(PointerEventData eventData)
    {
        GameObject dragedObject = Drag.dragedObject;
        if(dragedObject == null)
        {
            return;
        }
        InventoryCell currentDragedItem = dragedObject.GetComponent<InventoryCell>();
        if (currentDragedItem != null)
        {
            Item currentItem = InventoryController.Items[GetComponent<InventoryCell>().index];
            InventoryController.Items[GetComponent<InventoryCell>().index] = InventoryController.Items[currentDragedItem.index];
            InventoryController.Items[currentDragedItem.index] = currentItem;         
            inventory.Display();
        }
    }
}
