using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunControl : MonoBehaviour
{

    static List<Item> items;

    public static List<Item> Items { get => items; set => items = value; }

    // Start is called before the first frame update
    void Start()
    {
        Items = new List<Item>();
        for (int i = 0; i < transform.childCount; i++)
        {
            Items.Add(new Item());
        }
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<InventoryCell>().index = i;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }



}
