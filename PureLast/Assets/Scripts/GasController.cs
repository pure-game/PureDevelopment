using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasController : MonoBehaviour
{
    [SerializeField] float startVelocity;
    [SerializeField] public float Damage;
    [SerializeField] public float O2Damage;

    int startTime = 0;
    Rigidbody2D rigidbody2D;
    // Start is called before the first frame update
    void Start()
    {
        startTime = DateTime.Now.Millisecond;
        rigidbody2D = GetComponent<Rigidbody2D>();
        rigidbody2D.velocity = new Vector2(startVelocity, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
