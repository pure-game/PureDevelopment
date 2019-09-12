using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhotografedContentManager : MonoBehaviour
{

    [SerializeField] GameObject PanelAndPrice;

    public static bool isDeath;

    public static int TotalMoneyForPhoto;

    private void Start()
    {
        isDeath = false;
        TotalMoneyForPhoto = 0;
    }

    private void Update()
    {
        if (isDeath)
        {
            AddContent();
            isDeath = false;
        }
    }

    public void AddContent()
    {
        foreach (var item in Procedure.mobsPhotographed)
        {
            GameObject panelAndPrice = Instantiate(PanelAndPrice, gameObject.transform) as GameObject;
            panelAndPrice.GetComponentInChildren<Text>().text = item.Key.ToString() + " " + item.Value.ToString() + " x "
                + GameController.PhotoPrices[item.Key].ToString() + " = " + item.Value * GameController.PhotoPrices[item.Key];
            TotalMoneyForPhoto += item.Value * GameController.PhotoPrices[item.Key];
        }
    }

}
