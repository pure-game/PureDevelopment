using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeastPanelController : MonoBehaviour
{

    public Beast currentBeast;
    public Button button;

    void Start()
    {
        button = GetComponent<Button>();
        transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>(currentBeast.BeastIconPath);
    }

    public void FillBeastMenu()
    {
        var mobName = GameObject.Find("MobName").GetComponent<Text>();
        mobName.text = currentBeast.BeastName;

        var mobDescription = GameObject.Find("Description").GetComponent<Text>();
        mobDescription.text = currentBeast.BeastDescription;

        var mobAnimator = GameObject.Find("BeastImage").GetComponent<Animator>();
        mobAnimator.Play(currentBeast.BeastName);
    }
    
    

}
