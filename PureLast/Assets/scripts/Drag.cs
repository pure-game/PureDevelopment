using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Drag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    public static GameObject dragedObject;
    InventoryController inventory;

    // Start is called before the first frame update
    void Start()
    {
        inventory = GameObject.Find("InventoryManager").GetComponent<InventoryController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        dragedObject = gameObject;
        inventory.dragPrefab.SetActive(true);
        if (dragedObject.GetComponent<InventoryCell>() != null)
        {
                int index = dragedObject.GetComponent<InventoryCell>().index;
                inventory.dragPrefab.GetComponent<Image>().sprite = Resources.Load<Sprite>(InventoryController.Items[index].iconPath);
                inventory.dragPrefab.transform.GetChild(0).GetComponent<Text>().text = InventoryController.Items[index].countItem.ToString();
                inventory.dragPrefab.GetComponent<CanvasGroup>().blocksRaycasts = false;
            if (inventory.dragPrefab.GetComponent<Image>().sprite == null)
            {
                dragedObject = null;
                inventory.dragPrefab.SetActive(false);
            }
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        inventory.dragPrefab.transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            int index = dragedObject.GetComponent<InventoryCell>().index;
            dragedObject.GetComponent<InventoryCell>().Drop(index);
        }
        dragedObject = null;
        inventory.dragPrefab.SetActive(false);
        inventory.dragPrefab.GetComponent<CanvasGroup>().blocksRaycasts = true;
    }
}
