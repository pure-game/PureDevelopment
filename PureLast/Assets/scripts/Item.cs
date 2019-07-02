using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{   
    public int id;
    public int countItem;
    public string prefabPath;
    public string iconPath;
    public bool stackable;

    public object Clone()
    {
        return this.MemberwiseClone();
    }
}
