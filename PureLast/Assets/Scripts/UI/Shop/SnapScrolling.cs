using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SnapScrolling : MonoBehaviour
{
    [SerializeField] private GameObject ScrollGunPanel;
    [SerializeField] int PanelsCount;
    [SerializeField] int PanelsOffset;
    [Range(0f, 20f)]
    [SerializeField] float snapSpeed;
    [Range(0f, 5f)]
    [SerializeField] float scaleOffset;
    [Range(0f, 20f)]
    [SerializeField] float scaleSpeed;

    private GameObject[] instPans;
    private Vector2[] PanelsPositions;
    private Vector2[] panelsScale;
    private Vector2 contentVector;
   
    private RectTransform contentRect;

    private int selectedPanelID;

    public bool isScrolling;
    public ScrollRect scrollRect;


    void Start()
    {
        panelsScale = new Vector2[GameController.gunStatsList.Count];
        contentRect = GetComponent<RectTransform>();
        PanelsPositions = new Vector2[GameController.gunStatsList.Count];
        instPans = new GameObject[GameController.gunStatsList.Count];
        for (int i = 0; i < GameController.gunStatsList.Count; i++)
        {
            instPans[i] = Instantiate(ScrollGunPanel, transform, false);
            if (i == 0) continue;
            instPans[i].transform.localPosition = new Vector2(instPans[i-1].transform.localPosition.x + 
                ScrollGunPanel.GetComponent<RectTransform>().sizeDelta.x + PanelsOffset, instPans[i].transform.localPosition.y);
            PanelsPositions[i] = -instPans[i].transform.localPosition;
        }
    }

    private void FixedUpdate()
    {
        FindNearAndSnap();
    }


    //Находит индекс ближайшей панели
    private void FindNearAndSnap()
    {
        float nearPosition = float.MaxValue;
        for (int i = 0; i < GameController.gunStatsList.Count; i++)
        {
            float distance = Mathf.Abs(contentRect.anchoredPosition.x - PanelsPositions[i].x);
            if (distance < nearPosition)
            {
                nearPosition = distance;
                selectedPanelID = i;
            }
            float scale = Mathf.Clamp(1 / (distance / PanelsOffset) * scaleOffset, 0.7f, 1f);
            panelsScale[i].x = Mathf.SmoothStep(instPans[i].transform.localScale.x, scale, scaleSpeed * Time.fixedDeltaTime);
            panelsScale[i].y = Mathf.SmoothStep(instPans[i].transform.localScale.y, scale, scaleSpeed * Time.fixedDeltaTime);
            instPans[i].transform.localScale = panelsScale[i];
        }
        float scrollVelocity = Mathf.Abs(scrollRect.velocity.x);
        if (scrollVelocity < 400 && !isScrolling) scrollRect.inertia = false;
        if (isScrolling || scrollVelocity > 400) return;
        contentVector.x = Mathf.SmoothStep(contentRect.anchoredPosition.x, PanelsPositions[selectedPanelID].x, snapSpeed * Time.fixedDeltaTime);
        contentRect.anchoredPosition = contentVector;
    }

    public void Scrolling(bool Scroll)
    {
        isScrolling = Scroll;
        if (Scroll) scrollRect.inertia = true;
    }

}
