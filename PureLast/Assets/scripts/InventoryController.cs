using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{

    static List<Item> items;
    [SerializeField] GameObject cellContainer;
    [SerializeField] KeyCode showInventory;

    public static List<Item> Items { get => items; set => items = value; }

    // Start is called before the first frame update
    void Start()
    {
        cellContainer.SetActive(false);
        Items = new List<Item>();
        for (int i = 0; i < cellContainer.transform.childCount; i++)
        {
            Items.Add(new Item());
        }
        for (int i = 0; i < cellContainer.transform.childCount; i++)
        {
            cellContainer.transform.GetChild(i).GetComponent<InventoryCell>().index = i;
        }
    }

    // Update is called once per frame
    void Update()
    {
        openInventory();
    }

    void openInventory()
    {
        if (Input.GetKeyDown(showInventory))
        {
            if (cellContainer.activeSelf)
            {
                cellContainer.SetActive(false);
            }
            else
            {
                cellContainer.SetActive(true);
            }
        }
    }

    public void Display()
    {
        for (int i = 0; i < Items.Count; i++)
        {
            Transform cell = cellContainer.transform.GetChild(i);
            Transform icon = cell.GetChild(0);
            Transform transformText = icon.GetChild(0);
            Transform panel = transformText.GetChild(0);
            Image img = icon.GetComponent<Image>();
            Text text = transformText.GetComponent<Text>();

            if (Items[i].id != 0)
            {
                img.enabled = true;
                img.sprite = Resources.Load<Sprite>(Items[i].iconPath);

                if (Items[i].countItem >= 1 && Items[i].stackable == true)
                {
                    text.enabled = true;
                    text.text = Items[i].countItem.ToString();
                }
            }
            else
            {
                panel.gameObject.SetActive(false);
                img.enabled = false;
                text.enabled = false;
            }
        }
    }

    // Отображает менюху дропа/эквипа
    public void DisplayMenu(int index)
    {
        Transform cell = cellContainer.transform.GetChild(index);
        Transform icon = cell.GetChild(0);
        Transform transformText = icon.GetChild(0);
        Transform panel = transformText.GetChild(0);

        if (panel.gameObject.activeSelf)
        {
            panel.gameObject.SetActive(false);
        }
        else
        {
            if(Items[index].id != 0)
            panel.gameObject.SetActive(true);
        }
        
    }

}
