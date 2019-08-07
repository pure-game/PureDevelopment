using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] float length = 100;
    [SerializeField] Transform tip;

    Vector3 defaultScale;

    void Start()
    {
        defaultScale = transform.localScale;
    }

    
    void Update()
    {
        CheckObstacles();
    }

    void CheckObstacles()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, length);
        Vector3 end = transform.position + transform.right * length;
        Vector3 scale = new Vector3(length, defaultScale.y, defaultScale.z);

        if (hit.collider != null)
        {
            end = hit.point;
            print(hit.point);
            scale.x = hit.distance;
        }
        tip.position = end;
        transform.localScale = scale;
    }
}
