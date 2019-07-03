using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GunControl : MonoBehaviour, IPointerClickHandler
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
            transform.GetChild(i).GetComponent<CurrentGun>().gunID = i;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Display()
    {
        for (int i = 0; i < Items.Count; i++)
        {
            Transform Gun = transform.GetChild(i).GetChild(0);
            Image img = Gun.GetComponent<Image>();

            if(items[i].id != 0)
            {
                img.enabled = true;
                img.sprite = Resources.Load<Sprite>(Items[i].iconPath);
            }
            else
            {
                img.enabled = false;
            }

        }
    }

    public void Swap()
    {
        Item pr = Items[0];
        for (int i = 0; i < Items.Count - 1; i++)
        {
            Items[i] = Items[i + 1];
        }
        Items[Items.Count - 1] = pr;
        Display();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Swap();
        Debug.Log("Swap");
    }

}
