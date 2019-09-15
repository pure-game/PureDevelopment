using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beast : MonoBehaviour
{
    public string BeastName;
    [HideInInspector] public string BeastAnimatorPath;
    [HideInInspector] public string BeastIconPath;
    public string BeastDescription;
    public int BeastPriceForPhoto;

    private void Start()
    {
        BeastAnimatorPath = "Beasts/Animators/" + BeastName;
        BeastIconPath = "Beasts/Icons/" + BeastName;
    }

}
