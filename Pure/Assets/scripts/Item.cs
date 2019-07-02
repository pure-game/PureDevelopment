using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour, ICloneable
{   
    public int id = 0;
    public int countItem = 0;
    public string prefabPath;
    public string iconPath;
    public bool stackable;

    public object Clone()
    {
        return this.MemberwiseClone();
    }

}
