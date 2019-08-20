using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTarget : MonoBehaviour
{
    void OnBecameVisible()
    {
        Procedure.mobsCurrentInCamera.Add(gameObject);       
    }
    void OnBecameInvisible()
    {
        Procedure.mobsCurrentInCamera.Remove(gameObject);
    }
}
