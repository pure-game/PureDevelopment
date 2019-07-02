using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{

    static List<Item> items;
    [SerializeField] GameObject cellContainer;
    [SerializeField] KeyCode showInventory;

    // Start is called before the first frame update
    void Start()
    {
        cellContainer.SetActive(false);
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

}
