using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhotografedContentManager : MonoBehaviour
{

    [SerializeField] GameObject PanelAndPrice;

    public static int TotalMoneyForPhoto;

    private void Start()
    {
        TotalMoneyForPhoto = 0;
    }

    public void AddContent()
    {
        foreach (var item in Procedure.mobsPhotographed)
        {
            GameObject panelAndPrice = Instantiate(PanelAndPrice, transform) as GameObject;
            panelAndPrice.GetComponentInChildren<Text>().text = item.Key.ToString() + " " + item.Value.ToString() + " x "
                + GameController.PhotoPrices[item.Key].ToString() + " = " + item.Value * GameController.PhotoPrices[item.Key];
            TotalMoneyForPhoto += item.Value * GameController.PhotoPrices[item.Key];
            print("SHTHRTHRt" + TotalMoneyForPhoto);
        }
    }

}
